// -----------------------------------------------------------------------
// <copyright file="AdvancedRetriever{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.Plugins
{
    using System.Linq;
    using System.Threading.Tasks;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging.Amqp.ExtensionPoints;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Messaging.Integration.Amqp;
    using Abbotware.Core.Messaging.Integration.Configuration;
    using Abbotware.Core.Messaging.Integration.Configuration.Models;
    using Abbotware.Core.Objects;

    /// <summary>
    ///     Retrieves a single message type from a queue
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public class AdvancedRetriever<TMessage> : BaseComponent, IMessageRetriever<TMessage>, IAmqpAcknowledger
    {
        /// <summary>
        ///     protocol to decode the messages
        /// </summary>
        private readonly IAmqpMessageProtocol<TMessage> protocol;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AdvancedRetriever{TMessage}" /> class.
        /// </summary>
        /// <param name="queue">name of the queue to retrieve from</param>
        /// <param name="channel">handle to the connection with retreival features</param>
        /// <param name="protocol">protocol to encode message with</param>
        /// <param name="logger">injected logger</param>
        public AdvancedRetriever(string queue, IAmqpRetriever channel, IAmqpMessageProtocol<TMessage> protocol, ILogger logger)
            : this(queue, false, channel, protocol, logger)
        {
            Arguments.NotNull(queue, nameof(queue));
            Arguments.NotNull(channel, nameof(channel));
            Arguments.NotNull(protocol, nameof(protocol));
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AdvancedRetriever{TMessage}" /> class.
        /// </summary>
        /// <param name="queue">name of the queue to retrieve from</param>
        /// <param name="useNoAck"> flag to indicate whether or not 'NOACK' is enabled</param>
        /// <param name="channel">handle to the connection with retreival features</param>
        /// <param name="protocol">protocol to encode message with</param>
        /// <param name="logger">injected logger</param>
        public AdvancedRetriever(string queue, bool useNoAck, IAmqpRetriever channel, IAmqpMessageProtocol<TMessage> protocol, ILogger logger)
            : base(logger)
        {
            Arguments.NotNull(queue, nameof(queue));
            Arguments.NotNull(channel, nameof(channel));
            Arguments.NotNull(protocol, nameof(protocol));
            Arguments.NotNull(logger, nameof(logger));

            this.UseNoAck = useNoAck;
            this.Queue = queue;
            this.Channel = channel;
            this.protocol = protocol;
        }

        /// <summary>
        ///     Gets the name of the queue used for retrieving
        /// </summary>
        public string Queue
        {
            get;
        }

        /// <summary>
        ///     Gets a value indicating whether or not to use NOACK mode when retrieving messages
        /// </summary>
        public bool UseNoAck
        {
            get;
        }

        /// <summary>
        ///     Gets the handle to the RabbitMQ channel's message retrieval features in derived classes
        /// </summary>
        protected IAmqpRetriever Channel
        {
            get;
        }

        /// <inheritdoc />
        public async Task<IReceived<TMessage>> RetrieveAsync()
        {
            var envelope = await this.Channel.Get(this.Queue, this.UseNoAck).ConfigureAwait(false);

            return new Received<TMessage>(this.protocol.Decode(envelope.Single()), envelope.Single());
        }

        /// <inheritdoc />
        public void Ack(string deliveryTag, bool multiple)
        {
            this.Channel.Ack(deliveryTag, multiple);
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
            this.Channel.Nack(deliveryTag, multiple, requeue);
        }

        /// <inheritdoc />
        public void Reject(string deliveryTag, bool requeue)
        {
            this.Channel.Reject(deliveryTag, requeue);
        }

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            this.Channel?.Dispose();
            base.OnDisposeManagedResources();
        }
    }
}