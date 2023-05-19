// -----------------------------------------------------------------------
// <copyright file="AccessControlToken.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Windows
{
    using System.Runtime.ConstrainedExecution;
    using System.Security;
    using Abbotware.Interop.Windows.Kernel32;
    using Microsoft.Win32.SafeHandles;

    /// <summary>
    ///     AccessControlToken wraps up a Win32 PHANDLE as a SafeHandle
    /// </summary>
    [SecurityCritical]
    public class AccessControlToken : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AccessControlToken" /> class.
        /// </summary>
        public AccessControlToken()
            : this(true)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AccessControlToken" /> class.
        /// </summary>
        /// <param name="owns">true if this Safe Handle owns the underlying handle</param>
        public AccessControlToken(bool owns)
            : base(owns)
        {
        }

        /// <summary>
        ///     Method to release the handle
        /// </summary>
        /// <returns>true/false on handle release success</returns>
        protected override bool ReleaseHandle()
        {
            return NativeMethods.CloseHandle(this.handle);
        }
    }
}