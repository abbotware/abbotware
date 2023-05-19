// -----------------------------------------------------------------------
// <copyright file="IAdvancedMessagePublisher{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging
{
    using System.Threading.Tasks;
    using Abbotware.Core.Messaging.Integration;

    /// <summary>
    ///     Interface for publishing objects of a single type
    /// </summary>
    /// <typeparam name="TMessage">type of message</typeparam>
    public interface IAdvancedMessagePublisher<TMessage> : IMessagePublisher<TMessage>
    {
        /// <summary>
        ///     Publishes a message with a topic
        /// </summary>
        /// <param name="message">message object</param>
        /// <param name="exchange">the exchange to publish to</param>
        /// <returns>task for the publish request</returns>
        Task<PublishStatus> PublishAsync(TMessage message, string exchange);

        /// <summary>
        ///     Publishes a message with a topic
        /// </summary>
        /// <param name="message">message object</param>
        /// <param name="exchange">the exchange to publish to</param>
        /// <param name="topic">the messages topic</param>
        /// <returns>task for the publish request</returns>
        Task<PublishStatus> PublishAsync(TMessage message, string exchange, string topic);

        /// <summary>
        ///     Publishes a message with a topic
        /// </summary>
        /// <param name="message">message object</param>
        /// <param name="exchange">the exchange to publish to</param>
        /// <param name="topic">the messages topic</param>
        /// <param name="mandatory">require message to be queued</param>
        /// <returns>task for the publish request</returns>
        Task<PublishStatus> PublishAsync(TMessage message, string exchange, string topic, bool mandatory);
    }
}