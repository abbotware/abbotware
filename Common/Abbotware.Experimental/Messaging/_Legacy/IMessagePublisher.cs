//-----------------------------------------------------------------------
// <copyright file="IMessagePublisher.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.Core.Messaging
{
    using System.Threading.Tasks;

    /// <summary>
    ///     Interface for publishing typed messages
    /// </summary>
    public interface IMessagePublisher
    {
        /// <summary>
        ///     Publishes a message
        /// </summary>
        /// <typeparam name="TMessage">Type of Message</typeparam>
        /// <param name="message">the messages to publish</param>
        /// <returns>task for the publish request</returns>
        Task<PublishStatus> Publish<TMessage>(TMessage message);

        /// <summary>
        ///     Publishes a message with a topic
        /// </summary>
        /// <typeparam name="TMessage">Type of Message</typeparam>
        /// <param name="message">the messages to publish</param>
        /// <param name="topic">the messages topic</param>
        /// <returns>task for the publish request</returns>
        Task<PublishStatus> Publish<TMessage>(TMessage message, string topic);

        /// <summary>
        ///     Publishes a message with a topic an priority flags
        /// </summary>
        /// <typeparam name="TMessage">Type of Message</typeparam>
        /// <param name="message">the messages to publish</param>
        /// <param name="topic">the messages topic</param>
        /// <param name="immediate">flag for immediate processing</param>
        /// <param name="mandatory">flag for mandatory subscriber</param>
        /// <returns>task for the publish request</returns>
        Task<PublishStatus> Publish<TMessage>(TMessage message, string topic, bool immediate, bool mandatory);
    }
}