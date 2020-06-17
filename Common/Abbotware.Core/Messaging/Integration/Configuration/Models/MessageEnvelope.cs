// -----------------------------------------------------------------------
// <copyright file="MessageEnvelope.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Configuration.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// POCO class for Message Envelope
    /// </summary>
    public class MessageEnvelope : IMessageEnvelope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageEnvelope"/> class.
        /// </summary>
        public MessageEnvelope()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageEnvelope"/> class.
        /// </summary>
        /// <param name="publishProperties">publish properties to initialize with</param>
        public MessageEnvelope(IPublishProperties publishProperties)
        {
            if (publishProperties != null)
            {
                this.PublishProperties.Exchange = publishProperties.Exchange;
                this.PublishProperties.Mandatory = publishProperties.Mandatory;
                this.PublishProperties.Persistent = publishProperties.Persistent;
                this.PublishProperties.RoutingKey = publishProperties.RoutingKey;
            }
        }

        /// <summary>
        /// Gets a Read/Write dictionary of headers
        /// </summary>
        public IDictionary<string, object> Headers { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets a Read/Write CommonHeaders
        /// </summary>
        public CommonHeaders CommonHeaders { get; } = new CommonHeaders();

        /// <summary>
        /// Gets a Read/Write PublishProperties
        /// </summary>
        public PublishProperties PublishProperties { get; } = new PublishProperties();

        /// <summary>
        /// Gets a Read/Write DeliveryProperties
        /// </summary>
        public DeliveryProperties DeliveryProperties { get; } = new DeliveryProperties();

        /// <inheritdoc/>
        IHeaderProperties IMessageEnvelope.CommonHeaders => this.CommonHeaders;

        /// <inheritdoc/>
        IPublishProperties IMessageEnvelope.PublishProperties => this.PublishProperties;

        /// <inheritdoc/>
        IDeliveryProperties IMessageEnvelope.DeliveryProperties => this.DeliveryProperties;

        /// <inheritdoc/>
        IReadOnlyDictionary<string, object> IMessageEnvelope.Headers => this.Headers.ToDictionary(x => x.Key, x => x.Value);

        /// <inheritdoc/>
        public ReadOnlyMemory<byte> Body { get; set; }
    }
}