// -----------------------------------------------------------------------
// <copyright file="IHostOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Host.Configuration
{
    using Abbotware.Core;
    using Abbotware.Core.Configuration;
    using Abbotware.Core.Runtime;

    /// <summary>
    /// Read Only Host Configuriation
    /// </summary>
    public interface IHostOptions
    {
        /// <summary>
        /// Gets the request shutdown service
        /// </summary>
        IRequestShutdown RequestShutdown { get; }

        /// <summary>
        /// Gets the monitor shutdown service
        /// </summary>
        IMonitorShutdown MonitorShutdown { get; }

        /// <summary>
        /// Gets the console arguments
        /// </summary>
        ConsoleArguments ConsoleArguments { get; }

        /// <summary>
        /// Gets the container options
        /// </summary>
        IContainerOptions ContainerOptions { get; }

        /// <summary>
        ///     Gets a value indicating whether SSL verification is disabled
        /// </summary>
        bool DisableSslVerification { get; }

        /// <summary>
        ///     Gets a value indicating whether first change exceptions should be logged
        /// </summary>
        bool LogFirstChanceExceptions { get; }
    }
}
