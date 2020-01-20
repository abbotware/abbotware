// -----------------------------------------------------------------------
// <copyright file="ShellCommandOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.ShellCommand.Configuration.Models
{
    using System;

    /// <summary>
    /// configuration for the shell command
    /// </summary>
    public sealed class ShellCommandOptions : IShellCommandOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShellCommandOptions"/> class.
        /// </summary>
        public ShellCommandOptions()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellCommandOptions"/> class.
        /// </summary>
        /// <param name="command">command</param>
        public ShellCommandOptions(string command)
            : this(command, string.Empty)
        {
            Abbotware.Core.Arguments.NotNullOrWhitespace(command, nameof(command));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellCommandOptions"/> class.
        /// </summary>
        /// <param name="command">command</param>
        /// <param name="arguments">arguments</param>
        public ShellCommandOptions(string command, string arguments)
        {
            command = Abbotware.Core.Arguments.EnsureNotNullOrWhitespace(command, nameof(command));

            this.Command = command;
            this.Arguments = arguments;
        }

        /// <inheritdoc/>
        public string Command { get; set; }

        /// <inheritdoc/>
        public string WorkingDirectory { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string Arguments { get; set; } = string.Empty;

        /// <inheritdoc/>
        public TimeSpan CommandTimeout { get; set; } = TimeSpan.FromSeconds(5);

        /// <inheritdoc/>
        public TimeSpan ExitDelay { get; set; } = TimeSpan.FromMilliseconds(250);
    }
}