// -----------------------------------------------------------------------
// <copyright file="ShellCommandOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.ShellCommand.Configuration.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abbotware.Core.Diagnostics;
    using Abbotware.ShellCommand.Plugins;

    /// <summary>
    /// configuration for the shell command
    /// </summary>
    public class ShellCommandOptions : IShellCommandOptions
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
        public virtual string Arguments { get; set; } = string.Empty;

        /// <inheritdoc/>
        public TimeSpan CommandTimeout { get; set; } = TimeSpan.FromSeconds(5);

        /// <inheritdoc/>
        public TimeSpan ExitDelay { get; set; } = TimeSpan.FromMilliseconds(250);

        /// <inheritdoc/>
        IEnumerable<string> IShellCommandOptions.SensitiveFragments => this.SensitiveFragments;

        /// <summary>
        /// Gets a list of sensitive log fragments
        /// </summary>
        public ICollection<string> SensitiveFragments { get; } = new List<string>();

        /// <summary>
        /// Renders an options class into command line arguments
        /// </summary>
        /// <typeparam name="TOptions">options class type</typeparam>
        /// <param name="options">options class</param>
        /// <returns>command line argurments</returns>
        protected static string RenderOptions<TOptions>(TOptions options)
        {
            var properties = ReflectionHelper.GetSimpleProperties<TOptions>();

            var list = new List<KeyValuePair<int, string>>();

            foreach (var p in properties)
            {
                if (p == null)
                {
                    continue;
                }

                var a = ReflectionHelper.SingleOrDefaultAttribute<ShellCommandOptionAttribute>(p);

                if (a == null)
                {
                    continue;
                }

                var v = p.GetValue(options);

                if (v == null)
                {
                    continue;
                }

                if (v is bool b)
                {
                    if (b == false)
                    {
                        continue;
                    }
                }

                list.Add(new KeyValuePair<int, string>(a.Position, a.Render(v)));
            }

            var rendered = list.OrderBy(x => x.Key)
                .Select(x => x.Value)
                .ToList();

            return string.Join(" ", rendered);
        }
    }
}