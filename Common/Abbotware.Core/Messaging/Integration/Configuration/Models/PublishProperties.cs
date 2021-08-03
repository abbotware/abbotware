// -----------------------------------------------------------------------
// <copyright file="PublishProperties.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Configuration.Models
{
    /// <summary>
    /// POCO class for Publish Properties
    /// </summary>
    public class PublishProperties : IPublishProperties
    {
        /// <inheritdoc/>
        public string Exchange { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string RoutingKey { get; set; } = string.Empty;

        /// <inheritdoc/>
        public bool Persistent { get; set; }

        /// <inheritdoc/>
        public bool Mandatory { get; set; }
    }
}