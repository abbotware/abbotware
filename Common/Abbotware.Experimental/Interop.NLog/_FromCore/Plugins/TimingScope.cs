// -----------------------------------------------------------------------
// <copyright file="TimingScope.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Logging.Plugins
{
    using System;
    using System.Diagnostics;
    using Abbotware.Core.Extensions;

    /// <summary>
    /// scoped logger use for timeing a using block
    /// </summary>
    internal sealed class TimingScope : ITimingScope
    {
        private readonly Stopwatch stopwatch;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimingScope"/> class.
        /// </summary>
        /// <param name="logger">Logger to be used by this scope</param>
        /// <param name="scopeName">Name of the scope</param>
        /// <param name="memberName">Name of the member the scope created for</param>
        public TimingScope(ILogger logger, string scopeName, string memberName)
        {
            this.stopwatch = Stopwatch.StartNew();
            this.logger = logger;
            this.ScopeName = scopeName;
            this.MemberName = memberName;
        }

        /// <inheritdoc/>
        public string ScopeName { get; }

        /// <inheritdoc/>
        public string MemberName { get; }

        /// <inheritdoc/>
        public TimeSpan Elapsed => this.stopwatch.Elapsed;

        /// <inheritdoc/>
        public void Dispose()
        {
            this.stopwatch.Stop();
            this.logger.Debug($"Exit Scope:{this.ScopeName} in {this.MemberName} - Elapsed ms:{this.stopwatch.ElapsedMilliseconds}");
        }

        /// <summary>
        /// work around for NLog's parameterless constructor requirement
        /// </summary>
        internal void Enter()
        {
            this.logger.Debug($"Enter Scope:{this.ScopeName} in {this.MemberName}");
        }
    }
}