// -----------------------------------------------------------------------
// <copyright file="BaseMessageProtocol.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.ExtensionPoints
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Exceptions;
    using Abbotware.Core.Messaging.Configuration;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Messaging.Integration.Configuration;
    using Abbotware.Core.Messaging.Integration.Configuration.Models;
    using Abbotware.Core.Messaging.Integration.Plugins;
    using Abbotware.Core.Serialization;

    /// <summary>
    ///     base class for encoding messages with type information
    /// </summary>
    public abstract class BaseMessageProtocol : IMessageProtocol
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseMessageProtocol" /> class.
        /// </summary>
        /// <param name="serializer">message encoder</param>
        /// <param name="defaults">override for protocol defaults</param>
        protected BaseMessageProtocol(IBinarySerializaton serializer, IProtocolDefaults defaults)
                   : this(serializer, new NoCSharpType(), defaults)
        {
            Arguments.NotNull(serializer, nameof(serializer));
            Arguments.NotNull(defaults, nameof(defaults));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseMessageProtocol" /> class.
        /// </summary>
        /// <param name="serializer">message encoder</param>
        /// <param name="typeEncoder">type information encoder</param>
        /// <param name="defaults">override for protocol defaults</param>
        protected BaseMessageProtocol(IBinarySerializaton serializer, ICSharpTypeEncoder typeEncoder, IProtocolDefaults defaults)
        {
            Arguments.NotNull(serializer, nameof(serializer));
            Arguments.NotNull(typeEncoder, nameof(typeEncoder));
            Arguments.NotNull(defaults, nameof(defaults));

            this.BinaryEncoder = serializer;
            this.TypeEncoder = typeEncoder;
            this.Defaults = defaults;
        }

        /// <summary>
        ///     Gets the binary encoder type
        /// </summary>
        protected ISerialization<ReadOnlyMemory<byte>> BinaryEncoder { get; }

        /// <summary>
        /// Gets the protocol Defaults
        /// </summary>
        protected IProtocolDefaults Defaults { get; }

        /// <summary>
        ///  Gets the the class type information encoder type
        /// </summary>
        protected ICSharpTypeEncoder TypeEncoder { get; }

        /// <inheritdoc />
        public virtual IMessageEnvelope Encode<TMessage>(TMessage message)
        {
            return this.Encode<TMessage>(message, default(IPublishProperties));
        }

        /// <inheritdoc />
        public virtual IMessageEnvelope Encode<TMessage>(TMessage message, string desintation)
        {
            var p = new PublishProperties
            {
                Exchange = desintation,
            };

            return this.Encode<TMessage>(message, p);
        }

        /// <inheritdoc />
        public virtual IMessageEnvelope Encode<TMessage>(TMessage message, IPublishProperties properties)
        {
            var payload = this.BinaryEncoder.Encode(message);

            var envelope = new MessageEnvelope(properties)
            {
                Body = payload,
            };

            this.TypeEncoder.Encode(typeof(TMessage), envelope);

            this.OnMessageEnvelope(message, envelope);

            return envelope;
        }

        /// <inheritdoc />
        public virtual TMessage Decode<TMessage>(IMessageEnvelope envelope)
        {
            envelope = Arguments.EnsureNotNull(envelope, nameof(envelope));

            var type = this.TypeEncoder.Decode(envelope);

            if (type != null && type != typeof(TMessage))
            {
                throw AbbotwareException.Create("Message type Mismatch! Message Contains:{0}  Caller Expects:{1}  maybe you should call the non generic decode, or use a MessageGetter / Cosumer that supports callback's per message type", type.AssemblyQualifiedName, typeof(TMessage).AssemblyQualifiedName);
            }

            return this.BinaryEncoder.Decode<TMessage>(envelope.Body.ToArray());
        }

        /// <inheritdoc />
        public virtual object Decode(IMessageEnvelope envelope)
        {
            envelope = Arguments.EnsureNotNull(envelope, nameof(envelope));

            var type = this.TypeEncoder.Decode(envelope);

            return this.BinaryEncoder.Decode(envelope.Body.ToArray(), type);
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