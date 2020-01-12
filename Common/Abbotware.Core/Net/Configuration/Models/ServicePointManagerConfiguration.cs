// -----------------------------------------------------------------------
// <copyright file="ServicePointManagerConfiguration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Net.Configuration.Models
{
    using System;
    using System.Net;
    using System.Net.Security;
    using Abbotware.Core.Helpers;

    /// <summary>
    /// Configuration that encapsulates the settable properties on the ServicePointManager
    /// </summary>
    public class ServicePointManagerConfiguration : IServicePointManagerConfiguration
    {
        /// <summary>
        /// Gets or sets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        public bool? ReusePort { get; set; }

        /// <summary>
        /// Gets or sets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        public RemoteCertificateValidationCallback? ServerCertificateValidationCallback { get; set; }

        /// <summary>
        /// Gets or sets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        public TimeSpan? DnsRefreshTimeout { get; set; }

        /// <summary>
        /// Gets or sets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        public bool? EnableDnsRoundRobin { get; set; }

        /// <summary>
        /// Gets or sets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        public bool? Expect100Continue { get; set; }

        /// <summary>
        /// Gets or sets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        public bool? UseNagleAlgorithm { get; set; }

        /// <summary>
        /// Gets or sets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        public TimeSpan? MaxServicePointIdleTime { get; set; }

        /// <summary>
        /// Gets or sets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        public int? DefaultConnectionLimit { get; set; }

        /// <summary>
        /// Gets or sets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        public int? MaxServicePoints { get; set; }

        /// <summary>
        /// Gets or sets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        public SecurityProtocolType? SecurityProtocol { get; set; }

        /// <summary>
        /// Gets or sets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        public bool? CheckCertificateRevocationList { get; set; }

        /// <summary>
        /// Gets or sets the ServicePointManager config value
        /// <see cref="ServicePointManager"/>
        /// </summary>
        public TcpKeepAlive? TcpKeepAlive { get; set; }

        /// <summary>
        /// Creates the legacy values for the service point manager
        /// </summary>
        /// <returns>config object</returns>
        public static ServicePointManagerConfiguration CreateDefault()
        {
            var cfg = new ServicePointManagerConfiguration()
            {
                Expect100Continue = false,
                UseNagleAlgorithm = false,
                CheckCertificateRevocationList = false,
                SecurityProtocol = SecurityProtocolType.Tls12,

                TcpKeepAlive = new TcpKeepAlive
                {
                    KeepAliveTime = TimeSpan.FromMilliseconds(100),
                    KeepAliveInterval = TimeSpan.FromMilliseconds(500),
                },

                DefaultConnectionLimit = 100,
            };

#if DEBUG
#pragma warning disable CA5359 // Do Not Disable Certificate Validation
            cfg.ServerCertificateValidationCallback += (sender, cert, chain, error) => true;
#pragma warning restore CA5359 // Do Not Disable Certificate Validation
#endif

            if (!PlatformHelper.IsUnix)
            {
                cfg.ReusePort = true;
                cfg.EnableDnsRoundRobin = false;
            }

            return cfg;
        }
    }
}