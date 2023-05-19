// -----------------------------------------------------------------------
// <copyright file="MemoryStatusEx.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Windows.Kernel32
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     Contains information about the current state of both physical and virtual memory, including extended memory. The GlobalMemoryStatusEx function stores information in this structure.
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Only used in Wrapped Windows API calls")]
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "Matching Windows API Name Specificiation")]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct MemoryStatusEx
    {
        /// <summary>
        ///     The size of the structure, in bytes. You must set this member before calling GlobalMemoryStatusEx
        /// </summary>
        public uint Length;

        /// <summary>
        ///     A number between 0 and 100 that specifies the approximate percentage of physical memory that is in use (0 indicates no memory use and 100 indicates full memory use).
        /// </summary>
        public uint MemoryLoad;

        /// <summary>
        ///     The amount of actual physical memory, in bytes.
        /// </summary>
        public ulong TotalPhysical;

        /// <summary>
        ///     The amount of physical memory currently available, in bytes. This is the amount of physical memory that can be
        ///     immediately reused without having to write its contents to disk first. It is the sum of the size of the standby,
        ///     free, and zero lists.
        /// </summary>
        public ulong AvailablePhysical;

        /// <summary>
        ///     The current committed memory limit for the system or the current process, whichever is smaller, in bytes. To get
        ///     the system-wide committed memory limit, call GetPerformanceInfo.
        /// </summary>
        public ulong TotalPageFile;

        /// <summary>
        ///     The maximum amount of memory the current process can commit, in bytes. This value is equal to or smaller than the
        ///     system-wide available commit value. To calculate the system-wide available commit value, call GetPerformanceInfo
        ///     and subtract the value of CommitTotal from the value of CommitLimit.
        /// </summary>
        public ulong AvailablePageFile;

        /// <summary>
        ///     The size of the user-mode portion of the virtual address space of the calling process, in bytes. This value depends
        ///     on the type of process, the type of processor, and the configuration of the operating system. For example, this
        ///     value is approximately 2 GB for most 32-bit processes on an x86 processor and approximately 3 GB for 32-bit
        ///     processes that are large address aware running on a system with 4-gigabyte tuning enabled.
        /// </summary>
        public ulong TotalVirtual;

        /// <summary>
        ///     The amount of unreserved and uncommitted memory currently in the user-mode portion of the virtual address space of
        ///     the calling process, in bytes.
        /// </summary>
        public ulong AvailableVirtual;

        /// <summary>
        ///     Reserved. This value is always 0.
        /// </summary>
        public ulong AvailableExtendedVirtual;
    }
}