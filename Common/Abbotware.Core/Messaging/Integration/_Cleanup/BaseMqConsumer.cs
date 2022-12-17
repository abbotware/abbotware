// -----------------------------------------------------------------------
// <copyright file="BaseMqConsumer.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Base
{
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;

    /// <summary>
    ///     base class used to consume messages
    /// </summary>
    public abstract class BaseMqConsumer : BaseComponent
    {
        private readonly IBasicConsumer consumer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseMqConsumer" /> class.
        /// </summary>
        /// <param name="acknowledger">message acknowledger</param>
        /// <param name="consumer">consumer implementation</param>
        /// <param name="logger">injected logger</param>
        protected BaseMqConsumer(IBasicAcknowledger acknowledger, IBasicConsumer consumer, ILogger logger)
            : base(logger)
        {
            acknowledger = Arguments.EnsureNotNull(acknowledger, nameof(acknowledger));
            consumer = Arguments.EnsureNotNull(consumer, nameof(consumer));

            this.Acknowledger = acknowledger;
            this.consumer = consumer;

            this.consumer.OnDelivery += this.OnDelivery;
        }

        /// <summary>
        ///     Gets manager used for acknowledging the messages of this consumer
        /// </summary>
        protected IBasicAcknowledger Acknowledger { get; }

        /// <summary>
        /// Callback hook for message envelope delivery
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="args">delivery args</param>
        protected abstract void OnDelivery(object? sender, DeliveryEventArgs args);

        /// <inheritdoc/>
        protected override void OnInitialize()
        {
            this.consumer.Initialize();

            base.OnInitialize();
        }

        /// <inheritdoc/>
        protected override void OnDisposeManagedResources()
        {
            this.consumer.Dispose();

            base.OnDisposeManagedResources();
        }
    }
}