// -----------------------------------------------------------------------
// <copyright file="ShellCommandOptionAttribute.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.ShellCommand.Plugins
{
    using System;

    /// <summary>
    /// Attribute for command line argument options classes
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ShellCommandOptionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShellCommandOptionAttribute"/> class.
        /// </summary>
        /// <param name="option">option</param>
        public ShellCommandOptionAttribute(char option)
            : this((char?)option)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellCommandOptionAttribute"/> class.
        /// </summary>
        public ShellCommandOptionAttribute()
            : this(null)
        {
            this.Format = "{value}";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellCommandOptionAttribute"/> class.
        /// </summary>
        /// <param name="option">option</param>
        private ShellCommandOptionAttribute(char? option)
        {
            this.Option = option?.ToString();
            this.Format = "-{option}";
        }

                  /// <summary>
        /// Gets or sets command line option
        /// </summary>
        public string? Option { get; set; }

        /// <summary>
        /// Gets or sets command line option
        /// </summary>
        public ushort Position { get; set; }

        /// <summary>
        /// Gets or sets the format string for the command line option
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Renders the command line options from the value
        /// </summary>
        /// <param name="value">value for the command line option</param>
        /// <returns>rendered option</returns>
        public string Render(object? value)
        {
#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
            var output = this.Format.Replace("{option}", this.Option ?? string.Empty, StringComparison.InvariantCultureIgnoreCase);
            output = output.Replace("{value}", value?.ToString(), StringComparison.InvariantCultureIgnoreCase);
#else
            var output = this.Format.Replace("{option}", this.Option ?? string.Empty);
            output = output.Replace("{value}", value?.ToString());
#endif
            return output;
        }
    }
}
