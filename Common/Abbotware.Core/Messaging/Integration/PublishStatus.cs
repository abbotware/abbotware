// -----------------------------------------------------------------------
// <copyright file="PublishStatus.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Integration
{
    /// <summary>
    ///     Status for a message publication
    /// </summary>
    public enum PublishStatus
    {
        /// <summary>
        ///     Unknown publish status
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///     Message was transferred successfully to AMQP
        /// </summary>
        Confirmed,

        /// <summary>
        ///     Message was returned (no route or no free subscriptions)
        /// </summary>
        Returned,
    }
}