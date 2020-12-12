// -----------------------------------------------------------------------
// <copyright file="GraphSafeHandle.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Graphviz.Api
{
    using System.Runtime.ConstrainedExecution;
    using System.Security;
    using Abbotware.Interop.Graphviz.GraphDll;
    using Microsoft.Win32.SafeHandles;

    /// <summary>
    ///     Graph Object SafeHandle
    /// </summary>
    [SecurityCritical]
    public sealed class GraphSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GraphSafeHandle" /> class.
        /// </summary>
        public GraphSafeHandle()
            : this(true)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GraphSafeHandle" /> class.
        /// </summary>
        /// <param name="owns">true if this Safe Handle owns the underlying handle</param>
        public GraphSafeHandle(bool owns)
            : base(owns)
        {
        }

        /// <inheritdoc />
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        protected override bool ReleaseHandle()
        {
            NativeMethods.agclose(this.handle);
            return true;
        }
    }
}