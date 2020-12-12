//-----------------------------------------------------------------------
// <copyright file="ExchangePublisher.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2013.  All rights reserved.
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.Interop.RabbitMQ.Patterns
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Abbotware.Core.Messaging;
    using Abbotware.Core.Services;
    using Abbotware.Messaging.Interop.RabbitMQ;

    /// <summary>
    /// publishes messages of different types to an exchange
    /// </summary>
    public class ExchangePublisher : BaseExchangePublisher, IMessagePublisher
    {
        /// <summary>
        /// protocol for encoding the messages
        /// </summary>
        private readonly IGenericMessageProtocol protocol;

        /// <summary>
        /// Initializes a new instance of the ExchangePublisher class
        /// </summary>
        /// <param name="exchangeName">Name of exchange used for publishing</param>
        /// <param name="channel">rabbitmq connection channel</param>
        /// <param name="protocol">protocol for encoding the messages</param>
        /// <param name="logger">injected logger</param>
        public ExchangePublisher(string exchangeName, IPublishManager channel, IGenericMessageProtocol protocol, ILogger logger)
            : base(exchangeName, channel, logger)
        {
            Contract.Requires<ArgumentNullException>(exchangeName != null, "exchangeName is null");
            Contract.Requires<ArgumentNullException>(channel != null, "channel is null");
            Contract.Requires<ArgumentNullException>(protocol != null, "protocol is null");
            Contract.Requires<ArgumentNullException>(logger != null, "logger is null");

            this.protocol = protocol;
        }

        /// <inheritdoc/>
        public Task<PublishStatus> Publish<T>(T message)
        {
            return this.Channel.Publish(this.protocol.Encode(message, this.ExchangeName));
        }

        /// <inheritdoc/>
        public Task<PublishStatus> Publish<T>(T message, string topic)
        {
            return this.Channel.Publish(this.protocol.Encode(message, this.ExchangeName, topic));
        }

        /// <inheritdoc/>
        public Task<PublishStatus> Publish<T>(T message, string topic, bool immediate, bool mandatory)
        {
            return this.Channel.Publish(this.protocol.Encode(message, this.ExchangeName, topic, immediate, mandatory));
        }
    }
}