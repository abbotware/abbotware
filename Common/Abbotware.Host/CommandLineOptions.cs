// -----------------------------------------------------------------------
// <copyright file="CommandLineOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Host
{
    using Abbotware.Host.Configuration;
    using CommandLine;

    /// <summary>
    /// command line options for the host
    /// </summary>
    [Verb("run", HelpText = "runs a component / plugin")]
    public class CommandLineOptions : IHostOptions
    {
        /// <inheritdoc/>
        [Option('c', "component", Required = true, HelpText = "the component name use when searching for plugins")]
        public string Component { get; set; } = string.Empty;

        /// <inheritdoc/>
        [Option("disable-ssl", Required = false, HelpText = "disable ssl verification")]
        public bool DisableSslVerification { get; set; }

        /// <inheritdoc/>
        [Option("log-first-chance", Required = false, HelpText = "log first chance exceptions")]
        public bool LogFirstChanceExceptions { get; set; }

        /// <inheritdoc/>
        [Option("disable-startable", Required = false, HelpText = "disable startable components")]
        public bool DisableStartable { get; set; }
    }
}
