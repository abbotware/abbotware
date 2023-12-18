// -----------------------------------------------------------------------
// <copyright file="SpecificNodes.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.Configuration.HighAvailability
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abbotware.Core;

    /// <summary>
    ///     HA Policy configured for 'Specific Nodes' provided
    /// </summary>
    public class SpecificNodes : HighAvailabilityPolicyConfiguration
    {
        /// <summary>
        ///     list of cluster node names used in the HA Policy
        /// </summary>
        private readonly string[] nodeNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecificNodes"/> class.
        /// </summary>
        /// <param name="nodes">list of cluster node names used in the HA Policy</param>
        public SpecificNodes(params string[] nodes)
            : base(HighAvailabilityPolicy.Nodes)
        {
            Arguments.NotNull(nodes, nameof(nodes));

            if (nodes.Length == 0)
            {
                throw new ArgumentException("must provide at least one cluster node name", nameof(nodes));
            }

            this.nodeNames = nodes;
        }

        /// <inheritdoc />
        internal override void ConfigureArguments(IDictionary<string, object> dictionary)
        {
            base.ConfigureArguments(dictionary);

            dictionary[HighAvailabilityPolicyConfiguration.HAPolicyParamsArgumentName] = this.nodeNames.ToList();
        }
    }
}