// -----------------------------------------------------------------------
// <copyright file="BaseCommand{TConfiguration,TResult}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Objects
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Logging;

    /// <summary>
    /// Base class for a command that has configuration
    /// </summary>
    /// <typeparam name="TConfiguration">configuration type</typeparam>
    /// <typeparam name="TResult">result of the command</typeparam>
    public abstract class BaseCommand<TConfiguration, TResult> : BaseComponent<TConfiguration>, ICommand<TResult>
        where TConfiguration : class
    {
        /// <summary>
        /// internal lock object
        /// </summary>
        private readonly object mutex = new();

        /// <summary>
        /// flag indicating if the command was executred
        /// </summary>
        private bool alreadyRunOrRunning;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCommand{TConfiguration, TResult}"/> class.
        /// </summary>
        /// <param name="config">configuration</param>
        /// <param name="logger">injected logger</param>
        protected BaseCommand(TConfiguration config, ILogger logger)
            : base(config, logger)
        {
        }

        /// <summary>
        /// executes the command
        /// </summary>
        /// <returns>result of the command</returns>
        public TResult Execute()
        {
            return this.ExecuteAsync(default).Result;
        }

        /// <inheritdoc/>
        public async Task<TResult> ExecuteAsync(CancellationToken ct)
        {
            this.InitializeIfRequired();

            lock (this.mutex)
            {
                if (this.alreadyRunOrRunning)
                {
                    throw new InvalidOperationException("can't execute command more than once");
                }

                this.alreadyRunOrRunning = true;
            }

            var result = await this.OnExecuteAsync(ct)
                .ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// logic Hook for base classes to implement custom execute logic
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>result of the command</returns>
        protected abstract Task<TResult> OnExecuteAsync(CancellationToken ct);
    }
}