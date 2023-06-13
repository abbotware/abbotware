// -----------------------------------------------------------------------
// <copyright file="SimpleChannel{TItem,TOptions}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Channels;
    using System.Threading.Tasks;
    using Abbotware.Core.Objects;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// basic wrapper for a channel
    /// </summary>
    /// <typeparam name="TItem">item type</typeparam>
    /// <typeparam name="TOptions">channel options</typeparam>
    public class SimpleChannel<TItem, TOptions> : BaseAsyncComponent
        where TOptions : ChannelOptions
    {
        private readonly Channel<TItem> channel;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleChannel{TData, TOptions}"/> class.
        /// </summary>
        /// <param name="options">channel options</param>
        /// <param name="logger">injected logger</param>
        public SimpleChannel(TOptions options, ILogger<SimpleChannel<TItem, TOptions>> logger)
            : base(logger)
        {
            switch (options)
            {
                case BoundedChannelOptions bco:
                    this.channel = Channel.CreateBounded<TItem>(bco);
                    break;
                case UnboundedChannelOptions uco:
                    this.channel = Channel.CreateUnbounded<TItem>(uco);
                    break;
                default:
                    throw new NotSupportedException($"{options} is not supported");
            }
        }

        /// <summary>
        /// Gets the reader for this channel
        /// </summary>
        public ChannelReader<TItem> Reader => this.channel.Reader;

        /// <summary>
        /// Enqueues data
        /// </summary>
        /// <param name="data">data item</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        public ValueTask EnqueueAsync(TItem data, CancellationToken ct)
        {
            return this.EnqueueAsync(new[] { data }, ct);
        }

        /// <summary>
        /// Enqueues data
        /// </summary>
        /// <param name="data">data items</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        public ValueTask EnqueueAsync(TItem[] data, CancellationToken ct)
        {
            return this.EnqueueAsync(data, ct);
        }

        /// <summary>
        /// Enqueues data
        /// </summary>
        /// <param name="data">data items</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        public async ValueTask EnqueueAsync(IEnumerable<TItem> data, CancellationToken ct)
        {
            foreach (var d in data)
            {
                await this.channel.Writer.WriteAsync(d, ct)
                    .ConfigureAwait(false);
            }
        }
    }
}
