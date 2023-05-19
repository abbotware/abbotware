// -----------------------------------------------------------------------
// <copyright file="IMessageProtocol.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration
{
    using Abbotware.Core.Messaging.Integration.Configuration;
    using Abbotware.Core.Serialization;

    /// <summary>
    ///     Interface that represents a Generic Type / POCO binary encoder
    /// </summary>
    public interface IMessageProtocol : IProtocol<IMessageEnvelope>
    {
        /// <summary>
        ///     Encodes an object into a configuration for publishing
        /// </summary>
        /// <typeparam name="TMessage">Message type</typeparam>
        /// <param name="message">message object to encode</param>
        /// <param name="destination">destination for message</param>
        /// <returns>message publishing configuration</returns>
        IMessageEnvelope Encode<TMessage>(TMessage message, string destination);

        /// <summary>
        ///     Encodes an object into a configuration for publishing
        /// </summary>
        /// <typeparam name="TMessage">Message type</typeparam>
        /// <param name="message">message object to encode</param>
        /// <param name="properties">publish properties</param>
        /// <returns>message publishing configuration</returns>
        IMessageEnvelope Encode<TMessage>(TMessage message, IPublishProperties properties);
    }
}