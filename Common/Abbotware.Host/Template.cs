// -----------------------------------------------------------------------
// <copyright file="Template.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Host
{
    using System;
    using System.Threading.Tasks;
    using Abbotware.Host.Configuration;
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
        public static async Task<int> RunAsync<THostService>(string[] args)
            where THostService : AbbotwareHostService
        {
            try
            {
                var capture = new CommandLineOptions();

                var parsed = Parser.Default.ParseArguments<CommandLineOptions>(args)
                    .WithParsed(d => { capture = d; });

                var host = new HostBuilder()
                  .ConfigureServices((hostContext, services) =>
                  {
                      services.AddSingleton(new ConsoleArguments(args));
                      services.AddOptions();
                      services.AddSingleton<IHostOptions>(capture);
                      services.AddSingleton<IHostedService, THostService>();
                  })
                    .ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                        logging.AddConsole();
                    })
                    .Build();

                await host.RunAsync(default)
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
