// -----------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Microsoft
{
    using System;
    using System.IO;
    using Abbotware.Core.Helpers;
    using global::Microsoft.Extensions.Configuration;

    /// <summary>
    /// Settings file related helpers
    /// </summary>
    public static class AppSettings
    {
        /// <summary>
        /// locates and loads the settings file
        /// </summary>
        /// <param name="environment">current environment</param>
        /// <returns>loaded configuration</returns>
        public static IConfiguration LocateAndLoad(string environment = null)
        {
            return ConfigurationHelper.AppSettingsJson(Locate(), environment);
        }

        /// <summary>
        /// Locates the Settings file based on some patterns
        /// </summary>
        /// <returns>name of found file</returns>
        public static string Locate()
        {
            var paths = new[]
            {
                $"AppSettings{Path.DirectorySeparatorChar}{Environment.MachineName}.json",
                $"AppSettings{Path.DirectorySeparatorChar}ci.json",
                $"AppSettings{Path.DirectorySeparatorChar}dev.json",
                $"AppSettings{Path.DirectorySeparatorChar}{ConfigurationHelper.AppSettingsFileName}",
                ConfigurationHelper.AppSettingsFileName,
            };

            return PathHelper.FindFirstFile(paths, "No Settings File found");
        }
    }
}
