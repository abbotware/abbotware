// -----------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Windows.User32
{
    using System.Runtime.InteropServices;

    /// <summary>
    ///     Dll Import Wrapper for the user32.dll
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        ///     Name of the unmanaged  user32 dll
        /// </summary>
        public const string LIB_USER32 = "user32.dll";

        /// <summary>
        ///     Dll Import for the ExitWindowsEx function from the user32.dll
        /// </summary>
        /// <param name="shutdownMethod">method of shutdown as per MSDN</param>
        /// <param name="shutdownReasons">reasons for shutdown as per MSDN</param>
        /// <returns>true if successful</returns>
        [DllImport(LIB_USER32, CallingConvention = CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ExitWindowsEx(uint shutdownMethod, ShutdownReasons shutdownReasons);
    }
}