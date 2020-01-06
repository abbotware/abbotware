// -----------------------------------------------------------------------
// <copyright file="AbbotwareLoggerFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Using.Castle.Internal
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using global::Castle.Core.Logging;
    using log4net;
    using log4net.Config;
    using log4net.Repository;
    using AbbotwareILoggerV2 = Abbotware.Core.Logging.ILogger;
    using CastleILogger = global::Castle.Core.Logging.ILogger;

    /// <summary>
    ///     Logger Factory for Abbotware
    /// </summary>
    public class AbbotwareLoggerFactory : ILoggerFactory
    {
        /// <summary>
        ///     Name of the default config file
        /// </summary>
        internal const string DefaultConfigFileName = "log4net.config";

        /// <summary>
        /// logger repository
        /// </summary>
        private readonly ILoggerRepository repo;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbbotwareLoggerFactory" /> class.
        /// </summary>
        public AbbotwareLoggerFactory()
            : this(DefaultConfigFileName)
        {
            Arguments.NotNullOrWhitespace(DefaultConfigFileName, nameof(DefaultConfigFileName));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbbotwareLoggerFactory" /> class.
        /// </summary>
        /// <param name="configFile">name of the logger config file</param>
        public AbbotwareLoggerFactory(string configFile)
        {
            Arguments.NotNullOrWhitespace(configFile, nameof(configFile));

            var file = GetConfigFile(configFile);

            this.repo = this.GetOrCreateRepository();

            XmlConfigurator.ConfigureAndWatch(this.repo, file);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbbotwareLoggerFactory" /> class.
        /// </summary>
        /// <param name="configuredExternally">indicates the logger will be configured externally</param>
        public AbbotwareLoggerFactory(bool configuredExternally)
        {
            if (configuredExternally)
            {
                return;
            }

            Arguments.NotNullOrWhitespace(DefaultConfigFileName, nameof(DefaultConfigFileName));

            var file = GetConfigFile(DefaultConfigFileName);

            this.repo = this.GetOrCreateRepository();

            XmlConfigurator.ConfigureAndWatch(this.repo, file);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbbotwareLoggerFactory" /> class.
        /// </summary>
        /// <param name="configStream">stream of the config file</param>
        public AbbotwareLoggerFactory(Stream configStream)
        {
            Arguments.NotNull(configStream, nameof(configStream));

            this.repo = this.GetOrCreateRepository();

            XmlConfigurator.Configure(this.repo, configStream);
        }

        /// <inheritdoc />
        CastleILogger ILoggerFactory.Create(string name, LoggerLevel level)
        {
            return this.Create(name, level) as CastleILogger;
        }

        /// <inheritdoc />
        CastleILogger ILoggerFactory.Create(Type type, LoggerLevel level)
        {
            return this.Create(type, level) as CastleILogger;
        }

        /// <inheritdoc />
        CastleILogger ILoggerFactory.Create(string name)
        {
            return this.Create(name) as CastleILogger;
        }

        /// <inheritdoc />
        CastleILogger ILoggerFactory.Create(Type type)
        {
            return this.Create(type) as CastleILogger;
        }

        /// <summary>
        ///     Creates a new logger.
        /// </summary>
        /// <param name="name">Name of logger</param>
        /// <returns>new logger</returns>
        public AbbotwareILoggerV2 Create(string name)
        {
            Arguments.NotNull(name, nameof(name));

            var log = LogManager.GetLogger(this.repo.Name, name);

            return new AbbotwareLogger(log, this);
        }

        /// <summary>
        ///     Creates a new logger.
        /// </summary>
        /// <param name="name">Name of logger</param>
        /// <param name="level">level of logger</param>
        /// <returns>new logger</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "name", Justification = "as per castle's logger factory")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "level", Justification = "as per castle's logger factory")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "as per castle's logger factory")]
        public AbbotwareILoggerV2 Create(string name, LoggerLevel level)
        {
            throw new NotSupportedException($"Logger levels cannot be set at runtime. Please review your configuration file. name:{name} level:{level}");
        }

        /// <summary>
        ///     Creates a new logger, getting the logger name from the specified type.
        /// </summary>
        /// <param name="type">type to create logger as</param>
        /// <returns>new logger</returns>
        public virtual AbbotwareILoggerV2 Create(Type type)
        {
            Arguments.NotNull(type, nameof(type));

            return this.Create(type.GetFriendlyName());
        }

        /// <summary>
        ///     Creates a new logger, getting the logger name from the specified type.
        /// </summary>
        /// <param name="type">type to create logger as</param>
        /// <param name="level">level of logger</param>
        /// <returns>new logger</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "type", Justification = "as per castle's logger factory")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "level", Justification = "as per castle's logger factory")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "as per castle's logger factory")]
        public virtual AbbotwareILoggerV2 Create(Type type, LoggerLevel level)
        {
            throw new NotSupportedException("Logger levels cannot be set at runtime. Please review your configuration file.");
        }

        /// <summary>
        ///     Gets the FileInfo for the config file
        /// </summary>
        /// <param name="fileName">file name of the config</param>
        /// <returns>file info object</returns>
        protected static FileInfo GetConfigFile(string fileName)
        {
            Arguments.NotNullOrWhitespace(fileName, nameof(fileName));

            if (Path.IsPathRooted(fileName))
            {
                return new FileInfo(fileName);
            }

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            return new FileInfo(filePath);
        }

        /// <summary>
        ///     Gets the Log4Net repo
        /// </summary>
        /// <returns>logger repo</returns>
        private ILoggerRepository GetOrCreateRepository()
        {
            var repos = LogManager.GetAllRepositories();

            foreach (var r in repos)
            {
                if (r.Name == "default")
                {
                    return r;
                }
            }

            return LogManager.CreateRepository("default");
        }
    }
}