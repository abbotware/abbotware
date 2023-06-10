// -----------------------------------------------------------------------
// <copyright file="LoggerFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.NLog.Plugins
{
    using System;
    using Abbotware.Core.Logging;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Logger factory implementation
    /// </summary>
    public class LoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerFactory"/> class.
        /// </summary>
        public LoggerFactory()
        {
        }

#if NETSTANDARD2_0
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerFactory"/> class.
        /// </summary>
        /// <param name="configFile">config file</param>
        public LoggerFactory(string configFile)
        {
            global::NLog.LogManager.LoadConfiguration(configFile);
        }
#endif

        /// <inheritdoc/>
        public virtual ILogger Create(string name)
        {
            return this.Create<Logger>(name);
        }

        /// <summary>
        /// Create a logger with a given name for a logger-derived class
        /// </summary>
        /// <typeparam name="TLogger">Type of logger class to create</typeparam>
        /// <param name="name">Logger name</param>
        /// <returns>Created logger instance</returns>
        internal virtual TLogger Create<TLogger>(string name)
            where TLogger : Logger
        {
            var l = global::NLog.LogManager.GetLogger(name, typeof(TLogger));

            if (l is not TLogger logger)
            {
                throw new InvalidOperationException("logger is null");
            }

            // this is set after construction as a work around to LogManager.GetLogger requiring parameterless constructors
            logger.LoggerFactory = this;

            return logger;
        }
    }
}