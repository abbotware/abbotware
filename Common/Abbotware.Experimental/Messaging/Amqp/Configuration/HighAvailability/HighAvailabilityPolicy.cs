// -----------------------------------------------------------------------
// <copyright file="HighAvailabilityPolicy.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.Configuration.HighAvailability
{
    /// <summary>
    ///     Different types of High Availability Policies
    /// </summary>
    public enum HighAvailabilityPolicy
    {
        /// <summary>
        ///     High Availability Policy should use all nodes
        /// </summary>
        All,

        /// <summary>
        ///     High Availability Policy should use only the nodes specified in 'x-ha-policy-params' property
        /// </summary>
        Nodes,
    }
}