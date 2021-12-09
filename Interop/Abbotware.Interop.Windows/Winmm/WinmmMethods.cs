// -----------------------------------------------------------------------
// <copyright file="WinmmMethods.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Windows.Winmm
{
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     Wrapper for the Winmm.dll
    /// </summary>
    public static class WinmmMethods
    {
        /// <summary>
        ///     The timeBeginPeriod function requests a minimum resolution for periodic timers.
        /// </summary>
        /// <param name="period">
        ///     Minimum timer resolution, in milliseconds, for the application or device driver. A lower value
        ///     specifies a higher (more accurate) resolution.
        /// </param>
        public static void TimeBeginPeriod(uint period)
        {
            if (NativeMethods.TimeBeginPeriod(period) != NativeMethods.TIMERR_NOERROR)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "TimeBeginPeriod failed");
            }
        }

        /// <summary>
        ///     The timeEndPeriod function clears a previously set minimum timer resolution.
        /// </summary>
        /// <param name="period">Minimum timer resolution specified in the previous call to the timeBeginPeriod function.</param>
        public static void TimeEndPeriod(uint period)
        {
            if (NativeMethods.TimeEndPeriod(period) != NativeMethods.TIMERR_NOERROR)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "TimeEndPeriod failed");
            }
        }
    }
}