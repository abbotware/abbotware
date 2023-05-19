// -----------------------------------------------------------------------
// <copyright file="RabbitPublisher.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.RabbitMQ.Plugins
{
    using System;
    using System.Collections.Concurrent;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging;
    using Abbotware.Core.Messaging.Amqp.Configuration;
    using Abbotware.Core.Messaging.Amqp.ExtensionPoints;
    using Abbotware.Core.Messaging.Configuration;
    using Abbotware.Core.Messaging.Configuration.Models;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Messaging.Integration.Configuration;
    using Abbotware.Core.Messaging.Integration.Configuration.Models;
    using Abbotware.Core.Threading.Counters;
    using Abbotware.Interop.RabbitMQ.ExtensionPoints;
    using Abbotware.Interop.RabbitMQ.Extensions;
    using global::RabbitMQ.Client;
    using global::RabbitMQ.Client.Events;

    /// <summary>
    ///     Channel manager used for publishing operations
    /// </summary>
    public class RabbitPublisher : BaseChannelManager, IAmqpPublisher
    {
        /// <summary>
        ///     counter of last confirmed sequence number
        /// </summary>
        private readonly AtomicCounter lastConfirmedSequenceNumber = new();

        /// <summary>
        ///     counter for the next confirmation sequence number
        /// </summary>
        private readonly AtomicCounter nextConfirmationSequenceNumber = new();

        /// <summary>
        ///     collection of outstanding publishes
        /// </summary>
        private readonly ConcurrentDictionary<ulong, TaskCompletionSource<PublishStatus>> outstandingConfirmations = new();

        /// <summary>
        ///     Counter for messages published
        /// </summary>
        private readonly AtomicCounter published = new();

        /// <summary>
        ///     Counter for returned messages
        /// </summary>
        private readonly AtomicCounter returnedMessages = new();

        /// <summary>
        ///     Counter for messages returned because no matching consumer
        /// </summary>
        private readonly AtomicCounter returnedNoConsummers = new();

        /// <summary>
        ///     Counter for messages returned because no matching route
        /// </summary>
        private readonly AtomicCounter returnedNoRoute = new();

        /// <summary>
        ///     Initializes a new instance of the <see cref="RabbitPublisher" /> class.
        /// </summary>
        /// <param name="configuration">channel configuration</param>
        /// <param name="rabbitMQChannel">RabbitMQ channel/model</param>
        /// <param name="logger">injected logger</param>
        public RabbitPublisher(ChannelConfiguration configuration, IModel rabbitMQChannel, ILogger logger)
            : base(configuration, rabbitMQChannel, logger)
        {
            Arguments.NotNull(configuration, nameof(configuration));
            Arguments.NotNull(rabbitMQChannel, nameof(rabbitMQChannel));
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <summary>
        ///     Gets the number of messaged published
        /// </summary>
        public long PublishedMessages => this.published.Value;

        /// <summary>
        ///     Gets the number of returned messages
        /// </summary>
        public long ReturnedMessages => this.returnedMessages.Value;

        /// <summary>
        ///     Gets the number of returned messages because there was no route
        /// </summary>
        public long ReturnedNoRoute => this.returnedNoRoute.Value;

        /// <summary>
        ///     Gets the number of returned messages because there was no consumer
        /// </summary>
        public long ReturnedNoConsumers => this.returnedNoConsummers.Value;

        /// <inheritdoc />
        public Task<PublishStatus> Publish(string exchange, string topic, bool mandatory, byte[] body)
        {
            var envelope = new MessageEnvelope();
            envelope.PublishProperties.Exchange = exchange;
            envelope.PublishProperties.RoutingKey = topic;
            envelope.PublishProperties.Mandatory = mandatory;
            envelope.Body = body;

            return this.Publish(envelope);
        }

        /// <inheritdoc />
        public Task<PublishStatus> Publish(byte[] body, IPublishProperties properties)
        {
            Arguments.NotNull(body, nameof(body));
            Arguments.NotNull(properties, nameof(properties));

            var envelope = new MessageEnvelope();

            envelope.PublishProperties.Exchange = properties.Exchange;
            envelope.PublishProperties.RoutingKey = properties.RoutingKey;
            envelope.PublishProperties.Mandatory = properties.Mandatory;
            envelope.PublishProperties.Persistent = properties.Persistent;
            envelope.Body = body;

            return this.Publish(envelope);
        }

        /// <inheritdoc />
        public Task<PublishStatus> Publish(IMessageEnvelope envelope)
        {
            Arguments.NotNull(envelope, nameof(envelope));

            this.InitializeIfRequired();

            var properties = this.RabbitMQChannel.CreateBasicProperties();

            EnvelopeBuilder.UpdateBasicProperties(properties, envelope);

            lock (this.Mutex)
            {
                this.ThrowIfDisposed();

                this.published.Increment();

                if (this.Configuration.Mode == ChannelMode.TransactionMode)
                {
                    this.RabbitMQChannel.TxSelect();
                }

                if (properties == null)
                {
                    properties = this.RabbitMQChannel.CreateBasicProperties();
                }

                var sequenceNumber = this.NextSequenceNumber();

                properties.MessageId = sequenceNumber.ToString(CultureInfo.InvariantCulture);

                var tcs = new TaskCompletionSource<PublishStatus>();

                if (this.Configuration.Mode == ChannelMode.ConfirmationMode)
                {
                    var tryAddResult = this.outstandingConfirmations.TryAdd(sequenceNumber, tcs);

                    Debug.Assert(tryAddResult, "TryAdd for outstandingConfirmations failed");
                }

                this.RabbitMQChannel.BasicPublish(envelope.PublishProperties.Exchange, envelope.PublishProperties.RoutingKey, envelope.PublishProperties.Mandatory, properties, envelope.Body.ToArray());

                switch (this.Configuration.Mode)
                {
                    case ChannelMode.ConfirmationMode:
                        {
                            return tcs.Task;
                        }

                    case ChannelMode.TransactionMode:
                        {
                            this.RabbitMQChannel.TxCommit();
                            break;
                        }

                    case ChannelMode.None:
                        {
                            break;
                        }

                    default:
                        {
                            throw new InvalidOperationException("unexpected channel mode, this is a software bug");
                        }
                }

#pragma warning disable CA2008 // Do not create tasks without passing a TaskScheduler
                return Task.Factory.StartNew(() => { return PublishStatus.Unknown; });
#pragma warning restore CA2008 // Do not create tasks without passing a TaskScheduler
            }
        }

        /// <summary>
        ///     Gets next message sequence number
        /// </summary>
        /// <returns>next sequence number</returns>
        protected ulong NextSequenceNumber()
        {
            var sequenceNumber = (ulong)this.nextConfirmationSequenceNumber.Increment();

            if (this.Configuration.Mode == ChannelMode.ConfirmationMode)
            {
                Debug.Assert(sequenceNumber == this.RabbitMQChannel.NextPublishSeqNo, "sequence number mismatch");
            }

            return sequenceNumber;
        }

        /// <inheritdoc />
        protected override void OnBasicReturn(object? sender, BasicReturnEventArgs? eventArgs)
        {
            eventArgs = Arguments.EnsureNotNull(eventArgs, nameof(eventArgs));

            this.Logger.Warn($"OnBasicReturn Exchange:{eventArgs?.Exchange} RoutingKey:{eventArgs?.RoutingKey}, ReplyCode:{eventArgs?.ReplyCode} ReplyText:{eventArgs?.ReplyText} BasicProps:[{eventArgs?.BasicProperties?.ToFormatString()}]");

            switch (eventArgs!.ReplyCode)
            {
                case 312:
                    {
                        this.returnedNoRoute.Increment();
                        break;
                    }

                case 313:
                    {
                        this.returnedNoConsummers.Increment();
                        break;
                    }
            }

            this.returnedMessages.Increment();

            switch (this.Configuration.Mode)
            {
                case ChannelMode.ConfirmationMode:
                    {
                        var currentseqNo = (ulong)this.lastConfirmedSequenceNumber.Increment();

                        if (!this.outstandingConfirmations.TryRemove(currentseqNo, out var tcs))
                        {
                            this.Logger.Warn("TryRemove failed: currentseqNo:{0}", currentseqNo);
                            break;
                        }

                        tcs.SetResult(PublishStatus.Returned);

                        break;
                    }

                case ChannelMode.TransactionMode:
                    {
                        break;
                    }

                case ChannelMode.None:
                    {
                        break;
                    }

                default:
                    {
                        throw new InvalidOperationException("unexpected channel mode, this is a software bug");
                    }
            }
        }

        /// <inheritdoc />
        protected override void OnBasicAck(object? sender, BasicAckEventArgs? eventArgs)
        {
            eventArgs = Arguments.EnsureNotNull(eventArgs, nameof(eventArgs));

            this.Logger.Info($"OnBasicAcks: DeliveryTag:{eventArgs?.DeliveryTag} Multiple:{eventArgs?.Multiple} Outstanding:{this.outstandingConfirmations.Count}");

            switch (this.Configuration.Mode)
            {
                case ChannelMode.ConfirmationMode:
                    {
                        var confirmedSeqNo = (ulong)this.lastConfirmedSequenceNumber.Value;

                        while (confirmedSeqNo < eventArgs!.DeliveryTag)
                        {
                            confirmedSeqNo = (ulong)this.lastConfirmedSequenceNumber.Increment();

                            if (!this.outstandingConfirmations.TryRemove(confirmedSeqNo, out var tcs))
                            {
                                // may have already been removed by BasicReturn
                                var keys = string.Join(",", this.outstandingConfirmations.Keys.ToList().Select(x => x.ToString(CultureInfo.InvariantCulture)));
                                this.Logger.Warn("TryRemove SeqNo:{0}  Keys:{1}", eventArgs.DeliveryTag, keys);
                                continue;
                            }

                            tcs.SetResult(PublishStatus.Confirmed);
                        }

                        Debug.Assert(confirmedSeqNo == eventArgs.DeliveryTag, string.Format(CultureInfo.InvariantCulture, "unexpected args.DeliveryTag:{0} != confirmedSeqNo:{1}", eventArgs.DeliveryTag, confirmedSeqNo));
                        break;
                    }

                case ChannelMode.TransactionMode:
                    {
                        break;
                    }

                case ChannelMode.None:
                    {
                        break;
                    }

                default:
                    {
                        throw new InvalidOperationException("unexpected channel mode, this is a software bug");
                    }
            }
        }

        /// <inheritdoc />
        protected override void OnModelShutdown(object? sender, ShutdownEventArgs? eventArgs)
        {
            this.Logger.Info("channel_ModelShutdown:{0}", eventArgs);

            foreach (var k in this.outstandingConfirmations.ToList())
            {
                k.Value.SetResult(PublishStatus.Unknown);
            }
        }
    }
}