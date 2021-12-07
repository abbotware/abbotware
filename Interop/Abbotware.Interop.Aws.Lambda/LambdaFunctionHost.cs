// -----------------------------------------------------------------------
// <copyright file="LambdaFunctionHost.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Aws.Lambda
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Host;
    using Abbotware.Interop.Aws.Lambda.Configuration;
    using global::Amazon.Lambda.Core;
    using global::Amazon.Lambda.RuntimeSupport;
    using global::Amazon.Lambda.Serialization.Json;

    /// <summary>
    /// Lambda Function Host
    /// </summary>
    public class LambdaFunctionHost : AbbotwareHostService<ILambdaHostOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaFunctionHost"/> class.
        /// </summary>
        /// <param name="options">host options</param>
        public LambdaFunctionHost(ILambdaHostOptions options)
            : base(options)
        {
        }

        /// <inheritdoc/>
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var sw = Stopwatch.StartNew();
            var output = string.Empty;

            if (this.Configuration.RunAsConsole)
            {
                this.Logger.Info("Skipping AWS");

                output = await this.OnLambdaAsync(null, null, stoppingToken)
                    .ConfigureAwait(false);

                return;
            }

            async Task<string> Inject(string input, ILambdaContext context)
            {
                if (this.Configuration.SpoolAws)
                {
                    this.Logger.Info("Spooling");

                    output = await this.OnSpoolAsync(input, context, stoppingToken)
                        .ConfigureAwait(false);

                    return output;
                }
                else
                {
                    output = await this.OnLambdaAsync(input, context, stoppingToken)
                        .ConfigureAwait(false);

                    return output;
                }
            }

            using var handlerWrapper = HandlerWrapper.GetHandlerWrapper((Func<string, ILambdaContext, Task<string>>)Inject, new JsonSerializer());

            using var bootstrap = new LambdaBootstrap(handlerWrapper);

            await bootstrap.RunAsync(stoppingToken)
                .ConfigureAwait(false);

            this.Logger.Debug($"Return:{0} Time {sw.ElapsedMilliseconds} ms");
        }

        /// <summary>
        /// Callback for a test run / 'spool'
        /// </summary>
        /// <param name="input">message input</param>
        /// <param name="context">lambda context</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>return message</returns>
        protected virtual Task<string> OnSpoolAsync(string? input, ILambdaContext? context, CancellationToken ct)
        {
            return Task.FromResult("Spooling up FTL...");
        }

        /// <summary>
        /// Callback hook for implementing the lambda
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="context">lambda context</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>output</returns>
        protected virtual Task<string> OnLambdaAsync(string? input, ILambdaContext? context, CancellationToken ct)
        {
            if (this.Configuration.RunAsConsole)
            {
                return Task.FromResult("AWS SKIPPED");
            }

            return Task.FromResult("done");
        }
    }
}