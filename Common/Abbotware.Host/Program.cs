// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Host
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Program Entry Point
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// runs the host service
        /// </summary>
        /// <typeparam name="THostService">host service type</typeparam>
        /// <param name="args">command line args</param>
        /// <returns>async task</returns>
        public static async Task RunAsync<THostService>(string[] args)
            where THostService : AbbotwareHostService
        {
            using var cts = new CancellationTokenSource();

            Console.CancelKeyPress += (s, e) => cts.Cancel();

            // TODO: wire up to logging
            TaskScheduler.UnobservedTaskException += (s, e) => Console.WriteLine(e.Exception);

            // TODO:  wire up command args
            GC.KeepAlive(args);

            var host = new HostBuilder()
              .ConfigureServices((hostContext, services) =>
              {
                  services.AddOptions();
                  services.AddSingleton<IHostedService, THostService>();
              }).Build();

            await host.RunAsync(cts.Token)
                .ConfigureAwait(false);
        }
    }
}
