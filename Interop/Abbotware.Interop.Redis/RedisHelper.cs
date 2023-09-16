// -----------------------------------------------------------------------
// <copyright file="RedisHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Redis
{
    using Abbotware.Core.Objects;
    using Abbotware.Core.Objects.Configuration.Models;
    using Abbotware.Interop.Microsoft;
    using Abbotware.Interop.Redis.Configuration;
    using Abbotware.Interop.Redis.ExtensionPoints;
    using global::Microsoft.Extensions.Logging;

    /// <summary>
    /// Redis Helper function
    /// </summary>
    public static class RedisHelper
    {
        /// <summary>
        /// Create a redis configuration for from a settings file
        /// </summary>
        /// <param name="file">config file to use for settings</param>
        /// <returns>redis configuration</returns>
        public static IConnectionOptions GetRedisConfiguration(string file = ConfigurationHelper.AppSettingsFileName)
        {
            var settings = ConfigurationHelper.AppSettingsJson(file);

            var cfg = settings.BindSection<BindableConnectionOptions>(Defaults.ConfigurationSection);

            return cfg;
        }

        /// <summary>
        /// Create a redis connection using default AppSettings.json
        /// </summary>
        /// <param name="logger">logger to inject</param>
        /// <param name="file">config file to use for settings</param>
        /// <returns>redis connection</returns>
        public static IRedisConnection CreateRedisConnection(ILogger logger, string file = ConfigurationHelper.AppSettingsFileName)
        {
            var cfg = GetRedisConfiguration(file);

            return CreateRedisUsingConfiguration(cfg, logger);
        }

        /// <summary>
        /// Create a redis connection for the unit test
        /// </summary>
        /// <param name="logger">logger to inject</param>
        /// <returns>redis connection</returns>
        public static IRedisConnection CreateRedisUsingLocalHost(ILogger logger)
        {
            return CreateRedisUsingConfiguration(Defaults.ConnectionOptions, logger);
        }

        /// <summary>
        /// Create a redis connection from a config
        /// </summary>
        /// <param name="cfg">configuration</param>
        /// <param name="logger">logger to inject</param>
        /// <returns>redis connection</returns>
        public static IRedisConnection CreateRedisUsingConfiguration(IConnectionOptions cfg, ILogger logger)
        {
            using var cf = new RedisConnectionFactory(cfg, logger);

            return cf.Create();
        }
    }
}
