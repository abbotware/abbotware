// -----------------------------------------------------------------------
// <copyright file="ConfigurationHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Microsoft
{
    using System.Collections.Generic;
    using System.IO;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using global::Microsoft.Extensions.Configuration;

    /// <summary>
    /// Helper methods for interacting with IConfiguration
    /// </summary>
    public static class ConfigurationHelper
    {
        /// <summary>
        /// name of the app settings json file
        /// </summary>
        public const string AppSettingsFileName = "appsettings.json";

        /// <summary>
        /// Binds a configuration to a poco class
        /// </summary>
        /// <typeparam name="TConfig">config class (POCO)</typeparam>
        /// <param name="config">IConfiguration being extended</param>
        /// <param name="section">name of section</param>
        /// <returns>poco class with config values</returns>
        public static TConfig BindSection<TConfig>(this IConfiguration config, string section)
            where TConfig : new()
        {
            config = Arguments.EnsureNotNull(config, nameof(config));

            var cfg = new TConfig();

            if (!config.GetSection(section).Exists())
            {
                throw new KeyNotFoundException($"section: {section} not present in config");
            }

            return Bind(config, section, cfg);
        }

        /// <summary>
        /// Binds a configuration to a poco class
        /// </summary>
        /// <typeparam name="TConfig">config class (POCO)</typeparam>
        /// <param name="config">IConfiguration being extended</param>
        /// <param name="section">name of section</param>
        /// <returns>poco class with config values</returns>
        public static TConfig BindSectionOrNew<TConfig>(this IConfiguration config, string section)
            where TConfig : new()
        {
            config = Arguments.EnsureNotNull(config, nameof(config));

            var cfg = new TConfig();

            if (!config.GetSection(section).Exists())
            {
                return cfg;
            }

            return Bind(config, section, cfg);
        }

        /// <summary>
        /// Binds a configuration to a poco class
        /// </summary>
        /// <typeparam name="TConfig">config class (POCO)</typeparam>
        /// <param name="config">IConfiguration being extended</param>
        /// <param name="section">name of section</param>
        /// <returns>poco class with config values</returns>
        public static TConfig? BindSectionOrDefault<TConfig>(this IConfiguration config, string section)
            where TConfig : new()
        {
            config = Arguments.EnsureNotNull(config, nameof(config));

            var cfg = new TConfig();

            if (!config.GetSection(section).Exists())
            {
                return default;
            }

            return Bind(config, section, cfg);
        }

        /// <summary>
        /// Loads an IConfiguration from an appsettings file
        /// </summary>
        /// <param name="file">name of file</param>
        /// <returns>IConfiguration loaded from file</returns>
        public static IConfiguration AppSettingsJson(string file = AppSettingsFileName)
        {
            return AppSettingsJson(file, string.Empty);
        }

        /// <summary>
        /// Loads an IConfiguration from an appsettings file
        /// </summary>
        /// <param name="file">name of file</param>
        /// <param name="environment">current environment</param>
        /// <returns>IConfiguration loaded from file</returns>
        public static IConfiguration AppSettingsJson(string file, string environment)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile(file);

            // TODO: add option for env variables?
            ////.AddEnvironmentVariables()

            if (environment.IsNotBlank())
            {
                var fileName = Path.GetFileName(file);
                builder = builder.AddJsonFile($"{fileName}.{environment}.json", optional: true);
            }

            var config = builder.Build();

            return config;
        }

        private static TConfig Bind<TConfig>(IConfiguration config, string section, TConfig cfg)
            where TConfig : new()
        {
            config.GetSection(section)
                .Bind(cfg);

            return cfg;
        }
    }
}
