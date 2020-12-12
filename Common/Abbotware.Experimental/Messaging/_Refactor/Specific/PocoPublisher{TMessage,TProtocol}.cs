//-----------------------------------------------------------------------
// <copyright file="PocoPublisher.cs" company="Abbotware, LLC">
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
    /// publishes a single message type using a specified protocol to an exchange
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    /// <typeparam name="TProtocol">protocol type</typeparam>
    public class PocoPublisher<TMessage, TProtocol> : SingleMessageTypePublisher<TMessage>
        where TProtocol : ISingleMessageProtocol<TMessage>, new()
    {
        /// <summary>
        /// Initializes a new instance of the PocoPublisher class
        /// </summary>
        /// <param name="exchange">exchange to publish to</param>
        /// <param name="channel">rabbitmq connection channel</param>
        /// <param name="logger">injected logger</param>
        public PocoPublisher(string exchange, IPublishManager channel, ILogger logger)
            : this(exchange, channel, new TProtocol(), logger)
        {
            Contract.Requires<ArgumentNullException>(exchange != null, "exchange is null");
            Contract.Requires<ArgumentNullException>(channel != null, "channel is null");
            Contract.Requires<ArgumentNullException>(logger != null, "logger is null");
        }

        /// <summary>
        /// Initializes a new instance of the PocoPublisher class
        /// </summary>
        /// <param name="exchange">exchange to publish to</param>
        /// <param name="channel">rabbitmq connection channel</param>
        /// <param name="protocol">protocol to encode the message</param>
        /// <param name="logger">injected logger</param>
        public PocoPublisher(string exchange, IPublishManager channel, ISingleMessageProtocol<TMessage> protocol, ILogger logger)
            : base(exchange, channel, protocol, logger)
        {
            Contract.Requires<ArgumentNullException>(exchange != null, "exchange is null");
            Contract.Requires<ArgumentNullException>(channel != null, "channel is null");
            Contract.Requires<ArgumentNullException>(protocol != null, "protocol is null");
            Contract.Requires<ArgumentNullException>(logger != null, "logger is null");
        }
    }
}