// -----------------------------------------------------------------------
// <copyright file="BaseAmqpProtocol{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Amqp
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Messaging.Integration.Amqp.Configuration;
    using Abbotware.Core.Messaging.Integration.Amqp.Configuration.Models;
    using Abbotware.Core.Messaging.Integration.Configuration;
    using Abbotware.Core.Messaging.Integration.Configuration.Models;

    /// <summary>
    ///     base class to use when creating a single message type protocol
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public abstract class BaseAmqpProtocol<TMessage> : IAmqpMessageProtocol<TMessage>
    {
        /// <summary>
        ///  defaults for ampq protocol
        /// </summary>
        private readonly IAmqpProtocolDefaults defaults;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseAmqpProtocol{TMessage}" /> class.
        /// </summary>
        /// <param name="serializer">binary serializer for converting messages to bytes</param>
        protected BaseAmqpProtocol(IBidirectionalConverter<TMessage, ReadOnlyMemory<byte>> serializer)
            : this(serializer, new AmqpProtocolDefaults())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseAmqpProtocol{TMessage}" /> class.
        /// </summary>
        /// <param name="serializer">binary serializer for converting messages to bytes</param>
        /// <param name="defaults">override for protocol defaults</param>
        protected BaseAmqpProtocol(IBidirectionalConverter<TMessage, ReadOnlyMemory<byte>> serializer, IAmqpProtocolDefaults defaults)
        {
            Arguments.NotNull(serializer, nameof(serializer));
            Arguments.NotNull(defaults, nameof(defaults));

            this.Serializer = serializer;
            this.defaults = defaults;
        }

        /// <summary>
        ///     Gets the serializer
        /// </summary>
        protected IBidirectionalConverter<TMessage, ReadOnlyMemory<byte>> Serializer { get; }

        /// <inheritdoc />
        public IMessageEnvelope Encode(TMessage message)
        {
            return this.Encode(message, this.OnComputeExchange(message));
        }

        /// <inheritdoc />
        public IMessageEnvelope Encode(TMessage message, string destination)
        {
            return this.Encode(message, destination, this.OnComputeTopic(message, destination));
        }

        /// <inheritdoc />
        public IMessageEnvelope Encode(TMessage message, string exchange, string topic)
        {
            return this.Encode(message, exchange, topic, this.OnComputeMandatory(message, exchange, topic));
        }

        /// <inheritdoc />
        public virtual IMessageEnvelope Encode(TMessage message, string exchange, string topic, bool mandatory)
        {
            var p = new PublishProperties
            {
                Exchange = exchange,
                Mandatory = mandatory,
                RoutingKey = topic,
            };

            return this.Encode(message, p);
        }

        /// <inheritdoc />
        public virtual IMessageEnvelope Encode(TMessage message, IPublishProperties properties)
        {
            var payload = this.Serializer.Convert(message);

            var envelope = new MessageEnvelope(properties)
            {
                Body = payload,
            };

            this.OnMessageEnvelope(message, envelope);

            return envelope;
        }

        /// <inheritdoc />
        public virtual TMessage Decode(IMessageEnvelope envelope)
        {
            envelope = Arguments.EnsureNotNull(envelope, nameof(envelope));

            return this.Serializer.Convert(envelope.Body);
        }

        /// <summary>
        /// Callback for computing the exchange
        /// </summary>
        /// <param name="message">message</param>
        /// <returns>exchange name</returns>
        protected virtual string OnComputeExchange(TMessage message)
        {
            return this.defaults.Exchange;
        }

        /// <summary>
        /// Callback for computing message topic
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="exchange">exchange</param>
        /// <returns>routing topic</returns>
        protected virtual string OnComputeTopic(TMessage message, string exchange)
        {
            return this.defaults.Topic;
        }

        /// <summary>
        /// Callback for computing mandatory flag
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="exchange">exchange</param>
        /// <param name="topic">topic</param>
        /// <returns>mandatory flag</returns>
        protected virtual bool OnComputeMandatory(TMessage message, string exchange, string topic)
        {
            return this.defaults.Mandatory;
        }

        /// <summary>
        /// Callback for inpsecting the message envelope
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="envelope">message envelope</param>
        protected virtual void OnMessageEnvelope(TMessage message, MessageEnvelope envelope)
        {
            return;
        }
    }
}