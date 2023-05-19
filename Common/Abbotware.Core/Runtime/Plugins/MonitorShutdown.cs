// -----------------------------------------------------------------------
// <copyright file="MonitorShutdown.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Runtime.Plugins
{
    using System.Threading;

    /// <summary>
    /// monitor a cancellation token that can be used to signal shutdown
    /// </summary>
    /// <remarks>allows an application to 'self-terminate' when it knows it should exit</remarks>
    public class MonitorShutdown : IMonitorShutdown
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorShutdown"/> class.
        /// </summary>
        /// <param name="token">token used for shutdown</param>
        public MonitorShutdown(CancellationToken token)
        {
            this.Token = token;
        }

        /// <inheritdoc/>
        public CancellationToken Token { get; }
    }
}