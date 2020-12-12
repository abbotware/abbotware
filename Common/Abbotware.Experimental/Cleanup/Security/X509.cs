// -----------------------------------------------------------------------
// <copyright file="X509.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Security
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Security.Cryptography.X509Certificates;
    using Abbotware.Core.Exceptions;
    using Abbotware.Core.Logging.Plugins;
    using Abbotware.ShellCommand;
    using Abbotware.ShellCommand.Configuration.Models;

    /// <summary>
    ///     Creates an X509 certificate
    /// </summary>
    public static class X509
    {
        /// <summary>
        ///     command to generate certificates
        ///     <code>makecert -r -pe -n "CN=TestUser" -ss my -sr currentuser -sky exchange .\TestUser.cer</code>
        /// </summary>
        private const string Command = @"..\..\..\..\..\thirdparty\microsoft.platform.sdk\makecert.exe";

        /// <summary>
        ///     command line argument
        /// </summary>
        private const string ArgumentsTemplate = "-r -pe -n \"CN={0}\" -ss my -sr currentuser -sky exchange \"{1}\"";

        /// <summary>
        ///     Timeout used for waiting or certification creation
        /// </summary>
        private static readonly TimeSpan CommandTimeout = TimeSpan.FromSeconds(5);

        /// <summary>
        ///     Factory method to create an X509 certificate
        /// </summary>
        /// <returns>a generated X509 certificate</returns>
        public static X509Certificate2 Create()
        {
            var hostname = Guid.NewGuid()
                .ToString();

            var output = new Uri(Path.ChangeExtension(Path.GetTempFileName(), "cer"));

            X509.Create(hostname, output);

            var path = output.LocalPath;

            return new X509Certificate2(path);
        }

        /// <summary>
        ///     Creates an  X509 certificate
        /// </summary>
        /// <param name="hostName">hostname used to create certificate</param>
        /// <param name="output">output location</param>
        public static void Create(string hostName, Uri output)
        {
            Arguments.NotNullOrWhitespace(hostName, nameof(hostName));
            output = Arguments.EnsureNotNull(output, nameof(output));

            var arguments = string.Format(CultureInfo.InvariantCulture, X509.ArgumentsTemplate, hostName, output.LocalPath);

            var cfg = new ShellCommandOptions(X509.Command, arguments)
            {
                CommandTimeout = CommandTimeout,
            };

            using var cp = new AbbotwareShellCommand(cfg, NullLogger.Instance);

            var result = cp.Execute();

            if (!result.StartInfo.Started)
            {
                var message = FormattableString.Invariant($"Failed to Start:{X509.Command} {arguments}");

                throw new AbbotwareException(message);
            }

            if (!result.Exited)
            {
                var message = FormattableString.Invariant($"Wait For Exit failed::{X509.Command} {arguments}");

                throw new AbbotwareException(message);
            }
        }
    }
}