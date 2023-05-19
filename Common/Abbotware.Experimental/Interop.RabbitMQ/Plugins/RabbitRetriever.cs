// -----------------------------------------------------------------------
// <copyright file="RabbitRetriever.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.RabbitMQ.Plugins
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging.Amqp.Configuration;
    using Abbotware.Core.Messaging.Amqp.ExtensionPoints;
    using Abbotware.Core.Messaging.Integration.Configuration;
    using global::RabbitMQ.Client;

    /// <summary>
    ///     Channel manager used for single message get operations
    /// </summary>
    public class RabbitRetriever : RabbitAcknowledger, IAmqpRetriever
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RabbitRetriever" /> class.
        /// </summary>
        /// <param name="configuration">channel configuration</param>
        /// <param name="rabbitMQChannel">RabbitMQ channel/model</param>
        /// <param name="logger">injected logger</param>
        public RabbitRetriever(ChannelConfiguration configuration, IModel rabbitMQChannel, ILogger logger)
            : base(configuration, rabbitMQChannel, logger)
        {
            Arguments.NotNull(configuration, nameof(configuration));
            Arguments.NotNull(rabbitMQChannel, nameof(rabbitMQChannel));
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <inheritdoc />
        public Task<IEnumerable<IMessageEnvelope>> Get(string queueName, bool noAck)
        {
            this.InitializeIfRequired();

            lock (this.Mutex)
            {
                this.ThrowIfDisposed();

                var g = this.RabbitMQChannel.BasicGet(queueName, noAck);

                var e = EnvelopeBuilder.Create(g);

                var l = new List<IMessageEnvelope>
                {
                    e,
                };

                return Task.FromResult<IEnumerable<IMessageEnvelope>>(l);
            }
        }
    }
}