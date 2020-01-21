// -----------------------------------------------------------------------
// <copyright file="Template.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Host
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core;
    using Abbotware.Core.Runtime;
    using Abbotware.Core.Runtime.Plugins;
    using Abbotware.Host.Configuration;
    using Abbotware.Host.Plugins;
    using CommandLine;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Program Entry Point
    /// </summary>
    public static class Template
    {
        /// <summary>
        /// runs the host service
        /// </summary>
        /// <typeparam name="THostService">host service type</typeparam>
        /// <param name="args">command line args</param>
        /// <returns>async task</returns>
        public static Task<int> RunAsync<THostService>(string[] args)
            where THostService : AbbotwareHostService
        {
            return RunAsync<THostService, CommandLineOptions, IHostOptions, Configuration.HostOptions>(args);
        }

        /// <summary>
        /// runs the host service
        /// </summary>
        /// <typeparam name="THostService">host service type</typeparam>
        /// <typeparam name="TCmdOptions">options type</typeparam>
        /// <typeparam name="TIConfiguration">read only configuration type</typeparam>
        /// <typeparam name="TConfiguration">configurationtype</typeparam>
        /// <param name="args">command line args</param>
        /// <returns>async task</returns>
        public static async Task<int> RunAsync<THostService, TCmdOptions, TIConfiguration, TConfiguration>(string[] args)
            where THostService : AbbotwareHostService
            where TCmdOptions : class, new()
            where TConfiguration : class, TIConfiguration
            where TIConfiguration : class
        {
            try
            {
                // used to signal shutdown for the host.RunAsync
                using var cts = new CancellationTokenSource();

                // wrapper around the cts to inject into the running service
                var requestShutdown = new HostShutdown(cts);

                // wrapper around the cts to to allow monitoring for shutdown
                var monitorShutdown = new MonitorShutdown(cts.Token);

                var cmdOptions = new TCmdOptions();

                var parsed = Parser.Default.ParseArguments<TCmdOptions>(args);

                parsed.WithParsed(d => { cmdOptions = d; });

                var host = new HostBuilder()
                  .ConfigureServices((hostContext, services) =>
                  {
                      services.AddOptions();

                      // register only the minimal amount of dependencies - the service will use its own container
                      services.AddSingleton(new ConsoleArguments(args));
                      services.AddSingleton(cmdOptions);
                      services.AddSingleton<TIConfiguration, TConfiguration>();
                      services.AddSingleton<IRequestShutdown>(requestShutdown);
                      services.AddSingleton<IMonitorShutdown>(monitorShutdown);
                      services.AddSingleton<IHostedService, THostService>();
                  })
                    .ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                        logging.AddConsole();
                    })
                    .Build();

                await host.RunAsync(cts.Token)
                    .ConfigureAwait(false);

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR:{ex}");
                throw;
            }
        }
    }
}
