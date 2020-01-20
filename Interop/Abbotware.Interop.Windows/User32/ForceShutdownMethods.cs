// -----------------------------------------------------------------------
// <copyright file="ForceShutdownMethods.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Windows.User32
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     shutdown force actions to be taken
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags", Justification = "options can not be combined as per win32 MSDN")]
    [Flags]
    public enum ForceShutdownMethods
    {
        /// <summary>
        ///     No force options
        /// </summary>
        None = 0,

        /// <summary>
        ///     This flag has no effect if terminal services is enabled. Otherwise, the system does not send the WM_QUERYENDSESSION
        ///     message. This can cause applications to lose data. Therefore, you should only use this flag in an emergency.
        /// </summary>
        Force = 0x00000004,

        /// <summary>
        ///     Forces processes to terminate if they do not respond to the WM_QUERYENDSESSION or WM_ENDSESSION message within the
        ///     timeout interval. For more information, see the Remarks.
        /// </summary>
        ForceIfHung = 0x00000010,
    }
}