// -----------------------------------------------------------------------
// <copyright file="ProcessMemoryCounters.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Windows.Psapi
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     Contains the memory statistics for a process.
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Only used in Wrapped Windows API calls")]
    [StructLayout(LayoutKind.Sequential, Size = 72)]
    public struct ProcessMemoryCounters
    {
        /// <summary>
        ///     The size of the structure, in bytes (DWORD).
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "cb", Justification = "Msdn spelling")]
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Matching Win32 Struct Definition for DllImport")]
#pragma warning disable SA1307 // Justification = "Matching Win32 Struct Definition for DllImport")]
        public uint cb;
#pragma warning restore SA1307 // , Justification = "Matching Win32 Struct Definition for DllImport")]

        /// <summary>
        ///     The number of page faults (DWORD).
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Matching Win32 Struct Definition for DllImport")]
        public uint PageFaultCount;

        /// <summary>
        ///     The peak working set size, in bytes (SIZE_T).
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Matching Win32 Struct Definition for DllImport")]
        public ulong PeakWorkingSetSize;

        /// <summary>
        ///     The current working set size, in bytes (SIZE_T).
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Matching Win32 Struct Definition for DllImport")]
        public ulong WorkingSetSize;

        /// <summary>
        ///     The peak paged pool usage, in bytes (SIZE_T).
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Matching Win32 Struct Definition for DllImport")]
        public ulong QuotaPeakPagedPoolUsage;

        /// <summary>
        ///     The current paged pool usage, in bytes (SIZE_T).
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Matching Win32 Struct Definition for DllImport")]
        public ulong QuotaPagedPoolUsage;

        /// <summary>
        ///     The peak nonpaged pool usage, in bytes (SIZE_T).
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "NonPaged", Justification = "Matching Win32 Struct Definition for DllImport")]
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Matching Win32 Struct Definition for DllImport")]
        public ulong QuotaPeakNonPagedPoolUsage;

        /// <summary>
        ///     The current nonpaged pool usage, in bytes (SIZE_T).
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "NonPaged", Justification = "Matching Win32 Struct Definition for DllImport")]
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Matching Win32 Struct Definition for DllImport")]
        public ulong QuotaNonPagedPoolUsage;

        /// <summary>
        ///     The Commit Charge value in bytes for this process (SIZE_T). Commit Charge is the total amount of memory that the
        ///     memory manager has committed for a running process.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Pagefile", Justification = "Matching Win32 Struct Definition for DllImport")]
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Matching Win32 Struct Definition for DllImport")]
        public ulong PagefileUsage;

        /// <summary>
        ///     The peak value in bytes of the Commit Charge during the lifetime of this process (SIZE_T).
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Pagefile", Justification = "Matching Win32 Struct Definition for DllImport")]
        [SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Matching Win32 Struct Definition for DllImport")]
        public ulong PeakPagefileUsage;
    }
}