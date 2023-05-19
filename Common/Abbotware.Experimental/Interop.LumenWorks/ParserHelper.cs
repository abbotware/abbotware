// -----------------------------------------------------------------------
// <copyright file="ParserHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.LumenWorks
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Abbotware.Core.Data.Plugins.Configuration;
    using Abbotware.Core.Logging;

    // TODO: consolidate parsers via ParserHelper<T>

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
        ///     Parses a Csv file using the supplied model
        /// </summary>
        /// <typeparam name="TDataRow">model type for csv file row</typeparam>
        /// <param name="reader">text reader</param>
        /// <param name="logger">injected logger</param>
        /// <returns>data rows</returns>
        public static IEnumerable<TDataRow> CsvFile<TDataRow>(TextReader reader, ILogger logger)
            where TDataRow : new()
        {
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
        /// <param name="cfg">parser configuration</param>
        /// <param name="logger">injected logger</param>
        /// <returns>data rows</returns>
        private static IEnumerable<TDataRow> ParseFle<TDataRow>(string filePath, ParserConfiguration cfg, ILogger logger)
            where TDataRow : new()
        {
            using var parser = new LumenWorksCsvParser<TDataRow>(cfg, logger);

            var context = new ParserContext(new Uri(filePath));

            var result = parser.Retrieve(context);

            if (result.Metadata.Exception != null)
            {
                throw new InvalidOperationException("Parse Error", result.Metadata.Exception);
            }

            return result.Data;
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
            using var parser = new LumenWorksCsvParser<TDataRow>(cfg, logger);

            return parser.ParseStream(reader);
        }
    }
}