// -----------------------------------------------------------------------
// <copyright file="IMessagePublisher{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     Interface for publishing objects of a single type
    /// </summary>
    /// <typeparam name="TMessage">type of message</typeparam>
    public interface IMessagePublisher<TMessage> : IDisposable
    {
        /// <summary>
        ///     Publishes a message
        /// </summary>
        /// <param name="message">message object</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>task for the publish request</returns>
        ValueTask<PublishStatus> PublishAsync(TMessage message, CancellationToken ct);
    }
}