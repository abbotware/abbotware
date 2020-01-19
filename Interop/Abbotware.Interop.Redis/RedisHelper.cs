// -----------------------------------------------------------------------
// <copyright file="RedisHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Redis
{
    using Abbotware.Core.Objects;
    using Abbotware.Core.Objects.Configuration.Models;
    using Abbotware.Interop.Microsoft;
    using Abbotware.Interop.Redis.Configuration;
    using Abbotware.Interop.Redis.ExtensionPoints;

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
        /// <param name="file">config file to use for settings</param>
        /// <returns>redis connection</returns>
        public static IRedisConnection CreateRedisConnection(string file = ConfigurationHelper.AppSettingsFileName)
        {
            var cfg = GetRedisConfiguration(file);

            return CreateRedisUsingConfiguration(cfg);
        }

        /// <summary>
        /// Create a redis connection for the unit test
        /// </summary>
        /// <returns>redis connection</returns>
        public static IRedisConnection CreateRedisUsingLocalHost()
        {
            return CreateRedisUsingConfiguration(Defaults.ConnectionOptions);
        }

        /// <summary>
        /// Create a redis connection from a config
        /// </summary>
        /// <param name="cfg">configuration</param>
        /// <returns>redis connection</returns>
        public static IRedisConnection CreateRedisUsingConfiguration(IConnectionOptions cfg)
        {
            using var cf = new RedisConnectionFactory(cfg);

            return cf.Create();
        }
    }
}
