// -----------------------------------------------------------------------
// <copyright file="LinuxOperatingSystem.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Linux
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using Abbotware.Core.Exceptions;
    using Abbotware.Core.Logging.Plugins;
    using Abbotware.Core.Runtime;
    using Abbotware.ShellCommand;
    using Abbotware.ShellCommand.Configuration.Models;

    /// <summary>
    ///     Linux Operating System Specific functionality
    /// </summary>
    public class LinuxOperatingSystem : BaseEnvironment, IOperatingSystem
    {
        /// <inheritdoc />
        public override long SystemMemory { get; }

        /// <inheritdoc />
        public virtual TimeSpan? SystemUptime
        {
            get
            {
                if (!File.Exists("/proc/uptime"))
                {
                    return null;
                }

                var uptimeText = File.ReadAllText("/proc/uptime");

                var systemSeconds = uptimeText.Split(' ').FirstOrDefault();

                if (string.IsNullOrWhiteSpace(systemSeconds))
                {
                    return null;
                }

                if (!double.TryParse(systemSeconds, out double seconds))
                {
                    return null;
                }

                var ts = TimeSpan.FromSeconds(seconds);

                return ts;
            }
        }

        /// <summary>
        ///     Reboot implementation on UNIX Operating System
        /// </summary>
        public void Reboot()
        {
            var pathToShutdown = Path.Combine(Path.DirectorySeparatorChar.ToString(CultureInfo.InvariantCulture), "sbin", "shutdown");

            var shutdownArgs = string.Format(CultureInfo.InvariantCulture, "{0} -r now", pathToShutdown);

            var cfg = new ShellCommandOptions("sudo", shutdownArgs);

            using var process = new AbbotwareShellCommand(cfg, NullLogger.Instance);

            var result = process.Execute();

            // If there is data from standard error then the command
            // didn't work because we are not executing as root
            if (!result.StartInfo.Started)
            {
                throw AbbotwareException.Create("Error during shell command {0}", result.ErrorOutput.FirstOrDefault().Message);
            }

            if (!result.Exited)
            {
                throw AbbotwareException.Create("Error during shell command {0}", result.ErrorOutput.FirstOrDefault().Message);
            }

            if (result.ExitCode != 0)
            {
                throw AbbotwareException.Create("Error during shell command {0}", result.ErrorOutput.FirstOrDefault().Message);
            }
        }
    }
}