// -----------------------------------------------------------------------
// <copyright file="MemorySnapshotDelta.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Diagnostics;

using System.Globalization;
using System.Text;
using Abbotware.Core.Chrono;
using Abbotware.Core.Text;

/// <summary>
/// Represents a comparison between two memory snapshots, including the before and after states,
/// the delta between them, and the time duration over which the change occurred.
/// </summary>
/// <param name="Before">The memory snapshot before the operation.</param>
/// <param name="After">The memory snapshot after the operation.</param>
/// <param name="Duration">The time span between the two snapshots.</param>
public record MemorySnapshotDelta(
    MemorySnapshot Before,
    MemorySnapshot After,
    HighResolutionTimeSpan Duration)
{
    /// <summary>
    /// Gets the delta snapshot computed as After - Before.
    /// </summary>
    public MemorySnapshot Delta { get; } = After.Delta(Before);

    /// <inheritdoc/>
    public override string ToString()
    {
        static string Format(long bytes) => StringFormat.Bytes(bytes);
        var culture = CultureInfo.InvariantCulture;

        var sb = new StringBuilder();
        sb.AppendLine("[GC Memory Stats]");
        sb.AppendFormat(
            culture,
            "  Duration:         {0:N2} ms",
            this.Duration.TotalMilliseconds).AppendLine();

        sb.AppendFormat(
            culture,
            "  Working Set:      Before = {0,10} | After = {1,10} | Δ = {2,10}",
            Format(this.Before.WorkingSetBytes),
            Format(this.After.WorkingSetBytes),
            Format(this.Delta.WorkingSetBytes)).AppendLine();

        sb.AppendFormat(
            culture,
            "  Managed Heap:     Before = {0,10} | After = {1,10} | Δ = {2,10}",
            Format(this.Before.ManagedHeapBytes),
            Format(this.After.ManagedHeapBytes),
            Format(this.Delta.ManagedHeapBytes)).AppendLine();

        sb.AppendFormat(
            culture,
            "  Total Heap:       Before = {0,10} | After = {1,10} | Δ = {2,10}",
            Format(this.Before.TotalHeapBytes),
            Format(this.After.TotalHeapBytes),
            Format(this.Delta.TotalHeapBytes)).AppendLine();

        sb.AppendFormat(
            culture,
            "  Pinned Objects:   Before = {0,10} | After = {1,10} | Δ = {2,10}",
            this.Before.PinnedObjects,
            this.After.PinnedObjects,
            this.Delta.PinnedObjects).AppendLine();

        sb.AppendFormat(
            culture,
            "  Fragmented Bytes: Before = {0,10} | After = {1,10} | Δ = {2,10}",
            Format(this.Before.FragmentedBytes),
            Format(this.After.FragmentedBytes),
            Format(this.Delta.FragmentedBytes)).AppendLine();

        sb.AppendFormat(
            culture,
            "  Fragmented %:     Before = {0,6:F2}% | After = {1,6:F2}% | Δ = {2,6:F2}%",
            this.Before.FragmentationPercent,
            this.After.FragmentationPercent,
            this.After.FragmentationPercent - this.Before.FragmentationPercent).AppendLine();

        return sb.ToString();
    }
}