// -----------------------------------------------------------------------
// <copyright file="IShellCommand.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Process.Plugins
{
    using System;
    using System.Threading.Tasks;
    using Abbotware.Core.Objects;

    /// <summary>
    /// Interface for a shell command
    /// </summary>
    public interface IShellCommand : ICommand<IShellCommandResult>
    {
        /// <summary>
        /// Gets the error output
        /// </summary>
        IObservable<(DateTimeOffset, string)> ErrorOutput { get; }

        /// <summary>
        /// Gets the standard output
        /// </summary>
        IObservable<(DateTimeOffset, string)> StandardOutput { get; }

        /// <summary>
        /// Gets the task to signal the shell command started
        /// </summary>
        Task<IShellCommandStartInfo> Started { get; }

        /// <summary>
        /// Gets the task to signal the shell command exited
        /// </summary>
        Task<IShellCommandResult> Exited { get; }

        /// <summary>
        /// Writes input to the shell command (only sent if process is running)
        /// </summary>
        /// <param name="input">input text</param>
        void WriteInput(string input);
    }
}