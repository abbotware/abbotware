// -----------------------------------------------------------------------
// <copyright file="IAmqpExchangeManager.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Messaging.Amqp.ExtensionPoints
{
    using System;
    using Abbotware.Core.Messaging.Amqp.Configuration;

    /// <summary>
    ///     Interface that manages AMQP Exchanges via RabbitMQ Channel
    /// </summary>
    public interface IAmqpExchangeManager : IDisposable
    {
        /// <summary>
        ///     Determines if an Exchange ExchangeExists
        /// </summary>
        /// <param name="exchange">Exchange name</param>
        /// <returns>true if exchange exists</returns>
        bool ExchangeExists(string exchange);

        /// <summary>
        ///     Creates an Exchange
        /// </summary>
        /// <param name="exchangeConfiguration">exchange configuration to use when creating</param>
        void Create(ExchangeConfiguration exchangeConfiguration);

        /// <summary>
        ///     Binds or Unbinds an Exchange to another exchange
        /// </summary>
        /// <param name="exchangeBindingConfiguration">binding configuration to use when binding or unbinding</param>
        void Bind(ExchangeBindingConfiguration exchangeBindingConfiguration);

        /// <summary>
        ///     Deletes an Exchange
        /// </summary>
        /// <param name="exchange">Name of exchange to delete</param>
        /// <param name="ifUnused">
        ///     If set, the server will only delete the exchange if it has no queue bindings. If the exchange
        ///     has queue bindings the server does not delete it but raises a channel exception instead.
        /// </param>
        void Delete(string exchange, bool ifUnused);
    }
}