// -----------------------------------------------------------------------
// <copyright file="IBasicConsumer.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration
{
    using System;
    using Abbotware.Core.Messaging.Integration.Configuration;
    using Abbotware.Core.Objects;

    /// <summary>
    /// Interface for a basic message consumer
    /// </summary>
    public interface IBasicConsumer : IInitializable, IDisposable
    {
        /// <summary>
        /// Event for message delivery
        /// </summary>
        event Action<IMessageEnvelope> OnDelivery;

        /// <summary>
        /// Gets the consumer status
        /// </summary>
        ConsumerStatus Status { get; }

        /// <summary>
        /// Gets the number of delievered messages
        /// </summary>
        uint Delivered { get; }
    }
}
