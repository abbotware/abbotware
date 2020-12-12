// -----------------------------------------------------------------------
// <copyright file="ExporterConfiguration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Plugins.Configuration
{
    using System;
    using System.Globalization;
    using System.IO;
    using Abbotware.Core.Configuration;

    /// <summary>
    ///     Configuration class for the Exporter plugin
    /// </summary>
    public class ExporterConfiguration : BaseOptions
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExporterConfiguration" /> class.
        /// </summary>
        /// <param name="exportPath">the base export path</param>
        public ExporterConfiguration(Uri exportPath)
        {
            Arguments.NotNull(exportPath, nameof(exportPath));

            this.ExportPath = exportPath;
            this.LogOptions = true;
        }

        /// <summary>
        ///     Gets the base Export path
        /// </summary>
        public Uri ExportPath { get; }

        /// <summary>
        ///     generates the export path
        /// </summary>
        /// <param name="basePath">base path of exports</param>
        /// <param name="timestamp">timestamp to encode the path with</param>
        /// <returns>URI containing the export path</returns>
        public static Uri GenerateExportPath(Uri basePath, DateTimeOffset timestamp)
        {
            Arguments.NotNull(basePath, nameof(basePath));

            var folder = timestamp.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture);
#pragma warning disable CA1062 // Validate arguments of public methods
            var folderPath = Path.Combine(basePath.LocalPath, folder);
#pragma warning restore CA1062 // Validate arguments of public methods
            return new Uri(folderPath);
        }
    }
}