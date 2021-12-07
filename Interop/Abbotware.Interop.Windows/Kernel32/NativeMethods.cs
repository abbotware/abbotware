// -----------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Windows.Kernel32
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     Dll Import Wrapper for the Kernel32.dll
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        ///     Name of the unmanaged  kernel32 dll
        /// </summary>
        public const string LIB_KERNEL32 = "kernel32.dll";

        /// <summary>
        ///     Dll Import for the SetConsoleCtrlHandler(...) function
        /// </summary>
        /// <param name="handlerRoutine">
        ///     A pointer to the application-defined HandlerRoutine function to be added or removed. This
        ///     parameter can be NULL.
        /// </param>
        /// <param name="add">If this parameter is TRUE, the handler is added; if it is FALSE, the handler is removed.</param>
        /// <remarks>
        ///     If the HandlerRoutine parameter is NULL, a TRUE value causes the calling process to ignore CTRL+C input, and a
        ///     FALSE value restores normal processing of CTRL+C input. This attribute of ignoring or processing CTRL+C is
        ///     inherited by child processes.
        /// </remarks>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get
        ///     extended error information, call GetLastError.
        /// </returns>
        [DllImport(LIB_KERNEL32, CallingConvention = CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool SetConsoleCtrlHandler(ConsoleCtrlHandler handlerRoutine, [MarshalAs(UnmanagedType.Bool)] bool add);

        /// <summary>
        ///     Dll Import for the CloseHandle function
        /// </summary>
        /// <param name="handle">Handle to close</param>
        /// <returns>true/false if successful</returns>
        [DllImport(LIB_KERNEL32, CallingConvention = CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool CloseHandle(IntPtr handle);

        /// <summary>
        ///     Dll Import for the SetDllDirectory function
        /// </summary>
        /// <param name="path">path to add to dll search</param>
        /// <returns>true/false if successful</returns>
        [DllImport(LIB_KERNEL32, CallingConvention = CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool SetDllDirectory(char[]? path);

        /// <summary>
        ///     Retrieves information about the system's current usage of both physical and virtual memory.
        /// </summary>
        /// <param name="lpBuffer">
        ///     A pointer to a MEMORYSTATUSEX structure that receives information about current memory
        ///     availability.
        /// </param>
        /// <returns>true/false if the function succeeds</returns>
        [DllImport(LIB_KERNEL32, CallingConvention = CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool GlobalMemoryStatusEx([In] [Out] ref MemoryStatusEx lpBuffer);

        /// <summary>
        ///     Causes the calling thread to yield execution to another thread that is ready to run on the current processor. The
        ///     operating system selects the next thread to be executed.
        /// </summary>
        /// <returns>true/false if the function succeeds</returns>
        [DllImport(LIB_KERNEL32, CallingConvention = CallingConvention.StdCall, SetLastError = true, BestFitMapping = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool SwitchToThread();

        /// <summary>
        ///     Retrieves a pseudo handle for the current process.
        /// </summary>
        /// <returns>he return value is a pseudo handle to the current process.</returns>
        [DllImport(LIB_KERNEL32, CallingConvention = CallingConvention.StdCall)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern IntPtr GetCurrentProcess();

        /// <summary>
        ///     Sets the minimum and maximum working set sizes for the specified process.
        /// </summary>
        /// <param name="pProcess">A handle to the process whose working set sizes is to be set.</param>
        /// <param name="dwMinimumWorkingSetSize">The minimum working set size for the process, in bytes</param>
        /// <param name="dwMaximumWorkingSetSize">The maximum working set size for the process, in bytes</param>
        /// <returns>true/false if the function succeeds</returns>
        [DllImport(LIB_KERNEL32, EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern bool SetProcessWorkingSetSize64(IntPtr pProcess, long dwMinimumWorkingSetSize, long dwMaximumWorkingSetSize);
    }
}