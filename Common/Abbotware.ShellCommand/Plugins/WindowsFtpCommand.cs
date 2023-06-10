// -----------------------------------------------------------------------
// <copyright file="WindowsFtpCommand.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.ShellCommand.Plugins
{
    using System.Threading.Tasks;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.ShellCommand.Plugins.Configuration;
    using Abbotware.ShellCommand.Plugins.Configuration.Models;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Wrapper Shell Command for the Windows Ftp.exe command
    /// </summary>
    public class WindowsFtpCommand : AbbotwareShellCommand<IWindowsFtpOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsFtpCommand"/> class.
        /// </summary>
        /// <param name="options">options for windows ftp command</param>
        /// <param name="logger">injected logger</param>
        public WindowsFtpCommand(IWindowsFtpOptions options, ILogger logger)
            : base(options, logger)
        {
            options = Arguments.EnsureNotNull(options, nameof(options));

            if (options.Credential != null)
            {
                var hack = options as WindowsFtpOptions;

                if (options.Credential.Password.IsNotBlank())
                {
                    hack!.SensitiveFragments.Add(options.Credential.Password);
                }
            }
        }

        /// <inheritdoc/>>
        protected override async Task OnStarted()
        {
            this.WriteInput("quit");

            await Task.CompletedTask
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>>
        protected override void OnDisposeManagedResources()
        {
            base.OnDisposeManagedResources();
        }
    }
}