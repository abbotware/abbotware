// -----------------------------------------------------------------------
// <copyright file="GraphvizContextSafeHandle.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Graphviz.Api
{
    using System.Runtime.ConstrainedExecution;
    using System.Security;
    using Abbotware.Interop.Graphviz.Native.GvcDll;
    using Microsoft.Win32.SafeHandles;

    /// <summary>
    ///     GraphvizContext SafeHandle
    /// </summary>
    [SecurityCritical]
    public sealed class GraphvizContextSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GraphvizContextSafeHandle" /> class.
        /// </summary>
        public GraphvizContextSafeHandle()
            : this(true)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GraphvizContextSafeHandle" /> class.
        /// </summary>
        /// <param name="owns">true if this Safe Handle owns the underlying handle</param>
        public GraphvizContextSafeHandle(bool owns)
            : base(owns)
        {
        }

        /// <inheritdoc />
        protected override bool ReleaseHandle()
        {
            var result = NativeMethods.gvFreeContext(this.handle);

            if (result == NativeMethods.SUCCESS)
            {
                return true;
            }

            return false;
        }
    }
}