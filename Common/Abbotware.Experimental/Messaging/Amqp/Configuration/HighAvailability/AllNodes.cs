// -----------------------------------------------------------------------
// <copyright file="AllNodes.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.Configuration.HighAvailability
{
    /// <summary>
    ///     HA Policy configured for 'All Nodes'
    /// </summary>
    public class AllNodes : HighAvailabilityPolicyConfiguration
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AllNodes" /> class.
        /// </summary>
        public AllNodes()
            : base(HighAvailabilityPolicy.All)
        {
        }
    }
}