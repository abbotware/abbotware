// -----------------------------------------------------------------------
// <copyright file="WindowsFtpOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
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

        /// <summary>
        /// Gets or sets a value indicating whether -v Suppresses display of remote server responses.
        /// </summary>
        [ShellCommandOption('v')]
        public bool SuppressRemoteServerResponses { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to -n Suppresses auto-login upon initial connection.
        /// </summary>
        [ShellCommandOption('n')]
        public bool SuppressAutoLogin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to -i Turns off interactive prompting during multiple file transfers.
        /// </summary>
        [ShellCommandOption('i')]
        public bool DisableInteractiveMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to -d Enables debugging.
        /// </summary>
        [ShellCommandOption('d')]
        public bool EnableDebugging { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to  -g Disables filename globbing (see GLOB command).
        /// </summary>
        [ShellCommandOption('g')]
        public bool DisableFileNameGlobbing { get; set; }

        /// <summary>
        /// Gets or sets -s:filename Specifies a text file containing FTP commands; the commands will automatically run after FTP starts.
        /// </summary>
        [ShellCommandOption('s', Format = "-{option}:{value}")]
        public string? ScriptFileName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to -a Use any local interface when binding data connection.
        /// </summary>
        [ShellCommandOption('a')]
        public bool UseAnyLocalInterface { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to -A login as anonymous.
        /// </summary>
        [ShellCommandOption('A')]
        public bool LoginAsAnonymous { get; set; }

        /// <summary>
        /// Gets or sets -x:send sockbuf Overrides the default SO_SNDBUF size of 8192.
        /// </summary>
        [ShellCommandOption('x', Format = "-{option}:{value}")]
        public int? SendBufferSize { get; set; }

        /// <summary>
        /// Gets or sets -r:recv sockbuf Overrides the default SO_RCVBUF size of 8192.
        /// </summary>
        [ShellCommandOption('r', Format = "-{option}:{value}")]
        public int? RecieveBufferSize { get; set; }

        /// <summary>
        /// Gets or sets -b:async count Overrides the default async count of 3
        /// </summary>
        [ShellCommandOption('b', Format = "-{option}:{value}")]
        public char? AsyncCount { get; set; }

        /// <summary>
        /// Gets or sets -w:windowsize Overrides the default transfer buffer size of 65535.
        /// </summary>
        [ShellCommandOption('w', Format = "-{option}:{value}")]
        public int? WindowSize { get; set; }

        /// <summary>
        ///  Gets or sets host Specifies the host name or IP address of the remote host to connect to.
        /// </summary>
        [ShellCommandOption(Position = 1)]
        public string Host { get; set; }

        /// <inheritdoc/>
        public NetworkCredential? Credential { get; set; }

        /// <inheritdoc/>
        public override string Arguments { get => RenderOptions(this); }
    }
}
