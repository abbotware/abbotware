// -----------------------------------------------------------------------
// <copyright file="IAmqpQueueManager.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.ExtensionPoints
{
    using System;
    using Abbotware.Core.Messaging.Amqp.Configuration;

    /// <summary>
    ///     Interface that manages AMQP Queues via RabbitMQ Channel
    /// </summary>
    public interface IAmqpQueueManager : IDisposable
    {
        /// <summary>
        ///     checks if a queue exists (not guaranteed)
        /// </summary>
        /// <param name="queueName">queue name</param>
        /// <returns>true / false if the queue name exists</returns>
        bool QueueExists(string queueName);

        /// <summary>
        ///     Creates a queue with a given configuration
        /// </summary>
        /// <param name="queueConfiguration">configuration for creating the queue</param>
        /// <returns>actual creation properties</returns>
        QueueCreationConfiguration Create(QueueConfiguration queueConfiguration);

        /// <summary>
        ///     Creates a queue binding with a given configuration
        /// </summary>
        /// <param name="queueBindingConfiguration">configuration for creating a binding for the queue</param>
        void Bind(QueueBindingConfiguration queueBindingConfiguration);

        /// <summary>
        ///     Deletes a queue
        /// </summary>
        /// <param name="queueName">queue name</param>
        /// <param name="ifUnused">delete only if unused</param>
        /// <param name="ifEmpty">delete only if empty</param>
        /// <returns>number of deleted messages</returns>
        uint Delete(string queueName, bool ifUnused, bool ifEmpty);

        /// <summary>
        ///     Removes all messages from the named queue
        /// </summary>
        /// <param name="queueName">queue name</param>
        /// <returns>number of deleted messages</returns>
        uint Purge(string queueName);
    }
}