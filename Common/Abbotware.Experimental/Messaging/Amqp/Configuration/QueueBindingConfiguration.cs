// -----------------------------------------------------------------------
// <copyright file="QueueBindingConfiguration.cs" company="Abbotware, LLC">
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
    ///     Class that represents the Exchange->Queue binding configuration
    /// </summary>
    public class QueueBindingConfiguration : TopicBindingConfiguration
    {
        /// <summary>
        ///     internal set once field for source exchange
        /// </summary>
        private readonly SetOnceProperty<string> sourceExchange = new("SourceExchange");

        /// <summary>
        ///     internal set once field for destination queue
        /// </summary>
        private readonly SetOnceProperty<string> destinationQueue = new("DestinationQueue");

        /// <summary>
        ///     Gets or sets the name of the destination queue
        /// </summary>
        public string DestinationQueue
        {
            get
            {
                if (this.destinationQueue.HasValue)
                {
                    return this.destinationQueue.Value!;
                }

                throw new InvalidOperationException("Source Exchange is not set");
            }

            set
            {
                Core.Arguments.NotNullOrWhitespace(value, nameof(this.DestinationQueue), "Need a non-empty queue name for binding");

                this.destinationQueue.Value = value;
            }
        }

        /// <summary>
        ///     Gets or sets the name of the source exchange
        /// </summary>
        /// <remarks>
        ///     Cannot set bindings to the default exchange.  That is a violation of AMPQ
        /// </remarks>
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
                Core.Arguments.NotNullOrWhitespace(value, nameof(this.SourceExchange), "Need a non-empty exchange name: Cannot set bindings to the default exchange.  That is a violation of AMPQ");

                this.sourceExchange.Value = value;
            }
        }
    }
}