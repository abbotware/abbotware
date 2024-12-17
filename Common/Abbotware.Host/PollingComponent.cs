// -----------------------------------------------------------------------
// <copyright file="PollingComponent.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Host
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     base class for an application component that runs on a polling schedule
    /// </summary>
    public abstract class PollingComponent : BaseStartableComponent
    {
        /// <summary>
        /// cancellation source
        /// </summary>
        private readonly CancellationTokenSource cts = new();

        /// <summary>
        ///     timespan between polling intervals
        /// </summary>
        private readonly TimeSpan pollingTimeSpan;

        /// <summary>
        ///     the handle to the loop task
        /// </summary>
        private Task? loopTask;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PollingComponent" /> class.
        /// </summary>
        /// <param name="pollingTimeSpan">time span to use for polling intervals</param>
        /// <param name="logger">injected logger</param>
        protected PollingComponent(TimeSpan pollingTimeSpan, ILogger logger)
            : base(logger)
        {
            Arguments.NotNull(logger, nameof(logger));
            Arguments.NotValue(pollingTimeSpan, TimeSpan.Zero, nameof(pollingTimeSpan));

            this.pollingTimeSpan = pollingTimeSpan;
        }

        /// <inheritdoc />
        protected override void OnStart()
        {
            var loopTask = Task.Run(OnDispatchAsync, cts.Token);
        }

        /// <inheritdoc />
        protected override void OnStop()
        {
            cts.Cancel();

            loopTask?.Dispose();

            loopTask = null;
        }

        /// <summary>
        ///     Hook to implement custom polling interval logic
        /// </summary>
        /// <returns>async task</returns>
        protected abstract Task OnIterationAsync();

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            base.OnDisposeManagedResources();

            loopTask?.Dispose();

            cts.Dispose();
        }

        /// <summary>
        ///     task entry point for polling via await / async
        /// </summary>
        /// <returns>async task</returns>
        private async Task OnDispatchAsync()
        {
            Logger.Debug($"started with polling interval:{pollingTimeSpan}");

            while (!cts.IsCancellationRequested)
            {
                try
                {
                    await OnIterationAsync()
                        .ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "Iteration");
                }

                await Task.Delay(pollingTimeSpan, cts.Token)
                    .ConfigureAwait(false);
            }

            Logger.Debug("exiting loop");
        }
    }
}