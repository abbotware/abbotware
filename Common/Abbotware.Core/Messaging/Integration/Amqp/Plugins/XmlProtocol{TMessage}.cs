// -----------------------------------------------------------------------
// <copyright file="XmlProtocol{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Amqp.Plugins
{
    using Abbotware.Core;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Messaging.Integration.Amqp;
    using Abbotware.Core.Messaging.Integration.Configuration;
    using Abbotware.Core.Messaging.Integration.Plugins;

    /// <summary>
    ///     Protocol using XmlSerializer and Type Info encoding for a specific message type
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public class XmlProtocol<TMessage> : XmlProtocol, IAmqpMessageProtocol<TMessage>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="XmlProtocol{TMessage}" /> class.
        /// </summary>
        public XmlProtocol()
            : this(new AssemblyQualifiedNameInHeader())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="XmlProtocol{TMessage}" /> class.
        /// </summary>
        /// <param name="typeEncoder">type info encoder </param>
        public XmlProtocol(ICSharpTypeEncoder typeEncoder)
            : base(typeEncoder)
        {
            Arguments.NotNull(typeEncoder, nameof(typeEncoder));
        }

        /// <inheritdoc />
        public IMessageEnvelope Encode(TMessage message)
        {
            return this.Encode<TMessage>(message);
        }

        /// <inheritdoc />
        public IMessageEnvelope Encode(TMessage message, string destination)
        {
            return this.Encode<TMessage>(message, destination);
        }

        /// <inheritdoc />
        public IMessageEnvelope Encode(TMessage message, string exchange, string topic)
        {
            return this.Encode<TMessage>(message, exchange, topic);
        }

        /// <inheritdoc />
        public TMessage Decode(IMessageEnvelope envelope)
        {
            return this.Decode<TMessage>(envelope);
        }

        /// <inheritdoc />
        public IMessageEnvelope Encode(TMessage message, string exchange, string topic, bool mandatory)
        {
            return this.Encode<TMessage>(message, exchange, topic, mandatory);
        }

        /// <inheritdoc />
        public IMessageEnvelope Encode(TMessage message, IPublishProperties properties)
        {
            return this.Encode<TMessage>(message, properties);
        }
    }
}