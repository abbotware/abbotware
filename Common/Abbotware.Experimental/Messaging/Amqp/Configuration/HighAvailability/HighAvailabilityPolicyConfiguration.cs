// -----------------------------------------------------------------------
// <copyright file="HighAvailabilityPolicyConfiguration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.Configuration.HighAvailability
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using Abbotware.Core;

    /// <summary>
    ///     class that represents the High Availability Policy for a queue
    /// </summary>
    public abstract class HighAvailabilityPolicyConfiguration
    {
        /// <summary>
        ///     Name of the HA Policy Parameters property
        /// </summary>
        internal const string HAPolicyParamsArgumentName = "x-ha-policy-params";

        /// <summary>
        ///     Name of the HA Policy property
        /// </summary>
        internal const string HAPolicyArgumentName = "x-ha-policy";

        /// <summary>
        ///     the policy type to use
        /// </summary>
        private readonly HighAvailabilityPolicy policyType;

        /// <summary>
        /// Initializes a new instance of the <see cref="HighAvailabilityPolicyConfiguration"/> class.
        /// </summary>
        /// <param name="policy">policy type to use</param>
        protected HighAvailabilityPolicyConfiguration(HighAvailabilityPolicy policy)
        {
            this.policyType = policy;
        }

        /// <summary>
        ///     Configures the queue's arguments
        /// </summary>
        /// <param name="dictionary">the arguments dictionary for the queue</param>
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Matching RabbitMQ Docs")]
        internal virtual void ConfigureArguments(IDictionary<string, object> dictionary)
        {
            Arguments.NotNull(dictionary, nameof(dictionary));

            dictionary[HighAvailabilityPolicyConfiguration.HAPolicyArgumentName] = this.policyType
                .ToString()
                .ToLower(CultureInfo.InvariantCulture);
        }
    }
}