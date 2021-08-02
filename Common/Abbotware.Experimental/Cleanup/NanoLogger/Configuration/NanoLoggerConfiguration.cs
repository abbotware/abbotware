//--------------------------------------------------------------------
// -----------------------------------------------------------------------
// <copyright file="NanoLoggerConfiguration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Diagnostics.Plugins.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     Configuration class that helps pre-allocate all the MemoryLogger data
    /// </summary>
    public class NanoLoggerConfiguration
    {
        /// <summary>
        ///     internal dictionary for checkpoint names
        /// </summary>
        private readonly Dictionary<ushort, string> checkpointNames = new ();

        /// <summary>
        ///     internal dictionary for correlation counts to preallocate buffers
        /// </summary>
        private readonly Dictionary<byte, int> correlationTypeLogCounts = new ();

        /// <summary>
        ///     internal dictionary for correlation type names
        /// </summary>
        private readonly Dictionary<byte, string> correlationTypeNames = new ();

        /// <summary>
        ///     Initializes a new instance of the <see cref="NanoLoggerConfiguration" /> class.
        /// </summary>
        public NanoLoggerConfiguration()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NanoLoggerConfiguration" /> class.
        /// </summary>
        /// <param name="counts">dictionary of counts</param>
        /// <param name="correlationTypeNames">names of correlation types</param>
        /// <param name="checkpointNames">names of checkpoints</param>
        public NanoLoggerConfiguration(IReadOnlyDictionary<byte, int> counts, IReadOnlyDictionary<byte, string> correlationTypeNames, IReadOnlyDictionary<ushort, string> checkpointNames)
        {
            Arguments.NotNull(counts, nameof(counts));

            this.correlationTypeLogCounts = counts.ToDictionary(x => x.Key, x => x.Value);
            this.correlationTypeNames = correlationTypeNames.ToDictionary(x => x.Key, x => x.Value);
            this.checkpointNames = checkpointNames.ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NanoLoggerConfiguration" /> class.
        /// </summary>
        /// <param name="logCount">log count for a single correlation type (0)</param>
        public NanoLoggerConfiguration(int logCount)
            : this(new Dictionary<byte, int> { { 0, logCount } }, new Dictionary<byte, string> { { 0, "unknown" } }, new Dictionary<ushort, string> { { 0, "unknown" } })
        {
        }

        /// <summary>
        ///     Gets the Correlation Type counts
        /// </summary>
        public IReadOnlyDictionary<byte, string> CorrelationType => this.correlationTypeNames;

        /// <summary>
        ///     Gets the Correlation Type counts
        /// </summary>
        public IReadOnlyDictionary<ushort, string> Checkpoint => this.checkpointNames;

        /// <summary>
        ///     Gets the Correlation Type counts
        /// </summary>
        public IReadOnlyDictionary<byte, int> CorrelationTypeLogCounts => this.correlationTypeLogCounts;

        /// <summary>
        ///     Gets the Correlation Type counts
        /// </summary>
        public int MaxCorrelationType => this.correlationTypeLogCounts.Keys.Max();

        /// <summary>
        ///     Adds a row count for a specific correlation type id
        /// </summary>
        /// <param name="correlationTypeId">correlation Type</param>
        /// <param name="logCount">row count</param>
        public void Add(byte correlationTypeId, int logCount)
        {
            this.correlationTypeLogCounts.Add(correlationTypeId, logCount);
        }
    }
}