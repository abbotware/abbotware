// -----------------------------------------------------------------------
// <copyright file="BaseEnvironment.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Runtime
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    /// <summary>
    ///     base class for operating system specific environment information
    /// </summary>
    public abstract class BaseEnvironment : IEnvironmentInformation
    {
        /// <summary>
        /// Gets the Shutdown task
        /// </summary>
        [Obsolete("use IMonitorShutdown")]
        public Task Shutdown => this.ShutdownSignal.Task;

        /// <inheritdoc />
        public bool Is64BitOperatingSystem => Environment.Is64BitOperatingSystem;

        /// <inheritdoc />
        public string UserName => Environment.UserName;

        /// <inheritdoc />
        public string MachineName => Environment.MachineName;

        /// <inheritdoc />
        public int ProcessorCount => Environment.ProcessorCount;

        /// <inheritdoc />
        public long ProcessorFrequency => Stopwatch.Frequency;

        /// <inheritdoc />
        public abstract long SystemMemory { get; }

        /// <summary>
        /// Gets the shutdown signal
        /// </summary>
        protected TaskCompletionSource<bool> ShutdownSignal { get; } = new TaskCompletionSource<bool>();
    }
}