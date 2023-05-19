// -----------------------------------------------------------------------
// <copyright file="ShutdownMethods.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Windows.User32
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     Type of shutdown being performed
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "following msdn Win32 defintion of ShutdownMethod")]
    [SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "following msdn Win32 defintion of ShutdownMethod")]
    [Flags]
    public enum ShutdownMethods : uint
    {
        /// <summary>
        ///     Beginning with Windows 8:  You can prepare the system for a faster startup by combining the EWX_HYBRID_SHUTDOWN
        ///     flag with the EWX_SHUTDOWN flag.
        /// </summary>
        HybridShutdown = 0x00400000,

        /// <summary>
        ///     Shuts down all processes running in the logon session of the process that called the ExitWindowsEx function. Then
        ///     it logs the user off.
        ///     This flag can be used only by processes running in an interactive user's logon session.
        /// </summary>
        LogOff = 0,

        /// <summary>
        ///     Shuts down the system and turns off the power. The system must support the power-off feature.
        ///     The calling process must have the SE_SHUTDOWN_NAME privilege. For more information, see the following Remarks
        ///     section.
        /// </summary>
        PowerOff = 0x00000008,

        /// <summary>
        ///     Shuts down the system and then restarts the system.
        ///     The calling process must have the SE_SHUTDOWN_NAME privilege. For more information, see the following Remarks
        ///     section.
        /// </summary>
        Reboot = 0x00000002,

        /// <summary>
        ///     Shuts down the system and then restarts it, as well as any applications that have been registered for restart using
        ///     the RegisterApplicationRestart function. These application receive the WM_QUERYENDSESSION message with lParameter
        ///     set to the ENDSESSION_CLOSEAPP value. For more information, see Guidelines for Applications.
        /// </summary>
        RestartApps = 0x00000040,

        /// <summary>
        ///     Shuts down the system to a point at which it is safe to turn off the power. All file buffers have been flushed to
        ///     disk, and all running processes have stopped.
        ///     The calling process must have the SE_SHUTDOWN_NAME privilege. For more information, see the following Remarks
        ///     section.
        ///     Specifying this flag will not turn off the power even if the system supports the power-off feature. You must
        ///     specify EWX_POWEROFF to do this.
        /// </summary>
        Shutdown = 0x00000001,
    }
}