// -----------------------------------------------------------------------
// <copyright file="TcpKeepAlive.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Net.Configuration.Models
{
    using System;

    /// <summary>
    /// config optiosn related to tcp keep alive
    /// </summary>
    public class TcpKeepAlive
    {
        /// <summary>
        /// Gets or sets the Keep Alive Time
        /// </summary>
        public TimeSpan KeepAliveTime { get; set; }

        /// <summary>
        /// Gets or sets the Keep Alive Interval
        /// </summary>
        public TimeSpan KeepAliveInterval { get; set; }
    }
}