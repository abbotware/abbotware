// -----------------------------------------------------------------------
// <copyright file="CommandLineOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Host
{
    using CommandLine;

    /// <summary>
    /// command line options for the host
    /// </summary>
    [Verb("run", HelpText = "runs a component / plugin")]
    public class CommandLineOptions
    {
        /// <summary>
        /// Gets or sets the component name
        /// </summary>
        [Option('c', "component", Required = false, HelpText = "the component name use when searching for plugins")]
        public string Component { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether or not to disable ssl verification
        /// </summary>
        [Option("disable-ssl", Required = false, HelpText = "disable ssl verification")]
        public bool DisableSslVerification { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to log first chance exceptions
        /// </summary>
        [Option("log-first-chance", Required = false, HelpText = "log first chance exceptions")]
        public bool LogFirstChanceExceptions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to disable startable components
        /// </summary>
        [Option("disable-startable", Required = false, HelpText = "disable startable components")]
        public bool DisableStartable { get; set; }
    }
}
