// -----------------------------------------------------------------------
// <copyright file="Advapi32Methods.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Windows.Advapi32
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;

    /// <summary>
    ///     Advapi32 Win32 Wrapper class
    /// </summary>
    public static class Advapi32Methods
    {
        /// <summary>
        ///     C# Wrapper for Win32 LogonUser call.  Can be used to impersonate a local or remote user.  This is safe to call
        /// </summary>
        /// <param name="userName">username to authenticate with</param>
        /// <param name="domain">domain to authenticate with</param>
        /// <param name="password">plaintext password to authenticate with</param>
        /// <param name="logOnType">Win32 Logon Type to use</param>
        /// <param name="logOnProvider">Win32 Logon Provider to use</param>
        /// <returns>LogonToken wrapped in a SafeHandle</returns>
        public static AccessControlToken LogonUser(
            string userName,
            string domain,
            string password,
            LogOnType logOnType,
            LogOnProviderType logOnProvider)
        {
            Arguments.NotNullOrWhitespace(userName, nameof(userName));
            Arguments.NotNullOrWhitespace(password, nameof(password));

            if (!NativeMethods.LogonUser(userName, domain, password, logOnType, logOnProvider, out var token))
            {
                var errorInfo = Marshal.GetLastWin32Error();

                if (token != null)
                {
                    if (token.IsInvalid)
                    {
                        token.Dispose();
                    }
                }

                throw new Win32Exception(errorInfo, "Logon User failed");
            }

            return token;
        }
    }
}