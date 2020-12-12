// -----------------------------------------------------------------------
// <copyright file="TopicBindingConfiguration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.Configuration
{
    using System;
    using System.Collections.Generic;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;

    /// <summary>
    ///     Class that represents the Exchange-Exchange binding configuration
    /// </summary>
    public abstract class TopicBindingConfiguration
    {
        /// <summary>
        ///     internal set once field for binding action
        /// </summary>
        private readonly SetOnceProperty<BindingAction> action = new SetOnceProperty<BindingAction>("action");

        /// <summary>
        ///     internal set once field for binding topic / routing key
        /// </summary>
        private readonly SetOnceProperty<string> topic = new SetOnceProperty<string>("topic");

        /// <summary>
        ///     dictionary of additional arguments for this configuration object
        /// </summary>
        private readonly IDictionary<string, object> arguments = new Dictionary<string, object>();

        /// <summary>
        ///     Gets or sets once the binding action
        /// </summary>
        public BindingAction Action
        {
            get { return this.action.Value; }

            set { this.action.Value = value; }
        }

        /// <summary>
        ///     Gets or sets once the topic / routing key
        /// </summary>
        public string Topic
        {
            get
            {
                if (this.topic.HasValue)
                {
                    return this.topic.Value;
                }

                throw new InvalidOperationException("Topic is not set");
            }

            set
            {
                Core.Arguments.NotNullOrWhitespace(value, nameof(this.Topic), "Need a non-empty topic name for binding");

                this.topic.Value = value;
            }
        }

        /// <summary>
        ///     Gets the dictionary of additional arguments for this configuration object
        /// </summary>
        public IDictionary<string, object> Arguments
        {
            get
            {
                // TODO: covnert to IReadOnlyDictionary?
                return this.arguments;
            }
        }
    }
}