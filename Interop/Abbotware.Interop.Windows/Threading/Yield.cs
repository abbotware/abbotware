// -----------------------------------------------------------------------
// <copyright file="Yield.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Windows.Threading
{
    using System;
    using System.Threading;
    using Abbotware.Core.Threading;
    using Abbotware.Interop.Windows.Kernel32;
    using Abbotware.Interop.Windows.Winmm;

    /// <summary>
    ///     Helper class to control thread yield operations
    /// </summary>
    public static class Yield
    {
        /// <summary>
        ///     yields time slice of current thread to specified target threads
        /// </summary>
        /// <param name="threadYieldTarget">yield type</param>
        public static void To(YieldTarget threadYieldTarget)
        {
            switch (threadYieldTarget)
            {
                case YieldTarget.None:
                    break;

                case YieldTarget.AnyThreadOnAnyProcessor:

                    // reduce sleep to actually 1ms instead of system time slice with is around 15ms
                    WinmmMethods.TimeBeginPeriod(1);

                    Thread.Sleep(1);

                    // undo
                    WinmmMethods.TimeEndPeriod(1);
                    break;

                case YieldTarget.SameOrHigherPriorityThreadOnAnyProcessor:
                    Thread.Sleep(0);
                    break;

                case YieldTarget.AnyThreadOnSameProcessor:
                    Kernel32Methods.SwitchToThread();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(threadYieldTarget));
            }
        }
    }
}