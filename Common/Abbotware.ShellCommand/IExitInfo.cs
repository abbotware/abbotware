// -----------------------------------------------------------------------
// <copyright file="IExitInfo.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.ShellCommand
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for shell command result
    /// </summary>
    public interface IExitInfo
    {
        /// <summary>
        ///     Gets the start info for the command
        /// </summary>
        IStartInfo StartInfo { get; }

        /// <summary>
        ///     Gets the exit code of the process
        /// </summary>
        int? ExitCode { get; }

        /// <summary>
        ///     Gets a value indicating whether if the process it exited (if false, it means a timeout)
        /// </summary>
        bool Exited { get; }

        /// <summary>
        ///     Gets the end time of the shell command (if null, it means a timeout)
        /// </summary>
        DateTimeOffset? End { get; }

       /// <summary>
       /// Gets the standard output written by the shell command
       /// </summary>
        public IEnumerable<(DateTimeOffset Time, string Message)> StandardOutput { get; }

        /// <summary>
        /// Gets the error output written by the shell command
        /// </summary>
        public IEnumerable<(DateTimeOffset Time, string Message)> ErrorOutput { get; }
    }
}