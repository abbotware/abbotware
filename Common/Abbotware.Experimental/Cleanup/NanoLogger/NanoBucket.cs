//--------------------------------------------------------------------
// -----------------------------------------------------------------------
// <copyright file="NanoBucket.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Diagnostics.Plugins
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;

    /// <summary>
    ///     Container for NanoLogRows which is merged into the NanoLogger at a future point in time.
    /// </summary>
    public class NanoBucket
    {
        /// <summary>
        ///     Gets the logging data rows
        /// </summary>
        private readonly NanoLogRow[] data;

        /// <summary>
        ///     Index for the bucket entries
        ///     Note: no interlocked or locking since this is used by single thread
        /// </summary>
        private int index = -1;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NanoBucket" /> class.
        /// </summary>
        /// <param name="correlationTypeId">correlation type id for bucket</param>
        /// <param name="rows">number of rows to allocate</param>
        public NanoBucket(byte correlationTypeId, uint rows)
        {
            this.data = new NanoLogRow[rows];

            for (var j = 0U; j < rows; ++j)
            {
                this.data[j] = new NanoLogRow
                {
                    InstanceId = j,
                    CorrelationTypeId = correlationTypeId,
                    ThreadId = Thread.CurrentThread.ManagedThreadId,
                };
            }
        }

        /// <summary>
        ///     Set the beginning  of an event
        /// </summary>
        /// <param name="instanceId">instance id</param>
        /// <param name="ticks">ticks data</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetBegin(uint instanceId, long ticks)
        {
            var row = this.data[++this.index];

            Debug.Assert(row.Ticks == 0, "can only set ticks value once");
            Debug.Assert(row.ThreadId == Thread.CurrentThread.ManagedThreadId, "bucket used by wrong thread");

            row.Ticks = ticks;
            row.InstanceId = instanceId;
        }

        /// <summary>
        ///     Set the end of an event
        /// </summary>
        /// <param name="instanceId">instance id</param>
        /// <param name="ticks">ticks data</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetEnd(uint instanceId, long ticks)
        {
            var row = this.data[++this.index];

            Debug.Assert(row.Ticks == 0, "can only set ticks value once");
            Debug.Assert(row.ThreadId == Thread.CurrentThread.ManagedThreadId, "bucket used by wrong thread");

            row.Ticks = ticks;
            row.InstanceId = instanceId;

            row.IsEnd = true;
        }

        /// <summary>
        ///     Set the beginning of an event
        /// </summary>
        /// <param name="instanceId">instance id</param>
        /// <param name="checkpointId">checkpoint Id</param>
        /// <param name="ticks">ticks data</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetEnd(uint instanceId, ushort checkpointId, long ticks)
        {
            var row = this.data[++this.index];

            Debug.Assert(row.Ticks == 0, "can only set ticks value once");

            row.Ticks = ticks;
            row.InstanceId = instanceId;
            row.CheckpointId = checkpointId;
            row.IsEnd = true;
        }

        /// <summary>
        ///     Set the end of an event
        /// </summary>
        /// <param name="instanceId">instance id</param>
        /// <param name="checkpointId">checkpoint Id</param>
        /// <param name="ticks">ticks data</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetBegin(uint instanceId, ushort checkpointId, long ticks)
        {
            var row = this.data[++this.index];

            Debug.Assert(row.Ticks == 0, "can only set ticks value once");

            row.Ticks = ticks;
            row.InstanceId = instanceId;
            row.CheckpointId = checkpointId;
        }

        /// <summary>
        ///     Set the beginning of an event
        /// </summary>
        /// <param name="instanceId">instance id</param>
        /// <param name="checkpointId">checkpoint Id</param>
        /// <param name="ticks">ticks data</param>
        /// <param name="threadId">thread id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetBegin(uint instanceId, ushort checkpointId, long ticks, int threadId)
        {
            var row = this.data[++this.index];

            Debug.Assert(row.Ticks == 0, "can only set ticks value once");

            row.ThreadId = threadId;
            row.Ticks = ticks;
            row.InstanceId = instanceId;
            row.CheckpointId = checkpointId;
        }

        /// <summary>
        ///     Set the end of an event
        /// </summary>
        /// <param name="instanceId">instance id</param>
        /// <param name="checkpointId">checkpoint Id</param>
        /// <param name="ticks">ticks data</param>
        /// <param name="threadId">thread id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetEnd(uint instanceId, ushort checkpointId, long ticks, int threadId)
        {
            var row = this.data[++this.index];

            Debug.Assert(row.Ticks == 0, "can only set ticks value once");

            row.ThreadId = threadId;
            row.Ticks = ticks;
            row.InstanceId = instanceId;
            row.CheckpointId = checkpointId;
            row.IsEnd = true;
        }

        /// <summary>
        ///     Logs the beginning of an event
        /// </summary>
        /// <param name="instanceId">instance id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Begin(uint instanceId)
        {
            var row = this.data[++this.index];

            Debug.Assert(row.Ticks == 0, "can only set ticks value once");
            Debug.Assert(row.ThreadId == Thread.CurrentThread.ManagedThreadId, "bucket used by wrong thread");

            row.Ticks = Stopwatch.GetTimestamp();
            row.InstanceId = instanceId;
        }

        /// <summary>
        ///     Logs the end of an event
        /// </summary>
        /// <param name="instanceId">instance id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void End(uint instanceId)
        {
            var row = this.data[++this.index];

            Debug.Assert(row.Ticks == 0, "can only set ticks value once");
            Debug.Assert(row.ThreadId == Thread.CurrentThread.ManagedThreadId, "bucket used by wrong thread");

            row.Ticks = Stopwatch.GetTimestamp();
            row.InstanceId = instanceId;

            row.IsEnd = true;
        }

        /// <summary>
        ///     Logs the beginning of an event
        /// </summary>
        /// <param name="instanceId">instance id</param>
        /// <param name="checkpointId">checkpoint id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Begin(uint instanceId, ushort checkpointId)
        {
            var row = this.data[++this.index];

            Debug.Assert(row.Ticks == 0, "can only set ticks value once");
            Debug.Assert(row.ThreadId == Thread.CurrentThread.ManagedThreadId, "bucket used by wrong thread");

            row.Ticks = Stopwatch.GetTimestamp();
            row.InstanceId = instanceId;
            row.CheckpointId = checkpointId;
        }

        /// <summary>
        ///     Logs the end of an event
        /// </summary>
        /// <param name="instanceId">instance id</param>
        /// <param name="checkpointId">checkpoint id</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void End(uint instanceId, ushort checkpointId)
        {
            var row = this.data[++this.index];

            Debug.Assert(row.Ticks == 0, "can only set ticks value once");
            Debug.Assert(row.ThreadId == Thread.CurrentThread.ManagedThreadId, "bucket used by wrong thread");

            row.Ticks = Stopwatch.GetTimestamp();
            row.InstanceId = instanceId;

            row.CheckpointId = checkpointId;
            row.IsEnd = true;
        }

        /// <summary>
        ///     Logs the beginning of an event
        /// </summary>
        /// <param name="instanceId">instance id</param>
        /// <param name="checkpointId">checkpoint id</param>
        /// <param name="checkpointProgress">checkpoint progress data</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Begin(uint instanceId, ushort checkpointId, long checkpointProgress)
        {
            var row = this.data[++this.index];

            Debug.Assert(row.Ticks == 0, "can only set ticks value once");
            Debug.Assert(row.ThreadId == Thread.CurrentThread.ManagedThreadId, "bucket used by wrong thread");

            row.Ticks = Stopwatch.GetTimestamp();
            row.InstanceId = instanceId;

            row.CheckpointId = checkpointId;
            row.CheckpointProgress = checkpointProgress;
        }

        /// <summary>
        ///     Logs the end of an event
        /// </summary>
        /// <param name="instanceId">instance id</param>
        /// <param name="checkpointId">checkpoint id</param>
        /// <param name="checkpointProgress">checkpoint progress data</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void End(uint instanceId, ushort checkpointId, long checkpointProgress)
        {
            var row = this.data[++this.index];

            Debug.Assert(row.Ticks == 0, "can only set ticks value once");
            Debug.Assert(row.ThreadId == Thread.CurrentThread.ManagedThreadId, "bucket used by wrong thread");

            row.Ticks = Stopwatch.GetTimestamp();
            row.InstanceId = instanceId;

            row.CheckpointId = checkpointId;
            row.CheckpointProgress = checkpointProgress;
            row.IsEnd = true;
        }

        /// <summary>
        /// Gets the internal data rows
        /// </summary>
        /// <returns>logger data rows</returns>
        public NanoLogRow[] GetRows()
        {
            return this.data;
        }
    }
}