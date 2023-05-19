// -----------------------------------------------------------------------
// <copyright file="IConsumerConfiguration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Configuration
{
    /// <summary>
    /// Read only interface for a consumer configuration
    /// </summary>
    public interface IConsumerConfiguration
    {
        /// <summary>
        /// Gets a value indicating whether the consumer requires acks
        /// </summary>
        bool RequiresAcks { get; }

        /// <summary>
        /// Gets the queue name
        /// </summary>
        string Queue { get; }
    }
}