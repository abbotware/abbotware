// -----------------------------------------------------------------------
// <copyright file="NoGCScope.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Memory
{
    using System;
    using Abbotware.Core.Objects;

    /// <summary>
    /// Wrapper to encapsulate TryStartNoGCRegion / EndNoGCRegion
    /// </summary>
    public class NoGCScope : BaseComponent
    {
        private readonly bool isInNoGCScope;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoGCScope"/> class.
        /// </summary>
        /// <param name="totalSize">total managed heap size</param>
        public NoGCScope(long totalSize)
            : this(totalSize, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoGCScope"/> class.
        /// </summary>
        /// <param name="totalSize">total managed heap size</param>
        /// <param name="lohSize">total large object heap size</param>
        /// <exception cref="InsufficientMemoryException">unable to enter No GC Scope</exception>
        public NoGCScope(long totalSize, long lohSize)
        {
            this.isInNoGCScope = GC.TryStartNoGCRegion(totalSize, lohSize, true);

            if (!this.isInNoGCScope)
            {
                throw new InsufficientMemoryException("unable to enter No GC Scope");
            }
        }

        /// <inheritdoc/>
        protected override void OnDisposeManagedResources()
        {
            base.OnDisposeManagedResources();

            if (this.isInNoGCScope)
            {
                GC.EndNoGCRegion();
            }
        }
    }
}