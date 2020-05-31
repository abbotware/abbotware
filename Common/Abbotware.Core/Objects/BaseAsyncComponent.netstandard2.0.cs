// -----------------------------------------------------------------------
// <copyright file="BaseAsyncComponent.netstandard2.0.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    /// <summary>
    ///     Abstract base class for writing component-like objects
    /// </summary>
    public partial class BaseAsyncComponent
    {
        /// <inheritdoc/>
        protected override void OnDisposeManagedResources()
        {
            this.DisposeRequested.Cancel();

            base.OnDisposeManagedResources();
        }
    }
}