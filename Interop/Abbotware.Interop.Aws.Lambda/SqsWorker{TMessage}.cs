// -----------------------------------------------------------------------
// <copyright file="SqsWorker{TMessage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Lambda
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Messaging;
    using Abbotware.Host;
    using Abbotware.Interop.Aws.Sqs.Plugins;
    using global::Microsoft.Extensions.Logging;
    using ProtoBuf;

    /// <summary>
    /// SQS Message handler wrapper
    /// </summary>
    /// <typeparam name="TMessage">message type</typeparam>
    public class SqsWorker<TMessage> : BaseStartableComponent
    {
        private readonly IMessageHandler<TMessage> handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqsWorker{TMessage}"/> class.
        /// </summary>
        /// <param name="configuration">configuratioj</param>
        /// <param name="handler">injected message handler</param>
        /// <param name="logger">injected loggerr</param>
        public SqsWorker(ISqsWorkerOptions configuration, IMessageHandler<TMessage> handler, ILogger logger)
            : base(logger)
        {
            Arguments.NotNull(handler, nameof(handler));
            configuration = Arguments.EnsureNotNull(configuration, nameof(configuration));

            this.handler = handler;
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets configuration
        /// </summary>
        protected ISqsWorkerOptions Configuration { get; }

        /// <inheritdoc/>
        protected async override void OnStart()
        {
            var timer = this.Configuration.LambdaHostOptions.TimeSlice;
            var targetQueue = this.Configuration.ConnectionFactory.DefaultOptions.Queue;

            this.Logger.Debug($"SQS Queue:{targetQueue}");

            if (this.Configuration.LambdaContext.RemainingTime < timer)
            {
                // expire 250 ms early
                timer = this.Configuration.LambdaContext.RemainingTime - TimeSpan.FromMilliseconds(250);
            }

            using var timeSliceExipration = new CancellationTokenSource(timer);
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(timeSliceExipration.Token, this.Configuration.MonitorShutdown.Token);

            var sw = Stopwatch.StartNew();

            var connection = this.Configuration.ConnectionFactory.Create();

            int processed = 0;

            while (!cts.IsCancellationRequested)
            {
                using var r = connection.CreateRetriever<SqsRetriever>();

                var envelopes = await r.Get(targetQueue, false)
                    .ConfigureAwait(false);

                if (!envelopes.Any())
                {
                    break;
                }

                foreach (var e in envelopes)
                {
                    ++processed;

                    using (var stream = new MemoryStream(e.Body.ToArray()))
                    {
                        var message = Serializer.Deserialize<TMessage>(stream);

                        await this.handler.HandleAsync(message, timeSliceExipration.Token)
                                  .ConfigureAwait(false);
                    }

                    r.Delete(targetQueue, envelopes);
                }
            }

            this.Logger.Info($"processed:{processed} messages in {sw.Elapsed} timeslice ct:{timeSliceExipration.IsCancellationRequested} shutdown ct:{this.Configuration.MonitorShutdown.Token.IsCancellationRequested}");
        }
    }
}