// -----------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Windows.Advapi32
{
    using System.Runtime.InteropServices;

    /// <summary>
    ///     Dll Import Wrapper for the advapi32.dll
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        ///     Name of the unmanaged advapi32 dll
        /// </summary>
        public const string LIB_ADVAPI32 = "advapi32.dll";

        /// <summary>
        ///     Dll Import for Win32 LogonUser function
        /// </summary>
        /// <param name="username">username to authenticate with</param>
        /// <param name="domain">domain to authenticate with</param>
        /// <param name="password">plaintext password to authenticate with</param>
        /// <param name="logonType">Win32 Logon Type to use</param>
        /// <param name="logonProvider">Win32 Logon Provider to use</param>
        /// <param name="token">Win32 HANDLE to the AccessControl Token</param>
        /// <returns>true/false if successful</returns>
        [DllImport(LIB_ADVAPI32, CallingConvention = CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool LogonUser(
            [MarshalAs(UnmanagedType.LPStr)] string username,
            [MarshalAs(UnmanagedType.LPStr)] string domain,
            [MarshalAs(UnmanagedType.LPStr)] string password,
            LogOnType logonType,
            LogOnProviderType logonProvider,
            out AccessControlToken token);
    }
}