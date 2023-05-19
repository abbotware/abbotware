// -----------------------------------------------------------------------
// <copyright file="ExchangeConfiguration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.Configuration
{
    using System;
    using Abbotware.Core;

    /// <summary>
    ///     strongly typed class for configuring an exchange. Useful for dependency injection
    /// </summary>
    public class ExchangeConfiguration : CommonConfiguration
    {
        /// <summary>
        ///     internal set once property for type
        /// </summary>
        private readonly SetOnceProperty<ExchangeType> exchangeType = new("ExchangeType");

        /// <summary>
        ///     internal set once property for alternateExchange
        /// </summary>
        private readonly SetOnceProperty<string> alternateExchange;

        /// <summary>
        ///     internal set once property for name
        /// </summary>
        private readonly SetOnceProperty<string> name = new("Name");

        /// <summary>
        /// Initializes a new instance of the <see cref="ExchangeConfiguration"/> class.
        /// </summary>
        public ExchangeConfiguration()
        {
            this.alternateExchange = new SetOnceProperty<string>(
                "AlternateExchange",
                x => { this.Arguments["alternate-exchange"] = x; });
        }

        /// <summary>
        ///     Gets or sets the name of the exchange/queue
        /// </summary>
        public string Name
        {
            get
            {
                if (this.name.HasValue)
                {
                    return this.name.Value!;
                }

                throw new InvalidOperationException("Exchange name is not set");
            }

            set
            {
                Core.Arguments.NotNullOrWhitespace(value, nameof(this.Name), "Non-empty name required for exchange name");

                this.name.Value = value;
            }
        }

        /// <summary>
        ///     Gets or sets the type of the exchange
        /// </summary>
        public ExchangeType ExchangeType
        {
            get { return this.exchangeType.Value; }

            set { this.exchangeType.Value = value; }
        }

        /// <summary>
        ///     Gets or sets the alternate-exchange for the exchange
        /// </summary>
        public string? AlternateExchange
        {
            get
            {
                if (this.alternateExchange.HasValue)
                {
                    return this.alternateExchange.Value!;
                }

                return null;
            }

            set
            {
                Core.Arguments.NotNullOrWhitespace(value!, nameof(this.AlternateExchange), "Non-empty name required for alternate exchange name");

                this.alternateExchange.Value = value;
            }
        }
    }
}