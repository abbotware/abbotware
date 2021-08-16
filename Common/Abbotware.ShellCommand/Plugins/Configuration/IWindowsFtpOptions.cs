// -----------------------------------------------------------------------
// <copyright file="IWindowsFtpOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
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
        /// Gets a value indicating whether -v Suppresses display of remote server responses.
        /// </summary>
        public bool SuppressRemoteServerResponses { get; }

        /// <summary>
        /// Gets a value indicating whether to -n Suppresses auto-login upon initial connection.
        /// </summary>
        public bool SuppressAutoLogin { get; }

        /// <summary>
        /// Gets a value indicating whether to -i Turns off interactive prompting during multiple file transfers.
        /// </summary>
        public bool DisableInteractiveMode { get; }

        /// <summary>
        /// Gets a value indicating whether to -d Enables debugging.
        /// </summary>
        public bool EnableDebugging { get; }

        /// <summary>
        /// Gets a value indicating whether to  -g Disables filename globbing (see GLOB command).
        /// </summary>
        public bool DisableFileNameGlobbing { get; }

        /// <summary>
        /// Gets -s:filename Specifies a text file containing FTP commands; the commands will automatically run after FTP starts.
        /// </summary>
        public string? ScriptFileName { get; }

        /// <summary>
        /// Gets a value indicating whether to -a Use any local interface when binding data connection.
        /// </summary>
        public bool UseAnyLocalInterface { get; }

        /// <summary>
        /// Gets a value indicating whether to -A login as anonymous.
        /// </summary>
        public bool LoginAsAnonymous { get; }

        /// <summary>
        /// Gets -x:send sockbuf Overrides the default SO_SNDBUF size of 8192.
        /// </summary>
        public int? SendBufferSize { get; }

        /// <summary>
        /// Gets -r:recv sockbuf Overrides the default SO_RCVBUF size of 8192.
        /// </summary>
        public int? RecieveBufferSize { get; }

        /// <summary>
        /// Gets -b:async count Overrides the default async count of 3
        /// </summary>
        public char? AsyncCount { get; }

        /// <summary>
        /// Gets -w:windowsize Overrides the default transfer buffer size of 65535.
        /// </summary>
        public int? WindowSize { get; }

        /// <summary>
        ///  Gets host Specifies the host name or IP address of the remote host to connect to.
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Gets the credential
        /// </summary>
        public NetworkCredential? Credential { get; }
    }
}