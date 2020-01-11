// -----------------------------------------------------------------------
// <copyright file="IServicePointManagerConfiguration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Net.Configuration
{
    using System;
    using System.Net;
    using System.Net.Security;
    using Abbotware.Core.Net.Configuration.Models;

    /// <summary>
    /// Readonly Configuration interface that encapsulates the settable properties on the ServicePointManager
    /// </summary>
    public interface IServicePointManagerConfiguration
    {
        /// <summary>
        /// Gets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        bool? CheckCertificateRevocationList { get; }

        /// <summary>
        /// Gets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        int? DefaultConnectionLimit { get; }

        /// <summary>
        /// Gets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        TimeSpan? DnsRefreshTimeout { get; }

        /// <summary>
        /// Gets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        bool? EnableDnsRoundRobin { get; }

        /// <summary>
        /// Gets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        bool? Expect100Continue { get; }

        /// <summary>
        /// Gets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        TimeSpan? MaxServicePointIdleTime { get; }

        /// <summary>
        /// Gets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        int? MaxServicePoints { get; }

        /// <summary>
        /// Gets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        bool? ReusePort { get; }

        /// <summary>
        /// Gets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        SecurityProtocolType? SecurityProtocol { get; }

        /// <summary>
        /// Gets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        RemoteCertificateValidationCallback? ServerCertificateValidationCallback { get; }

        /// <summary>
        /// Gets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        TcpKeepAlive? TcpKeepAlive { get; }

        /// <summary>
        /// Gets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        bool? UseNagleAlgorithm { get; }
    }
}