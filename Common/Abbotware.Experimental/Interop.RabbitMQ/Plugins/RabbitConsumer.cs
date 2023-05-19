// -----------------------------------------------------------------------
// <copyright file="RabbitConsumer.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.RabbitMQ.Plugins
{
    using System;
    using System.Globalization;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging.Configuration;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.RabbitMQ.ExtensionPoints.Services;
    using global::RabbitMQ.Client;
    using global::RabbitMQ.Client.Events;

    /// <summary>
    ///     Base class for consuming messages from a RabbitMQ Channel
    /// </summary>
    public class RabbitConsumer : BaseComponent<IConsumerConfiguration>, Core.Messaging.Integration.IBasicConsumer, global::RabbitMQ.Client.IBasicConsumer
    {
        private readonly IAmqpConsumerManager<global::RabbitMQ.Client.IBasicConsumer> consumerManager;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RabbitConsumer" /> class.
        /// </summary>
        /// <param name="consumerManager">interface that can manager a consumer</param>
        /// <param name="configuration">configuratiom</param>
        /// <param name="logger">injected logger</param>
        protected RabbitConsumer(IAmqpConsumerManager<global::RabbitMQ.Client.IBasicConsumer> consumerManager, IConsumerConfiguration configuration, ILogger logger)
            : base(configuration, logger)
        {
            Arguments.NotNull(consumerManager, nameof(consumerManager));
            Arguments.NotNull(configuration, nameof(configuration));
            Arguments.NotNull(logger, nameof(logger));

            this.Status = ConsumerStatus.Unknown;
            this.consumerManager = consumerManager;
        }

        /// <summary>
        ///     Event signaling consumer was cancelled
        /// </summary>
        public event EventHandler<ConsumerEventArgs>? ConsumerCancelled;

        /// <summary>
        ///     Event signaling message is delievered
        /// </summary>
        public event EventHandler<DeliveryEventArgs>? OnDelivery;

        /// <summary>
        ///     Gets the current state of the Consumer
        /// </summary>
        public ConsumerStatus Status { get; private set; }

        /// <summary>
        ///     Gets the AMQP ConsumerTag for this Consumer
        /// </summary>
        public string? ConsumerTag { get; private set; }

        /// <summary>
        ///     Gets the shutdown reason if the Consumer was stopped
        /// </summary>
        public ShutdownEventArgs? ShutdownReason { get; private set; }

        /// <inheritdoc />
        public IModel Model
        {
            get { throw new InvalidOperationException("MODEL hopefully this is never called"); }
        }

        /// <inheritdoc />
        public uint Delivered { get; private set; }

        /// <inheritdoc />
        public void HandleBasicCancel(string consumerTag)
        {
            this.Status = ConsumerStatus.CancelRequested;
            this.LogStatus();

            this.ConsumerCancelled?.Invoke(null, new ConsumerEventArgs(new string[] { consumerTag }));
        }

        /// <inheritdoc />
        public void HandleBasicCancelOk(string consumerTag)
        {
            this.Status = ConsumerStatus.Canceled;
            this.LogStatus();
        }

        /// <inheritdoc />
        public void HandleBasicConsumeOk(string consumerTag)
        {
            this.ConsumerTag = consumerTag;
            this.Status = ConsumerStatus.Running;
            this.LogStatus();
        }

        /// <inheritdoc />
        public void HandleModelShutdown(object model, ShutdownEventArgs reason)
        {
            this.ShutdownReason = reason;
            this.Status = ConsumerStatus.Shutdown;
            this.LogStatus();
        }

        /// <inheritdoc />
        public void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            var e = EnvelopeBuilder.Create(deliveryTag.ToString(CultureInfo.InvariantCulture), redelivered, exchange, routingKey, properties, body.ToArray(), consumerTag);

            this.OnDelivery?.Invoke(this, new DeliveryEventArgs(e));

            ++this.Delivered;
        }

        /// <inheritdoc />
        protected override void OnInitialize()
        {
            base.OnInitialize();

            this.ConsumerTag = this.consumerManager.Start(this.Configuration.Queue, this.Configuration.RequiresAcks, this);
        }

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            this.consumerManager.Cancel(this.ConsumerTag!);
        }

        /// <summary>
        ///     helper method to log the consumer status
        /// </summary>
        private void LogStatus()
        {
            this.Logger.Info("Consumer:{0} State:{1}", this.ConsumerTag, this.Status);
        }
    }
}