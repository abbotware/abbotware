// -----------------------------------------------------------------------
// <copyright file="HostShutdown.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Host.Plugins
{
    using System;
    using System.Threading;
    using Abbotware.Core.Runtime;

    /// <summary>
    /// wrapper for a CancellationTokenSource that is used to signal shutdown
    /// </summary>
    /// <remarks>allows an application to 'self-terminate' when it knows it should exit</remarks>
    public class HostShutdown : IRequestShutdown
    {
        private readonly CancellationTokenSource cts;

        /// <summary>
        /// Initializes a new instance of the <see cref="HostShutdown"/> class.
        /// </summary>
        /// <param name="cts">cancellation token source</param>
        public HostShutdown(CancellationTokenSource cts)
        {
            this.cts = cts;
        }

        /// <inheritdoc/>
        public void Shutdown()
        {
            try
            {
                this.cts.Cancel();
            }
            catch (ObjectDisposedException)
            {
            }
        }
    }
}