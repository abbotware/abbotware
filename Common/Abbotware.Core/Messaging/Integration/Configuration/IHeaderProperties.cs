// -----------------------------------------------------------------------
// <copyright file="IHeaderProperties.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Configuration
{
    using System;

    /// <summary>
    /// Read only interface for message header properties
    /// </summary>
    public interface IHeaderProperties
    {
        /// <summary>
        /// Gets the app id
        /// </summary>
        string? AppId { get; }

        /// <summary>
        /// Gets the cluster id
        /// </summary>
        string? ClusterId { get; }

        /// <summary>
        /// Gets the content encoding
        /// </summary>
        string? ContentEncoding { get; }

        /// <summary>
        /// Gets the content type
        /// </summary>
        string? ContentType { get; }

        /// <summary>
        /// Gets the correlation id
        /// </summary>
        string? CorrelationId { get; }

        /// <summary>
        /// Gets the message expiration id
        /// </summary>
        TimeSpan? Expiration { get; }

        /// <summary>
        /// Gets the message id
        /// </summary>
        string? MessageId { get; }

        /// <summary>
        /// Gets the message priority
        /// </summary>
        byte? Priority { get; }

        /// <summary>
        /// Gets the reply to
        /// </summary>
        string? ReplyTo { get; }

        /// <summary>
        /// Gets the app id
        /// </summary>
        DateTimeOffset? Timestamp { get; }

        /// <summary>
        /// Gets the message type
        /// </summary>
        string? MessageType { get; }

        /// <summary>
        /// Gets the user id
        /// </summary>
        string? UserId { get; }
    }
}