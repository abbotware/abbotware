// -----------------------------------------------------------------------
// <copyright file="IMonitorShutdown.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Runtime
{
    using System.Threading;

    /// <summary>
    ///     Interface that can be used for components that need to monitor for shutdown events
    /// </summary>
    public interface IMonitorShutdown
    {
        /// <summary>
        ///     Gets the shutdown cancellation token
        /// </summary>
        /// <returns>async task</returns>
        CancellationToken Token { get; }
    }
}