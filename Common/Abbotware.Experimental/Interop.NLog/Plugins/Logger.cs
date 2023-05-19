// -----------------------------------------------------------------------
// <copyright file="Logger.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.NLog.Plugins
{
    using System;
    using System.ComponentModel;
    using Abbotware.Core.Logging;

    /// <summary>
    /// Logger Implementation via NLog
    /// </summary>
    public class Logger : global::NLog.Logger, ILogger
    {
        /// <summary>
        /// Gets or sets the logger factory for creating child loggers
        /// </summary>
        /// <remarks>set to internal as a workaround for NLog.LogManager.GetLogger requiring parameterless constructors</remarks>
        internal LoggerFactory? LoggerFactory { get; set; }

        /// <inheritdoc/>
        public IDisposable BeginScope(string context)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public ILogger Create(string name)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Debug<T>(Func<T, string> function, T parameter)
        {
            if (this.IsDebugEnabled)
            {
                if (function is null)
                {
                    return;
                }

                this.Debug(function(parameter));
            }
        }

        /// <inheritdoc/>
        public void Error<T>(Func<T, string> function, T parameter)
        {
            if (this.IsErrorEnabled)
            {
                if (function is null)
                {
                    return;
                }

                this.Error(function(parameter));
            }
        }

        /// <inheritdoc/>
        public void Fatal<T>(Func<T, string> function, T parameter)
        {
            if (this.IsFatalEnabled)
            {
                if (function is null)
                {
                    return;
                }

                this.Fatal(function(parameter));
            }
        }

        /// <inheritdoc/>
        public void Info<T>(Func<T, string> function, T parameter)
        {
            if (this.IsInfoEnabled)
            {
                if (function is null)
                {
                    return;
                }

                this.Info(function(parameter));
            }
        }

        /// <inheritdoc/>
        public void Trace<T>(Func<T, string> function, T parameter)
        {
            if (this.IsTraceEnabled)
            {
                if (function is null)
                {
                    return;
                }

                this.Trace(function(parameter));
            }
        }

        /// <inheritdoc/>
        public void Warn<T>(Func<T, string> function, T parameter)
        {
            if (this.IsWarnEnabled)
            {
                if (function is null)
                {
                    return;
                }

                this.Warn(function(parameter));
            }
        }
    }
}