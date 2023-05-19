// -----------------------------------------------------------------------
// <copyright file="QueueCreationConfiguration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.Configuration
{
    using Abbotware.Core;
    using Abbotware.Core.Extensions;

    /// <summary>
    ///     data returned from a queue creation operation
    /// </summary>
    public class QueueCreationConfiguration
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="QueueCreationConfiguration" /> class.
        /// </summary>
        /// <param name="queueName">queue name</param>
        /// <param name="message">number of messages</param>
        /// <param name="consumer">number of consumers</param>
        public QueueCreationConfiguration(string queueName, uint message, uint consumer)
        {
            Arguments.NotNullOrWhitespace(queueName, nameof(queueName));

            this.Name = queueName;
            this.MessageCount = message;
            this.ConsumerCount = consumer;
        }

        /// <summary>
        ///     Gets the name of the queue
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Gets the number of messages in the queue
        /// </summary>
        public uint MessageCount { get; private set; }

        /// <summary>
        ///     Gets the number of consumers on the queue
        /// </summary>
        public uint ConsumerCount { get; private set; }
    }
}