// -----------------------------------------------------------------------
// <copyright file="IWindowsFtpOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.ShellCommand.Plugins.Configuration
{
    using System.Net;
    using Abbotware.ShellCommand.Configuration;

    /// <summary>
    /// read only interface for windows ftp options
    /// </summary>
    public interface IWindowsFtpOptions : IShellCommandOptions
    {
        /// <summary>
        /// Gets a value indicating whether to use the -v option: Suppress display of remote server responses.
        /// </summary>
        public bool SuppressRemoteServerResponses { get; }

        /// <summary>
        /// Gets a value indicating whether to use the -n option: Suppress auto-login upon initial connection.
        /// </summary>
        public bool SuppressAutoLogin { get; }

        /// <summary>
        /// Gets a value indicating whether to use the -i option: Turn off interactive prompting during multiple file transfers.
        /// </summary>
        public bool DisableInteractiveMode { get; }

        /// <summary>
        /// Gets a value indicating whether to use the -d option: Enable debugging.
        /// </summary>
        public bool EnableDebugging { get; }

        /// <summary>
        /// Gets a value indicating whether to use the -g option: Disable filename globbing.
        /// </summary>
        public bool DisableFileNameGlobbing { get; }

        /// <summary>
        /// Gets the -s:filename option: a text file containing FTP commands; the commands will automatically run after FTP starts.
        /// </summary>
        public string? ScriptFileName { get; }

        /// <summary>
        /// Gets a value indicating whether to use the -a option: Use any local interface when binding data connection.
        /// </summary>
        public bool UseAnyLocalInterface { get; }

        /// <summary>
        /// Gets a value indicating whether to use the -A option: login as anonymous.
        /// </summary>
        public bool LoginAsAnonymous { get; }

        /// <summary>
        /// Gets the -x:send sockbuf option: Override the default SO_SNDBUF size of 8192.
        /// </summary>
        public int? SendBufferSize { get; }

        /// <summary>
        /// Gets the -r:recv sockbuf option: Override the default SO_RCVBUF size of 8192.
        /// </summary>
        public int? RecieveBufferSize { get; }

        /// <summary>
        /// Gets the -b:async count option: Override the default async count of 3
        /// </summary>
        public char? AsyncCount { get; }

        /// <summary>
        /// Gets the -w:windowsize option: Override the default transfer buffer size of 65535.
        /// </summary>
        public int? WindowSize { get; }

        /// <summary>
        ///  Gets the host name or IP address of the remote host to connect to.
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Gets the credential
        /// </summary>
        public NetworkCredential? Credential { get; }
    }
}