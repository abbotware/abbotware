// -----------------------------------------------------------------------
// <copyright file="WindowsFtpOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.ShellCommand.Plugins.Configuration.Models
{
    using System;
    using System.Net;
    using Abbotware.ShellCommand.Configuration.Models;

    /// <summary>
    /// Editable options class for windows ftp options
    /// </summary>
    public class WindowsFtpOptions : ShellCommandOptions, IWindowsFtpOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsFtpOptions"/> class.
        /// </summary>
        /// <param name="host">ftp host name</param>
        public WindowsFtpOptions(string host)
            : base($"{Environment.GetEnvironmentVariable("SystemRoot")}\\System32\\ftp.exe")
        {
            this.Host = host;
        }

        /// <inheritdoc/>
        [ShellCommandOption('v')]
        public bool SuppressRemoteServerResponses { get; set; }

        /// <inheritdoc/>
        [ShellCommandOption('n')]
        public bool SuppressAutoLogin { get; set; }

        /// <inheritdoc/>
        [ShellCommandOption('i')]
        public bool DisableInteractiveMode { get; set; }

        /// <inheritdoc/>
        [ShellCommandOption('d')]
        public bool EnableDebugging { get; set; }

        /// <inheritdoc/>
        [ShellCommandOption('g')]
        public bool DisableFileNameGlobbing { get; set; }

        /// <inheritdoc/>
        [ShellCommandOption('s', Format = "-{option}:{value}")]
        public string? ScriptFileName { get; set; }

        /// <inheritdoc/>
        [ShellCommandOption('a')]
        public bool UseAnyLocalInterface { get; set; }

        /// <inheritdoc/>
        [ShellCommandOption('A')]
        public bool LoginAsAnonymous { get; set; }

        /// <inheritdoc/>
        [ShellCommandOption('x', Format = "-{option}:{value}")]
        public int? SendBufferSize { get; set; }

        /// <inheritdoc/>
        [ShellCommandOption('r', Format = "-{option}:{value}")]
        public int? RecieveBufferSize { get; set; }

        /// <inheritdoc/>
        [ShellCommandOption('b', Format = "-{option}:{value}")]
        public char? AsyncCount { get; set; }

        /// <inheritdoc/>
        [ShellCommandOption('w', Format = "-{option}:{value}")]
        public int? WindowSize { get; set; }

        /// <inheritdoc/>
        [ShellCommandOption(Position = 1)]
        public string Host { get; set; }

        /// <inheritdoc/>
        public NetworkCredential? Credential { get; set; }

        /// <inheritdoc/>
        public override string Arguments { get => RenderOptions(this); }
    }
}
