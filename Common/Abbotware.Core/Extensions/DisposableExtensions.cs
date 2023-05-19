// -----------------------------------------------------------------------
// <copyright file="DisposableExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Extensions
{
    using System;
    using System.Threading;

    /// <summary>
    ///     IDisposable Extensions methods
    /// </summary>
    public static class DisposableExtensions
    {
        /// <summary>
        /// creates a disposable wrapper - This has only 1 use case - NonCancellable Async operations
        /// </summary>
        /// <remarks>https://stackoverflow.com/questions/21468137/async-network-operations-never-finish/21468138#21468138</remarks>
        /// <param name="asyncOperation">wrapped asyncOperation / disposable</param>
        /// <param name="timeSpan">time span to wait before disposing</param>
        /// <returns>disposable handle for using block</returns>
        public static IDisposable DisposeAsyncOperationAfterTimeout(this IDisposable asyncOperation, TimeSpan timeSpan)
        {
            asyncOperation = Arguments.EnsureNotNull(asyncOperation, nameof(asyncOperation));

            return new DisposableAsyncOperation(timeSpan, asyncOperation);
        }

        internal sealed class DisposableAsyncOperation : IDisposable
        {
            private readonly CancellationTokenSource cts;

            private readonly CancellationTokenRegistration ctr;

            public DisposableAsyncOperation(TimeSpan timeSpan, IDisposable asyncOperation)
            {
                asyncOperation = Arguments.EnsureNotNull(asyncOperation, nameof(asyncOperation));

                this.cts = new CancellationTokenSource(timeSpan);
                this.ctr = this.cts.Token.Register(asyncOperation.Dispose);
            }

            public void Dispose()
            {
                this.ctr.Dispose();
                this.cts.Dispose();
            }
        }
    }
}