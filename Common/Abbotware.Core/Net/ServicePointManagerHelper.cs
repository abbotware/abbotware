// -----------------------------------------------------------------------
// <copyright file="ServicePointManagerHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Net
{
    using System;
    using System.Net;
    using Abbotware.Core.Net.Configuration;

    /// <summary>
    /// helper class for the ServicePointManager
    /// </summary>
    [Obsolete("not in use")]
    public static class ServicePointManagerHelper
    {
        /// <summary>
        /// Configures the service point manager
        /// </summary>
        /// <param name="config">configuration class</param>
        public static void Configure(IServicePointManagerOptions config)
        {
            config = Arguments.EnsureNotNull(config, nameof(config));

            if (config.ReusePort.HasValue)
            {
                ServicePointManager.ReusePort = config.ReusePort.Value;
            }

            if (config.ServerCertificateValidationCallback != null)
            {
                ServicePointManager.ServerCertificateValidationCallback = config.ServerCertificateValidationCallback;
            }

            if (config.DnsRefreshTimeout.HasValue)
            {
                if (config.DnsRefreshTimeout == TimeSpan.MaxValue)
                {
                    ServicePointManager.DnsRefreshTimeout = -1;
                }
                else
                {
                    ServicePointManager.DnsRefreshTimeout = (int)config.DnsRefreshTimeout.Value.TotalMilliseconds;
                }
            }

            if (config.EnableDnsRoundRobin.HasValue)
            {
                ServicePointManager.EnableDnsRoundRobin = config.EnableDnsRoundRobin.Value;
            }

            if (config.Expect100Continue.HasValue)
            {
                ServicePointManager.Expect100Continue = config.Expect100Continue.Value;
            }

            if (config.UseNagleAlgorithm.HasValue)
            {
                ServicePointManager.UseNagleAlgorithm = config.UseNagleAlgorithm.Value;
            }

            if (config.MaxServicePointIdleTime.HasValue)
            {
                ServicePointManager.MaxServicePointIdleTime = (int)config.MaxServicePointIdleTime.Value.TotalMilliseconds;
            }

            if (config.DefaultConnectionLimit.HasValue)
            {
                ServicePointManager.DefaultConnectionLimit = config.DefaultConnectionLimit.Value;
            }

            if (config.MaxServicePoints.HasValue)
            {
                ServicePointManager.MaxServicePoints = config.MaxServicePoints.Value;
            }

            if (config.SecurityProtocol.HasValue)
            {
                ServicePointManager.SecurityProtocol = config.SecurityProtocol.Value;
            }

            if (config.CheckCertificateRevocationList.HasValue)
            {
                ServicePointManager.CheckCertificateRevocationList = config.CheckCertificateRevocationList.Value;
            }

            if (config.TcpKeepAlive != null)
            {
                var kat = (int)config.TcpKeepAlive.KeepAliveTime.TotalMilliseconds;
                var kai = (int)config.TcpKeepAlive.KeepAliveInterval.TotalMilliseconds;

                ServicePointManager.SetTcpKeepAlive(true, kat, kai);
            }
        }
    }
}