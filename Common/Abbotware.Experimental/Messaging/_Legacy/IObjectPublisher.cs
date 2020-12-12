//-----------------------------------------------------------------------
// <copyright file="IObjectPublisher.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.Core.Messaging
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    ///     Interface for publishing objects of any type
    /// </summary>
    [Obsolete("this might be development code and unused")]
    public interface IObjectPublisher
    {
        /// <summary>
        ///     Publishes a message
        /// </summary>
        /// <param name="message">message object</param>
        /// <returns>task for the publish request</returns>
        Task<PublishStatus> Publish(object message);

        /// <summary>
        ///     Publishes a message with a topic
        /// </summary>
        /// <param name="message">message object</param>
        /// <param name="topic">the messages topic</param>
        /// <returns>task for the publish request</returns>
        Task<PublishStatus> Publish(object message, string topic);

        /// <summary>
        ///     Publishes a message with a topic and priority flags
        /// </summary>
        /// <param name="message">message object</param>
        /// <param name="topic">the messages topic</param>
        /// <param name="immediate">flag for immediate processing</param>
        /// <param name="mandatory">flag for mandatory subscriber</param>
        /// <returns>task for the publish request</returns>
        Task<PublishStatus> Publish(object message, string topic, bool immediate, bool mandatory);
    }
}