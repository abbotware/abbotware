// -----------------------------------------------------------------------
// <copyright file="IAmqpMessageProtocol.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Amqp
{
    using Abbotware.Core.Messaging.Integration.Configuration;

    /// <summary>
    ///     Interface that represents a Generic Type / POCO binary encoder
    /// </summary>
    public interface IAmqpMessageProtocol : IMessageProtocol
    {
        /// <summary>
        ///     Encodes an object into a configuration for publishing
        /// </summary>
        /// <typeparam name="TMessage">Message type</typeparam>
        /// <param name="message">message object to encode</param>
        /// <param name="exchange">exchange to publish to</param>
        /// <param name="topic">topic to use when publishing </param>
        /// <remarks>topic may be overridden by the type info encoder</remarks>
        /// <returns>message publishing configuration</returns>
        IMessageEnvelope Encode<TMessage>(TMessage message, string exchange, string topic);

        /// <summary>
        ///     Encodes an object into a configuration for publishing
        /// </summary>
        /// <typeparam name="TMessage">Message type</typeparam>
        /// <param name="message">message object to encode</param>
        /// <param name="exchange">exchange to publish to</param>
        /// <param name="topic">topic to use when publishing</param>
        /// <param name="mandatory">mandatory flag to use when publishing</param>
        /// <remarks>topic may be overridden by the type info encoder</remarks>
        /// <returns>message publishing configuration</returns>
        IMessageEnvelope Encode<TMessage>(TMessage message, string exchange, string topic, bool mandatory);
    }
}