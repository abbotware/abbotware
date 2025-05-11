// -----------------------------------------------------------------------
// <copyright file="GarbageCollector.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

#if NET8_0_OR_GREATER

namespace Abbotware.Core.Diagnostics;

using System;
using System.Diagnostics;
using Abbotware.Core.Chrono;

/// <summary>
/// Provides utilities for explicitly invoking full garbage collection and capturing memory usage statistics.
/// </summary>
public static partial class GarbageCollector
{
    /// <summary>
    /// Forces a full, blocking, compacting garbage collection and returns a delta snapshot of memory usage.
    /// </summary>
    /// <returns>A <see cref="MemorySnapshotDelta"/> containing before and after memory states, their delta, and duration.</returns>
    public static MemorySnapshotDelta ForceFullCollection()
    {
        var sw = HighResolutionStopWatch.StartNew();

        using var p = Process.GetCurrentProcess();

        var before = CaptureSnapshot(p);

        // Step 1: Trigger full GC (all generations)
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, blocking: true, compacting: true);

        // Step 2: Ensure all finalizers have run
        GC.WaitForPendingFinalizers();

        // Step 3: Wait for any in-progress full GC to complete
        var status = GC.WaitForFullGCComplete();

        if (status != GCNotificationStatus.Succeeded)
        {
            // Consider logging a warning or error — full GC notification failed
        }

        // Step 4: Final cleanup pass
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, blocking: true, compacting: true);

        p.Refresh();

        var after = CaptureSnapshot(p);

        return new MemorySnapshotDelta(before, after, sw.Elapsed);

        static MemorySnapshot CaptureSnapshot(Process process)
        {
            var gcInfo = GC.GetGCMemoryInfo();

            return new MemorySnapshot(
                WorkingSetBytes: process.WorkingSet64,
                ManagedHeapBytes: GC.GetTotalMemory(forceFullCollection: false),
                TotalHeapBytes: gcInfo.HeapSizeBytes,
                PinnedObjects: gcInfo.PinnedObjectsCount,
                FragmentedBytes: gcInfo.FragmentedBytes);
        }
    }
}

#endif
