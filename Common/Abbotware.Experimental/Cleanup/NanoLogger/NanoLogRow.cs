// -----------------------------------------------------------------------
// <copyright file="NanoLogRow.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Diagnostics.Plugins
{
    /// <summary>
    ///     Row of logging data
    /// </summary>
    public class NanoLogRow
    {
        /// <summary>
        ///     Gets or sets the ticks (stopwatch.timestamp)
        /// </summary>
        public long Ticks { get; set; }

        /// <summary>
        ///     Gets or sets the Thread Id
        /// </summary>
        public int ThreadId { get; set; }

        /// <summary>
        ///     Gets or sets the correlation type for the log item
        /// </summary>
        public byte CorrelationTypeId { get; set; }

        /// <summary>
        ///     Gets or sets the Instance Id for the log item
        /// </summary>
        public uint InstanceId { get; set; }

        /// <summary>
        ///     Gets or sets the checkpoint id for the log item
        /// </summary>
        public ushort? CheckpointId { get; set; }

        /// <summary>
        ///     Gets or sets the checkpoint detail id for the log item
        /// </summary>
        public int? CheckpointDetailId { get; set; }

        /// <summary>
        ///     Gets or sets the checkpoint progress for the log item
        /// </summary>
        public long? CheckpointProgress { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether or not this log item is the end of the even
        /// </summary>
        public bool IsEnd { get; set; }

        /// <summary>
        ///     Gets or sets the message (use judiciously) for the log row
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets a double value for this log row
        /// </summary>
        public double? Numeric { get; set; }
    }
}