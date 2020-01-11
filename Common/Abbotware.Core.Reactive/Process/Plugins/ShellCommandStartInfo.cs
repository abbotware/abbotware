// -----------------------------------------------------------------------
// <copyright file="ShellCommandStartInfo.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Process.Plugins
{
    using System;

    /// <summary>
    ///     POCO class for shell command start info
    /// </summary>
    public sealed class ShellCommandStartInfo : IShellCommandStartInfo
    {
        /// <inheritdoc />
        public int? ProcessId { get; set; }

        /// <inheritdoc />
        public bool Started { get; set; }

        /// <inheritdoc />
        public DateTimeOffset Start { get; set; } = DateTimeOffset.Now;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Started:{this.Started} ProcessId:{this.ProcessId}";
        }
    }
}