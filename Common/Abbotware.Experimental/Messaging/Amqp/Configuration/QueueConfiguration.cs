// -----------------------------------------------------------------------
// <copyright file="QueueConfiguration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.Configuration
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Messaging.Amqp.Configuration.HighAvailability;

    /// <summary>
    ///     strongly typed class for configuring a queue. Useful for dependency injection
    /// </summary>
    public class QueueConfiguration : CommonConfiguration
    {
        /// <summary>
        ///     internal set once property for name
        /// </summary>
        private readonly SetOnceProperty<string> name = new SetOnceProperty<string>("Name");

        /// <summary>
        ///     internal set once field for queue's exclusive flag
        /// </summary>
        private readonly SetOnceProperty<bool> isExclusive = new SetOnceProperty<bool>("exclusive");

        /// <summary>
        ///     internal set once field for queue message time to live
        /// </summary>
        private readonly SetOnceProperty<TimeSpan> messageTimeToLive = new SetOnceProperty<TimeSpan>("messageTTL");

        /// <summary>
        ///     internal set once field for queue expiration
        /// </summary>
        private readonly SetOnceProperty<TimeSpan> expires = new SetOnceProperty<TimeSpan>("expires");

        /// <summary>
        ///     internal set once field for queue's dead letter exchange
        /// </summary>
        private readonly SetOnceProperty<string> deadLetterExchange = new SetOnceProperty<string>("deadLetterExchange");

        /// <summary>
        ///     internal set once field for queue's dead letter routing key
        /// </summary>
        private readonly SetOnceProperty<string> deadLetterRoutingKey = new SetOnceProperty<string>("deadLetterRoutingKey");

        /// <summary>
        ///     internal set once field for queue's HA Policy
        /// </summary>
        private readonly SetOnceProperty<HighAvailabilityPolicyConfiguration> highAvailabilityPolicy;

        /// <summary>
        ///     Initializes a new instance of the <see cref="QueueConfiguration" /> class.
        /// </summary>
        public QueueConfiguration()
        {
            this.messageTimeToLive = new SetOnceProperty<TimeSpan>(
                "messageTTL",
                x =>
                {
                    var ms = (int)x.TotalMilliseconds;
                    this.Arguments["x-message-ttl"] = ms;
                });

            this.expires = new SetOnceProperty<TimeSpan>(
                "expires",
                x =>
                {
                    var ms = (int)x.TotalMilliseconds;
                    this.Arguments["x-expires"] = ms;
                });

            this.deadLetterExchange = new SetOnceProperty<string>(
                "deadLetterExchange",
                x => { this.Arguments["x-dead-letter-exchange"] = x; });

            this.deadLetterRoutingKey = new SetOnceProperty<string>(
                "deadLetterRoutingKey",
                x => { this.Arguments["x-dead-letter-routing-key"] = x; });

            this.highAvailabilityPolicy = new SetOnceProperty<HighAvailabilityPolicyConfiguration>(
                "HighAvailabilityPolicyConfiguration",
                x => { x.ConfigureArguments(this.Arguments); });
        }

        /// <summary>
        ///     Gets or sets the name of the queue
        /// </summary>
        /// <remarks>use string.Empty for a auto generated queue name</remarks>
        public string Name
        {
            get
            {
                if (this.name.HasValue)
                {
                    return this.name.Value;
                }

                throw new InvalidOperationException("Queue name is not set");
            }

            set
            {
                Core.Arguments.NotNullOrWhitespace(value, nameof(this.Name), "Non-null name required for queue name");

                this.name.Value = value;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether or not the queue is exclusive
        /// </summary>
        public bool IsExclusive
        {
            get { return this.isExclusive.Value; }

            set { this.isExclusive.Value = value; }
        }

        /// <summary>
        ///     Gets or sets the time to live for messages in the queue
        /// </summary>
        public TimeSpan? MessageTimeToLive
        {
            get
            {
                if (this.messageTimeToLive.HasValue)
                {
                    return this.messageTimeToLive.Value;
                }

                return null;
            }

            set
            {
                Core.Arguments.NotNull(value, nameof(this.MessageTimeToLive));

                this.messageTimeToLive.Value = value.Value;
            }
        }

        /// <summary>
        ///     Gets or sets the dead letter exchange for the queue
        /// </summary>
        public string DeadLetterExchange
        {
            get
            {
                if (this.deadLetterExchange.HasValue)
                {
                    return this.deadLetterExchange.Value;
                }

                return null;
            }

            set
            {
                Core.Arguments.NotNullOrWhitespace(value, nameof(this.DeadLetterExchange), "Non-empty name required for an exchange name");

                this.deadLetterExchange.Value = value;
            }
        }

        /// <summary>
        ///     Gets or sets the dead letter exchange routing key for the queue
        /// </summary>
        public string DeadLetterRoutingKey
        {
            get
            {
                if (this.deadLetterRoutingKey.HasValue)
                {
                    return this.deadLetterRoutingKey.Value;
                }

                return null;
            }

            set
            {
                Core.Arguments.NotNullOrWhitespace(value, nameof(this.DeadLetterRoutingKey), "Non-empty name required for alternate exchange name");

                this.deadLetterRoutingKey.Value = value;
            }
        }

        /// <summary>
        ///     Gets or sets the queue expiration time for the queue to auto delete
        /// </summary>
        public TimeSpan? Expires
        {
            get
            {
                if (this.expires.HasValue)
                {
                    return this.expires.Value;
                }

                return null;
            }

            set
            {
                Core.Arguments.NotNull(value, nameof(this.Expires));

                this.expires.Value = value.Value;
            }
        }

        /// <summary>
        ///     Gets or sets the high availability configuration for the queue
        /// </summary>
        public HighAvailabilityPolicyConfiguration HighAvailabilityPolicy
        {
            get
            {
                if (this.highAvailabilityPolicy.HasValue)
                {
                    return this.highAvailabilityPolicy.Value;
                }

                return null;
            }

            set
            {
                Core.Arguments.NotNull(value, nameof(this.HighAvailabilityPolicy));

                this.highAvailabilityPolicy.Value = value;
            }
        }
    }
}