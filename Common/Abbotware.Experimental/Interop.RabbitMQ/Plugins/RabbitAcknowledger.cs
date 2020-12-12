// -----------------------------------------------------------------------
// <copyright file="RabbitAcknowledger.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.RabbitMQ.Plugins
{
    using System.Globalization;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging.Amqp.Configuration;
    using Abbotware.Core.Messaging.Amqp.ExtensionPoints;
    using Abbotware.Core.Messaging.Integration.Configuration;
    using Abbotware.Interop.RabbitMQ.ExtensionPoints;
    using global::RabbitMQ.Client;

    /// <summary>
    /// Channel manager used for ack,nack, and reject operations
    /// </summary>
    public class RabbitAcknowledger : BaseChannelManager, IAmqpAcknowledger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitAcknowledger"/> class.
        /// </summary>
        /// <param name="configuration">channel configuration</param>
        /// <param name="rabbitMQChannel">RabbitMQ channel/model</param>
        /// <param name="logger">injected logger</param>
        public RabbitAcknowledger(ChannelConfiguration configuration, IModel rabbitMQChannel, ILogger logger)
            : base(configuration, rabbitMQChannel, logger)
        {
            Arguments.NotNull(configuration, nameof(configuration));
            Arguments.NotNull(rabbitMQChannel, nameof(rabbitMQChannel));
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <inheritdoc />
        public void Ack(string deliveryTag, bool multiple)
        {
            this.InitializeIfRequired();

            lock (this.Mutex)
            {
                this.ThrowIfDisposed();

                var tag = ulong.Parse(deliveryTag, CultureInfo.InvariantCulture);

                this.RabbitMQChannel.BasicAck(tag, multiple);
            }
        }

        /// <inheritdoc />
        public void Ack(IMessageEnvelope envelope)
        {
            this.Ack(envelope, false);
        }

        /// <inheritdoc />
        public void Ack(IMessageEnvelope envelope, bool multiple)
        {
            Arguments.NotNull(envelope, nameof(envelope));

#pragma warning disable CA1062 // Validate arguments of public methods
            this.Ack(envelope.DeliveryProperties.DeliveryTag, multiple);
#pragma warning restore CA1062 // Validate arguments of public methods
        }

        /// <inheritdoc />
        public void Nack(string deliveryTag, bool multiple, bool requeue)
        {
            this.InitializeIfRequired();

            lock (this.Mutex)
            {
                this.ThrowIfDisposed();

                var tag = ulong.Parse(deliveryTag, CultureInfo.InvariantCulture);

                this.RabbitMQChannel.BasicNack(tag, multiple, requeue);
            }
        }

        /// <inheritdoc />
        public void Reject(string deliveryTag, bool requeue)
        {
            this.InitializeIfRequired();

            lock (this.Mutex)
            {
                this.ThrowIfDisposed();

                var tag = ulong.Parse(deliveryTag, CultureInfo.InvariantCulture);

                this.RabbitMQChannel.BasicReject(tag, requeue);
            }
        }
    }
}