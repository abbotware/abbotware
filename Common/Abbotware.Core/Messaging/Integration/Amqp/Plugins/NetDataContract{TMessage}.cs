// -----------------------------------------------------------------------
// <copyright file="NetDataContract{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Messaging.Integration.Amqp.Plugins
{
    using Abbotware.Core.Messaging.Amqp.ExtensionPoints;
    using Abbotware.Core.Messaging.Plugins.Protocol;
    using Abbotware.Core.Plugins.Serialization;

    /// <summary>
    ///     Protocol using NetDataContractSerializer and Type Info encoding for a specific message type
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public class NetDataContract<TMessage> : NetDataContract, IAmqpMessageProtocol<TMessage>
    {
        /// <inheritdoc />
        public IMessageEnvelope Encode(TMessage message)
        {
            return this.Encode<TMessage>(message);
        }

        /// <inheritdoc />
        public IMessageEnvelope Encode(TMessage message, string exchange)
        {
            return this.Encode<TMessage>(message, exchange);
        }

        /// <inheritdoc />
        public IMessageEnvelope Encode(TMessage message, string exchange, string topic)
        {
            return this.Encode<TMessage>(message, exchange, topic);
        }

        /// <inheritdoc />
        public new TMessage Decode(IMessageEnvelope envelope)
        {
            return this.Decode<TMessage>(envelope);
        }

        /// <inheritdoc />
        public IMessageEnvelope Encode(TMessage message, string exchange, string topic, bool mandatory)
        {
            return this.Encode<TMessage>(message, exchange, topic, mandatory);
        }
    }
}