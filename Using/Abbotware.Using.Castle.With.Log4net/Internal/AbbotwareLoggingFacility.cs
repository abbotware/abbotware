// -----------------------------------------------------------------------
// <copyright file="AbbotwareLoggingFacility.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Using.Castle.Internal
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Configuration;
    using Abbotware.Core.Extensions;
    using Abbotware.Interop.Log4net;
    using global::Castle.Core.Logging;
    using global::Castle.MicroKernel.Facilities;
    using global::Castle.MicroKernel.Registration;
    using CastleILogger = global::Castle.Core.Logging.ILogger;
    using Log4netILog = global::log4net.ILog;

    /// <summary>
    ///     Abbotware Castle Facility for Logging
    /// </summary>
    public class AbbotwareLoggingFacility : AbstractFacility
    {
        /// <summary>
        ///     name of default log
        /// </summary>
        private string defaultLogName = string.Empty;

        /// <summary>
        ///     config file name
        /// </summary>
        private string configFileName = "log4net.config";

        /// <summary>
        ///     level of the default logger
        /// </summary>
        private LoggerLevel? defaultLoggerLevel;

        /// <summary>
        /// the container name
        /// </summary>
        private string containerName = string.Empty;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbbotwareLoggingFacility" /> class.
        /// </summary>
        public AbbotwareLoggingFacility()
        {
        }

        /// <summary>
        ///     Fluent API to Indicate Logging Facility should use the App.Config for configuration
        /// </summary>
        /// <returns>the logging facility</returns>
        public AbbotwareLoggingFacility WithAppConfig()
        {
            throw new NotImplementedException("NET STANDARD CONVERSION");

            ////Contract.Assume(!StringExtensions.IsNullOrWhiteSpace(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));

            ////this.configFileName = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

            ////return this;
        }

        /// <summary>
        ///     Fluent API to Indicate Logging Facility should use the specified file for configuration
        /// </summary>
        /// <param name="configFile">name of the logger config file</param>
        /// <returns>the logging facility</returns>
        public AbbotwareLoggingFacility WithConfig(string configFile)
        {
            configFile = Arguments.EnsureNotNullOrWhitespace(configFile, nameof(configFile));

            this.configFileName = configFile;

            return this;
        }

        /// <summary>
        ///     Fluent API to Indicate Logging Facility should use the specified options
        /// </summary>
        /// <param name="options">container options</param>
        /// <returns>the logging facility</returns>
        public AbbotwareLoggingFacility WithOptions(IContainerOptions options)
        {
            options = Arguments.EnsureNotNull(options, nameof(options));

            this.containerName = options.Component;

            return this;
        }

        /// <summary>
        ///     Fluent API to Indicate Logging Facility should use the specified logging level
        /// </summary>
        /// <param name="level">level of log</param>
        /// <returns>the logging facility</returns>
        public AbbotwareLoggingFacility WithLevel(LoggerLevel level)
        {
            this.defaultLoggerLevel = level;

            return this;
        }

        /// <summary>
        ///     Fluent API to Indicate Logging Facility should use the specified name for the default log
        /// </summary>
        /// <param name="name">name of log</param>
        /// <returns>the logging facility</returns>
        public AbbotwareLoggingFacility ToLog(string name)
        {
            Abbotware.Core.Arguments.NotNull(name, nameof(name));

            this.defaultLogName = name;

            return this;
        }

        /// <inheritdoc />
        protected override void Init()
        {
            Log4netHelper.SetComponent(this.containerName);
            Log4netHelper.SetVersion();
            Log4netHelper.SetCommandLine();

            if (this.FacilityConfig != null && this.FacilityConfig.Attributes != null)
            {
                var configFile = this.FacilityConfig.Attributes["configFile"];

                if (!string.IsNullOrWhiteSpace(configFile))
                {
                    this.configFileName = configFile;
                }
            }

            var loggerFactory = new AbbotwareLoggerFactory(this.configFileName);

            AbbotwareILogger defaultLogger;

            if (this.defaultLoggerLevel.HasValue)
            {
                defaultLogger = loggerFactory.Create(this.defaultLogName, this.defaultLoggerLevel.Value);
            }
            else
            {
                defaultLogger = loggerFactory.Create(this.defaultLogName);
            }

            this.KernelWiring(loggerFactory, defaultLogger);
        }

        /// <summary>
        ///     refactored for to disable code contract verifications
        /// </summary>
        /// <param name="loggerFactory">factory to register</param>
        /// <param name="defaultLogger">logger name</param>
        private void KernelWiring(AbbotwareLoggerFactory loggerFactory, AbbotwareILogger defaultLogger)
        {
            this.Kernel.Register(Component.For<ILoggerFactory>().NamedAutomatically("castleiloggerfactory").Instance(loggerFactory));
            this.Kernel.Register(Component.For<AbbotwareLoggerFactory>().NamedAutomatically("abbotwareloggerfactory").Instance(loggerFactory));
            this.Kernel.Register(Component.For<AbbotwareILogger>().NamedAutomatically("abbotwareilogger.default").Instance(defaultLogger));
            this.Kernel.Register(Component.For<CastleILogger>().NamedAutomatically("castleilogger.default").Instance((CastleILogger)defaultLogger));
            this.Kernel.Register(Component.For<Log4netILog>().NamedAutomatically("log4netilog.default").Instance((Log4netILog)defaultLogger));
            this.Kernel.Resolver.AddSubResolver(new AbbotwareLoggerResolver(loggerFactory, this.defaultLogName));
        }
    }
}