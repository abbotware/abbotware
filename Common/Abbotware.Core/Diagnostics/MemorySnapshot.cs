// -----------------------------------------------------------------------
// <copyright file="MemorySnapshot.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Diagnostics;

using System.Text;
using Abbotware.Core.Text;

/// <summary>
/// Represents a coarse snapshot of memory usage, including working set, managed heap, and pinned object count.
/// </summary>
/// <param name="WorkingSetBytes">The total physical memory (in bytes) currently in use by the process.</param>
/// <param name="ManagedHeapBytes">The memory (in bytes) allocated in the managed heap.</param>
/// <param name="TotalHeapBytes">The total memory (in bytes) allocated by the GC heap (Gen0, Gen1, Gen2, LOH).</param>
/// <param name="PinnedObjects">The number of objects currently pinned in memory by the GC.</param>
/// <param name="FragmentedBytes">Unusable free memory caused by heap fragmentation.</param>
public record MemorySnapshot(
   long WorkingSetBytes,
   long ManagedHeapBytes,
   long TotalHeapBytes,
   long PinnedObjects,
   long FragmentedBytes)
{
    /// <summary>
    /// Gets the percentage of heap fragmentation.
    /// </summary>
    public double FragmentationPercent =>
        this.TotalHeapBytes == 0 ? 0 : (double)this.FragmentedBytes / this.TotalHeapBytes * 100;

    /// <summary>
    /// Computes a new <see cref="MemorySnapshot"/> representing the delta between this snapshot and another.
    /// </summary>
    /// <param name="other">The earlier snapshot to subtract from this one.</param>
    /// <returns>A new <see cref="MemorySnapshot"/> containing the differences between the two snapshots.</returns>
    public MemorySnapshot Delta(MemorySnapshot other) =>
        new(
            WorkingSetBytes: this.WorkingSetBytes - other.WorkingSetBytes,
            ManagedHeapBytes: this.ManagedHeapBytes - other.ManagedHeapBytes,
            TotalHeapBytes: this.TotalHeapBytes - other.TotalHeapBytes,
            PinnedObjects: this.PinnedObjects - other.PinnedObjects,
            FragmentedBytes: this.FragmentedBytes - other.FragmentedBytes);

    /// <summary>
    /// Returns a human-readable string representation of the memory snapshot.
    /// </summary>
    /// <returns>A formatted string showing memory statistics.</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("WorkingSet: ").Append(StringFormat.Bytes(this.WorkingSetBytes)).Append(", ");
        sb.Append("Managed: ").Append(StringFormat.Bytes(this.ManagedHeapBytes)).Append(", ");
        sb.Append("Heap: ").Append(StringFormat.Bytes(this.TotalHeapBytes)).Append(", ");
        sb.Append("Pinned: ").Append(this.PinnedObjects);
        return sb.ToString();
    }
}