// -----------------------------------------------------------------------
// <copyright file="SqsHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Sqs
{
    using System;
    using Abbotware.Core;
    using Abbotware.Interop.Aws.Sqs.Configuration;
    using Abbotware.Interop.Aws.Sqs.Configuration.Models;
    using Abbotware.Interop.Aws.Sqs.Plugins;
    using Abbotware.Interop.Microsoft;
    using global::Microsoft.Extensions.Logging;

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
            var cfg = ConfigurationHelper.AppSettingsJson(file).BindSection<SqsSettings>(section);

            if (cfg.Username == "USE_ENV")
            {
                cfg.Username = Arguments.EnsureNotNullOrWhitespace(Environment.GetEnvironmentVariable("SQS_USERNAME"), "SQS_USERNAME");
            }

            if (cfg.Password == "USE_ENV")
            {
                cfg.Password = Arguments.EnsureNotNullOrWhitespace(Environment.GetEnvironmentVariable("SQS_PASSWORD"), "SQS_PASSWORD");
            }

            return cfg;
        }

        /// <summary>
        /// Create a sqs connection from a config
        /// </summary>
        /// <param name="cfg">configuration</param>
        /// <param name="factory">injected logger factory</param>
        /// <returns>sqs factory</returns>
        public static ISqsConnectionFactory CreateFactory(ISqsSettings cfg, ILoggerFactory factory)
        {
            return new SqsConnectionFactory(cfg, factory);
        }

        /// <summary>
        /// Create a sqs factory
        /// </summary>
        /// <param name="factory">injected logger factory</param>
        /// <param name="section">config section to use for binding</param>
        /// <param name="file">config file to use for settings</param>
        /// <returns>sqs factory</returns>
        public static ISqsConnectionFactory CreateFactory(ILoggerFactory factory, string section = SqsSettings.DefaultSection, string file = ConfigurationHelper.AppSettingsFileName)
        {
            var cfg = GetSqsConfigurationFromFile(section, file);

            return CreateFactory(cfg, factory);
        }

        /// <summary>
        /// Create a sqs connection
        /// </summary>
        /// <param name="factory">injected logger factory</param>
        /// <param name="section">config section to use for binding</param>
        /// <param name="file">config file to use for settings</param>
        /// <returns>redis connection</returns>
        public static ISqsConnection CreateConnection(ILoggerFactory factory, string section = SqsSettings.DefaultSection, string file = ConfigurationHelper.AppSettingsFileName)
        {
            using var f = CreateFactory(factory, section, file);

            return f.Create();
        }
    }
}