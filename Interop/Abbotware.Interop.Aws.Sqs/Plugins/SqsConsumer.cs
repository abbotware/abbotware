// -----------------------------------------------------------------------
// <copyright file="SqsConsumer.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Sqs.Plugins
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Interop.Castle.ExtensionPoints;

    /// <summary>
    ///     Base class for consuming messages from a RabbitMQ Channel
    /// </summary>
    public class SqsConsumer : PollingComponent, IBasicConsumer
    {
        private readonly SqsRetriever retriever;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SqsConsumer" /> class.
        /// </summary>
        /// <param name="retriever">injected retriever</param>
        /// <param name="logger">injected logger</param>
        public SqsConsumer(SqsRetriever retriever, ILogger logger)
            : base(TimeSpan.FromSeconds(10), logger)
        {
            Arguments.NotNull(retriever, nameof(retriever));
            Arguments.NotNull(logger, nameof(logger));

            this.Status = ConsumerStatus.Unknown;
            this.retriever = retriever;
        }

        /// <inheritdoc/>
        public event EventHandler<DeliveryEventArgs> OnDelivery;

        /// <summary>
        ///     Gets the current state of the Consumer
        /// </summary>
        public ConsumerStatus Status { get; private set; }

        /// <inheritdoc/>
        public uint Delivered { get; private set; }

        /// <summary>
        /// Gets the number of empty gets
        /// </summary>
        public uint EmptyGets { get; private set; }

        /// <inheritdoc/>
        protected override async Task OnIterationAsync()
        {
            while (true)
            {
                var msgs = await this.retriever.Get(this.retriever.Configuration.Queue, false).ConfigureAwait(false);

                foreach (var m in msgs)
                {
                    this.OnDelivery(this, new DeliveryEventArgs(m));

                    ++this.Delivered;
                }

                if (!msgs.Any())
                {
                    ++this.EmptyGets;
                }

                if (msgs.Count() < 10)
                {
                    break;
                }
            }
        }

        /// <inheritdoc/>
        protected override void OnStart()
        {
            this.Status = ConsumerStatus.Running;

            base.OnStart();
        }

        /// <inheritdoc/>
        protected override void OnStop()
        {
            this.Status = ConsumerStatus.CancelRequested;

            base.OnStop();
        }

        /// <inheritdoc/>
        protected override void OnDisposeManagedResources()
        {
            this.Stop();

            base.OnDisposeManagedResources();
        }
    }
}