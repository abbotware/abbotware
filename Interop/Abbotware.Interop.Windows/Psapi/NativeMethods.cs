// -----------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Windows.Psapi
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     Dll Import Wrapper for the Psapi.dll
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        ///     Name of the unmanaged Psapi dll
        /// </summary>
        public const string LIB_PSAPI = "psapi.dll";

        /// <summary>
        ///     Retrieves information about the memory usage of the specified process.
        /// </summary>
        /// <param name="hProcess">A handle to the process</param>
        /// <param name="counters">A pointer to the ProcessMemoryCounters </param>
        /// <param name="size">The size of the ProcessMemoryCounters structure, in bytes</param>
        /// <returns>true/false if successful</returns>
        [DllImport(LIB_PSAPI, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetProcessMemoryInfo(IntPtr hProcess, out ProcessMemoryCounters counters, uint size);
    }
}