// -----------------------------------------------------------------------
// <copyright file="IShellCommandOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.ShellCommand.Configuration
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     interface for a shell command options
    /// </summary>
    public interface IShellCommandOptions
    {
        /// <summary>
        ///     Gets the command line arguments to the shell command
        /// </summary>
        string Arguments { get; }

        /// <summary>
        ///     Gets the command to run
        /// </summary>
        string Command { get; }

        /// <summary>
        ///     Gets the working directory for the shell command's execution
        /// </summary>
        string WorkingDirectory { get; }

        /// <summary>
        ///     Gets a timeout value to wait for process.  If timeout expires, the process will be killed
        /// </summary>
        TimeSpan CommandTimeout { get; }

        /// <summary>
        ///     Gets a timeout value to wait after a process exits. This to capture data from the callback events
        /// </summary>
        TimeSpan ExitDelay { get; }

        /// <summary>
        ///     Gets sensitve a list of sensitive log fragments
        /// </summary>
        public IEnumerable<string> SensitiveFragments { get; }
    }
}