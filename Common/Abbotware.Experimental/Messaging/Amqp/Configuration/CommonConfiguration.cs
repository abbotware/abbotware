// -----------------------------------------------------------------------
// <copyright file="CommonConfiguration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.Configuration
{
    using System.Collections.Generic;
    using Abbotware.Core;

    /// <summary>
    ///     strongly typed class for configuring common properties of an exchange/queue. Useful for dependency injection
    /// </summary>
    public abstract class CommonConfiguration
    {
        /// <summary>
        ///     internal set once property for durable
        /// </summary>
        private readonly SetOnceProperty<bool> isDurable = new SetOnceProperty<bool>("IsDurable");

        /// <summary>
        ///     internal set once property for autoDelete
        /// </summary>
        private readonly SetOnceProperty<bool> isAutoDelete = new SetOnceProperty<bool>("IsAutoDelete");

        /// <summary>
        ///     internal set once property for arguments
        /// </summary>
        private readonly IDictionary<string, object> arguments = new Dictionary<string, object>();

        /// <summary>
        ///     Gets or sets a value indicating whether or not the queue/exchange is durable
        /// </summary>
        public bool IsDurable
        {
            get { return this.isDurable.Value; }

            set { this.isDurable.Value = value; }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether or not the queue/exchange is set to auto-delete when there are no messages
        ///     / consumers
        /// </summary>
        public bool IsAutoDelete
        {
            get { return this.isAutoDelete.Value; }

            set { this.isAutoDelete.Value = value; }
        }

        /// <summary>
        ///     Gets the custom properties for the queue/exchange
        /// </summary>
        public IDictionary<string, object> Arguments
        {
            get
            {
                return this.arguments;
            }
        }
    }
}