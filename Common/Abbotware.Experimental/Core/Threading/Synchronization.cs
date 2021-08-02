// -----------------------------------------------------------------------
// <copyright file="Synchronization.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Threading
{
    using System;
    using System.Threading;

    /// <summary>
    ///     helper class to deal with synchronization / blocking
    /// </summary>
    public static class Synchronization
    {
        /// <summary>
        ///     Waits for an event to trigger, or cancellation
        /// </summary>
        /// <param name="waitFor">event to wait for</param>
        /// <param name="cancelNotification">cancellation</param>
        /// <returns>true if the event fired, false if it was canceled </returns>
        public static bool WaitAndShouldContinue(ManualResetEventSlim waitFor, ManualResetEventSlim cancelNotification)
        {
            Arguments.NotNull(waitFor, nameof(waitFor));
            Arguments.NotNull(cancelNotification, nameof(cancelNotification));

            return WaitAndShouldContinue(waitFor.WaitHandle, cancelNotification.WaitHandle);
        }

        /// <summary>
        ///     Waits for an event to trigger, or cancellation
        /// </summary>
        /// <param name="waitFor">event to wait for</param>
        /// <param name="cancelNotification">cancellation</param>
        /// <returns>true if the event fired, false if it was canceled </returns>
        public static bool WaitAndShouldContinue(WaitHandle waitFor, WaitHandle cancelNotification)
        {
            Arguments.NotNull(waitFor, nameof(waitFor));
            Arguments.NotNull(cancelNotification, nameof(cancelNotification));

            var handles = new[]
            {
                waitFor,
                cancelNotification,
            };

            // indicates the array position 1 received a signal which is the cancel signal
            if (WaitHandle.WaitAny(handles) == 1)
            {
                return false;
            }

            return true;
        }
    }
}