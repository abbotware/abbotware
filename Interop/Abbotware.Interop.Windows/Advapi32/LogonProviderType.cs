// -----------------------------------------------------------------------
// <copyright file="LogonProviderType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Windows.Advapi32
{
    /// <summary>
    ///     Win32 enumeration for Logon Provider Type
    /// </summary>
    public enum LogOnProviderType : int
    {
        /// <summary>
        ///     Use the standard logon provider for the system.
        ///     The default security provider is negotiate, unless you pass NULL for the domain name and the user name
        ///     is not in UPN format. In this case, the default provider is NTLM.
        ///     NOTE: Windows 2000/NT:   The default security provider is NTLM.
        /// </summary>
        Default = 0,
    }
}