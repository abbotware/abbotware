// -----------------------------------------------------------------------
// <copyright file="BinaryFormatter{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Amqp.Plugins
{
    using Abbotware.Core.Messaging.Integration.Amqp;
    using Abbotware.Core.Messaging.Integration.Configuration;

    /// <summary>
    ///     protocol using the .Net BinaryFormatter for a single message type
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public class BinaryFormatter<TMessage> : BinaryFormatter, IAmqpMessageProtocol<TMessage>
    {
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