// -----------------------------------------------------------------------
// <copyright file="ConsumerConfiguration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Configuration.Models
{
    /// <summary>
    /// POCO class for Consumer Configuration
    /// </summary>
    public class ConsumerConfiguration : IConsumerConfiguration
    {
        /// <inheritdoc/>
        public string Queue { get; set; } = string.Empty;

        /// <inheritdoc/>
        public bool RequiresAcks { get; set; }
    }
}