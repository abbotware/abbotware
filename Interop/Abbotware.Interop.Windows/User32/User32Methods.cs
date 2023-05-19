// -----------------------------------------------------------------------
// <copyright file="User32Methods.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Windows.User32
{
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     Wrapper for the User32.dll
    /// </summary>
    public static class User32Methods
    {
        /// <summary>
        ///     Wrapper for rebooting the Operating System
        /// </summary>
        /// <param name="shutdownMethod">type of shutdown</param>
        /// <param name="forceOptions">force the shutdown</param>
        /// <param name="shutdownReason">reason of shutdown</param>
        [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "matching win32 MSDN documentation")]
        public static void ExitWindowsEx(ShutdownMethods shutdownMethod, ForceShutdownMethods forceOptions, ShutdownReasons shutdownReason)
        {
            var method = (uint)shutdownMethod | (uint)forceOptions;

            if (!NativeMethods.ExitWindowsEx(method, shutdownReason))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "ExitWindowsEx failed");
            }
        }
    }
}