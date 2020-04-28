// -----------------------------------------------------------------------
// <copyright file="CommonHeaders.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Configuration.Models
{
    using System;

    /// <summary>
    /// POCO class for header properties
    /// </summary>
    public class CommonHeaders : IHeaderProperties
    {
        /// <inheritdoc/>
        public string? UserId { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset? Timestamp { get; set; }

        /// <inheritdoc/>
        public string? ReplyTo { get; set; }

        /// <inheritdoc/>
        public byte? Priority { get; set; }

        /// <inheritdoc/>
        public string? MessageId { get; set; }

        /// <inheritdoc/>
        public TimeSpan? Expiration { get; set; }

        /// <inheritdoc/>
        public string? CorrelationId { get; set; }

        /// <inheritdoc/>
        public string? ContentType { get; set; }

        /// <inheritdoc/>
        public string? ContentEncoding { get; set; }

        /// <inheritdoc/>
        public string? ClusterId { get; set; }

        /// <inheritdoc/>
        public string? AppId { get; set; }

        /// <inheritdoc/>
        public string? MessageType { get; set; }
    }
}