// -----------------------------------------------------------------------
// <copyright file="NanoLogger.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Diagnostics.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Abbotware.Core.Diagnostics.Plugins.Configuration;

    /// <summary>
    ///     low latency logger
    /// </summary>
    public class NanoLogger
    {
        /// <summary>
        ///     configuration of of the logger
        /// </summary>
        private readonly NanoLoggerConfiguration configuration;

        /// <summary>
        ///     index for each correlation type
        /// </summary>
        private readonly int[] correlationTypeIndex;

        /// <summary>
        ///     internal logging data split into correlation types
        /// </summary>
        private readonly NanoLogRow[][] data;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NanoLogger" /> class.
        /// </summary>
        /// <param name="rowCount">row count</param>
        public NanoLogger(int rowCount)
            : this(new NanoLoggerConfiguration(rowCount))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NanoLogger" /> class.
        /// </summary>
        /// <param name="configuration">configuration of correlation type ids</param>
        public NanoLogger(NanoLoggerConfiguration configuration)
        {
            Arguments.NotNull(configuration, nameof(configuration));

            this.configuration = configuration;

            var max = configuration.MaxCorrelationType + 1;

            this.data = new NanoLogRow[max][];
            this.correlationTypeIndex = new int[max];

            for (byte i = 0; i < this.data.Length; ++i)
            {
                if (!configuration.CorrelationTypeLogCounts.ContainsKey(i))
                {
                    continue;
                }

                var correlationInstanceCount = configuration.CorrelationTypeLogCounts[i];
                this.data[i] = new NanoLogRow[correlationInstanceCount];
                this.correlationTypeIndex[i] = -1;

                for (var j = 0; j < correlationInstanceCount; ++j)
                {
                    this.data[i][j] = new NanoLogRow { CorrelationTypeId = i };
                }
            }
        }

        /// <summary>
        ///     Gets the number of ticks in a second
        /// </summary>
        public static long TicksPerSecond
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return Stopwatch.Frequency; }
        }

        /// <summary>
        ///     Gets the number of nanoseconds in a tick
        /// </summary>
        public static long NanosecondsPerTick
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return 1000000000 / Stopwatch.Frequency; }
        }

        /// <summary>
        ///     Gets the current timestamp in ticks
        /// </summary>
        public static long CurrentTimestamp
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return Stopwatch.GetTimestamp(); }
        }

        /// <summary>
        ///     computes the number of ticks for a given timespan
        /// </summary>
        /// <param name="timeSpan">time span</param>
        /// <returns>number of ticks</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ConvertTimeSpanToTicks(TimeSpan timeSpan)
        {
            return timeSpan.TotalSeconds * Stopwatch.Frequency;
        }

        /// <summary>
        ///     coverts ticks to milliseconds
        /// </summary>
        /// <param name="ticks">ticks</param>
        /// <returns>number of ticks</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ConvertTicksToMilliseconds(long ticks)
        {
            return ticks / (double)Stopwatch.Frequency * 1000d;
        }

        /// <summary>
        ///     coverts ticks to microseconds
        /// </summary>
        /// <param name="ticks">ticks</param>
        /// <returns>number of ticks</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ConvertTicksToMicroseconds(long ticks)
        {
            return ticks / (double)Stopwatch.Frequency * 1000000d;
        }

        /// <summary>
        ///     coverts ticks to nanoseconds
        /// </summary>
        /// <param name="ticks">ticks</param>
        /// <returns>number of ticks</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ConvertTicksToNanoseconds(long ticks)
        {
            return ticks / (double)Stopwatch.Frequency * 1000000000d;
        }

        /// <summary>
        ///     Generate calibration data
        /// </summary>
        /// <param name="sampleSize">sample size to use for calibrating</param>
        /// <returns>calibration data</returns>
        public static GetTimestampCalibrationData CalibrateTicks(int sampleSize)
        {
            var partition = 500;
            var smallDelta = new long[partition];
            var bigDelta = new Dictionary<long, int>();

            var last = Stopwatch.GetTimestamp();

            for (var i = 0; i < sampleSize; ++i)
            {
                var next = Stopwatch.GetTimestamp();

                var delta = next - last;

                last = next;

                if (delta < partition)
                {
                    ++smallDelta[delta];
                }
                else
                {
                    if (!bigDelta.ContainsKey(delta))
                    {
                        bigDelta.Add(delta, 1);
                    }
                    else
                    {
                        ++bigDelta[delta];
                    }
                }
            }

            var variance = new Dictionary<long, int>();

            for (int i = 0; i < smallDelta.Length; ++i)
            {
                var d = smallDelta[i];

                if (d > 0)
                {
                    variance.Add(i, (int)d);
                }
            }

            foreach (var kvp in bigDelta)
            {
                variance.Add(kvp.Key, kvp.Value);
            }

            var cb = new GetTimestampCalibrationData
            {
                SampleSize = sampleSize,
                DeltaHistogram = variance,
                Max = variance.Max(k => k.Key),
                Min = variance.Where(x => x.Key != 0)
                    .Min(k => k.Value),
            };

            return cb;
        }

        /// <summary>
        ///     Exports the logger data to a csv file
        /// </summary>
        /// <param name="path">path to export</param>
        public void Export(Uri path)
        {
            Arguments.NotNull(path, nameof(path));

            using var sw = File.CreateText(path.LocalPath);

            sw.WriteLine("correlation_type_id,instance_id,ticks,thread_id,checkpoint_id,is_end,checkpoint_detail_id,checkpoint_progress,delta_first,delta_prev");

            foreach (var ctype in this.data)
            {
                foreach (var instanceGroup in ctype.GroupBy(x => x.InstanceId, x => x))
                {
                    NanoLogRow? first = null;

                    NanoLogRow? prev = null;

                    foreach (var row in instanceGroup.Where(x => x.CheckpointId.HasValue).OrderBy(x => x.Ticks))
                    {
                        if (first == null)
                        {
                            first = row;
                            prev = row;
                        }

                        var msFromFirst = ConvertTicksToMilliseconds(row.Ticks - first.Ticks);
                        var msFromPrev = ConvertTicksToMilliseconds(row.Ticks - prev!.Ticks);

                        var correlationTypeName = FormattableString.Invariant($"Unknown:{row.CorrelationTypeId}");

                        this.configuration.CorrelationType.ContainsKey(row.CorrelationTypeId);
                        {
                            correlationTypeName = this.configuration.CorrelationType[row.CorrelationTypeId];
                        }

                        var checkpointName = FormattableString.Invariant($"Unknown:{row.CheckpointId}");

                        if (row.CheckpointId.HasValue)
                        {
                            this.configuration.Checkpoint.ContainsKey(row.CheckpointId.Value);
                            {
                                checkpointName = this.configuration.Checkpoint[row.CheckpointId.Value];
                            }
                        }

                        sw.WriteLine(FormattableString.Invariant($"{correlationTypeName},{row.InstanceId},{row.Ticks},{row.ThreadId},{checkpointName},{row.IsEnd},{row.CheckpointDetailId},{row.CheckpointProgress},{msFromFirst}, {msFromPrev} "));

                        prev = row;
                    }
                }
            }
        }

        /// <summary>
        ///     Logs the beginning of an event
        /// </summary>
        /// <param name="correlationTypeId">correlation type id</param>
        /// <param name="instanceId">instance id</param>
        /// <returns>tick value actually recoreded</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long Begin(byte correlationTypeId, uint instanceId)
        {
            var index = Interlocked.Increment(ref this.correlationTypeIndex[correlationTypeId]);
            var row = this.data[correlationTypeId][index];

            row.Ticks = CurrentTimestamp;
            row.InstanceId = instanceId;
            row.ThreadId = Environment.CurrentManagedThreadId;

            return row.Ticks;
        }

        /// <summary>
        ///     Logs the end of an event
        /// </summary>
        /// <param name="correlationTypeId">correlation type id</param>
        /// <param name="instanceId">instance id</param>
        /// <returns>tick value actually recoreded</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long End(byte correlationTypeId, uint instanceId)
        {
            var index = Interlocked.Increment(ref this.correlationTypeIndex[correlationTypeId]);
            var row = this.data[correlationTypeId][index];

            row.Ticks = CurrentTimestamp;
            row.InstanceId = instanceId;
            row.ThreadId = Environment.CurrentManagedThreadId;

            row.IsEnd = true;

            return row.Ticks;
        }

        /// <summary>
        ///     Logs the beginning of an event
        /// </summary>
        /// <param name="correlationTypeId">correlation type id</param>
        /// <param name="instanceId">instance id</param>
        /// <param name="checkpointId">checkpoint id</param>
        /// <returns>tick value actually recoreded</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long Begin(int correlationTypeId, uint instanceId, ushort checkpointId)
        {
            var index = Interlocked.Increment(ref this.correlationTypeIndex[correlationTypeId]);
            var row = this.data[correlationTypeId][index];

            row.Ticks = CurrentTimestamp;
            row.InstanceId = instanceId;
            row.ThreadId = Environment.CurrentManagedThreadId;
            row.CheckpointId = checkpointId;

            return row.Ticks;
        }

        /// <summary>
        ///     Logs the end of an event
        /// </summary>
        /// <param name="correlationTypeId">correlation type id</param>
        /// <param name="instanceId">instance id</param>
        /// <param name="checkpointId">checkpoint id</param>
        /// <returns>tick value actually recoreded</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long End(byte correlationTypeId, uint instanceId, ushort checkpointId)
        {
            var index = Interlocked.Increment(ref this.correlationTypeIndex[correlationTypeId]);
            var row = this.data[correlationTypeId][index];

            row.Ticks = CurrentTimestamp;
            row.InstanceId = instanceId;
            row.ThreadId = Environment.CurrentManagedThreadId;
            row.CheckpointId = checkpointId;

            row.IsEnd = true;

            return row.Ticks;
        }

        /// <summary>
        ///     Logs the beginning of an event
        /// </summary>
        /// <param name="correlationTypeId">correlation type id</param>
        /// <param name="instanceId">instance id</param>
        /// <param name="checkpointId">checkpoint id</param>
        /// <param name="checkpointProgress">checkpoint progress data</param>
        /// <returns>tick value actually recoreded</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long Begin(byte correlationTypeId, uint instanceId, ushort checkpointId, long checkpointProgress)
        {
            var index = Interlocked.Increment(ref this.correlationTypeIndex[correlationTypeId]);
            var row = this.data[correlationTypeId][index];

            row.Ticks = CurrentTimestamp;
            row.InstanceId = instanceId;
            row.ThreadId = Environment.CurrentManagedThreadId;
            row.CheckpointId = checkpointId;
            row.CheckpointProgress = checkpointProgress;

            return row.Ticks;
        }

        /// <summary>
        ///     Logs the end of an event
        /// </summary>
        /// <param name="correlationTypeId">correlation type id</param>
        /// <param name="instanceId">instance id</param>
        /// <param name="checkpointId">checkpoint id</param>
        /// <param name="checkpointProgress">checkpoint progress data</param>
        /// <returns>tick value actually recoreded</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long End(byte correlationTypeId, uint instanceId, ushort checkpointId, long checkpointProgress)
        {
            var index = Interlocked.Increment(ref this.correlationTypeIndex[correlationTypeId]);
            var row = this.data[correlationTypeId][index];

            row.Ticks = CurrentTimestamp;
            row.InstanceId = instanceId;
            row.ThreadId = Environment.CurrentManagedThreadId;
            row.CheckpointId = checkpointId;
            row.CheckpointProgress = checkpointProgress;

            row.IsEnd = true;

            return row.Ticks;
        }

        /// <summary>
        ///     Merge raw tick values
        /// </summary>
        /// <param name="correlationTypeId">correlation type id</param>
        /// <param name="timestamps">tick values</param>
        public void Merge(byte correlationTypeId, long[] timestamps)
        {
            Arguments.NotNull(timestamps, nameof(timestamps));

            for (var i = 0; i < timestamps.Length; ++i)
            {
                var index = Interlocked.Increment(ref this.correlationTypeIndex[correlationTypeId]);
                var row = this.data[correlationTypeId][index];

                row.Ticks = CurrentTimestamp;
                row.InstanceId = (uint)i;
                row.ThreadId = Environment.CurrentManagedThreadId;
                row.CheckpointId = (ushort)i;
            }
        }

        /// <summary>
        ///     Merge data from a bucket
        /// </summary>
        /// <param name="bucket">bucket to merge</param>
        public void Merge(NanoBucket bucket)
        {
            Arguments.NotNull(bucket, nameof(bucket));

            var rows = bucket.GetRows();

            for (var i = 0; i < rows.Length; ++i)
            {
                var bucketRow = rows[i];

                if (bucketRow.Ticks == 0)
                {
                    continue;
                }

                var index = Interlocked.Increment(ref this.correlationTypeIndex[bucketRow.CorrelationTypeId]);
                var row = this.data[bucketRow.CorrelationTypeId][index];

                row.Ticks = bucketRow.Ticks;
                row.InstanceId = bucketRow.InstanceId;
                row.ThreadId = bucketRow.ThreadId;
                row.CheckpointId = bucketRow.CheckpointId;
                row.CheckpointDetailId = bucketRow.CheckpointDetailId;
                row.CheckpointProgress = bucketRow.CheckpointProgress;
                row.IsEnd = bucketRow.IsEnd;
            }
        }
    }
}