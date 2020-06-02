// -----------------------------------------------------------------------
// <copyright file="BaseAsyncComponent.netstandard2.0.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     Abstract base class for writing component-like objects
    /// </summary>
    public partial class BaseAsyncComponent
    {
        /// <summary>
        ///     Hook to implement custom initialization logic
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task handle</returns>
        protected virtual Task OnInitializeAsync(CancellationToken ct)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        protected override void OnDisposeManagedResources()
        {
            this.DisposeRequested.Cancel();

            base.OnDisposeManagedResources();
        }

        partial void SetInitializeTask(CancellationToken ct)
        {
            this.initializeTask = this.OnInitializeAsync(ct);
        }
    }
}