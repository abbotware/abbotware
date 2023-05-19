// -----------------------------------------------------------------------
// <copyright file="IAmqpPublisher.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.ExtensionPoints
{
    using System.Threading.Tasks;
    using Abbotware.Core.Messaging.Integration;

    /// <summary>
    ///     Interface that manages publishing messages via RabbitMQ Channel
    /// </summary>
    public interface IAmqpPublisher : IBasicPublisher
    {
        /// <summary>
        ///     Gets the count of total returned messages on the channel
        /// </summary>
        long ReturnedMessages { get; }

        /// <summary>
        ///     Gets the count of returned messages that had no route (mandatory flag failed)
        /// </summary>
        long ReturnedNoRoute { get; }

        /// <summary>
        ///     Gets the count of returned messages that had no consumer (immediate flag failed)
        /// </summary>
        long ReturnedNoConsumers { get; }

        /// <summary>
        ///     Asynchronously Publishes message data to an exchange
        /// </summary>
        /// <param name="exchange">exchange to publish to</param>
        /// <param name="topic">message topic</param>
        /// <param name="mandatory">true requires an valid route to at least 1 queue</param>
        /// <param name="body">binary payload</param>
        /// <returns>Task that can be used synchronously or Asynchronously to determine the publish status</returns>
        /// <remarks>the task object only works when the channel is in confirmation mode, otherwise all publish status is unknown</remarks>
        Task<PublishStatus> Publish(string exchange, string topic, bool mandatory, byte[] body);
    }
}