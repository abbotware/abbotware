// -----------------------------------------------------------------------
// <copyright file="MemoryLoggerFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.NLog.Plugins
{
    using System;
    using System.Collections.Generic;
    using Abbotware.Core.Logging;
    using global::NLog;
    using global::NLog.Targets;

    /// <summary>
    /// Logger factory that uses memory targets
    /// </summary>
    public sealed class MemoryLoggerFactory : LoggerFactory, IMemoryLoggerFactory, IDisposable
    {
        private readonly MemoryTarget target;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryLoggerFactory"/> class.
        /// </summary>
        public MemoryLoggerFactory()
        {
#if !NETSTANDARD2_0
            this.target = new MemoryTarget();
#else
            this.target = new MemoryTarget("memory");
#endif
            this.target.Layout = "${message}";

            global::NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(this.target, LogLevel.Debug);
        }

        /// <summary>
        /// Gets the memory targets
        /// </summary>
        public IEnumerable<string> Messages => this.target.Logs;

        /// <inheritdoc/>
        public void Dispose()
        {
            this.target.Dispose();
        }
    }
}