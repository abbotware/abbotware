// -----------------------------------------------------------------------
// <copyright file="QueueManager.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.RabbitMQ.Plugins
{
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
    ///     Channel manager used for queue operations
    /// </summary>
    public class QueueManager : BaseChannelManager, IAmqpQueueManager
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="QueueManager" /> class.
        /// </summary>
        /// <param name="configuration">channel configuration</param>
        /// <param name="rabbitMQChannel">RabbitMQ channel/model</param>
        /// <param name="logger">injected logger</param>
        public QueueManager(ChannelConfiguration configuration, IModel rabbitMQChannel, ILogger logger)
            : base(configuration, rabbitMQChannel, logger)
        {
            Arguments.NotNull(configuration, nameof(configuration));
            Arguments.NotNull(rabbitMQChannel, nameof(rabbitMQChannel));
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <inheritdoc />
        public bool QueueExists(string queueName)
        {
            this.InitializeIfRequired();

            lock (this.Mutex)
            {
                this.ThrowIfDisposed();

                try
                {
                    var result = this.RabbitMQChannel.QueueDeclarePassive(queueName);

                    this.Logger.Debug("QUEUE EXISTS:'{0}' messages:{1}  consumers:{2}", result.QueueName, result.MessageCount, result.ConsumerCount);
                }
                catch (OperationInterruptedException)
                {
                    // TODO: this seems like a hack
                    this.Logger.Debug("QUEUE EXISTS:{0} - false", queueName);
                    return false;
                }

                return true;
            }
        }

        /// <inheritdoc />
        public QueueCreationConfiguration Create(QueueConfiguration queueConfiguration)
        {
            queueConfiguration = Arguments.EnsureNotNull(queueConfiguration, nameof(queueConfiguration));

            this.InitializeIfRequired();

            lock (this.Mutex)
            {
                this.ThrowIfDisposed();

                this.Logger.Debug($"QUEUE DECLARE:[{queueConfiguration?.Name}] durable:{queueConfiguration?.IsDurable} exclusive:{queueConfiguration?.IsExclusive} auto delete:{queueConfiguration?.IsAutoDelete} arguments:[{queueConfiguration?.Arguments?.StringFormat()}]");

                var queueDeclareResult = this.RabbitMQChannel.QueueDeclare(queueConfiguration.Name, queueConfiguration.IsDurable, queueConfiguration.IsExclusive, queueConfiguration.IsAutoDelete, queueConfiguration.Arguments);

                this.Logger.Debug($"QUEUE DECLARED:[{queueDeclareResult?.QueueName}] messages:{queueDeclareResult?.MessageCount} consumers:{queueDeclareResult?.ConsumerCount}");

                return new QueueCreationConfiguration(queueDeclareResult.QueueName, queueDeclareResult.MessageCount, queueDeclareResult.ConsumerCount);
            }
        }

        /// <inheritdoc />
        public void Bind(QueueBindingConfiguration queueBindingConfiguration)
        {
            queueBindingConfiguration = Arguments.EnsureNotNull(queueBindingConfiguration, nameof(queueBindingConfiguration));

            this.InitializeIfRequired();

            lock (this.Mutex)
            {
                this.ThrowIfDisposed();

                this.Logger.Debug($"QUEUE {queueBindingConfiguration?.Action}:[{queueBindingConfiguration?.DestinationQueue}] exchange:{queueBindingConfiguration?.SourceExchange} topic:{queueBindingConfiguration?.Topic} arguments:[{queueBindingConfiguration?.Arguments?.StringFormat()}]");

                if (queueBindingConfiguration.Action == BindingAction.Bind)
                {
                    this.RabbitMQChannel.QueueBind(queueBindingConfiguration.DestinationQueue, queueBindingConfiguration.SourceExchange, queueBindingConfiguration.Topic, queueBindingConfiguration.Arguments);
                }
                else
                {
                    this.RabbitMQChannel.QueueUnbind(queueBindingConfiguration.DestinationQueue, queueBindingConfiguration.SourceExchange, queueBindingConfiguration.Topic, queueBindingConfiguration.Arguments);
                }
            }
        }

        /// <inheritdoc />
        public uint Delete(string queueName, bool ifUnused, bool ifEmpty)
        {
            this.InitializeIfRequired();

            lock (this.Mutex)
            {
                this.ThrowIfDisposed();

                var result = this.RabbitMQChannel.QueueDelete(queueName, ifUnused, ifEmpty);
                this.Logger.Debug("QUEUE DELETE:[{0}] with {1} messages", queueName, result);
                return result;
            }
        }

        /// <inheritdoc />
        public uint Purge(string queueName)
        {
            this.InitializeIfRequired();

            lock (this.Mutex)
            {
                this.ThrowIfDisposed();

                var result = this.RabbitMQChannel.QueuePurge(queueName);
                this.Logger.Debug("QUEUE PURGE:[{0}] Purged {1} messages", queueName, result);
                return result;
            }
        }
    }
}