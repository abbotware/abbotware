//-----------------------------------------------------------------------
// <copyright file="QueueRetriever{TProtocol}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2013.  All rights reserved.
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.Interop.RabbitMQ.Patterns
{
    
    using Abbotware.Core.Messaging;
    using Abbotware.Core.Services;
    using Abbotware.Messaging.Interop.RabbitMQ;

    /// <summary>
    /// Retrieves different message object types from a queue
    /// </summary>
    /// <typeparam name="TProtocol">protocol type for decoding messages</typeparam>
    public class QueueRetriever<TProtocol> : QueueRetriever
        where TProtocol : IGenericMessageProtocol, new()
    {
        /// <summary>
        /// Initializes a new instance of the QueueRetriever class
        /// </summary>
        /// <param name="queue">name of the queue to retrieve from</param>
        /// <param name="channel">RabbitMQ connection channel</param>
        /// <param name="logger">injected logger</param>
        public QueueRetriever(string queue, IMessageRetrievalManager channel, ILogger logger)
            : this(queue, false, channel, new TProtocol(), logger)
        {
            Contract.Requires(queue != null);
            Contract.Requires(channel != null);
            Contract.Requires(logger != null);
        }

        /// <summary>
        /// Initializes a new instance of the QueueRetriever class
        /// </summary>
        /// <param name="queue">name of the queue to retrieve from</param>
        /// <param name="channel">RabbitMQ connection channel</param>
        /// <param name="protocol">protocol used to decode the messages</param>
        /// <param name="logger">injected logger</param>
        public QueueRetriever(string queue, IMessageRetrievalManager channel, TProtocol protocol, ILogger logger)
            : this(queue, false, channel, protocol, logger)
        {
            Contract.Requires(queue != null);
            Contract.Requires(channel != null);
            Contract.Requires(protocol != null);
            Contract.Requires(logger != null);
        }

        /// <summary>
        /// Initializes a new instance of the QueueRetriever class
        /// </summary>
        /// <param name="queue">name of the queue to retrieve from</param>
        /// <param name="useNoAck">flag to indicate whether or not 'NOACK' is enabled</param>
        /// <param name="channel">RabbitMQ connection channel</param>
        /// <param name="logger">injected logger</param>
        public QueueRetriever(string queue, bool useNoAck, IMessageRetrievalManager channel, ILogger logger)
            : this(queue, useNoAck, channel, new TProtocol(), logger)
        {
            Contract.Requires(queue != null);
            Contract.Requires(channel != null);
            Contract.Requires(logger != null);
        }

        /// <summary>
        /// Initializes a new instance of the QueueRetriever class
        /// </summary>
        /// <param name="queue">name of the queue to retrieve from</param>
        /// <param name="noAck"> flag to indicate whether or not 'NOACK' is enabled</param>
        /// <param name="channel">RabbitMQ connection channel</param>
        /// <param name="protocol">protocol used to decode the messages</param>
        /// <param name="logger">injected logger</param>
        public QueueRetriever(string queue, bool noAck, IMessageRetrievalManager channel, TProtocol protocol, ILogger logger)
            : base(queue, noAck, channel, protocol, logger)
        {
            Contract.Requires(queue != null);
            Contract.Requires(channel != null);
            Contract.Requires(protocol != null);
            Contract.Requires(logger != null);
        }
    }
}