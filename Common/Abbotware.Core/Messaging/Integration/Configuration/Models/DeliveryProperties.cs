// -----------------------------------------------------------------------
// <copyright file="DeliveryProperties.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Configuration.Models
{
    /// <summary>
    /// POCO class for Delivery Properties
    /// </summary>
    public class DeliveryProperties : IDeliveryProperties
    {
        /// <inheritdoc/>
        public byte? DeliveryMode { get; set; }

        /// <inheritdoc/>
        public string? DeliveryTag { get; set; }

        /// <inheritdoc/>
        public string? ConsumerTag { get; set; }

        /// <inheritdoc/>
        public bool Redelivered { get; set; }

        /// <inheritdoc/>
        public uint? MessageCount { get; set; }
    }
}