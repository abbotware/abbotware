// -----------------------------------------------------------------------
// <copyright file="BaseAmqpProtocol.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Amqp
{
    using System;
    using System.ComponentModel;
    using Abbotware.Core;
    using Abbotware.Core.Exceptions;
    using Abbotware.Core.Messaging.Integration.Amqp.Configuration;
    using Abbotware.Core.Messaging.Integration.Amqp.Configuration.Models;
    using Abbotware.Core.Messaging.Integration.Configuration;
    using Abbotware.Core.Messaging.Integration.Configuration.Models;
    using Abbotware.Core.Messaging.Integration.Plugins;
    using Abbotware.Core.Serialization;

    /// <summary>
    ///     base class for encoding messages with type information
    /// </summary>
    public class BaseAmqpProtocol : IAmqpMessageProtocol
    {
        /// <summary>
        ///  defaults for ampq protocol
        /// </summary>
        private readonly IAmqpProtocolDefaults defaults;

        /// <summary>
        ///     the type information encoder type
        /// </summary>
        private readonly ICSharpTypeEncoder typeEncoder;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseAmqpProtocol" /> class.
        /// </summary>
        /// <param name="serializer">binary serializer for converting messages to bytes</param>
        protected BaseAmqpProtocol(IBinarySerializaton serializer)
            : this(serializer, new NoCSharpType())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseAmqpProtocol" /> class.
        /// </summary>
        /// <param name="serializer">binary serializer for converting messages to bytes</param>
        /// <param name="typeEncoder">type information encoder</param>
        protected BaseAmqpProtocol(IBinarySerializaton serializer, ICSharpTypeEncoder typeEncoder)
            : this(serializer, typeEncoder, new AmqpProtocolDefaults())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseAmqpProtocol" /> class.
        /// </summary>
        /// <param name="serializer">binary serializer for converting messages to bytes</param>
        /// <param name="defaults">override for protocol defaults</param>
        protected BaseAmqpProtocol(IBinarySerializaton serializer, IAmqpProtocolDefaults defaults)
            : this(serializer, new NoCSharpType(), defaults)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseAmqpProtocol" /> class.
        /// </summary>
        /// <param name="serializer">binary serializer for converting messages to bytes</param>
        /// <param name="typeEncoder">type information encoder</param>
        /// <param name="defaults">override for protocol defaults</param>
        protected BaseAmqpProtocol(IBinarySerializaton serializer, ICSharpTypeEncoder typeEncoder, IAmqpProtocolDefaults defaults)
        {
            Arguments.NotNull(serializer, nameof(serializer));
            Arguments.NotNull(typeEncoder, nameof(typeEncoder));
            Arguments.NotNull(defaults, nameof(defaults));

            this.Serializer = serializer;
            this.typeEncoder = typeEncoder;
            this.defaults = defaults;
        }

        /// <summary>
        ///     Gets the serializer
        /// </summary>
        protected ISerialization<ReadOnlyMemory<byte>> Serializer { get; }

        /// <inheritdoc />
        public virtual IMessageEnvelope Encode<TMessage>(TMessage value)
        {
            return this.Encode<TMessage>(value, this.OnComputeExchange(value));
        }

        /// <inheritdoc />
        public virtual IMessageEnvelope Encode<TMessage>(TMessage message, string destination)
        {
            return this.Encode<TMessage>(message, destination, this.OnComputeTopic(message, destination));
        }

        /// <inheritdoc />
        public virtual IMessageEnvelope Encode<TMessage>(TMessage message, string exchange, string topic)
        {
            return this.Encode<TMessage>(message, exchange, topic, this.OnComputeMandatory(message, exchange, topic));
        }

        /// <inheritdoc />
        public virtual IMessageEnvelope Encode<TMessage>(TMessage message, string exchange, string topic, bool mandatory)
        {
            var p = new PublishProperties
            {
                Exchange = exchange,
                Mandatory = mandatory,
                RoutingKey = topic,
            };

            return this.Encode<TMessage>(message, p);
        }

        /// <inheritdoc />
        public IMessageEnvelope Encode<TMessage>(TMessage message, IPublishProperties properties)
        {
            var payload = this.Serializer.Encode(message);

            var envelope = new MessageEnvelope(properties)
            {
                Body = payload,
            };

            this.typeEncoder.Encode(typeof(TMessage), envelope);

            this.OnMessageEnvelope(message, envelope);

            return envelope;
        }

        /// <inheritdoc />
        public virtual TMessage Decode<TMessage>(IMessageEnvelope storage)
        {
            storage = Arguments.EnsureNotNull(storage, nameof(storage));

            var type = this.typeEncoder.Decode(storage);

            if (type != null && type != typeof(TMessage))
            {
                throw AbbotwareException.Create("Message type Mismatch! Message Contains:{0}  Caller Expects:{1}  maybe you should call the non generic decode, or use a MessageGetter / Cosumer that supports callback's per message type", type.AssemblyQualifiedName, typeof(TMessage).AssemblyQualifiedName);
            }

            return this.Serializer.Decode<TMessage>(storage.Body.ToArray());
        }

        /// <summary>
        /// Callback for computing the exchange
        /// </summary>
        /// <typeparam name="TMessage">message type</typeparam>
        /// <param name="message">message</param>
        /// <returns>exchange name</returns>
        protected virtual string OnComputeExchange<TMessage>(TMessage message)
        {
            return this.defaults.Exchange;
        }

        /// <summary>
        /// Callback for computing message topic
        /// </summary>
        /// <typeparam name="TMessage">message type</typeparam>
        /// <param name="message">message</param>
        /// <param name="exchange">exchange</param>
        /// <returns>routing topic</returns>
        protected virtual string OnComputeTopic<TMessage>(TMessage message, string exchange)
        {
            return this.defaults.Topic;
        }

        /// <summary>
        /// Callback for computing mandatory flag
        /// </summary>
        /// <typeparam name="TMessage">message type</typeparam>
        /// <param name="message">message</param>
        /// <param name="exchange">exchange</param>
        /// <param name="topic">topic</param>
        /// <returns>mandatory flag</returns>
        protected virtual bool OnComputeMandatory<TMessage>(TMessage message, string exchange, string topic)
        {
            return this.defaults.Mandatory;
        }

        /// <summary>
        /// Callback for inpsecting the message envelope
        /// </summary>
        /// <typeparam name="TMessage">message type</typeparam>
        /// <param name="message">message</param>
        /// <param name="envelope">message envelope</param>
        protected virtual void OnMessageEnvelope<TMessage>(TMessage message, MessageEnvelope envelope)
        {
        }
    }
}