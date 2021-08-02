// -----------------------------------------------------------------------
// <copyright file="LumenWorksCsvParser.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.LumenWorks
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using Abbotware.Core;
    using Abbotware.Core.Data.ExtensionPoints.Retrieval;
    using Abbotware.Core.Data.ExtensionPoints.Text;
    using Abbotware.Core.Data.Plugins.Configuration;
    using Abbotware.Core.Diagnostics;
    using Abbotware.Core.Logging;
    using global::LumenWorks.Framework.IO.Csv;

    /// <summary>
    ///     Wrapper class for retrieving data from csv files via the LumenWorks csv parser
    /// </summary>
    /// <typeparam name="TRecord">class for record row type</typeparam>
    public class LumenWorksCsvParser<TRecord> : BaseCsvParser<TRecord, ParserContext>
        where TRecord : new()
    {
        /// <summary>
        ///     internal cache of headers
        /// </summary>
        private List<string> headersCache;

        /// <summary>
        ///     Initializes a new instance of the <see cref="LumenWorksCsvParser{TRecord}" /> class.
        /// </summary>
        /// <param name="configuration">parser configuration</param>
        /// <param name="logger">injected logger</param>
        public LumenWorksCsvParser(ParserConfiguration configuration, ILogger logger)
            : base(configuration, logger)
        {
            Arguments.NotNull(configuration, nameof(configuration));
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <inheritdoc />
        public override IEnumerable<string> FieldHeaders()
        {
            return this.headersCache;
        }

        /// <summary>
        /// Parse via stream
        /// </summary>
        /// <param name="stream">stream</param>
        /// <returns>records</returns>
        public IEnumerable<TRecord> ParseStream(TextReader stream)
        {
            using var reader = new CsvReader(stream, this.Configuration.HasHeaders, this.Configuration.DelimiterChar, this.Configuration.QuoteChar, this.Configuration.EscapeChar, this.Configuration.CommentChar, ValueTrimmingOptions.All, this.Configuration.BufferSize);

            this.headersCache = reader.GetFieldHeaders().Select(m => m.Trim('|')).ToList();

            this.Mapper.VerifyHeaders(this);

            foreach (var dataRow in reader)
            {
                var row = this.OnCreateRow(dataRow);

                this.AddRecord(row);
            }

            return this.Records;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Reviewed")]
        protected override IRetrievalResult<IEnumerable<TRecord>, IRetrievalMetadata, ParserContext> OnRetrieve(ParserContext context)
        {
            Arguments.NotNull(context, nameof(context));

            var metadata = new RetrievalMetadata
            {
                Endpoint = context.Path.ToString(),
            };

            try
            {
                using (var stream = this.CreateStream(context))
                {
                    this.ParseStream(stream);
                }

                metadata.Rows = this.Records.Count();

                foreach (var field in this.FieldHeaders())
                {
                    metadata.AddField(field);
                }
            }
            catch (Exception ex)
            {
                metadata.Exception = ex;
            }

            metadata.EndTime = DateTimeOffset.Now;

            var retrieval = new RetrievalResult<IEnumerable<TRecord>, RetrievalMetadata, ParserContext>(this.Records, metadata, context);
            return retrieval;
        }

        /// <summary>
        ///     Creates the stream reader
        /// </summary>
        /// <param name="context">context for the current parse</param>
        /// <returns>stream reader</returns>
        protected StreamReader CreateStream(ParserContext context)
        {
            Arguments.NotNull(context, nameof(context));

            StreamReader streamReader = null;

            try
            {
                // HACK: the LumenWorks CsvParser has a limitation where it can only use a single character as a delimeter
                // in this case we have to intercept the stream, and replace all "||" with '|' on the fly
                if (this.Configuration.DelimiterChar == '|')
                {
                    streamReader = new TransformationReader(context.Path.LocalPath);
                }
                else
                {
                    streamReader = new StreamReader(context.Path.LocalPath);
                }

                for (var skip = 0; skip < this.Configuration.SkipLinesBeforeHeader; ++skip)
                {
                    streamReader.ReadLine();
                }

                return streamReader;
            }
            catch (Exception)
            {
                streamReader?.Dispose();

                throw;
            }
        }

        /// <summary>
        ///     logic for creating a record class from the data cells
        /// </summary>
        /// <param name="dataCells">parsed cells</param>
        /// <returns>class created from cells</returns>
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "reviewed")]
        protected virtual TRecord OnCreateRow(string[] dataCells)
        {
            Arguments.NotNull(dataCells, nameof(dataCells));

            var row = new TRecord();

            foreach (var header in this.headersCache)
            {
                if (!this.Mapper.ContainsHeader(header))
                {
                    if (this.Configuration.AllowFileToHaveExtraProperties)
                    {
                        continue;
                    }

                    throw new KeyNotFoundException("Header:" + header + "not found on class");
                }

                var property = this.Mapper[header];

                // get underlying type
                var columnType = ReflectionHelper.GetPropertyDataType(property);

                // get column value in csv (in string)
                var columnName = header;
                var columnIdx = this.headersCache.FindIndex(m => m == header);

                var cellValue = dataCells[columnIdx].Trim('|');

                // get if white space
                var isWhiteSpace = string.IsNullOrWhiteSpace(cellValue) || cellValue == "*";

                // get if nullable
                var isNullableType = ReflectionHelper.IsNullableValueType(property);

                var propertyType = property.PropertyType;

                if (isNullableType)
                {
                    propertyType = Nullable.GetUnderlyingType(property.PropertyType);
                }

                if (propertyType.IsEnum)
                {
                    columnType = "Enum";
                }

                // parseFlag, true: parse the string
                var parseFlag = (!isNullableType) || (isNullableType && !isWhiteSpace);

                if (!parseFlag)
                {
                    continue;
                }

                // parse and assign
                switch (columnType)
                {
                    case "String":
                        {
                            var parsed = cellValue;
                            property.SetValue(row, parsed);
                            break;
                        }

                    case "Int16":
                        {
                            var parsed = this.ParseInt16(cellValue, columnName);
                            property.SetValue(row, parsed);
                            break;
                        }

                    case "Int32":
                        {
                            var parsed = this.ParseInt32(cellValue, columnName);
                            property.SetValue(row, parsed);

                            break;
                        }

                    case "Int64":
                        {
                            var parsed = this.ParseInt64(cellValue, columnName);
                            property.SetValue(row, parsed);
                            break;
                        }

                    case "Double":
                        {
                            var parsed = this.ParseDouble(cellValue, columnName);
                            property.SetValue(row, parsed);
                            break;
                        }

                    case "Decimal":
                        {
                            var parsed = this.ParseDecimal(cellValue, columnName);
                            property.SetValue(row, parsed);
                            break;
                        }

                    case "DateTime":
                        {
                            var parsed = this.ParseDateTime(cellValue, columnName);
                            property.SetValue(row, parsed);
                            break;
                        }

                    case "DateTimeOffset":
                        {
                            var parsed = this.ParseDateTimeOffset(cellValue, columnName);
                            property.SetValue(row, parsed);
                            break;
                        }

                    case "Boolean":
                        {
                            var parsed = this.ParseBoolean(cellValue, columnName);
                            property.SetValue(row, parsed);
                            break;
                        }

                    case "Guid":
                        {
                            var parsed = this.ParseGuid(cellValue, columnName);
                            property.SetValue(row, parsed);
                            break;
                        }

                    case "IPAddress":
                        {
                            var parsed = this.ParseIpAddress(cellValue, columnName);
                            property.SetValue(row, parsed);
                            break;
                        }

                    case "Enum":
                        {
                            var parsed = this.ParseEnum(cellValue, columnName, propertyType);
                            property.SetValue(row, parsed);
                            break;
                        }

                    default:
                        {
                            throw new ArgumentException(FormattableString.Invariant($"property datatype {columnType} not supported"));
                        }
                }
            }

            return row;
        }

        /// <inheritdoc/>
        protected override void OnInitialize()
        {
        }
    }
}