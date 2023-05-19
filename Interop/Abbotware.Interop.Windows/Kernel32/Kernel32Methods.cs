// -----------------------------------------------------------------------
// <copyright file="Kernel32Methods.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Windows.Kernel32
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;

    /// <summary>
    ///     Wrapper for the Kernel32.dll
    /// </summary>
    public static class Kernel32Methods
    {
        /// <summary>
        ///     Adds or removes a Console Control Handler
        /// </summary>
        /// <param name="handlerRoutine">handler to be added or removed</param>
        /// <param name="add">true = add handler, false = remove handler</param>
        public static void SetConsoleCtrlHandler(ConsoleCtrlHandler handlerRoutine, bool add)
        {
            Arguments.NotNull(handlerRoutine, nameof(handlerRoutine));

            if (!NativeMethods.SetConsoleCtrlHandler(handlerRoutine, add))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "unable to Set Console Ctrl Handler");
            }
        }

        /// <summary>
        ///     closes a Pointer / HANDLE
        /// </summary>
        /// <param name="handle">Pointer / HANDLE to close</param>
        public static void CloseHandle(IntPtr handle)
        {
            Arguments.NotZero(handle, nameof(handle));

            if (!NativeMethods.CloseHandle(handle))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "unable to Close Handle");
            }
        }

        /// <summary>
        ///     Sets the DLL Search Directory back to its default
        /// </summary>
        public static void ResetDllDirectory()
        {
            if (!NativeMethods.SetDllDirectory(null))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "unable to Reset Dll Directory");
            }
        }

        /// <summary>
        ///     adds the supplied path to the DLL Search Directory
        /// </summary>
        /// <param name="path">path to add to dll search Directory</param>
        public static void SetDllDirectory(string path)
        {
            path = Arguments.EnsureNotNullOrWhitespace(path, nameof(path));

            if (!NativeMethods.SetDllDirectory(path.ToCharArray()))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "unable to Set Dll Directory");
            }
        }

        /// <summary>
        ///     Retrieves information about the system's current usage of both physical and virtual memory.
        /// </summary>
        /// <returns>memory status ex object</returns>
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "Matching Win32 Name Specificiation")]
        public static MemoryStatusEx GlobalMemoryStatusEx()
        {
            var mem = default(MemoryStatusEx);

            mem.Length = (uint)Marshal.SizeOf(typeof(MemoryStatusEx));

            if (!NativeMethods.GlobalMemoryStatusEx(ref mem))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "GlobalMemoryStatusEx failed");
            }

            return mem;
        }

        /// <summary>
        ///     Causes the calling thread to yield execution to another thread that is ready to run on the current processor. The
        ///     operating system selects the next thread to be executed.
        /// </summary>
        public static void SwitchToThread()
        {
            if (!NativeMethods.SwitchToThread())
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "SwitchToThread failed");
            }
        }

        /// <summary>
        ///     Sets the minimum and maximum working set sizes for the specified process.
        /// </summary>
        /// <param name="minWorkingSet">The minimum working set size for the process, in bytes</param>
        /// <param name="maxWorkingSet">The maximum working set size for the process, in bytes</param>
        public static void SetProcessWorkingSetSize(long minWorkingSet, long maxWorkingSet)
        {
            var process = NativeMethods.GetCurrentProcess();

            if (!NativeMethods.SetProcessWorkingSetSize64(process, minWorkingSet, maxWorkingSet))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "Set Process Working Set Size");
            }
        }
    }
}