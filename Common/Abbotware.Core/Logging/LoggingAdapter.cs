// -----------------------------------------------------------------------
// <copyright file="LoggingAdapter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

#if NET5_0_OR_GREATER
namespace Abbotware.Core.Logging
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using Abbotware.Core.Logging.Plugins;

    /// <summary>
    /// Logging Adapter for Microsoft.Extensions.Logging.ILogger
    /// </summary>
    public class LoggingAdapter : ILogger
    {
        private readonly Microsoft.Extensions.Logging.ILogger external;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingAdapter"/> class.
        /// </summary>
        /// <param name="logger">microsoft logger</param>
        public LoggingAdapter(Microsoft.Extensions.Logging.ILogger logger)
        {
            this.external = logger;
        }

        /// <inheritdoc/>
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc/>
        public void Fatal<T>(Func<T, string> function, T parameter)
        {
            this.external.Critical(() => function(parameter));
        }

        /// <inheritdoc/>
        public void Fatal([Localizable(false)] string message, params object?[] args)
{
            this.external.Critical(() => string.Format(CultureInfo.InvariantCulture, message, args), null, null, null);
        }

        /// <inheritdoc/>
        public void Fatal(Exception exception, [Localizable(false)] string message, params object?[] args)
        {
            this.external.Critical(exception, () => string.Format(CultureInfo.InvariantCulture, message, args), null, null, null);
        }

        /// <inheritdoc/>
        public void Error<T>(Func<T, string> function, T parameter)
        {
            this.external.Error(() => function(parameter));
        }

        /// <inheritdoc/>
        public void Error([Localizable(false)] string message, params object?[] args)
        {
            this.external.Error(() => string.Format(CultureInfo.InvariantCulture, message, args), null, null, null);
        }

        /// <inheritdoc/>
        public void Error(Exception exception, [Localizable(false)] string message, params object?[] args)
        {
            this.external.Error(exception, () => string.Format(CultureInfo.InvariantCulture, message, args), null, null, null);
        }

        /// <inheritdoc/>
        public void Warn<T>(Func<T, string> function, T parameter)
        {
            this.external.Warn(() => function(parameter));
        }

        /// <inheritdoc/>
        public void Warn([Localizable(false)] string message, params object?[] args)
        {
            this.external.Warn(() => string.Format(CultureInfo.InvariantCulture, message, args), null, null, null);
        }

        /// <inheritdoc/>
        public void Warn(Exception exception, [Localizable(false)] string message, params object?[] args)
        {
            this.external.Warn(exception, () => string.Format(CultureInfo.InvariantCulture, message, args), null, null, null);
        }

        /// <inheritdoc/>
        public void Info<T>(Func<T, string> function, T parameter)
        {
            this.external.Info(() => function(parameter));
        }

        /// <inheritdoc/>
        public void Info([Localizable(false)] string message, params object?[] args)
        {
            this.external.Info(() => string.Format(CultureInfo.InvariantCulture, message, args), null, null, null);
        }

        /// <inheritdoc/>
        public void Info(Exception exception, [Localizable(false)] string message, params object?[] args)
        {
            this.external.Info(exception, () => string.Format(CultureInfo.InvariantCulture, message, args), null, null, null);
        }

        /// <inheritdoc/>
        public void Debug<T>(Func<T, string> function, T parameter)
        {
            this.external.Debug(() => function(parameter));
        }

        /// <inheritdoc/>
        public void Debug([Localizable(false)] string message, params object?[] args)
        {
            this.external.Debug(() => string.Format(CultureInfo.InvariantCulture, message, args), null, null, null);
        }

        /// <inheritdoc/>
        public void Debug(Exception exception, [Localizable(false)] string message, params object?[] args)
        {
            this.external.Debug(exception, () => string.Format(CultureInfo.InvariantCulture, message, args), null, null, null);
        }

        /// <inheritdoc/>
        public void Trace<T>(Func<T, string> function, T parameter)
        {
            this.external.Trace(() => function(parameter));
        }

        /// <inheritdoc/>
        public void Trace([Localizable(false)] string message, params object?[] args)
        {
            this.external.Trace(() => string.Format(CultureInfo.InvariantCulture, message, args), null, null, null);
        }

        /// <inheritdoc/>
        public void Trace(Exception exception, [Localizable(false)] string message, params object?[] args)
        {
            this.external.Trace(exception, () => string.Format(CultureInfo.InvariantCulture, message, args), null, null, null);
        }

        /// <inheritdoc/>
        public IDisposable BeginScope(string context)
        {
            return new NullLogger.NoOp();
        }

        /// <inheritdoc/>
        public ILogger Create(string name)
        {
            return this;
        }
    }
}
#endif