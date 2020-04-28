// -----------------------------------------------------------------------
// <copyright file="AmqpProtocolDefaults.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Amqp.Configuration.Models
{
    /// <summary>
    ///     class containing amqp protocol default values
    /// </summary>
    public class AmqpProtocolDefaults : IAmqpProtocolDefaults
    {
        /// <inheritdoc/>
        public bool Mandatory { get; set; } = true;

        /// <inheritdoc/>
        public string Exchange { get; set; } = Constants.DefaultExchange;

        /// <inheritdoc/>
        public string Topic { get; set; } = string.Empty;
    }
}