//-----------------------------------------------------------------------
// <copyright file="PocoRetriever.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2013.  All rights reserved.
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.Interop.RabbitMQ.Patterns
{
    using System;
    
    using Abbotware.Core.Services;
    using Abbotware.Messaging.Interop.RabbitMQ;

    /// <summary>
    /// retrieves a single message type using a specified protocol from a queue
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    /// <typeparam name="TProtocol">protocol type</typeparam>
    public class PocoRetriever<TMessage, TProtocol> : SingleMessageTypeRetriever<TMessage>
        where TProtocol : ISingleMessageProtocol<TMessage>, new()
    {
        /// <summary>
        /// Initializes a new instance of the PocoRetriever class
        /// </summary>
        /// <param name="queue">name of the queue to retrieve from</param>
        /// <param name="useNoAck"> flag to indicate whether or not 'NOACK' is enabled</param>
        /// <param name="channel">RabbitMQ connection channel</param>
        /// <param name="logger">injected logger</param>
        public PocoRetriever(string queue, bool useNoAck, IMessageRetrievalManager channel, ILogger logger)
            : this(queue, useNoAck, channel, new TProtocol(), logger)
        {
            Contract.Requires<ArgumentNullException>(queue != null, "queue is null");
            Contract.Requires<ArgumentNullException>(channel != null, "channel is null");
            Contract.Requires<ArgumentNullException>(logger != null, "logger is null");
        }

        /// <summary>
        /// Initializes a new instance of the PocoRetriever class
        /// </summary>
        /// <param name="queue">name of the queue to retrieve from</param>
        /// <param name="useNoAck"> flag to indicate whether or not 'NOACK' is enabled</param>
        /// <param name="channel">RabbitMQ connection channel</param>
        /// <param name="protocol">protocol to decode messages</param>
        /// <param name="logger">injected logger</param>
        public PocoRetriever(string queue, bool useNoAck, IMessageRetrievalManager channel, ISingleMessageProtocol<TMessage> protocol, ILogger logger)
            : base(queue, useNoAck, channel, protocol, logger)
        {
            Contract.Requires<ArgumentNullException>(queue != null, "queue is null");
            Contract.Requires<ArgumentNullException>(channel != null, "channel is null");
            Contract.Requires<ArgumentNullException>(protocol != null, "protocol is null");
            Contract.Requires<ArgumentNullException>(logger != null, "logger is null");
        }
    }
}