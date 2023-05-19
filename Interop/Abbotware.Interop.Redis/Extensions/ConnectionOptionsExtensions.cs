// -----------------------------------------------------------------------
// <copyright file="ConnectionOptionsExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Extensions
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.Redis.Configuration;
    using StackExchange.Redis;

    /// <summary>
    /// Extension methods for IConnectionOptions
    /// </summary>
    public static class ConnectionOptionsExtensions
    {
        /// <summary>
        /// converts the configuration to a service stack redis Uri
        /// </summary>
        /// <param name="options">connection options</param>
        /// <returns>redis uri</returns>
        public static ConfigurationOptions ToStackExchange(this IConnectionOptions options)
        {
            options = Arguments.EnsureNotNull(options, nameof(options));

            var port = options.Endpoint.Port;

            if (options.Endpoint.Port == -1)
            {
                port = Defaults.Port;
            }

            var cfg = new ConfigurationOptions();

            cfg.EndPoints.Add(options.Endpoint.Host, port);

            cfg.ClientName = options.Credential?.UserName;
            cfg.Password = options.Credential?.Password;

            return cfg;
        }

        /// <summary>
        /// converts the configuration to stack exchange config
        /// </summary>
        /// <param name="options">connection options</param>
        /// <returns>redis uri</returns>
        public static Uri ToServiceStackUri(this IConnectionOptions options)
        {
            options = Arguments.EnsureNotNull(options, nameof(options));

            var ub = new UriBuilder($"redis://{options.Endpoint.Host}")
            {
                Port = options.Endpoint.Port,
            };

            if (ub.Port == -1)
            {
                ub.Port = Defaults.Port;
            }

            ub.Query = options.Endpoint.Query;

            if (options.Credential != null)
            {
                ub.UserName = options.Credential.UserName;
                ub.Password = options.Credential.Password;
            }

            return ub.Uri;
        }
    }
}
