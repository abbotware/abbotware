// -----------------------------------------------------------------------
// <copyright file="ExchangeManager.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.RabbitMQ.Plugins
{
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging.Amqp.Configuration;
    using Abbotware.Core.Messaging.Amqp.ExtensionPoints;
    using Abbotware.Interop.RabbitMQ.ExtensionPoints;
    using global::RabbitMQ.Client;
    using global::RabbitMQ.Client.Exceptions;

    /// <summary>
    /// Channel manager used for exchange operations
    /// </summary>
    public class ExchangeManager : BaseChannelManager, IAmqpExchangeManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExchangeManager"/> class.
        /// </summary>
        /// <param name="configuration">channel configuration</param>
        /// <param name="rabbitMQChannel">RabbitMQ channel/model</param>
        /// <param name="logger">injected logger</param>
        public ExchangeManager(ChannelConfiguration configuration, IModel rabbitMQChannel, ILogger logger)
            : base(configuration, rabbitMQChannel, logger)
        {
            Arguments.NotNull(configuration, nameof(configuration));
            Arguments.NotNull(rabbitMQChannel, nameof(rabbitMQChannel));
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <inheritdoc />
        public bool ExchangeExists(string exchange)
        {
            exchange = Arguments.EnsureNotNullOrWhitespace(exchange, nameof(exchange));

            this.InitializeIfRequired();

            lock (this.Mutex)
            {
                this.ThrowIfDisposed();

                try
                {
                    this.RabbitMQChannel.ExchangeDeclarePassive(exchange);
                    this.Logger.Debug("EXCHANGE EXISTS:{0} ", exchange);
                }
                catch (OperationInterruptedException)
                {
                    // TODO: this seems like a hack
                    this.Logger.Debug("EXCHANGE EXISTS:{0} - false", exchange);
                    return false;
                }

                return true;
            }
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Matching RabbitMQ Docs")]
        public void Create(ExchangeConfiguration exchangeConfiguration)
        {
            exchangeConfiguration = Arguments.EnsureNotNull(exchangeConfiguration, nameof(exchangeConfiguration));

            this.InitializeIfRequired();

            lock (this.Mutex)
            {
                this.ThrowIfDisposed();

                this.Logger.Debug($"EXCHANGE DECLARE:{exchangeConfiguration?.Name} type:{exchangeConfiguration?.ExchangeType} durable:{exchangeConfiguration?.IsDurable} auto delete:{exchangeConfiguration?.IsAutoDelete} arguments:[{exchangeConfiguration?.Arguments?.StringFormat()}]");

                this.RabbitMQChannel.ExchangeDeclare(exchangeConfiguration!.Name, exchangeConfiguration.ExchangeType.ToString().ToLower(CultureInfo.InvariantCulture), exchangeConfiguration.IsDurable, exchangeConfiguration.IsAutoDelete, exchangeConfiguration.Arguments);
            }
        }

        /// <inheritdoc />
        public void Bind(ExchangeBindingConfiguration exchangeBindingConfiguration)
        {
            exchangeBindingConfiguration = Arguments.EnsureNotNull(exchangeBindingConfiguration, nameof(exchangeBindingConfiguration));

            this.InitializeIfRequired();

            lock (this.Mutex)
            {
                this.ThrowIfDisposed();

                this.Logger.Debug($"EXHANGE {exchangeBindingConfiguration?.Action}: source-exchange:{exchangeBindingConfiguration?.SourceExchange} destination-exchange:{exchangeBindingConfiguration?.DestinationExchange} topic:{exchangeBindingConfiguration?.Topic} arguments:[{exchangeBindingConfiguration?.Arguments?.StringFormat()}]");

                if (exchangeBindingConfiguration!.Action == BindingAction.Bind)
                {
                    this.RabbitMQChannel.ExchangeBind(exchangeBindingConfiguration.DestinationExchange, exchangeBindingConfiguration.SourceExchange, exchangeBindingConfiguration.Topic, exchangeBindingConfiguration.Arguments);
                }
                else
                {
                    this.RabbitMQChannel.ExchangeUnbind(exchangeBindingConfiguration.DestinationExchange, exchangeBindingConfiguration.SourceExchange, exchangeBindingConfiguration.Topic, exchangeBindingConfiguration.Arguments);
                }
            }
        }

        /// <inheritdoc />
        public void Delete(string exchange, bool ifUnused)
        {
            exchange = Arguments.EnsureNotNullOrWhitespace(exchange, nameof(exchange));

            this.InitializeIfRequired();

            lock (this.Mutex)
            {
                this.ThrowIfDisposed();

                this.Logger.Debug("EXHANGE DELETE:{0} IfUnused:{1}", exchange, ifUnused);
                this.RabbitMQChannel.ExchangeDelete(exchange, ifUnused);
            }
        }
    }
}