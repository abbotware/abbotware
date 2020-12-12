//-----------------------------------------------------------------------
// <copyright file="QueueRetriever.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2013.  All rights reserved.
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.Interop.RabbitMQ.Patterns
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    
    using Abbotware.Core.Messaging;
    using Abbotware.Core.Services;
    using Abbotware.Messaging.Interop.RabbitMQ;

    /// <summary>
    /// Retrieves messages from a queue
    /// </summary>
    public class QueueRetriever : BaseQueueRetriever
    {
        /// <summary>
        /// protocol used for decoding the messages
        /// </summary>
        protected readonly IGenericMessageProtocol Protocol;

        /// <summary>
        /// Initializes a new instance of the QueueRetriever class
        /// </summary>
        /// <param name="queue">name of the queue to retrieve from</param>
        /// <param name="channel">RabbitMQ connection channel</param>
        /// <param name="protocol">protocol used for decoding the messages</param>
        /// <param name="logger">injected logger</param>
        public QueueRetriever(string queue, IMessageRetrievalManager channel, IGenericMessageProtocol protocol, ILogger logger)
            : this(queue, false, channel, protocol, logger)
        {
            Contract.Requires<ArgumentNullException>(queue != null, "queue is null");
            Contract.Requires<ArgumentNullException>(channel != null, "channel is null");
            Contract.Requires<ArgumentNullException>(protocol != null, "protocol is null");
            Contract.Requires<ArgumentNullException>(logger != null, "logger is null");
        }

        /// <summary>
        /// Initializes a new instance of the QueueRetriever class
        /// </summary>
        /// <param name="queue">name of the queue to retrieve from</param>
        /// <param name="noAck"> flag to indicate whether or not 'NOACK' is enabled</param>
        /// <param name="channel">RabbitMQ connection channel</param>
        /// <param name="protocol">protocol used for decoding the messages</param>
        /// <param name="logger">injected logger</param>
        public QueueRetriever(string queue, bool noAck, IMessageRetrievalManager channel, IGenericMessageProtocol protocol, ILogger logger)
            : base(queue, noAck, channel, logger)
        {
            Contract.Requires<ArgumentNullException>(queue != null, "queue is null");
            Contract.Requires<ArgumentNullException>(channel != null, "channel is null");
            Contract.Requires<ArgumentNullException>(protocol != null, "protocol is null");
            Contract.Requires<ArgumentNullException>(logger != null, "logger is null");

            this.Protocol = protocol;
        }

        /// <summary>
        /// Retrieves a message with the specified type from the queue
        /// </summary>
        /// <typeparam name="TMessage">message type</typeparam>
        /// <returns>received message</returns>
        public ReceivedMessage<TMessage> Retrieve<TMessage>()
        {
            Contract.Ensures(Contract.Result<ReceivedMessage<TMessage>>() != null);

            var getResult = this.Channel.BasicGet(this.QueueName, this.UseNoAck);

            Contract.Assume(getResult.Exchange != null);
            Contract.Assume(getResult.RoutingKey != null);
            Contract.Assume(getResult.BasicProperties != null);
            Contract.Assume(getResult.Body != null);

            return new ReceivedMessage<TMessage>(this.Protocol.Decode<TMessage>(getResult.Body, getResult.Exchange, getResult.RoutingKey, getResult.BasicProperties), getResult.DeliveryTag);
        }

        /// <summary>
        /// Retrieves a message of unknown type from the queue
        /// </summary>
        /// <returns>received message</returns>
        public ReceivedMessage<object> Retrieve()
        {
            Contract.Ensures(Contract.Result<ReceivedMessage<object>>() != null);

            var getResult = this.Channel.BasicGet(this.QueueName, this.UseNoAck);

            Contract.Assume(getResult.Exchange != null);
            Contract.Assume(getResult.RoutingKey != null);
            Contract.Assume(getResult.BasicProperties != null);
            Contract.Assume(getResult.Body != null);

            return new ReceivedMessage<object>(this.Protocol.Decode(getResult.Body, getResult.Exchange, getResult.RoutingKey, getResult.BasicProperties), getResult.DeliveryTag);
        }
    }
}