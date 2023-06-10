// -----------------------------------------------------------------------
// <copyright file="StartablePollingComponent.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Castle.ExtensionPoints
{
    using System;
    using System.Reactive.Linq;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Logging;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     base class for an application component that runs on a polling schedule
    /// </summary>
    public abstract class StartablePollingComponent : BaseStartableComponent
    {
        /// <summary>
        ///     the observable
        /// </summary>
        private readonly IObservable<long> subscription;

        /// <summary>
        ///     timespan between polling intervals
        /// </summary>
        private readonly TimeSpan pollingTimeSpan;

        /// <summary>
        ///     the handle to the the polling subscription
        /// </summary>
        private IDisposable? subscriptionHandle;

        /// <summary>
        ///     Initializes a new instance of the <see cref="StartablePollingComponent" /> class.
        /// </summary>
        /// <param name="pollingTimeSpan">time span to use for polling intervals</param>
        /// <param name="logger">injected logger</param>
        protected StartablePollingComponent(TimeSpan pollingTimeSpan, ILogger logger)
            : base(logger)
        {
            Arguments.NotValue(pollingTimeSpan, TimeSpan.Zero, nameof(pollingTimeSpan));

            this.pollingTimeSpan = pollingTimeSpan;

            this.subscription = Observable.Interval(pollingTimeSpan);
        }

        /// <inheritdoc />
        protected override void OnStart()
        {
            this.subscriptionHandle = this.subscription
                .Subscribe(this.OnNext, this.OnError, this.OnComplete);

            this.Logger.Debug($"started with polling interval:{this.pollingTimeSpan}");
        }

        /// <inheritdoc />
        protected override void OnStop()
        {
            this.subscriptionHandle?.Dispose();

            this.subscriptionHandle = null;
        }

        /// <summary>
        ///     Hook to implement custom polling interval logic
        /// </summary>
        /// <param name="count">event count number</param>
        protected abstract void OnNext(long count);

        /// <summary>
        ///     Hook to implement custom error handling for subscription
        /// </summary>
        /// <param name="exception">exception thrown during subscription</param>
        protected virtual void OnError(Exception exception)
        {
            Arguments.NotNull(exception, nameof(exception));

            this.Logger.Error(exception, "OnError");
        }

        /// <summary>
        ///     Hook to implement custom completion handling for subscription
        /// </summary>
        protected virtual void OnComplete()
        {
            this.Logger.Info("OnComplete");
        }

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            base.OnDisposeManagedResources();

            this.subscriptionHandle?.Dispose();
        }
    }
}