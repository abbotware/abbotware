﻿// -----------------------------------------------------------------------
// <copyright file="AbbotwareHostService.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Host
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Runtime.ExceptionServices;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Runtime;
    using Abbotware.Core.Runtime.Plugins;
    using Abbotware.Host.Configuration;
    using Abbotware.Host.Extensions;
    using Abbotware.Interop.Castle.Plugins.Installers;
    using Abbotware.Using.Castle;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Abbotware Host Service
    /// </summary>
    public class AbbotwareHostService : BackgroundService
    {
        private readonly IWindsorContainer container;

        private readonly IHostOptions options;

        private readonly IEnvironment environment;

        private readonly ConsoleArguments consoleArguments;

        /// <summary>
        ///  registration override actions
        /// </summary>
        private readonly Queue<Action<IWindsorContainer>> registrationOverrides = new Queue<Action<IWindsorContainer>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="AbbotwareHostService"/> class.
        /// </summary>
        /// <param name="consoleArguments">console arguments</param>
        /// <param name="options">container options</param>
        public AbbotwareHostService(ConsoleArguments consoleArguments, IHostOptions options)
        {
            this.options = options;
            this.consoleArguments = consoleArguments;

            this.container = IocContainer.Create(options);
            this.container.AddLog4net();

            this.Logger = this.container.Resolve<ILogger>();

            this.container.AddOperatingSystem();

            this.container.Register(Component.For<ConsoleArguments>()
                .Instance(this.consoleArguments));

            this.container.Register(Component.For<IProcessInformation>()
                .ImplementedBy<ProcessInformation>().LifestyleSingleton());

            this.environment = this.container.Resolve<IEnvironment>();
        }

        /// <summary>
        /// Gets the logger
        /// </summary>
        protected ILogger Logger { get; }

        /// <inheritdoc/>
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            this.Logger.Info($"Starting Host:{this.options.Component} Version:{this.environment.SoftwareVersion} Arguments:{this.consoleArguments} ct:{cancellationToken.IsCancellationRequested}");

            this.AttachLoggingEvents();

            this.OnInstall(this.container);

            foreach (var action in this.registrationOverrides)
            {
                action.Invoke(this.container);
            }

            if (this.options.DisableSslVerification)
            {
                this.Logger.Warn($"SSL Validation Disabled");

                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
                {
                    this.Logger.Warn($"SSL Cert Validation:{sslPolicyErrors}");
                    return true;
                };
            }

            this.container.Install(this.OnFindComponentAssemblyInstallers());

            // NOTE: the base class calls ExecuteAsync
            var r = base.StartAsync(cancellationToken);

            this.Logger.Info($"Startup Complete - waiting for shutdown signal ct:{cancellationToken.IsCancellationRequested}");

            return r;
        }

        /// <inheritdoc/>
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            this.Logger.Info($"Stopping Host:{this.options.Component} ct:{cancellationToken.IsCancellationRequested}");

            this.DetachLoggingEvents();

            var r = base.StopAsync(cancellationToken);

            this.Logger.Info($"Stopping Complete:{this.options.Component} ct:{cancellationToken.IsCancellationRequested}");

            return r;
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            this.container.Dispose();

            base.Dispose();

            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // nothing to do if components are IStartable components
            return Task.CompletedTask;
        }

        /// <summary>
        /// callback for custom container configuration
        /// </summary>
        /// <param name="container">di container</param>
        protected virtual void OnInstall(IWindsorContainer container)
        {
        }

        /// <summary>
        /// adds an override registration action
        /// </summary>
        /// <param name="action">action</param>
        protected void AddOverride(Action<IWindsorContainer> action)
        {
            this.registrationOverrides.Enqueue(action);
        }

        /// <summary>
        /// Finds installers for the assemblies named after the comonent
        /// </summary>
        /// <returns>installer</returns>
        protected virtual IWindsorInstaller OnFindComponentAssemblyInstallers()
        {
            if (string.IsNullOrWhiteSpace(this.options.Component))
            {
                return NullInstaller.Instance;
            }

            var searchFilter = $"*{this.options.Component}*.dll";

            var results = Directory.GetFiles(Environment.CurrentDirectory, searchFilter, SearchOption.AllDirectories);

            var assemblies = results.Select(f => new Uri(f))
                .ToArray();

            var installer = new LoadInstallersInAssembly(assemblies, this.Logger);

            return installer;
        }

        private static string? LogSender(object sender)
        {
            return sender switch
            {
                AppDomain ad => $"AppDomain:{ad?.FriendlyName}",
                _ => sender?.ToString(),
            };
        }

        /// <summary>
        ///     Callback logic for handing unhandled exceptions
        /// </summary>
        /// <param name="sender">sender of the exception</param>
        /// <param name="args">exception arguments</param>
        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            try
            {
                if (args.ExceptionObject is Exception argsException)
                {
                    this.Logger.Error(argsException, $"UnhandledException Sender:[{LogSender(sender)}] IsTerminating:{args?.IsTerminating}");
                }
                else
                {
                    this.Logger.Error($"UnhandledException Sender:[{LogSender(sender)}] IsTerminating:{args?.IsTerminating}");
                }
            }
            catch (Exception ex)
            {
                this.Logger.Fatal(ex, "Should never reach this");
                throw;
            }
        }

        /// <summary>
        ///     Callback logic for handing first chance exceptions
        /// </summary>
        /// <param name="sender">sender of the exception</param>
        /// <param name="args">exception arguments</param>
        private void OnFirstChanceException(object sender, FirstChanceExceptionEventArgs args)
        {
            try
            {
                this.Logger.Error(args.Exception, $"FirstChanceException Sender:[{LogSender(sender)}]");
            }
            catch (Exception ex)
            {
                this.Logger.Fatal(ex, "Should never reach this");
                throw;
            }
        }

        /// <summary>
        ///     Callback logic for logging unobserved task exceptions
        /// </summary>
        /// <param name="sender">sender of the exception</param>
        /// <param name="args">exception arguments</param>
        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs args)
        {
            try
            {
                this.Logger.Error(args.Exception, $"FirstChanceException Sender:[{LogSender(sender)}] Observered:{args.Observed}");
            }
            catch (Exception ex)
            {
                this.Logger.Fatal(ex, "Should never reach this");
                throw;
            }
        }

        private void AttachLoggingEvents()
        {
            if (this.options.LogFirstChanceExceptions)
            {
                AppDomain.CurrentDomain.FirstChanceException += this.OnFirstChanceException;
            }

            AppDomain.CurrentDomain.UnhandledException += this.OnUnhandledException;

            TaskScheduler.UnobservedTaskException += this.OnUnobservedTaskException;
        }

        private void DetachLoggingEvents()
        {
            if (this.options.LogFirstChanceExceptions)
            {
                AppDomain.CurrentDomain.FirstChanceException -= this.OnFirstChanceException;
            }

            AppDomain.CurrentDomain.UnhandledException -= this.OnUnhandledException;

            TaskScheduler.UnobservedTaskException -= this.OnUnobservedTaskException;
        }
    }
}