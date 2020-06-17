// -----------------------------------------------------------------------
// <copyright file="BaseAsyncComponent.netstandard2.1.cs" company="Abbotware, LLC">
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
    public partial class BaseAsyncComponent : IAsyncDisposable
    {
        /// <inheritdoc/>
        public async ValueTask DisposeAsync()
        {
            this.DisposeRequested.Cancel();

            using var cts = new CancellationTokenSource();

            try
            {
                await this.OnDisposeAsync(cts.Token)
                    .ConfigureAwait(true);
            }
            finally
            {
                cts.Cancel();
            }
        }

             /// <summary>
        ///     Hook to implement custom initialization logic
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task handle</returns>
        protected virtual ValueTask OnInitializeAsync(CancellationToken ct)
        {
            return default;
        }

        /// <summary>
        ///     Hook to implement custom initialization logic
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task handle</returns>
        protected virtual ValueTask OnDisposeAsync(CancellationToken ct)
        {
            return default;
        }

        /// <inheritdoc/>
        protected override sealed void OnDisposeManagedResources()
        {
            // this just wires up a Dispose to DisposeAsync
            this.DisposeAsync().AsTask().GetAwaiter().GetResult();
        }

        partial void SetInitializeTask(CancellationToken ct)
        {
            this.initializeTask = this.OnInitializeAsync(ct).AsTask();
        }
    }
}