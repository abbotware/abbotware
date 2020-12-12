// -----------------------------------------------------------------------
// <copyright file="BaseCsvParser{TRecord,TRetrievalContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.ExtensionPoints.Text
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Net;
    using System.Threading;
    using Abbotware.Core.Data.ExtensionPoints.Retrieval;
    using Abbotware.Core.Data.Plugins.Configuration;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;

    /// <summary>
    ///     Base CSV parser classes
    /// </summary>
    /// <typeparam name="TRecord">type of data</typeparam>
    /// <typeparam name="TRetrievalContext">context of current retrievial</typeparam>
    public abstract class BaseCsvParser<TRecord, TRetrievalContext> : BaseComponent<ParserConfiguration>, IRecordListRetriever<TRecord, IRetrievalMetadata, TRetrievalContext>, IFieldBasedParser<TRecord>
        where TRecord : new()
    {
        /// <summary>
        ///     internal converter object for bool
        /// </summary>
        private readonly TypeConverter boolConverter;

        /// <summary>
        ///     internal set of custom converters
        /// </summary>
        private readonly Dictionary<string, Func<string, object>> customConvertors;

        /// <summary>
        ///     internal converter object for date time
        /// </summary>
        private readonly TypeConverter dateTimeConverter;

        /// <summary>
        ///     internal converter object for date time offest
        /// </summary>
        private readonly TypeConverter dateTimeOffsetConverter;

        /// <summary>
        ///     internal converter object for decimal
        /// </summary>
        private readonly TypeConverter decimalConverter;

        /// <summary>
        ///     internal converter object for double
        /// </summary>
        private readonly TypeConverter doubleConverter;

        /// <summary>
        ///     internal converter object for Guid
        /// </summary>
        private readonly TypeConverter guidConverter;

        /// <summary>
        ///     internal converter object for int
        /// </summary>
        private readonly TypeConverter intConverter;

        /// <summary>
        ///     internal converter object for long
        /// </summary>
        private readonly TypeConverter longConverter;

        /// <summary>
        ///     list of parsed records
        /// </summary>
        private readonly List<TRecord> records = new List<TRecord>();

        /// <summary>
        ///     internal converter object for short
        /// </summary>
        private readonly TypeConverter shortConverter;

        /// <summary>
        ///     internal converter object for uint
        /// </summary>
        private readonly TypeConverter uintConverter;

        /// <summary>
        ///     internal converter object for ushort
        /// </summary>
        private readonly TypeConverter ushortConverter;

        /// <summary>
        ///     counts the number of times the parser has run
        /// </summary>
        private int runCounter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseCsvParser{TRecord, TRetrievalContext}" /> class.
        /// </summary>
        /// <param name="configuration">injected configuration</param>
        /// <param name="logger">injected logger</param>
        protected BaseCsvParser(ParserConfiguration configuration, ILogger logger)
            : base(configuration, logger)
        {
            Arguments.NotNull(configuration, nameof(configuration));
            Arguments.NotNull(logger, nameof(logger));

            this.longConverter = TypeDescriptor.GetConverter(typeof(long));
            this.ushortConverter = TypeDescriptor.GetConverter(typeof(ushort));
            this.shortConverter = TypeDescriptor.GetConverter(typeof(short));
            this.decimalConverter = TypeDescriptor.GetConverter(typeof(decimal));
            this.doubleConverter = TypeDescriptor.GetConverter(typeof(double));
            this.boolConverter = TypeDescriptor.GetConverter(typeof(bool));
            this.uintConverter = TypeDescriptor.GetConverter(typeof(uint));
            this.intConverter = TypeDescriptor.GetConverter(typeof(int));
            this.dateTimeOffsetConverter = TypeDescriptor.GetConverter(typeof(DateTimeOffset));
            this.dateTimeConverter = TypeDescriptor.GetConverter(typeof(DateTime));
            this.guidConverter = TypeDescriptor.GetConverter(typeof(Guid));

#pragma warning disable CA1062 // Validate arguments of public methods
            this.customConvertors = configuration.CustomPropertyConvertors;
#pragma warning restore CA1062 // Validate arguments of public methods

            this.Mapper = new MappingHelper<TRecord>(configuration.AllowFileToHaveExtraProperties, configuration.AllowClassToHaveExtraProperties);
        }

        /// <summary>
        ///     Gets the mapping helper
        /// </summary>
        protected MappingHelper<TRecord> Mapper { get; }

        /// <summary>
        ///     Gets the collection of parsed records
        /// </summary>
        protected IEnumerable<TRecord> Records => this.records;

        /// <inheritdoc />
        public abstract IEnumerable<string> FieldHeaders();

        /// <inheritdoc />
        public IEnumerable<TRecord> Parse()
        {
            return this.Retrieve(default).Data;
        }

        /// <inheritdoc />
        public IRetrievalResult<IEnumerable<TRecord>, IRetrievalMetadata, TRetrievalContext> Retrieve(TRetrievalContext context)
        {
            if (Interlocked.Increment(ref this.runCounter) > 1)
            {
                throw new InvalidOperationException(FormattableString.Invariant($"already run{this.GetType().Name}"));
            }

            return this.OnRetrieve(context);
        }

        /// <summary>
        ///     Hook to implement custom retrieve logic
        /// </summary>
        /// <param name="context">retrival context</param>
        /// <returns>retrieval result</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "reviewed")]
        protected abstract IRetrievalResult<IEnumerable<TRecord>, IRetrievalMetadata, TRetrievalContext> OnRetrieve(TRetrievalContext context);

        /// <summary>
        ///     Adds record to internal list
        /// </summary>
        /// <param name="record">record to add</param>
        protected void AddRecord(TRecord record)
        {
            this.records.Add(record);
        }

        /// <summary>
        ///     Parses a Bool from the provided text string
        /// </summary>
        /// <param name="text">text to parse</param>
        /// <param name="propertyName">name of property field</param>
        /// <returns>parsed value</returns>
        protected bool ParseBoolean(string text, string propertyName)
        {
            try
            {
                var parsed = this.boolConverter.ConvertFrom(text);

                return (bool)parsed;
            }
            catch
            {
                FormattableString message = $"Unabled to convert:{text} to Bool for property:{propertyName}";
                throw new ArgumentException(message.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        ///     Parses a Guid from the provided text string
        /// </summary>
        /// <param name="text">text to parse</param>
        /// <param name="propertyName">name of property field</param>
        /// <returns>parsed value</returns>
        protected Guid ParseGuid(string text, string propertyName)
        {
            try
            {
                var parsed = this.guidConverter.ConvertFrom(text);

                return (Guid)parsed;
            }
            catch
            {
                FormattableString message = $"Unabled to convert:{text} to Guid for property:{propertyName}";
                throw new ArgumentException(message.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        ///     Parses an enum from the provided text string
        /// </summary>
        /// <param name="text">text to parse</param>
        /// <param name="propertyName">name of property field</param>
        /// <param name="enumType">type of the enum</param>
        /// <returns>parsed value</returns>
        protected object ParseEnum(string text, string propertyName, Type enumType)
        {
            try
            {
                var parsed = Enum.Parse(enumType, text, true);

                return parsed;
            }
            catch
            {
                FormattableString message = $"Unabled to convert:{text} to Enum:{enumType?.Name} for property:{propertyName}";
                throw new ArgumentException(message.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        ///     Parses an IPAddress Guid from the provided text string
        /// </summary>
        /// <param name="text">text to parse</param>
        /// <param name="propertyName">name of property field</param>
        /// <returns>parsed value</returns>
        protected IPAddress ParseIpAddress(string text, string propertyName)
        {
            try
            {
                var parsed = IPAddress.Parse(text);

                return parsed;
            }
            catch
            {
                FormattableString message = $"Unabled to convert:{text} to IPAddress for property:{propertyName}";
                throw new ArgumentException(message.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        ///     Parses an UInt16 (ushort) from the provided text string
        /// </summary>
        /// <param name="text">text to parse</param>
        /// <param name="propertyName">name of property field</param>
        /// <returns>parsed value</returns>
        protected ushort ParseUInt16(string text, string propertyName)
        {
            try
            {
                var parsed = this.ushortConverter.ConvertFrom(text);
                return (ushort)parsed;
            }
            catch
            {
                FormattableString message = $"Unabled to convert:{text} to UInt16 for property:{propertyName}";
                throw new ArgumentException(message.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        ///     Parses an Int16 (short) from the provided text string
        /// </summary>
        /// <param name="text">text to parse</param>
        /// <param name="propertyName">name of property field</param>
        /// <returns>parsed value</returns>
        protected short ParseInt16(string text, string propertyName)
        {
            try
            {
                if (this.customConvertors.ContainsKey(propertyName))
                {
                    return (short)this.customConvertors[propertyName].Invoke(text);
                }

                var parsed = this.shortConverter.ConvertFrom(text);
                return (short)parsed;
            }
            catch (Exception ex)
            {
                FormattableString message = $"Unabled to convert:{text} to Int16 for property:{propertyName}";
                throw new ArgumentException(message.ToString(CultureInfo.InvariantCulture), ex);
            }
        }

        /// <summary>
        ///     Parses an Int32 (int) from the provided text string
        /// </summary>
        /// <param name="text">text to parse</param>
        /// <param name="propertyName">name of property field</param>
        /// <returns>parsed value</returns>
        protected int ParseInt32(string text, string propertyName)
        {
            try
            {
                var parsed = this.intConverter.ConvertFrom(text);
                return (int)parsed;
            }
            catch
            {
                FormattableString message = $"Unabled to convert:{text} to Int32 for property:{propertyName}";
                throw new ArgumentException(message.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        ///     Parses an UInt32 (uint) from the provided text string
        /// </summary>
        /// <param name="text">text to parse</param>
        /// <param name="propertyName">name of property field</param>
        /// <returns>parsed value</returns>
        protected uint ParseUInt32(string text, string propertyName)
        {
            try
            {
                var parsed = this.uintConverter.ConvertFrom(text);
                return (uint)parsed;
            }
            catch
            {
                FormattableString message = $"Unabled to convert:{text} to UInt32 for property:{propertyName}";
                throw new ArgumentException(message.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        ///     Parses an Int64 (long) from the provided text string
        /// </summary>
        /// <param name="text">text to parse</param>
        /// <param name="propertyName">name of property field</param>
        /// <returns>parsed value</returns>
        protected long ParseInt64(string text, string propertyName)
        {
            try
            {
                var parsed = this.longConverter.ConvertFrom(text);
                return (long)parsed;
            }
            catch
            {
                FormattableString message = $"Unabled to convert:{text} to Int64 for property:{propertyName}";
                throw new ArgumentException(message.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        ///     Parses a Decimal from the provided text string
        /// </summary>
        /// <param name="text">text to parse</param>
        /// <param name="propertyName">name of property field</param>
        /// <returns>parsed value</returns>
        protected decimal ParseDecimal(string text, string propertyName)
        {
            try
            {
                var parsed = this.decimalConverter.ConvertFrom(text);
                return (decimal)parsed;
            }
            catch
            {
                FormattableString message = $"Unabled to convert:{text} to Decimal for property:{propertyName}";
                throw new ArgumentException(message.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        ///     Parses a DateTime from the provided text string
        /// </summary>
        /// <param name="text">text to parse</param>
        /// <param name="propertyName">name of property field</param>
        /// <returns>parsed value</returns>
        protected DateTime ParseDateTime(string text, string propertyName)
        {
            try
            {
                var parsed = this.dateTimeConverter.ConvertFrom(text);
                return (DateTime)parsed;
            }
            catch
            {
                FormattableString message = $"Unabled to convert:{text} to DateTimeOffset for property:{propertyName}";
                throw new ArgumentException(message.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        ///     Parses a DateTimeOffset from the provided text string
        /// </summary>
        /// <param name="text">text to parse</param>
        /// <param name="propertyName">name of property field</param>
        /// <returns>parsed value</returns>
        protected DateTimeOffset ParseDateTimeOffset(string text, string propertyName)
        {
            try
            {
                var parsed = this.dateTimeOffsetConverter.ConvertFrom(text);
                return (DateTimeOffset)parsed;
            }
            catch
            {
                FormattableString message = $"Unabled to convert:{text} to DateTimeOffset for property:{propertyName}";
                throw new ArgumentException(message.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        ///     Parses a double from the provided text string
        /// </summary>
        /// <param name="text">text to parse</param>
        /// <param name="propertyName">name of property field</param>
        /// <returns>parsed value</returns>
        protected double ParseDouble(string text, string propertyName)
        {
            try
            {
                var parsed = this.doubleConverter.ConvertFrom(text);
                return (double)parsed;
            }
            catch
            {
                FormattableString message = $"Unabled to convert:{text} to Double for property:{propertyName}";
                throw new ArgumentException(message.ToString(CultureInfo.InvariantCulture));
            }
        }
    }
}