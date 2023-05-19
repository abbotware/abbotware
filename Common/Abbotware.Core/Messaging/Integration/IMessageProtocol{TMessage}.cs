// -----------------------------------------------------------------------
// <copyright file="IMessageProtocol{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration
{
    using Abbotware.Core.Messaging.Integration.Configuration;

    /// <summary>
    ///     Represents a protocol for encoding/decoding a message type
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public interface IMessageProtocol<TMessage>
    {
        /// <summary>
        ///     Encodes a message into a publish configuration
        /// </summary>
        /// <param name="message">message to encode</param>
        /// <returns>publish configuration</returns>
        IMessageEnvelope Encode(TMessage message);

        /// <summary>
        ///     Encodes an object into a configuration for publishing
        /// </summary>
        /// <param name="message">message object to encode</param>
        /// <param name="destination">destination for message</param>
        /// <returns>message envelope</returns>
        IMessageEnvelope Encode(TMessage message, string destination);

        /// <summary>
        ///     Encodes an object into a configuration for publishing
        /// </summary>
        /// <param name="message">message object to encode</param>
        /// <param name="properties">publish properties</param>
        /// <returns>message envelope</returns>
        IMessageEnvelope Encode(TMessage message, IPublishProperties properties);

        /// <summary>
        ///     Decodes a message into an object
        /// </summary>
        /// <param name="envelope">message envelope</param>
        /// <returns>decoded message</returns>
        TMessage Decode(IMessageEnvelope envelope);
    }
}