// -----------------------------------------------------------------------
// <copyright file="CsvExporter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Abbotware.Core.Diagnostics;
    using Abbotware.Core.ExtensionPoints;
    using Abbotware.Core.Helpers;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using Abbotware.Core.Plugins.Configuration;

    /// <summary>
    ///     Exporter pluging that creates CSV files
    /// </summary>
    [SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance", Justification = "This is intended")]
    public class CsvExporter : BaseComponent<ExporterConfiguration>, IExporter
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CsvExporter" /> class.
        /// </summary>
        /// <param name="config">exporter configuration</param>
        /// <param name="logger">injected logger</param>
        public CsvExporter(ExporterConfiguration config, ILogger logger)
            : base(config, logger)
        {
            Arguments.NotNull(config, nameof(config));
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <inheritdoc />
        public Uri ExportSingle<TData>(TData data)
        {
            return this.ExportMany(new List<TData> { data }, string.Empty);
        }

        /// <inheritdoc />
        public Uri ExportSingle<TData>(TData data, string prefix)
        {
            return this.ExportMany(new List<TData> { data }, prefix);
        }

        /// <inheritdoc />
        public Uri ExportMany<TData>(IEnumerable<TData> data)
        {
            return this.ExportMany(data, string.Empty);
        }

        /// <summary>
        ///     exports a multiple objects to a report file
        /// </summary>
        /// <typeparam name="TData">type of data to export</typeparam>
        /// <param name="data">list of data to export</param>
        /// <param name="prefix">optional prefix to append to exported file name</param>
        /// <returns>path to generated report file</returns>
        public Uri ExportMany<TData>(IEnumerable<TData> data, string prefix)
        {
            Arguments.NotNull(data, nameof(data));

            var folderPath = this.Configuration.ExportPath.LocalPath;

            Directory.CreateDirectory(folderPath);

            var fileName = typeof(TData).Name + ".csv";

            if (!string.IsNullOrEmpty(prefix))
            {
                fileName = prefix + "-" + fileName;
            }

            fileName = PathHelper.CleanFile(fileName);

            var sb = new StringBuilder();

            var propertyInfos = ReflectionHelper.GetSimpleProperties<TData>();

            var propertyNames = propertyInfos
                .Select(x => x.Name)
                .ToList();

            var headerLine = string.Join(",", propertyNames);
            sb.AppendLine(headerLine);

            foreach (var row in data)
            {
                var propertyValues = propertyInfos
                    .Where(p => p.GetIndexParameters().Length == 0)
                    .Select(x => MakeValueCsvFriendly(x.GetValue(row, null)))
                    .ToList();

                var dataLine = string.Join(",", propertyValues);
                sb.AppendLine(dataLine);
            }

            var file = folderPath + @"\" + fileName;

            File.WriteAllText(file, sb.ToString());

            this.Logger.Info("File Create:{0} in Folder:{1}", fileName, folderPath);

            return new Uri(file);
        }

        /// <summary>
        ///     uset to convert the object into a CSV writeable string
        /// </summary>
        /// <param name="value">object to convert to string</param>
        /// <returns>string representation of object value</returns>
        private static string MakeValueCsvFriendly(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value is INullable sqlValue && sqlValue.IsNull)
            {
                return string.Empty;
            }

            if (value is DateTime)
            {
                var dt = (DateTime)value;

                if (Math.Abs(dt.TimeOfDay.TotalSeconds) < .0001)
                {
                    return dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                }

                return dt.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            }

            if (value is DateTimeOffset)
            {
                var dt = (DateTimeOffset)value;

                string result;

                if (Math.Abs(dt.TimeOfDay.TotalSeconds) < .0001)
                {
                    result = dt.ToString("yyyy-MM-dd zzz", CultureInfo.InvariantCulture);
                }
                else
                {
                    result = dt.ToString("yyyy-MM-dd HH:mm:ss zzz", CultureInfo.InvariantCulture);
                }

                return result;
            }

            var output = value.ToString() ?? string.Empty;

#if NETSTANDARD2_0
            if (output.Contains("\""))
            {
                output = output.Replace("\"", "\"\"");
            }

            if (output.Contains(","))
            {
                output = '"' + output + '"';
            }
#elif NETSTANDARD2_1
            if (output.Contains('"', StringComparison.InvariantCultureIgnoreCase))
            {
                output = output.Replace("\"", "\"\"", StringComparison.InvariantCultureIgnoreCase);
            }

            if (output.Contains(',', StringComparison.InvariantCultureIgnoreCase))
            {
                output = '"' + output + '"';
            }
#endif
            return output;
        }
    }
}