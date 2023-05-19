// -----------------------------------------------------------------------
// <copyright file="ConsumerManager.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.RabbitMQ.Plugins
{
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging.Amqp.Configuration;
    using Abbotware.Interop.RabbitMQ.ExtensionPoints.Services;
    using global::RabbitMQ.Client;

    /// <summary>
    /// Channel manager used for consumer operations
    /// </summary>
    public class ConsumerManager : RabbitAcknowledger, IAmqpConsumerManager<IBasicConsumer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumerManager"/> class.
        /// </summary>
        /// <param name="configuration">channel configuration</param>
        /// <param name="rabbitMQChannel">RabbitMQ channel/model</param>
        /// <param name="logger">injected logger</param>
        public ConsumerManager(ChannelConfiguration configuration, IModel rabbitMQChannel, ILogger logger)
            : base(configuration, rabbitMQChannel, logger)
        {
            Arguments.NotNull(configuration, nameof(configuration));
            Arguments.NotNull(rabbitMQChannel, nameof(rabbitMQChannel));
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <inheritdoc />
        public string Start(string queueName, bool requireAcknowledgements, IBasicConsumer consumer)
        {
            this.InitializeIfRequired();

            lock (this.Mutex)
            {
                this.ThrowIfDisposed();

                var consumerName = this.RabbitMQChannel.BasicConsume(queueName, !requireAcknowledgements, string.Empty, false, false, null, consumer);

                return consumerName;
            }
        }

        /// <inheritdoc />
        public void Cancel(string consumerTag)
        {
            lock (this.Mutex)
            {
                this.ThrowIfDisposed();

                this.RabbitMQChannel.BasicCancel(consumerTag);
            }
        }
    }
}