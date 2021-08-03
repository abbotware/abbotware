// -----------------------------------------------------------------------
// <copyright file="IPublishProperties.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration.Configuration
{
    /// <summary>
    /// Read only interface for publish properties
    /// </summary>
    public interface IPublishProperties
    {
        /// <summary>
        /// Gets the exchange name
        /// </summary>
        string Exchange { get; }

        /// <summary>
        /// Gets the message topic / routing key
        /// </summary>
        string RoutingKey { get; }

        /// <summary>
        /// Gets a value indicating whether the message is persistent
        /// </summary>
        bool Persistent { get; }

        /// <summary>
        /// Gets a value indicating whether message has a mandatory flag
        /// </summary>
        bool Mandatory { get; }
    }
}