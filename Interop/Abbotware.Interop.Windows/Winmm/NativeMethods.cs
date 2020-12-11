// -----------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Windows.Winmm
{
    using System.Runtime.InteropServices;

    /// <summary>
    ///     Dll Import Wrapper for the Winmm.dll
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        ///     Name of the unmanaged  Winmm dll
        /// </summary>
        public const string LIB_WINMM = "Winmm.dll";

        /// <summary>
        ///     Value from Windows Native API indicating success for the timeBeginPeriod/timeEndPeriod Methods
        /// </summary>
        /// <remarks>
        ///     From Win32 API:
        ///     const TIMERR_NOERROR= 0
        /// </remarks>
        public const uint TIMERR_NOERROR = 0;

        /// <summary>
        ///     Value from Windows Native API indicating error for the timeBeginPeriod/timeEndPeriod Methods
        /// </summary>
        /// <remarks>
        ///     From Win32 API:
        ///     const TIMERR_BASE=96
        ///     const TIMERR_NOCANDO= (TIMERR_BASE + 1) -->  97
        /// </remarks>
        public const uint TIMERR_NOCANDO = 97;

        /// <summary>
        ///     The timeBeginPeriod function requests a minimum resolution for periodic timers.
        /// </summary>
        /// <param name="period">
        ///     Minimum timer resolution, in milliseconds, for the application or device driver. A lower value
        ///     specifies a higher (more accurate) resolution.
        /// </param>
        /// <returns>Returns TIMERR_NOERROR if successful or TIMERR_NOCANDO if the resolution specified in uPeriod is out of range.</returns>
        [DllImport(LIB_WINMM, EntryPoint = "timeBeginPeriod", CallingConvention = CallingConvention.StdCall, SetLastError = true, BestFitMapping = false)]
        [return: MarshalAs(UnmanagedType.U4)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern uint TimeBeginPeriod(uint period);

        /// <summary>
        ///     The timeEndPeriod function clears a previously set minimum timer resolution.
        /// </summary>
        /// <param name="period">Minimum timer resolution specified in the previous call to the timeBeginPeriod function.</param>
        /// <returns>Returns TIMERR_NOERROR if successful or TIMERR_NOCANDO if the resolution specified in uPeriod is out of range.</returns>
        [DllImport(LIB_WINMM, EntryPoint = "timeEndPeriod", CallingConvention = CallingConvention.StdCall, SetLastError = true, BestFitMapping = false)]
        [return: MarshalAs(UnmanagedType.U4)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern uint TimeEndPeriod(uint period);
    }
}