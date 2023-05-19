// -----------------------------------------------------------------------
// <copyright file="ExchangeBindingConfiguration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.Configuration
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;

    /// <summary>
    ///     Class that represents the Exchange-Exchange binding configuration
    /// </summary>
    public class ExchangeBindingConfiguration : TopicBindingConfiguration
    {
        /// <summary>
        ///     internal set once field for source exchange
        /// </summary>
        private readonly SetOnceProperty<string> sourceExchange = new("SourceExchange");

        /// <summary>
        ///     internal set once field for destination exchange
        /// </summary>
        private readonly SetOnceProperty<string> destinationExchange = new("DestinationExchange");

        /// <summary>
        ///     Gets or sets the source exchange
        /// </summary>
        /// <remarks>Cannot set bindings for the default exchange! (violation of AMPQ), you must specify a different exchange"</remarks>
        public string SourceExchange
        {
            get
            {
                if (this.sourceExchange.HasValue)
                {
                    return this.sourceExchange.Value!;
                }

                throw new InvalidOperationException("Source Exchange is not set");
            }

            set
            {
                Abbotware.Core.Arguments.NotNullOrWhitespace(value, nameof(this.SourceExchange), "Provide a non-empty source exchange: Cannot set bindings for the default exchange! (violation of AMPQ), you must specify a different exchange");

                this.sourceExchange.Value = value;
            }
        }

        /// <summary>
        ///     Gets or sets the destination exchange
        /// </summary>
        /// <remarks>Cannot set bindings for the default exchange! (violation of AMPQ), you must specify a different exchange"</remarks>
        public string DestinationExchange
        {
            get
            {
                if (this.destinationExchange.HasValue)
                {
                    return this.destinationExchange.Value!;
                }

                throw new InvalidOperationException("Destination Exchange is not set");
            }

            set
            {
                Abbotware.Core.Arguments.NotNullOrWhitespace(value, nameof(this.DestinationExchange), "Provide a non-empty destination exchange: Cannot set bindings for default exchange! (violation of AMPQ), you must specify a different exchange");

                this.destinationExchange.Value = value;
            }
        }
    }
}