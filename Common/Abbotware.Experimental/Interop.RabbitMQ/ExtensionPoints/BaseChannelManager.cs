// -----------------------------------------------------------------------
// <copyright file="BaseChannelManager.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.RabbitMQ.ExtensionPoints
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging.Amqp.Configuration;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.RabbitMQ.Extensions;
    using global::RabbitMQ.Client;
    using global::RabbitMQ.Client.Events;

    /// <summary>
    ///     base class for channel managers
    /// </summary>
    public abstract class BaseChannelManager : BaseComponent<ChannelConfiguration>
    {
        /// <summary>
        ///     internal RabbitMQ Channel / Model
        /// </summary>
        private readonly IModel rabbitMQChannel;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseChannelManager" /> class.
        /// </summary>
        /// <param name="configuration">connection configuration</param>
        /// <param name="rabbitMQChannel">wrapped RabbitMQ Channel</param>
        /// <param name="logger">injected logger</param>
        protected BaseChannelManager(ChannelConfiguration configuration, IModel rabbitMQChannel, ILogger logger)
            : base(configuration, logger)
        {
            Arguments.NotNull(configuration, nameof(configuration));
            Arguments.NotNull(rabbitMQChannel, nameof(rabbitMQChannel));
            Arguments.NotNull(logger, nameof(logger));

            this.rabbitMQChannel = rabbitMQChannel;
        }

        /// <summary>
        ///     Gets the underlying channel number
        /// </summary>
        public int ChannelId => this.rabbitMQChannel.ChannelNumber;

        /// <summary>
        ///     Gets a value indicating whether or not the channel is open/usable
        /// </summary>
        public bool IsOpen => this.rabbitMQChannel.IsOpen;

        /// <summary>
        ///     Gets the RabbitMQ Client Channel/Model
        /// </summary>
        protected IModel RabbitMQChannel => this.rabbitMQChannel;

        /// <summary>
        ///     Gets the internal sync object
        /// </summary>
        protected object Mutex { get; } = new object();

        /// <inheritdoc />
        protected override void OnInitialize()
        {
            switch (this.Configuration.Mode)
            {
                case ChannelMode.ConfirmationMode:
                    {
                        this.Logger.Info("Setting Confirmation Mode");
                        this.rabbitMQChannel.ConfirmSelect();
                        break;
                    }

                case ChannelMode.TransactionMode:
                    {
                        this.Logger.Info("Setting Transaction Mode");
                        this.rabbitMQChannel.TxSelect();
                        break;
                    }

                case ChannelMode.None:
                    {
                        this.Logger.Info("Channel Mode unspecified");
                        break;
                    }

                default:
                    {
                        throw new InvalidOperationException("unexpected channel mode, this is a software bug");
                    }
            }

            var qos = this.Configuration.QualityOfService;

            if (qos != null)
            {
                this.Logger.Info("Setting up QOS: PreFetchSize:{0} PreFetchCount:{1} PreFetchGlobal{2}", qos.PreFetchSize, qos.PreFetchCount, qos.PreFetchGlobal);
                this.rabbitMQChannel.BasicQos(qos.PreFetchSize.Value, qos.PreFetchCount.Value, qos.PreFetchGlobal.Value);
            }

            this.rabbitMQChannel.BasicAcks += this.OnBasicAck;
            this.rabbitMQChannel.BasicNacks += this.OnBasicNack;
            this.rabbitMQChannel.BasicRecoverOk += this.OnBasicRecoverOk;
            this.rabbitMQChannel.BasicReturn += this.OnBasicReturn;
            this.rabbitMQChannel.CallbackException += this.OnCallbackException;
            this.rabbitMQChannel.FlowControl += this.OnFlowControl;
            this.rabbitMQChannel.ModelShutdown += this.OnModelShutdown;
            base.OnInitialize();
        }

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            this.RemoveChannel("disposing");

            base.OnDisposeManagedResources();
        }

        /// <summary>
        ///     Removes the channel from the connection
        /// </summary>
        /// <param name="reason">reason for removing channel</param>
        protected void RemoveChannel(string reason)
        {
            if (this.rabbitMQChannel.IsOpen)
            {
                this.rabbitMQChannel.Close(200, reason);
            }

            this.rabbitMQChannel.BasicAcks -= this.OnBasicAck;
            this.rabbitMQChannel.BasicNacks -= this.OnBasicNack;
            this.rabbitMQChannel.BasicRecoverOk -= this.OnBasicRecoverOk;
            this.rabbitMQChannel.BasicReturn -= this.OnBasicReturn;
            this.rabbitMQChannel.CallbackException -= this.OnCallbackException;
            this.rabbitMQChannel.FlowControl -= this.OnFlowControl;
            this.rabbitMQChannel.ModelShutdown -= this.OnModelShutdown;

            this.rabbitMQChannel?.Dispose();
        }

        /// <summary>
        ///     Callback logic for handling the 'Basic Ack' event
        /// </summary>
        /// <param name="sender">source IModel</param>
        /// <param name="eventArgs">event arguments</param>
        protected virtual void OnBasicAck(object sender, BasicAckEventArgs eventArgs)
        {
            this.Logger.Info($"OnBasicAcks: DeliveryTag:{eventArgs?.DeliveryTag} Multiple:{eventArgs?.Multiple}");

            Debug.Assert(false, "This channel should not receieve ack'd messages");
        }

        /// <summary>
        ///     Callback logic for handling the 'Basic Return' event
        /// </summary>
        /// <param name="sender">source IModel</param>
        /// <param name="eventArgs">event arguments</param>
        protected virtual void OnBasicReturn(object sender, BasicReturnEventArgs eventArgs)
        {
            this.Logger.Warn($"OnBasicReturn Exchange:{eventArgs?.Exchange} RoutingKey:{eventArgs?.RoutingKey}, ReplyCode:{eventArgs?.ReplyCode} ReplyText:{eventArgs?.ReplyText} BasicProps:[{eventArgs?.BasicProperties?.ToFormatString()}]");

            Debug.Assert(false, "This channel should not receieve returned messages");
        }

        /// <summary>
        ///     Callback logic for handling the 'Shutdown' event
        /// </summary>
        /// <param name="sender">source IModel</param>
        /// <param name="eventArgs">event arguments</param>
        protected virtual void OnModelShutdown(object sender, ShutdownEventArgs eventArgs)
        {
            this.Logger.Info("channel_ModelShutdown:{0}", eventArgs);
        }

        /// <summary>
        ///     Callback logic for handling the 'Flow Control' event
        /// </summary>
        /// <param name="sender">source IModel</param>
        /// <param name="eventArgs">event arguments</param>
        private void OnFlowControl(object sender, FlowControlEventArgs eventArgs)
        {
            this.Logger.Info("OnFlowControl Active:{0}", eventArgs.Active);
        }

        /// <summary>
        ///     Callback logic for handling the 'Callback Exception' event
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="eventArgs">event arguments</param>
        private void OnCallbackException(object sender, CallbackExceptionEventArgs eventArgs)
        {
            this.Logger.Error("OnCallbackException Detail:{0} Exception:{1}", eventArgs.Detail.StringFormat(), eventArgs.Exception);
        }

        /// <summary>
        ///     Callback logic for handling the 'Basic Recover Ok' event
        /// </summary>
        /// <param name="sender">sender IModel</param>
        /// <param name="eventArgs">event arguments</param>
        private void OnBasicRecoverOk(object sender, EventArgs eventArgs)
        {
            this.Logger.Info("OnBasicRecoverOk");
        }

        /// <summary>
        ///     Callback logic for handling the 'Basic Nack' event
        /// </summary>
        /// <param name="sender">source IModel</param>
        /// <param name="eventArgs">event arguments</param>
        private void OnBasicNack(object sender, BasicNackEventArgs eventArgs)
        {
            this.Logger.Warn("OnBasicNacks: DeliveryTag:{0} Multiple:{1} Requeue:{2}", eventArgs.DeliveryTag, eventArgs.Multiple, eventArgs.Requeue);
        }
    }
}