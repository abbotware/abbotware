// -----------------------------------------------------------------------
// <copyright file="LambdaHostOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Lambda.Configuration.Models
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Runtime;
    using Abbotware.Host.Configuration;

    /// <summary>
    /// Lambda host options
    /// </summary>
    public class LambdaHostOptions : HostOptions, ILambdaHostOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaHostOptions"/> class.
        /// </summary>
        /// <param name="commandLineOptions">command line Options</param>
        /// <param name="consoleArguments">console arguments</param>
        /// <param name="requestShutdown">injected request shutdown service</param>
        /// <param name="monitorShutdown">injected monitor shutdown service</param>
        public LambdaHostOptions(LambdaCommandLineOptions commandLineOptions, ConsoleArguments consoleArguments, IRequestShutdown requestShutdown, IMonitorShutdown monitorShutdown)
            : base(commandLineOptions, consoleArguments, requestShutdown, monitorShutdown)
        {
            commandLineOptions = Arguments.EnsureNotNull(commandLineOptions, nameof(commandLineOptions));

            this.SpoolAws = commandLineOptions.SpoolAws;
            this.RunAsConsole = commandLineOptions.RunAsConsole;
        }

        /// <inheritdoc/>
        public bool RunAsConsole { get; set; }

        /// <inheritdoc/>
        public bool SpoolAws { get; set; }

        /// <inheritdoc/>
        public TimeSpan TimeSlice { get; set; } = Defaults.LambdaTimeSlice;
    }
}
