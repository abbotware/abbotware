// -----------------------------------------------------------------------
// <copyright file="PsapiMethods.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Windows.Psapi
{
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using Abbotware.Interop.Windows.Kernel32;

    /// <summary>
    ///     Wrapper for the Psapi.dll
    /// </summary>
    public static class PsapiMethods
    {
        /// <summary>
        ///     Retrieves information about the memory usage of the specified process.
        /// </summary>
        /// <returns>Process Memory Counters object</returns>
        public static ProcessMemoryCounters GetProcessMemoryInfo()
        {
            var currentProcessHandle = Kernel32.NativeMethods.GetCurrentProcess();

            var mem = default(ProcessMemoryCounters);

            mem.cb = (uint)Marshal.SizeOf(typeof(ProcessMemoryCounters));

            if (!NativeMethods.GetProcessMemoryInfo(currentProcessHandle, out mem, mem.cb))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "Get Process Memory Info failed");
            }

            return mem;
        }
    }
}