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
    using Abbotware.Core.Extensions;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// base class for a Channel based Queue
    /// </summary>
    /// <typeparam name="TItem">item type</typeparam>
    /// <typeparam name="TOptions">channel options</typeparam>
    public abstract class QueueChannel<TItem, TOptions> : SimpleChannel<TItem, TOptions>
        where TOptions : ChannelOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueueChannel{TData, TOptions}"/> class.
        /// </summary>
        /// <param name="options">channel options</param>
        /// <param name="logger">injected logger</param>
        protected QueueChannel(TOptions options, ILogger<QueueChannel<TItem, TOptions>> logger)
            : base(options, logger)
        {
            _ = this.StartReaderAsync();
        }

        /// <summary>
        /// hook to implement item processing logic
        /// </summary>
        /// <param name="data">data item to process</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        protected abstract ValueTask OnItemAsync(TItem data, CancellationToken ct);

        private async Task StartReaderAsync()
        {
            while (await this.Reader.WaitToReadAsync(this.DisposeRequested.Token)
                .ConfigureAwait(false))
            {
                try
                {
#if NETSTANDARD2_0
                    while (true)
                    {
                        var data = await this.Reader.ReadAsync(this.DisposeRequested.Token).ConfigureAwait(false);
#else
                    await foreach (var data in this.Reader.ReadAllAsync(this.DisposeRequested.Token))
                    {
#endif
                        try
                        {
                            await this.OnItemAsync(data, this.DisposeRequested.Token)
                                .ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            this.Logger.Error(ex, "StartReaderAsync-OnItemAsync");
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Logger.Error(ex, "StartReaderAsync-Foreach");
                }
            }
        }
    }
}
