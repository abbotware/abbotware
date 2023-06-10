// -----------------------------------------------------------------------
// <copyright file="RabbitConnection.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.RabbitMQ.Plugins
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Messaging.Amqp.Configuration;
    using Abbotware.Core.Messaging.Amqp.ExtensionPoints;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.RabbitMQ.ExtensionPoints;
    using Abbotware.Interop.RabbitMQ.ExtensionPoints.Services;
    using global::RabbitMQ.Client;
    using global::RabbitMQ.Client.Events;
    using IRabbitMQClientIConnection = global::RabbitMQ.Client.IConnection;

    /// <summary>
    ///     Connection manager class
    /// </summary>
    public class RabbitConnection : BaseComponent, IRabbitConnection
    {
        private readonly ILoggerFactory factory;

        /// <summary>
        ///     RabbitMQ ConnectionFactory to create the initial connection
        /// </summary>
        private readonly ChannelConfiguration defaultChannelConfiguration = Templates.Channel.Default();

        /// <summary>
        ///     the wrapped RabbitMQ connection object
        /// </summary>
        private readonly IRabbitMQClientIConnection rabbitMQConnection;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RabbitConnection" /> class.
        /// </summary>
        /// <param name="connection">connection configuration</param>
        /// <param name="factory">injected logger factory</param>
        public RabbitConnection(IRabbitMQClientIConnection connection, ILoggerFactory factory)
            : base(factory.CreateLogger<RabbitConnection>())
        {
            Arguments.NotNull(connection, nameof(connection));
            this.factory = Arguments.EnsureNotNull(factory, nameof(factory));

            this.rabbitMQConnection = connection;

            this.rabbitMQConnection.ConnectionShutdown += this.OnConnectionShutdown;
            this.rabbitMQConnection.CallbackException += this.OnCallbackException;
            this.rabbitMQConnection.ConnectionBlocked += this.OnConnectionBlocked;
            this.rabbitMQConnection.ConnectionUnblocked += this.OnConnectionUnblocked;

            //// connection.CloseReason;
            //// connection.IsOpen;
            //// connection.AutoClose
            this.Logger.Debug($"CONNECTION INFO: IsOpen:{this.rabbitMQConnection.IsOpen} CloseReason:{this.rabbitMQConnection.CloseReason}");
            Debug.Assert(this.rabbitMQConnection.IsOpen == (this.rabbitMQConnection.CloseReason == null), "according to documentation, these checks are identical");

            //// connection.Heartbeat;
            //// connection.FrameMax;
            //// connection.ChannelMax;
            this.Logger.Info($"CONNECTION INFO:  HeartBeat:{this.rabbitMQConnection.Heartbeat} ChannelMax:{this.rabbitMQConnection.ChannelMax} FrameMax:{this.rabbitMQConnection.FrameMax}");

            //// connection.Endpoint;
            //// connection.KnownHosts;
            //// connection.Protocol;
            //// connection.LocalPort;
            //// connection.RemotePort;
            this.Logger.Info($"CONNECTION INFO:  Endpoint:{this.rabbitMQConnection.Endpoint} Protocol:{this.rabbitMQConnection.Protocol} KnownHosts:{this.rabbitMQConnection.KnownHosts} LocalPort:{this.rabbitMQConnection.LocalPort} RemotePort:{this.rabbitMQConnection.RemotePort}");

            // connection.ClientProperties;
            this.Logger.Info($"CONNECTION INFO:  ClientProperties:[{this.rabbitMQConnection.ClientProperties.StringFormat()}] ");

            // connection.ServerProperties;
            this.Logger.Info($"CONNECTION INFO:  ServerProperties:[{this.rabbitMQConnection.ServerProperties.StringFormat()}] ");

            ////this.Logger.Info("CONNECTION INFO:  ConsumerWorkService:[{0}] ", this.rabbitMQConnection.ConsumerWorkService);

            var shutdownReport = string.Join(", ", this.rabbitMQConnection.ShutdownReport.Select(x => $"Description{x.Description} Ex:{x.Exception}"));

            this.Logger.Info($"CONNECTION INFO:  ShutdownReport:[{shutdownReport}] ");

            Debug.Assert(this.rabbitMQConnection.IsOpen, "something is wrong, connection should be open by now");
        }

        /// <inheritdoc />
        public bool IsOpen
        {
            get { return this.rabbitMQConnection?.IsOpen ?? false; }
        }

        /// <inheritdoc />
        public IAmqpQueueManager CreateQueueManager()
        {
            this.InitializeIfRequired();

            using var auto = new AutoFactory<QueueManager>(() => new QueueManager(this.defaultChannelConfiguration, this.rabbitMQConnection.CreateModel(), this.factory.CreateLogger("Channel")));

            return auto.Return();
        }

        /// <inheritdoc />
        public IAmqpExchangeManager CreateExchangeManager()
        {
            this.InitializeIfRequired();

            using var auto = new AutoFactory<ExchangeManager>(() => new ExchangeManager(this.defaultChannelConfiguration, this.rabbitMQConnection.CreateModel(), this.factory.CreateLogger("Channel")));

            return auto.Return();
        }

        /// <inheritdoc />
        public IAmqpPublisher CreatePublishManager()
        {
            this.InitializeIfRequired();

            return this.CreatePublishManager(this.defaultChannelConfiguration);
        }

        /// <inheritdoc />
        public IAmqpPublisher CreatePublishManager(ChannelConfiguration channelConfiguration)
        {
            this.InitializeIfRequired();

            using var auto = new AutoFactory<RabbitPublisher>(() => new RabbitPublisher(channelConfiguration, this.rabbitMQConnection.CreateModel(), this.factory.CreateLogger("PublishManager")));

            return auto.Return();
        }

        /// <inheritdoc />
        public IAmqpRetriever CreateRetriever()
        {
            var channel = this.CreateChannel();

            var ret = new RabbitRetriever(this.defaultChannelConfiguration, channel, this.factory.CreateLogger("MessageRetrievalManager"));

            return ret;
        }

        /// <inheritdoc />
        public IAmqpConsumerManager<IBasicConsumer> CreateConsumerManager(ChannelConfiguration channelConfiguration)
        {
            this.InitializeIfRequired();

            using var auto = new AutoFactory<ConsumerManager>(() => new ConsumerManager(channelConfiguration, this.rabbitMQConnection.CreateModel(), this.factory.CreateLogger("ConsumerManager")));

            return auto.Return();
        }

        /// <inheritdoc />
        public IAmqpConsumerManager<IBasicConsumer> CreateConsumerManager()
        {
            this.InitializeIfRequired();

            using var auto = new AutoFactory<ConsumerManager>(() => new ConsumerManager(this.defaultChannelConfiguration, this.rabbitMQConnection.CreateModel(), this.factory.CreateLogger("ConsumerManager")));

            return auto.Return();
        }

        /// <inheritdoc />
        public IAmqpAcknowledger CreateAcknowledger()
        {
            var channel = this.CreateChannel();

            var ret = new RabbitAcknowledger(this.defaultChannelConfiguration, channel, this.factory.CreateLogger("MessageRetrievalManager"));

            return ret;
        }

        /// <inheritdoc />
        public IAmqpResourceManager CreateResourceManager()
        {
            this.InitializeIfRequired();

            using var auto = new AutoFactory<ResourceManager>(() => new ResourceManager(this.CreateQueueManager(), this.CreateExchangeManager(), this.factory.CreateLogger("ResourceManager")));

            return auto.Return();
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "reviewed")]
        protected override void OnInitialize()
        {
        }

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            if (this.rabbitMQConnection != null)
            {
                if (this.rabbitMQConnection.IsOpen)
                {
                    this.Logger.Info("Connection still open, closing");

                    this.rabbitMQConnection.Close(200, "Disposing");
                }

                this.rabbitMQConnection.ConnectionBlocked -= this.OnConnectionBlocked;
                this.rabbitMQConnection.ConnectionUnblocked -= this.OnConnectionUnblocked;
                this.rabbitMQConnection.ConnectionShutdown -= this.OnConnectionShutdown;
                this.rabbitMQConnection.CallbackException -= this.OnCallbackException;

                this.rabbitMQConnection.Dispose();
            }

            base.OnDisposeManagedResources();
        }

        private IModel CreateChannel()
        {
            this.InitializeIfRequired();

            return this.rabbitMQConnection.CreateModel();
        }

        /// <summary>
        ///     Callback logic for handling the 'Connection Unblocked' event
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="eventArgs">event arguments</param>
        private void OnConnectionUnblocked(object? sender, EventArgs? eventArgs)
        {
            this.Logger.Info($"OnConnectionBlocked Sender:{sender} Args:{eventArgs} ");
        }

        /// <summary>
        ///     Callback logic for handling the 'Connection Blocked' event
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="eventArgs">event arguments</param>
        private void OnConnectionBlocked(object? sender, ConnectionBlockedEventArgs? eventArgs)
        {
            this.Logger.Info($"OnConnectionBlocked Sender:{sender} Reason:{eventArgs?.Reason} ");
        }

        /// <summary>
        ///     Callback logic for handling the 'Connection Shutdown' event
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="eventArgs">event arguments</param>
        private void OnConnectionShutdown(object? sender, ShutdownEventArgs? eventArgs)
        {
            this.Logger.Info($"OnConnectionShutdown Sender:{sender} Cause:{eventArgs?.Cause} ClassId:{eventArgs?.ClassId}, Initiator:{eventArgs?.Initiator} Method Id:{eventArgs?.MethodId} Reply Code:{eventArgs?.ReplyCode}, Reply Text:{eventArgs?.ReplyText}");
        }

        /// <summary>
        ///     Callback logic for handling the 'Callback Exception' event
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="eventArgs">event arguments</param>
        private void OnCallbackException(object? sender, CallbackExceptionEventArgs? eventArgs)
        {
            this.Logger.Error($"OnCallbackException Sender:{sender} Detail:{eventArgs?.Detail?.StringFormat()} Exception:{eventArgs?.Exception}");
        }
    }
}