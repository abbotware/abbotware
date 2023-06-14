// -----------------------------------------------------------------------
// <copyright file="BufferedTimestreamPublisher{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Channels;
    using System.Threading.Tasks;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Threading;
    using Abbotware.Interop.Aws.Timestream.Configuration;
    using Abbotware.Interop.Aws.Timestream.Protocol;
    using Amazon.TimestreamWrite;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Typed Message Publisher
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public class BufferedTimestreamPublisher<TMessage> : TimestreamPublisher<TMessage>
        where TMessage : notnull
    {
        private readonly SimpleChannel<TMessage, UnboundedChannelOptions> channel;

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferedTimestreamPublisher{TMessage}"/> class.
        /// </summary>
        /// <param name="options">options</param>
        /// <param name="protocol">message encoding protocol</param>
        /// <param name="factory">injected logger factory</param>
        /// <param name="logger">injected logger</param>
        public BufferedTimestreamPublisher(TimestreamOptions options, ITimestreamProtocol<TMessage> protocol, ILoggerFactory factory, ILogger<TimestreamPublisher<TMessage>> logger)
            : this(new AmazonTimestreamWriteClient(), options, protocol, factory, logger)
        {
            _ = this.Start();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BufferedTimestreamPublisher{TMessage}"/> class.
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="options">options</param>
        /// <param name="protocol">message encoding protocol</param>
        /// <param name="factory">injected logger factory</param>
        /// <param name="logger">injected logger</param>
        public BufferedTimestreamPublisher(AmazonTimestreamWriteClient client, TimestreamOptions options, ITimestreamProtocol<TMessage> protocol, ILoggerFactory factory, ILogger logger)
            : base(client, options, protocol, logger)
        {
            var o = new UnboundedChannelOptions();

            this.channel = new SimpleChannel<TMessage, UnboundedChannelOptions>(o, factory.CreateLogger<ActionQueueChannel<TMessage, UnboundedChannelOptions>>());
        }

        /// <inheritdoc/>
        protected override async ValueTask<PublishStatus> OnPublishAsync(IEnumerable<TMessage> messages, CancellationToken ct)
        {
            foreach (var m in messages)
            {
                await this.channel.EnqueueAsync(m, ct)
                    .ConfigureAwait(false);
            }

            return PublishStatus.Unknown;
        }

        /// <inheritdoc/>
        protected override void OnDisposeManagedResources()
        {
            this.channel.Dispose();

            base.OnDisposeManagedResources();
        }

        private async Task Start()
        {
            while (!this.IsDisposedOrDisposing)
            {
                try
                {
                    // TODO: wire up cancellation token
                    await Task.Delay(TimeSpan.FromSeconds(1), default)
                        .ConfigureAwait(false);

                    while (this.channel.Reader.Count > 0)
                    {
                        var messages = new List<TMessage>(TimesreamConstants.MaxRecordBatch);

                        while (this.channel.Reader.TryRead(out var m))
                        {
                            messages.Add(m);

                            if (messages.Count == TimesreamConstants.MaxRecordBatch)
                            {
                                break;
                            }
                        }

                        // TODO: wire up cancellation token
                        await this.OnWriteRecordsAsync(messages, default)
                            .ConfigureAwait(false);

                        // If the channel has less than a full batch then we will break the inner forcing a delay before writing again
                        // This effectively 'throttles' lots of small writes
                        if (this.channel.Reader.Count < TimesreamConstants.MaxRecordBatch)
                        {
                            break;
                        }
                    }
                }
                catch (AmazonTimestreamWriteException ex)
                {
                    this.Logger.Error(ex, "OnWriteRecordsAsync");
                }
            }
        }
    }
}
