// -----------------------------------------------------------------------
// <copyright file="HostOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Host.Configuration
{
    using Abbotware.Core;
    using Abbotware.Core.Configuration;
    using Abbotware.Core.Configuration.Models;
    using Abbotware.Core.Runtime;
    using Abbotware.Host;

    /// <summary>
    /// Host Options class
    /// </summary>
    public class HostOptions : IHostOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HostOptions"/> class.
        /// </summary>
        /// <param name="commandLineOptions">command line Options</param>
        /// <param name="consoleArguments">console arguments</param>
        /// <param name="requestShutdown">injected request shutdown service</param>
        /// <param name="monitorShutdown">injected monitor shutdown service</param>
        public HostOptions(CommandLineOptions commandLineOptions, ConsoleArguments consoleArguments, IRequestShutdown requestShutdown, IMonitorShutdown monitorShutdown)
            : this(
            consoleArguments,
            new ContainerOptions
            {
                Component = Arguments.EnsureNotNull(commandLineOptions, nameof(commandLineOptions)).Component,
                DisableStartable = Arguments.EnsureNotNull(commandLineOptions, nameof(commandLineOptions)).DisableStartable,
            },
            Arguments.EnsureNotNull(requestShutdown, nameof(requestShutdown)),
            monitorShutdown)
        {
            commandLineOptions = Arguments.EnsureNotNull(commandLineOptions, nameof(commandLineOptions));

            this.LogFirstChanceExceptions = commandLineOptions.LogFirstChanceExceptions;
            this.DisableSslVerification = commandLineOptions.DisableSslVerification;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HostOptions"/> class.
        /// </summary>
        /// <param name="consoleArguments">console arguments</param>
        /// <param name="containerOptions">container options</param>
        /// <param name="requestShutdown">injected request shutdown service</param>
        /// <param name="monitorShutdown">injected monitor shutdown service</param>
        public HostOptions(ConsoleArguments consoleArguments, IContainerOptions containerOptions, IRequestShutdown requestShutdown, IMonitorShutdown monitorShutdown)
        {
            this.RequestShutdown = requestShutdown;
            this.MonitorShutdown = monitorShutdown;
            this.ConsoleArguments = consoleArguments;
            this.ContainerOptions = containerOptions;
        }

        /// <inheritdoc/>
        public IRequestShutdown RequestShutdown { get; set; }

        /// <inheritdoc/>
        public IMonitorShutdown MonitorShutdown { get; set; }

        /// <inheritdoc/>
        public ConsoleArguments ConsoleArguments { get; set; }

        /// <inheritdoc/>
        public IContainerOptions ContainerOptions { get; set; }

        /// <inheritdoc/>
        public bool DisableSslVerification { get; set; }

        /// <inheritdoc/>
        public bool LogFirstChanceExceptions { get; set; }
    }
}
