// -----------------------------------------------------------------------
// <copyright file="WindowsFtpCommand.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.ShellCommand.Plugins
{
    using System.Threading.Tasks;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Logging;
    using Abbotware.ShellCommand.Plugins.Configuration;
    using Abbotware.ShellCommand.Plugins.Configuration.Models;

    /// <summary>
    /// Wrapper Shell Command for the Windows Ftp.exe command
    /// </summary>
    public class WindowsFtpCommand : AbbotwareShellCommand<IWindowsFtpOptions>
    {
#if NET5_0_OR_GREATER
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsFtpCommand"/> class.
        /// </summary>
        /// <param name="options">options for windows ftp command</param>
        /// <param name="logger">injected logger</param>
        public WindowsFtpCommand(IWindowsFtpOptions options, Microsoft.Extensions.Logging.ILogger<WindowsFtpCommand> logger)
            : this(options, new LoggingAdapter(logger))
        {
        }
#endif

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