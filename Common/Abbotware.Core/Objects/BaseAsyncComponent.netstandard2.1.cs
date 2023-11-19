// -----------------------------------------------------------------------
// <copyright file="BaseAsyncComponent.netstandard2.1.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
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
            await this.OnDisposeAsyncCore().ConfigureAwait(false);

            this.Dispose(disposing: false);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Hook to implement custom initialization logic
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task handle</returns>
        protected virtual ValueTask OnInitializeAsync(CancellationToken ct)
#if NETSTANDARD2_1
            => default;
#else
            => ValueTask.CompletedTask;
#endif

        /// <summary>
        ///     Hook to implement custom initialization logic
        /// </summary>
        /// <returns>async task handle</returns>
        protected virtual ValueTask OnDisposeAsyncCore()
#if NETSTANDARD2_1
            => default;
#else
            => ValueTask.CompletedTask;
#endif

        partial void SetInitializeTask(CancellationToken ct)
        {
            this.initializeTask = this.OnInitializeAsync(ct).AsTask();
        }
    }
}