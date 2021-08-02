// -----------------------------------------------------------------------
// <copyright file="ResourceManager.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.RabbitMQ.Plugins
{
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging.Amqp.Configuration;
    using Abbotware.Core.Messaging.Amqp.ExtensionPoints;
    using Abbotware.Core.Objects;

    /// <summary>
    ///     Channel manager used for queue and exchange operations
    /// </summary>
    public class ResourceManager : BaseComponent, IAmqpResourceManager
    {
        /// <summary>
        /// injected exchange manager
        /// </summary>
        private readonly IAmqpExchangeManager exchangeManager;

        /// <summary>
        /// injected queue manager
        /// </summary>
        private readonly IAmqpQueueManager queueManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceManager"/> class.
        /// </summary>
        /// <param name="queueManager">channel for queue operations</param>
        /// <param name="exchangeManager">channel for exchange operations</param>
        /// <param name="logger">injected logger</param>
        public ResourceManager(IAmqpQueueManager queueManager, IAmqpExchangeManager exchangeManager, ILogger logger)
            : base(logger)
        {
            Arguments.NotNull(queueManager, nameof(queueManager));
            Arguments.NotNull(exchangeManager, nameof(exchangeManager));
            Arguments.NotNull(logger, nameof(logger));

            this.exchangeManager = exchangeManager;
            this.queueManager = queueManager;
        }

        /// <inheritdoc />
        public bool ExchangeExists(string exchange)
        {
            return this.exchangeManager.ExchangeExists(exchange);
        }

        /// <inheritdoc />
        public void Create(ExchangeConfiguration exchangeConfiguration)
        {
            this.exchangeManager.Create(exchangeConfiguration);
        }

        /// <inheritdoc />
        public void Bind(ExchangeBindingConfiguration exchangeBindingConfiguration)
        {
            this.exchangeManager.Bind(exchangeBindingConfiguration);
        }

        /// <inheritdoc />
        public void Delete(string exchange, bool ifUnused)
        {
            this.exchangeManager.Delete(exchange, ifUnused);
        }

        /// <inheritdoc />
        public bool QueueExists(string queueName)
        {
            return this.queueManager.QueueExists(queueName);
        }

        /// <inheritdoc />
        public QueueCreationConfiguration Create(QueueConfiguration queueConfiguration)
        {
            return this.queueManager.Create(queueConfiguration);
        }

        /// <inheritdoc />
        public void Bind(QueueBindingConfiguration queueBindingConfiguration)
        {
            this.queueManager.Bind(queueBindingConfiguration);
        }

        /// <inheritdoc />
        public uint Delete(string queueName, bool ifUnused, bool ifEmpty)
        {
            return this.queueManager.Delete(queueName, ifUnused, ifEmpty);
        }

        /// <inheritdoc />
        public uint Purge(string queueName)
        {
            return this.queueManager.Purge(queueName);
        }

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            this.queueManager?.Dispose();
            this.exchangeManager?.Dispose();

            base.OnDisposeManagedResources();
        }
    }
}