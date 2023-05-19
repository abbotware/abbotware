// -----------------------------------------------------------------------
// <copyright file="IAmqpMessageProtocol{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Amqp
{
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Messaging.Integration.Configuration;

    /// <summary>
    ///     Represents a protocol for encoding/decoding a message type
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public interface IAmqpMessageProtocol<TMessage> : IMessageProtocol<TMessage>
    {
        /// <summary>
        ///     Encodes a message into a publish configuration
        /// </summary>
        /// <param name="message">message to encode</param>
        /// <param name="exchange">exchange to publish message to</param>
        /// <param name="topic">topic to publish message with</param>
        /// <returns>publish configuration</returns>
        IMessageEnvelope Encode(TMessage message, string exchange, string topic);

        /// <summary>
        ///     Encodes a message into a publish configuration
        /// </summary>
        /// <param name="message">message to encode</param>
        /// <param name="exchange">exchange to publish message to</param>
        /// <param name="topic">topic to publish message with</param>
        /// <param name="mandatory">set mandatory flag to true</param>
        /// <returns>publish configuration</returns>
        IMessageEnvelope Encode(TMessage message, string exchange, string topic, bool mandatory);
    }
}