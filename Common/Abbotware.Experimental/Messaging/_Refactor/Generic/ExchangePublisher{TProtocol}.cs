//-----------------------------------------------------------------------
// <copyright file="ExchangePublisher{TProtocol}.cs" company="Abbotware, LLC">
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
    /// publishes messages of different types to an exchange
    /// </summary>
    /// <typeparam name="TProtocol">protocol type for encoding messages</typeparam>
    public class ExchangePublisher<TProtocol> : ExchangePublisher
        where TProtocol : IGenericMessageProtocol, new()
    {
        /// <summary>
        /// Initializes a new instance of the ExchangePublisher class
        /// </summary>
        /// <param name="exchangeName">Name of exchange used for publishing</param>
        /// <param name="channel">rabbitmq connection channel</param>
        /// <param name="logger">injected logger</param>
        public ExchangePublisher(string exchangeName, IPublishManager channel, ILogger logger)
            : this(exchangeName, channel, new TProtocol(), logger)
        {
            Contract.Requires<ArgumentNullException>(exchangeName != null, "exchangeName is null");
            Contract.Requires<ArgumentNullException>(channel != null, "channel is null");
            Contract.Requires<ArgumentNullException>(logger != null, "logger is null");
        }

        /// <summary>
        /// Initializes a new instance of the ExchangePublisher class
        /// </summary>
        /// <param name="exchangeName">Name of exchange used for publishing</param>
        /// <param name="channel">rabbitmq connection channel</param>
        /// <param name="protocol">protocol for encoding the messages</param>
        /// <param name="logger">injected logger</param>
        public ExchangePublisher(string exchangeName, IPublishManager channel, IGenericMessageProtocol protocol, ILogger logger)
            : base(exchangeName, channel, protocol, logger)
        {
            Contract.Requires<ArgumentNullException>(exchangeName != null, "exchangeName is null");
            Contract.Requires<ArgumentNullException>(channel != null, "channel is null");
            Contract.Requires<ArgumentNullException>(protocol != null, "protocol is null");
            Contract.Requires<ArgumentNullException>(logger != null, "logger is null");
        }
    }
}