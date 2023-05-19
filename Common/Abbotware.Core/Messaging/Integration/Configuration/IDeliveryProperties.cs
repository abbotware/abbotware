// -----------------------------------------------------------------------
// <copyright file="IDeliveryProperties.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Configuration
{
    /// <summary>
    /// Read only interface for message delivery properties
    /// </summary>
    public interface IDeliveryProperties
    {
        /// <summary>
        /// Gets the delivery tag
        /// </summary>
        string? DeliveryTag { get; }

        /// <summary>
        /// Gets the consumer tag
        /// </summary>
        string? ConsumerTag { get; }

        /// <summary>
        /// Gets a value indicating whether the message was redilevered
        /// </summary>
        bool Redelivered { get; }

        /// <summary>
        /// Gets the message count
        /// </summary>
        uint? MessageCount { get; }

        /// <summary>
        /// Gets the delivery mode
        /// </summary>
        byte? DeliveryMode { get; }
    }
}