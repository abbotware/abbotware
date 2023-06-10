// -----------------------------------------------------------------------
// <copyright file="AbbotwareShellCommand.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.ShellCommand
{
    using Abbotware.ShellCommand.Configuration;
    using Abbotware.ShellCommand.Configuration.Models;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// class the can run a shell command
    /// </summary>
    public class AbbotwareShellCommand : AbbotwareShellCommand<IShellCommandOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbbotwareShellCommand"/> class.
        /// </summary>
        /// <param name="command">command</param>
        /// <param name="logger">injected logger</param>
        public AbbotwareShellCommand(string command, ILogger logger)
            : this(command, string.Empty, logger)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbbotwareShellCommand"/> class.
        /// </summary>
        /// <param name="command">command</param>
        /// <param name="arguments">arguments</param>
        /// <param name="logger">injected logger</param>
        public AbbotwareShellCommand(string command, string arguments, ILogger logger)
            : this(new ShellCommandOptions(command, arguments), logger)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbbotwareShellCommand"/> class.
        /// </summary>
        /// <param name="config">configuration</param>
        /// <param name="logger">injected logger</param>
        public AbbotwareShellCommand(IShellCommandOptions config, ILogger logger)
            : base(config, logger)
        {
        }
    }
}