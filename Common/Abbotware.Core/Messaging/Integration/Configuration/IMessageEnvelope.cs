// -----------------------------------------------------------------------
// <copyright file="IMessageEnvelope.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Configuration
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Read only interface for message eveloper
    /// </summary>
    public interface IMessageEnvelope
    {
        /// <summary>
        /// gets the strongly typed common headers
        /// </summary>
        IHeaderProperties CommonHeaders { get; }

        /// <summary>
        /// Gets all headers
        /// </summary>
        IReadOnlyDictionary<string, object> Headers { get; }

        /// <summary>
        /// Gets the publish properties
        /// </summary>
        IPublishProperties PublishProperties { get; }

        /// <summary>
        /// Gets the delivery properties
        /// </summary>
        IDeliveryProperties DeliveryProperties { get; }

        /// <summary>
        /// Gets the message body
        /// </summary>
        ReadOnlyMemory<byte> Body { get; }
    }
}