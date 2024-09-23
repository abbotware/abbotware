// -----------------------------------------------------------------------
// <copyright file="Csv.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.CsvHelper
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using Abbotware.Core;
    using Abbotware.Core.Data.Serialization.Options;
    using Abbotware.Core.Extensions;
    using global::CsvHelper;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Helper class that provides ease of use functions for the parser class
    /// </summary>
    public static class Csv
    {
        /// <summary>
        ///     Parses a Csv file using the supplied model
        /// </summary>
        /// <typeparam name="TDataRow">model type for csv file row</typeparam>
        /// <param name="filePath">path to file</param>
        /// <param name="logger">injected logger</param>
        /// <returns>data rows</returns>
        public static IEnumerable<TDataRow> Parse<TDataRow>(string filePath, ILogger logger)
            where TDataRow : new()
        {
            var cfg = new ParserConfiguration();

            return Parse<TDataRow>(filePath, cfg, logger);
        }

        /// <summary>
        ///     Parses a Csv file using the supplied model.  File and Class must match exactly
        /// </summary>
        /// <typeparam name="TDataRow">model type for csv file row</typeparam>
        /// <param name="reader">text reader</param>
        /// <param name="logger">injected logger</param>
        /// <returns>data rows</returns>
        public static IEnumerable<TDataRow> Parse<TDataRow>(TextReader reader, ILogger logger)
            where TDataRow : new()
        {
            Arguments.NotNull(reader, nameof(reader));
            Arguments.NotNull(logger, nameof(logger));

            var cfg = new ParserConfiguration();

            return Parse<TDataRow>(reader, cfg, logger);
        }

        /// <summary>
        ///     Parses a Csv file using the supplied model.  File and Class must match exactly
        /// </summary>
        /// <typeparam name="TDataRow">model type for csv file row</typeparam>
        /// <param name="reader">text reader</param>
        /// <param name="logger">injected logger</param>
        /// <returns>data rows</returns>
        public static IEnumerable<TDataRow> ParseExact<TDataRow>(TextReader reader, ILogger logger)
            where TDataRow : new()
        {
            Arguments.NotNull(reader, nameof(reader));
            Arguments.NotNull(logger, nameof(logger));

            var cfg = new ParserConfiguration
            {
                AllowClassToHaveExtraProperties = false,
                AllowFileToHaveExtraProperties = false,
            };

            return Parse<TDataRow>(reader, cfg, logger);
        }

        /// <summary>
        ///     Parses a Csv file using the supplied model.  File and Class must match exactly
        /// </summary>
        /// <typeparam name="TDataRow">model type for csv file row</typeparam>
        /// <param name="filePath">path to file</param>
        /// <param name="logger">injected logger</param>
        /// <returns>data rows</returns>
        public static IEnumerable<TDataRow> ParseExact<TDataRow>(string filePath, ILogger logger)
            where TDataRow : new()
        {
            var cfg = new ParserConfiguration
            {
                AllowClassToHaveExtraProperties = false,
                AllowFileToHaveExtraProperties = false,
            };

            return Parse<TDataRow>(filePath, cfg, logger);
        }

        /// <summary>
        ///     Parses a Csv file using the supplied model.  File and Class must match exactly
        /// </summary>
        /// <typeparam name="TDataRow">model type for csv file row</typeparam>
        /// <param name="filePath">path to file</param>
        /// <param name="cfg">parser configuration</param>
        /// <param name="logger">injected logger</param>
        /// <returns>data rows</returns>
        private static IEnumerable<TDataRow> Parse<TDataRow>(string filePath, ParserConfiguration cfg, ILogger logger)
            where TDataRow : new()
        {
            logger = Arguments.EnsureNotNull(logger, nameof(logger));

            logger.Info($"Csv File:{filePath}");

            using var reader = new StreamReader(filePath);

            return Parse<TDataRow>(reader, cfg, logger);
        }

        /// <summary>
        ///     Parses a Csv file using the supplied model.  File and Class must match exactly
        /// </summary>
        /// <typeparam name="TDataRow">model type for csv file row</typeparam>
        /// <param name="reader">text reader</param>
        /// <param name="cfg">parser configuration</param>
        /// <param name="logger">injected logger</param>
        /// <returns>data rows</returns>
        private static IEnumerable<TDataRow> Parse<TDataRow>(TextReader reader, ParserConfiguration cfg, ILogger logger)
        where TDataRow : new()
        {
            var c = new global::CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                BufferSize = cfg.BufferSize,
                HasHeaderRecord = cfg.HasHeaders,
            };

            using var csv = new CsvReader(reader, c);

            ////csv.Read();

            ////csv.ReadHeader();

            ////csv.ValidateHeader<TDataRow>();

            ////csv.Read();

            var records = csv.GetRecords<TDataRow>();

            logger.Info($"rows parsed::{records.Count()}");

            return records;
        }
    }
}