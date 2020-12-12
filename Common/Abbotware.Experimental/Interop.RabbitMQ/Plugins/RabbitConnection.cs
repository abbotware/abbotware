// -----------------------------------------------------------------------
// <copyright file="RabbitConnection.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
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
    using Abbotware.Core.Logging;
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
        /// <param name="logger">injected logger</param>
        public RabbitConnection(IRabbitMQClientIConnection connection, ILogger logger)
            : base(logger)
        {
            Arguments.NotNull(connection, nameof(connection));
            Arguments.NotNull(logger, nameof(logger));

            this.rabbitMQConnection = connection;

#pragma warning disable CA1062 // Validate arguments of public methods
            this.rabbitMQConnection.ConnectionShutdown += this.OnConnectionShutdown;
#pragma warning restore CA1062 // Validate arguments of public methods
            this.rabbitMQConnection.CallbackException += this.OnCallbackException;
            this.rabbitMQConnection.ConnectionBlocked += this.OnConnectionBlocked;
            this.rabbitMQConnection.ConnectionUnblocked += this.OnConnectionUnblocked;

            //// connection.CloseReason;
            //// connection.IsOpen;
            //// connection.AutoClose
            this.Logger.Debug("CONNECTION INFO: IsOpen:{0} CloseReason:{2}", this.rabbitMQConnection.IsOpen, this.rabbitMQConnection.CloseReason);
            Debug.Assert(this.rabbitMQConnection.IsOpen == (this.rabbitMQConnection.CloseReason == null), "according to documentation, these checks are identical");

            //// connection.Heartbeat;
            //// connection.FrameMax;
            //// connection.ChannelMax;
            this.Logger.Info("CONNECTION INFO:  HeartBeat:{0} ChannelMax:{1} FrameMax:{2}", this.rabbitMQConnection.Heartbeat, this.rabbitMQConnection.ChannelMax, this.rabbitMQConnection.FrameMax);

            //// connection.Endpoint;
            //// connection.KnownHosts;
            //// connection.Protocol;
            //// connection.LocalPort;
            //// connection.RemotePort;
            this.Logger.Info("CONNECTION INFO:  Endpoint:{0} Protocol:{1} KnownHosts:{2} LocalPort:{3} RemotePort:{4}", this.rabbitMQConnection.Endpoint, this.rabbitMQConnection.Protocol, this.rabbitMQConnection.KnownHosts, this.rabbitMQConnection.LocalPort, this.rabbitMQConnection.RemotePort);

            // connection.ClientProperties;
            this.Logger.Info("CONNECTION INFO:  ClientProperties:[{0}] ", this.rabbitMQConnection.ClientProperties.StringFormat());

            // connection.ServerProperties;
            this.Logger.Info("CONNECTION INFO:  ServerProperties:[{0}] ", this.rabbitMQConnection.ServerProperties.StringFormat());

            ////this.Logger.Info("CONNECTION INFO:  ConsumerWorkService:[{0}] ", this.rabbitMQConnection.ConsumerWorkService);

            var shutdownReport = string.Join(", ", this.rabbitMQConnection.ShutdownReport.Select(x => $"Description{x.Description} Ex:{x.Exception}"));

            this.Logger.Info("CONNECTION INFO:  ShutdownReport:[{0}] ", shutdownReport);

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

            using var auto = new AutoFactory<QueueManager>(() => new QueueManager(this.defaultChannelConfiguration, this.rabbitMQConnection.CreateModel(), this.Logger.Create("Channel")));

            return auto.Return();
        }

        /// <inheritdoc />
        public IAmqpExchangeManager CreateExchangeManager()
        {
            this.InitializeIfRequired();

            using var auto = new AutoFactory<ExchangeManager>(() => new ExchangeManager(this.defaultChannelConfiguration, this.rabbitMQConnection.CreateModel(), this.Logger.Create("Channel")));

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

            using var auto = new AutoFactory<RabbitPublisher>(() => new RabbitPublisher(channelConfiguration, this.rabbitMQConnection.CreateModel(), this.Logger.Create("PublishManager")));

            return auto.Return();
        }

        /// <inheritdoc />
        public IAmqpRetriever CreateRetriever()
        {
            var channel = this.CreateChannel();

            var ret = new RabbitRetriever(this.defaultChannelConfiguration, channel, this.Logger.Create("MessageRetrievalManager"));

            return ret;
        }

        /// <inheritdoc />
        public IAmqpConsumerManager<IBasicConsumer> CreateConsumerManager(ChannelConfiguration channelConfiguration)
        {
            this.InitializeIfRequired();

            using var auto = new AutoFactory<ConsumerManager>(() => new ConsumerManager(channelConfiguration, this.rabbitMQConnection.CreateModel(), this.Logger.Create("ConsumerManager")));

            return auto.Return();
        }

        /// <inheritdoc />
        public IAmqpConsumerManager<IBasicConsumer> CreateConsumerManager()
        {
            this.InitializeIfRequired();

            using var auto = new AutoFactory<ConsumerManager>(() => new ConsumerManager(this.defaultChannelConfiguration, this.rabbitMQConnection.CreateModel(), this.Logger.Create("ConsumerManager")));

            return auto.Return();
        }

        /// <inheritdoc />
        public IAmqpAcknowledger CreateAcknowledger()
        {
            var channel = this.CreateChannel();

            var ret = new RabbitAcknowledger(this.defaultChannelConfiguration, channel, this.Logger.Create("MessageRetrievalManager"));

            return ret;
        }

        /// <inheritdoc />
        public IAmqpResourceManager CreateResourceManager()
        {
            this.InitializeIfRequired();

            using var auto = new AutoFactory<ResourceManager>(() => new ResourceManager(this.CreateQueueManager(), this.CreateExchangeManager(), this.Logger.Create("ResourceManager")));

            return auto.Return();
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)", Justification = "ignored")]
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
        private void OnConnectionUnblocked(object sender, EventArgs eventArgs)
        {
            this.Logger.Info("OnConnectionBlocked Sender:{0} Args:{1} ", sender, eventArgs);
        }

        /// <summary>
        ///     Callback logic for handling the 'Connection Blocked' event
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="eventArgs">event arguments</param>
        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs eventArgs)
        {
            this.Logger.Info("OnConnectionBlocked Sender:{0} Reason:{1} ", sender, eventArgs.Reason);
        }

        /// <summary>
        ///     Callback logic for handling the 'Connection Shutdown' event
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="eventArgs">event arguments</param>
        private void OnConnectionShutdown(object sender, ShutdownEventArgs eventArgs)
        {
            this.Logger.Info("OnConnectionShutdown Sender:{0} Cause:{1} ClassId:{2}, Initiator:{3} Method Id:{4} Reply Code:{5}, Reply Text:{6}", sender, eventArgs.Cause, eventArgs.ClassId, eventArgs.Initiator, eventArgs.MethodId, eventArgs.ReplyCode, eventArgs.ReplyText);
        }

        /// <summary>
        ///     Callback logic for handling the 'Callback Exception' event
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="eventArgs">event arguments</param>
        private void OnCallbackException(object sender, CallbackExceptionEventArgs eventArgs)
        {
            this.Logger.Error("OnCallbackException Sender:{0} Detail:{1} Exception:{2}", sender, eventArgs.Detail.StringFormat(), eventArgs.Exception);
        }
    }
}