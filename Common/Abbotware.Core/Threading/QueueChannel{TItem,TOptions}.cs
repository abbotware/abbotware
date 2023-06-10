// -----------------------------------------------------------------------
// <copyright file="QueueChannel{TItem,TOptions}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Threading
{
    using System;
    using System.Threading;
    using System.Threading.Channels;
    using System.Threading.Tasks;
    using Abbotware.Core.Objects;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// base class for a Channel based Queue
    /// </summary>
    /// <typeparam name="TItem">item type</typeparam>
    /// <typeparam name="TOptions">channel options</typeparam>
    public abstract class QueueChannel<TItem, TOptions> : BaseAsyncComponent
        where TOptions : ChannelOptions
    {
        private readonly Channel<TItem> channel;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueChannel{TData, TOptions}"/> class.
        /// </summary>
        /// <param name="options">channel options</param>
        /// <param name="logger">injected logger</param>
        protected QueueChannel(TOptions options, ILogger<QueueChannel<TItem, TOptions>> logger)
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

            _ = this.Start();
        }

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
        public async ValueTask EnqueueAsync(TItem[] data, CancellationToken ct)
        {
            foreach (var d in data)
            {
                await this.channel.Writer.WriteAsync(d, ct)
                    .ConfigureAwait(false);
            }
        }

        /// <summary>
        /// hook to implement item processing logic
        /// </summary>
        /// <param name="data">data item to process</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        protected abstract ValueTask OnItemAsync(TItem data, CancellationToken ct);

        private async Task Start()
        {
            while (await this.channel.Reader.WaitToReadAsync(this.DisposeRequested.Token)
                .ConfigureAwait(false))
            {
                ////await foreach (var data in this.channel.Reader.ReadAllAsync(this.DisposeRequested.Token))
                ////{
                ////    try
                ////    {
                ////        await this.OnItemAsync(data, this.DisposeRequested.Token)
                ////            .ConfigureAwait(false);
                ////    }
                ////    catch (Exception ex)
                ////    {
                ////        this.Logger.Error(ex, "OnItemAsync");
                ////    }
                ////}
            }
        }
    }
}
