// -----------------------------------------------------------------------
// <copyright file="ActionQueueChannel{TItem,TOptions}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Threading
{
    using System;
    using System.Threading;
    using System.Threading.Channels;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///  Channel based Queue that takes an action
    /// </summary>
    /// <typeparam name="TItem">item type</typeparam>
    /// <typeparam name="TOptions">channel options</typeparam>
    public class ActionQueueChannel<TItem, TOptions> : QueueChannel<TItem, TOptions>
    where TOptions : ChannelOptions
    {
        private readonly Func<TItem, CancellationToken, ValueTask> action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionQueueChannel{TItem, TOptions}"/> class.
        /// </summary>
        /// <param name="action">item processing action</param>
        /// <param name="options">channel options</param>
        /// <param name="logger">injected logger</param>
        public ActionQueueChannel(Func<TItem, CancellationToken, ValueTask> action, TOptions options, ILogger<ActionQueueChannel<TItem, TOptions>> logger)
            : base(options, logger)
        {
            this.action = action;
        }

        /// <inheritdoc/>
        protected override ValueTask OnItemAsync(TItem data, CancellationToken ct)
        {
            return this.action(data, ct);
        }
    }
}
