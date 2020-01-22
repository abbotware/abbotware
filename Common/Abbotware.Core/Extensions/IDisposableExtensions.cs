// -----------------------------------------------------------------------
// <copyright file="IDisposableExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
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
    public static class IDisposableExtensions
    {
        /// <summary>
        /// creates a disposable wrapper
        /// </summary>
        /// <remarks>https://stackoverflow.com/questions/21468137/async-network-operations-never-finish/21468138#21468138</remarks>
        /// <param name="disposable">wrapped disposable</param>
        /// <param name="timeSpan">time span to wait before disposing</param>
        /// <returns>disposable handle for using block</returns>
        public static IDisposable CreateTimeoutScope(this IDisposable disposable, TimeSpan timeSpan)
        {
            disposable = Arguments.EnsureNotNull(disposable, nameof(disposable));

            var cancellationTokenSource = new CancellationTokenSource(timeSpan);

            var cancellationTokenRegistration = cancellationTokenSource.Token.Register(disposable.Dispose);

            return new DisposableScope(
                () =>
                {
                    cancellationTokenRegistration.Dispose();
                    cancellationTokenSource.Dispose();
                    disposable.Dispose();
                });
        }

        internal sealed class DisposableScope : IDisposable
        {
            private readonly Action action;

            public DisposableScope(Action closeScopeAction)
            {
                Arguments.NotNull(closeScopeAction, nameof(closeScopeAction));

                this.action = closeScopeAction;
            }

            public void Dispose()
            {
                this.action();
            }
        }
    }
}