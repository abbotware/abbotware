// -----------------------------------------------------------------------
// <copyright file="ExitInfo.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.ShellCommand
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    /// <summary>
    ///     result of the shell command
    /// </summary>
    public class ExitInfo : IExitInfo
    {
        private readonly ConcurrentBag<(DateTimeOffset Time, string)> error = new();

        private readonly ConcurrentBag<(DateTimeOffset Time, string)> output = new();

        /// <inheritdoc />
        public int? ExitCode { get; set; }

        /// <inheritdoc />
        public bool Exited { get; set; }

        /// <inheritdoc />
        public DateTimeOffset? End { get; set; }

        /// <inheritdoc />
        public IEnumerable<(DateTimeOffset Time, string Message)> StandardOutput => this.output;

        /// <inheritdoc />
        public IEnumerable<(DateTimeOffset Time, string Message)> ErrorOutput => this.error;

        /// <inheritdoc />
        IStartInfo IExitInfo.StartInfo => this.StartInfo;

        /// <summary>
        /// Gets the start info
        /// </summary>
        public StartInfo StartInfo { get; } = new StartInfo();

        /// <summary>
        ///     Appends error text to result
        /// </summary>
        /// <param name="error">error text</param>
        public void AppendError((DateTimeOffset Time, string Message) error)
        {
            this.error.Add(error);
        }

        /// <summary>
        ///     Appends output text to result
        /// </summary>
        /// <param name="output">output text</param>
        public void AppendOutput((DateTimeOffset Time, string Message) output)
        {
            this.output.Add(output);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Started:{this.StartInfo.Started} Exited:{this.Exited} ExitCode:{this.ExitCode} ProcessId:{this.StartInfo.ProcessId}";
        }
    }
}