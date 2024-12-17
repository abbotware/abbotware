// -----------------------------------------------------------------------
// <copyright file="LambdaCommandLineOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Lambda
{
    using Abbotware.Host.Configuration;
    using CommandLine;

    /// <summary>
    /// command line options for the lambda host
    /// </summary>
    public class LambdaCommandLineOptions : CommandLineOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether to run as a regular console app
        /// </summary>
        [Option("console-mode", Required = false, HelpText = "run as a regular console app")]
        public bool RunAsConsole { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to spool up aws but don't run the actual lambda"
        /// </summary>
        [Option("spool-aws", Required = false, HelpText = "spool up aws but don't run the actual lambda")]
        public bool SpoolAws { get; set; }
    }
}