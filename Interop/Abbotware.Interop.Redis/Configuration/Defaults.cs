// -----------------------------------------------------------------------
// <copyright file="Defaults.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Redis.Configuration
{
    using System;
    using System.Net;
    using Abbotware.Core.Objects;
    using Abbotware.Core.Objects.Configuration.Models;

    /// <summary>
    /// Redis Connection Configuration
    /// </summary>
    public static class Defaults
    {
        /// <summary>
        /// Default config section name
        /// </summary>
        public const string ConfigurationSection = "Redis";

        /// <summary>
        /// default redis port
        /// </summary>
        public const int Port = 6379;

        /// <summary>
        /// Gets the default redis endpoint
        /// </summary>
        public static Uri Endpoint { get; } = new Uri($"redis://localhost:{Port}");

        /// <summary>
        /// Gets the default redis user / pass
        /// </summary>
        public static NetworkCredential Credential { get; } = new NetworkCredential("guest", "guest");

        /// <summary>
        /// Gets the default connection options
        /// </summary>
        public static IConnectionOptions ConnectionOptions { get; } = new ConnectionOptions(Endpoint, Credential);
    }
}