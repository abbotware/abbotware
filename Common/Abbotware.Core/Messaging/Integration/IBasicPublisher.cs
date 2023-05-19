// -----------------------------------------------------------------------
// <copyright file="IBasicPublisher.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration
{
    using System;
    using System.Threading.Tasks;
    using Abbotware.Core.Messaging.Integration.Configuration;

    /// <summary>
    ///     Interface that manages publishing messages via RabbitMQ Channel
    /// </summary>
    public interface IBasicPublisher : IDisposable
    {
        /// <summary>
        ///     Gets the count of total messages published
        /// </summary>
        long PublishedMessages { get; }

        /// <summary>
        ///     Asynchronously Publishes message data to an exchange
        /// </summary>
        /// <param name="body">message bytes</param>
        /// <param name="properties">publish properties</param>
        /// <returns>Task that can be used synchronously or Asynchronously to determine the publish status</returns>
        /// <remarks>the task object only works when the channel is in confirmation mode, otherwise all publish status is unknown</remarks>
        Task<PublishStatus> Publish(byte[] body, IPublishProperties properties);

        /// <summary>
        ///     Asynchronously Publishes message data to an exchange
        /// </summary>
        /// <param name="envelope">entire message with properties</param>
        /// <returns>Task that can be used synchronously or Asynchronously to determine the publish status</returns>
        /// <remarks>the task object only works when the channel is in confirmation mode, otherwise all publish status is unknown</remarks>
        Task<PublishStatus> Publish(IMessageEnvelope envelope);
    }
}