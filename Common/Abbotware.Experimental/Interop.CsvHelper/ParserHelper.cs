// -----------------------------------------------------------------------
// <copyright file="ParserHelper.cs" company="Abbotware, LLC">
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
    using Abbotware.Core.Data.Plugins.Configuration;
    using Abbotware.Core.Logging;
    using global::CsvHelper;

    /// <summary>
    ///     Helper class that provides ease of use functions for the parser class
    /// </summary>
    public static class ParserHelper
    {
        /// <summary>
        ///     Parses a Csv file using the supplied model
        /// </summary>
        /// <typeparam name="TDataRow">model type for csv file row</typeparam>
        /// <param name="filePath">path to file</param>
        /// <param name="logger">injected logger</param>
        /// <returns>data rows</returns>
        public static IEnumerable<TDataRow> CsvFile<TDataRow>(string filePath, ILogger logger)
            where TDataRow : new()
        {
            var cfg = new ParserConfiguration();

            return ParseFle<TDataRow>(filePath, cfg, logger);
        }

        /// <summary>
        ///     Parses a Csv file using the supplied model.  File and Class must match exactly
        /// </summary>
        /// <typeparam name="TDataRow">model type for csv file row</typeparam>
        /// <param name="reader">text reader</param>
        /// <param name="logger">injected logger</param>
        /// <returns>data rows</returns>
        public static IEnumerable<TDataRow> ParseFle<TDataRow>(TextReader reader, ILogger logger)
            where TDataRow : new()
        {
            Arguments.NotNull(reader, nameof(reader));
            Arguments.NotNull(logger, nameof(logger));

            var cfg = new ParserConfiguration();

            return ParseFle<TDataRow>(reader, cfg, logger);
        }

        /// <summary>
        ///     Parses a Csv file using the supplied model.  File and Class must match exactly
        /// </summary>
        /// <typeparam name="TDataRow">model type for csv file row</typeparam>
        /// <param name="reader">text reader</param>
        /// <param name="logger">injected logger</param>
        /// <returns>data rows</returns>
        public static IEnumerable<TDataRow> CsvFileExact<TDataRow>(TextReader reader, ILogger logger)
            where TDataRow : new()
        {
            Arguments.NotNull(reader, nameof(reader));
            Arguments.NotNull(logger, nameof(logger));

            var cfg = new ParserConfiguration
            {
                AllowClassToHaveExtraProperties = false,
                AllowFileToHaveExtraProperties = false,
            };

            return ParseFle<TDataRow>(reader, cfg, logger);
        }

        /// <summary>
        ///     Parses a Csv file using the supplied model.  File and Class must match exactly
        /// </summary>
        /// <typeparam name="TDataRow">model type for csv file row</typeparam>
        /// <param name="filePath">path to file</param>
        /// <param name="logger">injected logger</param>
        /// <returns>data rows</returns>
        public static IEnumerable<TDataRow> CsvFileExact<TDataRow>(string filePath, ILogger logger)
            where TDataRow : new()
        {
            var cfg = new ParserConfiguration
            {
                AllowClassToHaveExtraProperties = false,
                AllowFileToHaveExtraProperties = false,
            };

            return ParseFle<TDataRow>(filePath, cfg, logger);
        }

        /// <summary>
        ///     Parses a Csv file using the supplied model.  File and Class must match exactly
        /// </summary>
        /// <typeparam name="TDataRow">model type for csv file row</typeparam>
        /// <param name="filePath">path to file</param>
        /// <param name="cfg">parser configuration</param>
        /// <param name="logger">injected logger</param>
        /// <returns>data rows</returns>
        private static IEnumerable<TDataRow> ParseFle<TDataRow>(string filePath, ParserConfiguration cfg, ILogger logger)
            where TDataRow : new()
        {
            logger = Arguments.EnsureNotNull(logger, nameof(logger));

            logger.Info($"Csv File:{filePath}");

            using var reader = new StreamReader(filePath);

            return ParseFle<TDataRow>(reader, cfg, logger);
        }

        /// <summary>
        ///     Parses a Csv file using the supplied model.  File and Class must match exactly
        /// </summary>
        /// <typeparam name="TDataRow">model type for csv file row</typeparam>
        /// <param name="reader">text reader</param>
        /// <param name="cfg">parser configuration</param>
        /// <param name="logger">injected logger</param>
        /// <returns>data rows</returns>
        private static IEnumerable<TDataRow> ParseFle<TDataRow>(TextReader reader, ParserConfiguration cfg, ILogger logger)
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