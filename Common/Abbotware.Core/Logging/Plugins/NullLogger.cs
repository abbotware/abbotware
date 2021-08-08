// -----------------------------------------------------------------------
// <copyright file="NullLogger.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Logging.Plugins
{
    using System;
    using System.ComponentModel;
    using Abbotware.Core.Logging;

    /// <summary>
    ///     Null Logger throws away any logging messages
    /// </summary>
    public class NullLogger : ILogger
    {
        /// <summary>
        ///     static singleton instance of the Null Logger
        /// </summary>
        private static readonly NullLogger InternalInstance = new();

        /// <summary>
        ///     Gets the Null Logger static singleton instance
        /// </summary>
        public static ILogger Instance
        {
            get
            {
                return InternalInstance;
            }
        }

        /// <inheritdoc/>
        public string Name { get; } = string.Empty;

        /// <inheritdoc/>
        public IDisposable BeginScope(string context)
        {
            return new NoOp();
        }
#if NET5_0_OR_GREATER

        /// <inheritdoc/>
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
#endif

        /// <inheritdoc/>
        public ILogger Create(string name)
        {
            return this;
        }

        /// <inheritdoc/>
        public void Debug<T>(Func<T, string> function, T parameter)
        {
        }

        /// <inheritdoc/>
        public void Debug([Localizable(false)] string message, params object?[] args)
        {
        }

        /// <inheritdoc/>
        public void Debug(Exception exception, [Localizable(false)] string message, params object?[] args)
        {
        }

        /// <inheritdoc/>
        public void Error<T>(Func<T, string> function, T parameter)
        {
        }

        /// <inheritdoc/>
        public void Error([Localizable(false)] string message, params object?[] args)
        {
        }

        /// <inheritdoc/>
        public void Error(Exception exception, [Localizable(false)] string message, params object?[] args)
        {
        }

        /// <inheritdoc/>
        public void Fatal<T>(Func<T, string> function, T parameter)
        {
        }

        /// <inheritdoc/>
        public void Fatal([Localizable(false)] string message, params object?[] args)
        {
        }

        /// <inheritdoc/>
        public void Fatal(Exception exception, [Localizable(false)] string message, params object?[] args)
        {
        }

        /// <inheritdoc/>
        public void Info<T>(Func<T, string> function, T parameter)
        {
        }

        /// <inheritdoc/>
        public void Info([Localizable(false)] string message, params object?[] args)
        {
        }

        /// <inheritdoc/>
        public void Info(Exception exception, [Localizable(false)] string message, params object?[] args)
        {
        }

#if NET5_0_OR_GREATER
        /// <inheritdoc/>
        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, Microsoft.Extensions.Logging.EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            throw new NotImplementedException();
        }
#endif

        /// <inheritdoc/>
        public void Trace<T>(Func<T, string> function, T parameter)
        {
        }

        /// <inheritdoc/>
        public void Trace([Localizable(false)] string message, params object?[] args)
        {
        }

        /// <inheritdoc/>
        public void Trace(Exception exception, [Localizable(false)] string message, params object?[] args)
        {
        }

        /// <inheritdoc/>
        public void Warn<T>(Func<T, string> function, T parameter)
        {
        }

        /// <inheritdoc/>
        public void Warn([Localizable(false)] string message, params object?[] args)
        {
        }

        /// <inheritdoc/>
        public void Warn(Exception exception, [Localizable(false)] string message, params object?[] args)
        {
        }

        internal class NoOp : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}