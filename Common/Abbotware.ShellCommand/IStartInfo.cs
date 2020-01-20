// -----------------------------------------------------------------------
// <copyright file="IStartInfo.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.ShellCommand
{
    using System;

    /// <summary>
    /// Interface for shell command start info
    /// </summary>
    public interface IStartInfo
    {
        /// <summary>
        ///     Gets the process id of the command
        /// </summary>
        int? ProcessId { get; }

        /// <summary>
        ///     Gets a value indicating whether or not a process was started/created by the os
        /// </summary>
        bool Started { get; }

        /// <summary>
        ///     Gets the start time of the shell command
        /// </summary>
        DateTimeOffset Start { get; }
    }
}