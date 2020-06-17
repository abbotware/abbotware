// -----------------------------------------------------------------------
// <copyright file="SqsHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Sqs
{
    using Abbotware.Core.Logging;
    using Abbotware.Interop.Aws.Sqs.Configuration;
    using Abbotware.Interop.Aws.Sqs.Configuration.Models;
    using Abbotware.Interop.Aws.Sqs.Plugins;
    using Abbotware.Interop.Microsoft;

    /// <summary>
    /// Redis Helper function
    /// </summary>
    public static class SqsHelper
    {
        /// <summary>
        /// Create a sqs configuration for from a settings file
        /// </summary>
        /// <param name="section">config section to use for binding</param>
        /// <param name="file">config file to use for settings</param>
        /// <returns>sqs connection</returns>
        public static ISqsSettings GetSqsConfigurationFromFile(string section = SqsSettings.DefaultSection,  string file = ConfigurationHelper.AppSettingsFileName)
        {
            var settings = ConfigurationHelper.AppSettingsJson(file);
            var cfg = settings.BindSection<SqsSettings>(section);
            return cfg;
        }

        /// <summary>
        /// Create a sqs connection from a config
        /// </summary>
        /// <param name="cfg">configuration</param>
        /// <param name="logger">injected logger</param>
        /// <returns>sqs factory</returns>
        public static ISqsConnectionFactory CreateFactory(ISqsSettings cfg, ILogger logger)
        {
            return new SqsConnectionFactory(cfg, logger);
        }

        /// <summary>
        /// Create a sqs factory
        /// </summary>
        /// <param name="logger">injected logger</param>
        /// <param name="section">config section to use for binding</param>
        /// <param name="file">config file to use for settings</param>
        /// <returns>sqs factory</returns>
        public static ISqsConnectionFactory CreateFactory(ILogger logger, string section = SqsSettings.DefaultSection, string file = ConfigurationHelper.AppSettingsFileName)
        {
            var cfg = GetSqsConfigurationFromFile(section, file);

            return CreateFactory(cfg, logger);
        }

        /// <summary>
        /// Create a sqs connection
        /// </summary>
        /// <param name="logger">injected logger</param>
        /// <param name="section">config section to use for binding</param>
        /// <param name="file">config file to use for settings</param>
        /// <returns>redis connection</returns>
        public static ISqsConnection CreateConnection(ILogger logger, string section = SqsSettings.DefaultSection, string file = ConfigurationHelper.AppSettingsFileName)
        {
            var factory = CreateFactory(logger, section, file);

            return factory.Create();
        }
    }
}