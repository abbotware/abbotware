//-----------------------------------------------------------------------
// <copyright file="MetricTypeId.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.IntegrationTests.Interop.Amazon
{
    /// <summary>
    ///     Global enum for Metric Type Id
    /// </summary>
    internal enum MetricTypeId
    {
        /// <summary>
        ///     Unknown Metric Type
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///     Metric type for a Ping - Roundtrip
        /// </summary>
        PingRoundtrip = 1,

        /// <summary>
        ///     Metric type for a Ping - Host
        /// </summary>
        PingHost = 2,

        /// <summary>
        ///     Metric type for a Ping - Response Payload
        /// </summary>
        PingResponsePayload = 3,

        /// <summary>
        ///     Metric type for a System - Uptime
        /// </summary>
        SystemUptime = 4,

        /// <summary>
        ///     Metric type for Process - Uptime
        /// </summary>
        ProcessUptime = 5,
    }
}
