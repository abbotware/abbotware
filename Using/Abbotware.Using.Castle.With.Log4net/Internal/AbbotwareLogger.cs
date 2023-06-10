// -----------------------------------------------------------------------
// <copyright file="AbbotwareLogger.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Using.Castle.Internal
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using Abbotware.Core;
    using log4net.Core;
    using log4net.Util;
    using AbbotwareILoggerV2 = Microsoft.Extensions.Logging.ILogger;
    using CastleILogger = global::Castle.Core.Logging.ILogger;
    using Log4netILog = global::log4net.ILog;
    using Log4netILogger = global::log4net.Core.ILogger;

    /// <summary>
    ///     Logger for Abbotware
    /// </summary>
    public sealed class AbbotwareLogger : CastleILogger, Log4netILog, AbbotwareILoggerV2
    {
        /// <summary>
        ///     type of AbbotwareLogger
        /// </summary>
        private static readonly Type DeclaringType = typeof(AbbotwareLogger);

        /// <summary>
        ///     Gets the Log4net logger
        /// </summary>
        private readonly Log4netILogger logger;

        /// <summary>
        ///     Gets the logger factory
        /// </summary>
        private readonly AbbotwareLoggerFactory factory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbbotwareLogger" /> class.
        /// </summary>
        /// <param name="log">Log4net's ILog</param>
        /// <param name="factory">logger factory</param>
        public AbbotwareLogger(Log4netILog log, AbbotwareLoggerFactory factory)
            : this(log.Logger, factory)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbbotwareLogger" /> class.
        /// </summary>
        /// <param name="logger">Log4net's ILogger</param>
        /// <param name="factory">logger factory</param>
        public AbbotwareLogger(Log4netILogger logger, AbbotwareLoggerFactory factory)
        {
            this.logger = Arguments.EnsureNotNull(logger, nameof(logger));
            this.factory = Arguments.EnsureNotNull(factory, nameof(factory));
        }

        /// <inheritdoc />
        public bool IsDebugEnabled
        {
            get { return this.logger.IsEnabledFor(Level.Debug); }
        }

        /// <inheritdoc />
        public bool IsErrorEnabled
        {
            get { return this.logger.IsEnabledFor(Level.Error); }
        }

        /// <inheritdoc />
        public bool IsFatalEnabled
        {
            get { return this.logger.IsEnabledFor(Level.Fatal); }
        }

        /// <inheritdoc />
        public bool IsInfoEnabled
        {
            get { return this.logger.IsEnabledFor(Level.Info); }
        }

        /// <inheritdoc />
        public bool IsWarnEnabled
        {
            get { return this.logger.IsEnabledFor(Level.Warn); }
        }

        /// <inheritdoc />
        public bool IsTraceEnabled
        {
            get { return this.logger.IsEnabledFor(Level.Trace); }
        }

        /// <inheritdoc />
        Log4netILogger ILoggerWrapper.Logger
        {
            get
            {
                return this.logger;
            }
        }

        /// <inheritdoc />
        string AbbotwareILoggerV2.Name { get; } = string.Empty;

        /// <inheritdoc />
        void Log4netILog.Debug(object message, Exception exception)
        {
            (this as AbbotwareILoggerV2).Debug(AbbotwareLogger.ObjectToString(message), exception);
        }

        /// <inheritdoc />
        void Log4netILog.Debug(object message)
        {
            this.Debug(AbbotwareLogger.ObjectToString(message));
        }

        /// <inheritdoc />
        void Log4netILog.DebugFormat(IFormatProvider provider, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Debug(format, args);
        }

        /// <inheritdoc />
        void Log4netILog.DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            this.DebugFormat(format, arg0, arg1, arg2);
        }

        /// <inheritdoc />
        void Log4netILog.DebugFormat(string format, object arg0, object arg1)
        {
            this.DebugFormat(format, arg0, arg1);
        }

        /// <inheritdoc />
        void Log4netILog.DebugFormat(string format, object arg0)
        {
            this.DebugFormat(format, arg0);
        }

        /// <inheritdoc />
        void Log4netILog.DebugFormat(string format, params object?[]? args)
        {
            this.DebugFormat(format, args);
        }

        /// <inheritdoc />
        void Log4netILog.Error(object message, Exception exception)
        {
            (this as AbbotwareILoggerV2).Error(AbbotwareLogger.ObjectToString(message), exception);
        }

        /// <inheritdoc />
        void Log4netILog.Error(object message)
        {
            this.Error(AbbotwareLogger.ObjectToString(message));
        }

        /// <inheritdoc />
        void Log4netILog.ErrorFormat(IFormatProvider provider, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Error(format, args);
        }

        /// <inheritdoc />
        void Log4netILog.ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            this.ErrorFormat(format, arg0, arg1, arg2);
        }

        /// <inheritdoc />
        void Log4netILog.ErrorFormat(string format, object arg0, object arg1)
        {
            this.ErrorFormat(format, arg0, arg1);
        }

        /// <inheritdoc />
        void Log4netILog.ErrorFormat(string format, object arg0)
        {
            this.ErrorFormat(format, arg0);
        }

        /// <inheritdoc />
        void Log4netILog.ErrorFormat(string format, params object?[]? args)
        {
            this.ErrorFormat(format, args);
        }

        /// <inheritdoc />
        void Log4netILog.Fatal(object message, Exception exception)
        {
            (this as AbbotwareILoggerV2).Error(AbbotwareLogger.ObjectToString(message), exception);
        }

        /// <inheritdoc />
        void Log4netILog.Fatal(object message)
        {
            this.Fatal(AbbotwareLogger.ObjectToString(message));
        }

        /// <inheritdoc />
        void Log4netILog.FatalFormat(IFormatProvider provider, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Error(format, args);
        }

        /// <inheritdoc />
        void Log4netILog.FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            this.FatalFormat(format, arg0, arg1, arg2);
        }

        /// <inheritdoc />
        void Log4netILog.FatalFormat(string format, object arg0, object arg1)
        {
            this.FatalFormat(format, arg0, arg1);
        }

        /// <inheritdoc />
        void Log4netILog.FatalFormat(string format, object arg0)
        {
            this.FatalFormat(format, arg0);
        }

        /// <inheritdoc />
        void Log4netILog.FatalFormat(string format, params object?[]? args)
        {
            this.FatalFormat(format, args);
        }

        /// <inheritdoc />
        void Log4netILog.Info(object message, Exception exception)
        {
            (this as AbbotwareILoggerV2).Info(AbbotwareLogger.ObjectToString(message), exception);
        }

        /// <inheritdoc />
        void Log4netILog.Info(object message)
        {
            this.Info(AbbotwareLogger.ObjectToString(message));
        }

        /// <inheritdoc />
        void Log4netILog.InfoFormat(IFormatProvider provider, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Info(format, args);
        }

        /// <inheritdoc />
        void Log4netILog.InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            this.InfoFormat(format, arg0, arg1, arg2);
        }

        /// <inheritdoc />
        void Log4netILog.InfoFormat(string format, object arg0, object arg1)
        {
            this.InfoFormat(format, arg0, arg1);
        }

        /// <inheritdoc />
        void Log4netILog.InfoFormat(string format, object arg0)
        {
            this.InfoFormat(format, arg0);
        }

        /// <inheritdoc />
        void Log4netILog.InfoFormat(string format, params object?[]? args)
        {
            this.InfoFormat(format, args);
        }

        /// <inheritdoc />
        void Log4netILog.Warn(object message, Exception exception)
        {
            (this as AbbotwareILoggerV2).Warn(AbbotwareLogger.ObjectToString(message), exception);
        }

        /// <inheritdoc />
        void Log4netILog.Warn(object message)
        {
            this.Warn(AbbotwareLogger.ObjectToString(message));
        }

        /// <inheritdoc />
        void Log4netILog.WarnFormat(IFormatProvider provider, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Warn(format, args);
        }

        /// <inheritdoc />
        void Log4netILog.WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            this.WarnFormat(format, arg0, arg1, arg2);
        }

        /// <inheritdoc />
        void Log4netILog.WarnFormat(string format, object arg0, object arg1)
        {
            this.WarnFormat(format, arg0, arg1);
        }

        /// <inheritdoc />
        void Log4netILog.WarnFormat(string format, object arg0)
        {
            this.WarnFormat(format, arg0);
        }

        /// <inheritdoc />
        void Log4netILog.WarnFormat(string format, params object?[]? args)
        {
            this.WarnFormat(format, args);
        }

        /// <inheritdoc />
        CastleILogger CastleILogger.CreateChildLogger(string name)
        {
            return (CastleILogger)this.factory.Create(this.logger.Name + "." + name);
        }

        /// <inheritdoc />
        public void Debug(string message)
        {
            if (this.IsDebugEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Debug, message, null);
            }
        }

        /// <inheritdoc />
        void CastleILogger.Debug(Func<string> messageFactory)
        {
            (this as AbbotwareILoggerV2).Debug(messageFactory());
        }

        /// <inheritdoc />
        void CastleILogger.Debug(string message, Exception exception)
        {
            (this as AbbotwareILoggerV2).Debug(message, exception);
        }

        /// <inheritdoc />
        public void DebugFormat(string format, params object?[]? args)
        {
            if (this.IsDebugEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Debug, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
            }
        }

        /// <inheritdoc />
        void CastleILogger.DebugFormat(Exception exception, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Debug(exception, format, args);
        }

        /// <inheritdoc />
        void CastleILogger.DebugFormat(IFormatProvider formatProvider, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Debug(format, args);
        }

        /// <inheritdoc />
        void CastleILogger.DebugFormat(Exception exception, IFormatProvider formatProvider, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Debug(exception, format, args);
        }

        /// <inheritdoc />
        public void Error(string message)
        {
            if (this.IsErrorEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Error, message, null);
            }
        }

        /// <inheritdoc />
        void CastleILogger.Error(Func<string> messageFactory)
        {
            (this as AbbotwareILoggerV2).Error(messageFactory());
        }

        /// <inheritdoc />
        void CastleILogger.Error(string message, Exception exception)
        {
            (this as AbbotwareILoggerV2).Error(message, exception);
        }

        /// <inheritdoc />
        public void ErrorFormat(string format, params object?[]? args)
        {
            if (this.IsErrorEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Error, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
            }
        }

        /// <inheritdoc />
        void CastleILogger.ErrorFormat(Exception exception, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Error(exception, format, args);
        }

        /// <inheritdoc />
        void CastleILogger.ErrorFormat(IFormatProvider formatProvider, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Error(format, args);
        }

        /// <inheritdoc />
        void CastleILogger.ErrorFormat(Exception exception, IFormatProvider formatProvider, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Error(exception, format, args);
        }

        /// <inheritdoc />
        public void Fatal(string message)
        {
            if (this.IsFatalEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Fatal, message, null);
            }
        }

        /// <inheritdoc />
        void CastleILogger.Fatal(Func<string> messageFactory)
        {
            (this as AbbotwareILoggerV2).Fatal(messageFactory());
        }

        /// <inheritdoc />
        void CastleILogger.Fatal(string message, Exception exception)
        {
            (this as AbbotwareILoggerV2).Fatal(message, exception);
        }

        /// <inheritdoc />
        public void FatalFormat(string format, params object?[]? args)
        {
            if (this.IsFatalEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Fatal, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
            }
        }

        /// <inheritdoc />
        void CastleILogger.FatalFormat(Exception exception, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Fatal(exception, format, args);
        }

        /// <inheritdoc />
        void CastleILogger.FatalFormat(IFormatProvider formatProvider, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Fatal(format, args);
        }

        /// <inheritdoc />
        void CastleILogger.FatalFormat(Exception exception, IFormatProvider formatProvider, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Fatal(exception, format, args);
        }

        /// <inheritdoc />
        public void Info(string message)
        {
            if (this.IsInfoEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Info, message, null);
            }
        }

        /// <inheritdoc />
        void CastleILogger.Info(Func<string> messageFactory)
        {
            (this as AbbotwareILoggerV2).Info(messageFactory());
        }

        /// <inheritdoc />
        void CastleILogger.Info(string message, Exception exception)
        {
            (this as AbbotwareILoggerV2).Info(message, exception);
        }

        /// <inheritdoc />
        public void InfoFormat(string format, params object?[]? args)
        {
            if (this.IsInfoEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Info, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
            }
        }

        /// <inheritdoc />
        void CastleILogger.InfoFormat(Exception exception, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Info(exception, format, args);
        }

        /// <inheritdoc />
        void CastleILogger.InfoFormat(IFormatProvider formatProvider, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Info(format, args);
        }

        /// <inheritdoc />
        void CastleILogger.InfoFormat(Exception exception, IFormatProvider formatProvider, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Info(exception, format, args);
        }

        /// <inheritdoc />
        public void Warn(string message)
        {
            if (this.IsWarnEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Warn, message, null);
            }
        }

        /// <inheritdoc />
        void CastleILogger.Warn(Func<string> messageFactory)
        {
            (this as AbbotwareILoggerV2).Warn(messageFactory());
        }

        /// <inheritdoc />
        void CastleILogger.Warn(string message, Exception exception)
        {
            (this as AbbotwareILoggerV2).Warn(message, exception);
        }

        /// <inheritdoc />
        public void WarnFormat(string format, params object?[]? args)
        {
            if (this.IsWarnEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Warn, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
            }
        }

        /// <inheritdoc />
        void CastleILogger.WarnFormat(Exception exception, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Warn(exception, format, args);
        }

        /// <inheritdoc />
        void CastleILogger.WarnFormat(IFormatProvider formatProvider, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Warn(format, args);
        }

        /// <inheritdoc />
        void CastleILogger.WarnFormat(Exception exception, IFormatProvider formatProvider, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Warn(exception, format, args);
        }

        /// <inheritdoc />
        public void Trace(string message)
        {
            if (this.IsTraceEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Trace, message, null);
            }
        }

        /// <inheritdoc />
        void CastleILogger.Trace(Func<string> messageFactory)
        {
            (this as AbbotwareILoggerV2).Trace(messageFactory());
        }

        /// <inheritdoc />
        void CastleILogger.Trace(string message, Exception exception)
        {
            (this as AbbotwareILoggerV2).Trace(message, exception);
        }

        /// <inheritdoc />
        public void TraceFormat(string format, params object?[]? args)
        {
            if (this.IsTraceEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Trace, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
            }
        }

        /// <inheritdoc />
        void CastleILogger.TraceFormat(Exception exception, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Trace(exception, format, args);
        }

        /// <inheritdoc />
        void CastleILogger.TraceFormat(IFormatProvider formatProvider, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Trace(format, args);
        }

        /// <inheritdoc />
        void CastleILogger.TraceFormat(Exception exception, IFormatProvider formatProvider, string format, params object?[]? args)
        {
            (this as AbbotwareILoggerV2).Trace(exception, format, args);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.logger?.ToString() ?? string.Empty;
        }

        /// <inheritdoc />
        void AbbotwareILoggerV2.Info<T>(Func<T, string> function, T parameter)
        {
            if (this.IsInfoEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Info, function(parameter), null);
            }
        }

        /// <inheritdoc />
        void AbbotwareILoggerV2.Info(string message, params object?[]? args)
        {
            if (this.IsInfoEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Info, new SystemStringFormat(CultureInfo.InvariantCulture, message, args), null);
            }
        }

        /// <inheritdoc />
        void AbbotwareILoggerV2.Info(Exception exception, string message, params object?[]? args)
        {
            if (this.IsInfoEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Info, new SystemStringFormat(CultureInfo.InvariantCulture, message, args), exception);
            }
        }

        /// <inheritdoc />
        void AbbotwareILoggerV2.Debug<T>(Func<T, string> function, T parameter)
        {
            if (this.IsDebugEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Debug, function(parameter), null);
            }
        }

        /// <inheritdoc />
        void AbbotwareILoggerV2.Debug(string message, params object?[]? args)
        {
            if (this.IsDebugEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Debug, new SystemStringFormat(CultureInfo.InvariantCulture, message, args), null);
            }
        }

        /// <inheritdoc />
        void AbbotwareILoggerV2.Debug(Exception exception, string message, params object?[]? args)
        {
            if (this.IsDebugEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Debug, new SystemStringFormat(CultureInfo.InvariantCulture, message, args), exception);
            }
        }

        /// <inheritdoc />
        void AbbotwareILoggerV2.Warn<T>(Func<T, string> function, T parameter)
        {
            if (this.IsWarnEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Warn, function(parameter), null);
            }
        }

        /// <inheritdoc />
        void AbbotwareILoggerV2.Warn(string message, params object?[]? args)
        {
            if (this.IsWarnEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Warn, new SystemStringFormat(CultureInfo.InvariantCulture, message, args), null);
            }
        }

        /// <inheritdoc />
        void AbbotwareILoggerV2.Warn(Exception exception, string message, params object?[]? args)
        {
            if (this.IsWarnEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Warn, new SystemStringFormat(CultureInfo.InvariantCulture, message, args), exception);
            }
        }

        /// <inheritdoc />
        void AbbotwareILoggerV2.Error<T>(Func<T, string> function, T parameter)
        {
            if (this.IsErrorEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Error, function(parameter), null);
            }
        }

        /// <inheritdoc />
        void AbbotwareILoggerV2.Error(string message, params object?[]? args)
        {
            if (this.IsErrorEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Error, new SystemStringFormat(CultureInfo.InvariantCulture, message, args), null);
            }
        }

        /// <inheritdoc />
        void AbbotwareILoggerV2.Error(Exception exception, string message, params object?[]? args)
        {
            if (this.IsErrorEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Error, new SystemStringFormat(CultureInfo.InvariantCulture, message, args), exception);
            }
        }

        /// <inheritdoc />
        IDisposable AbbotwareILoggerV2.BeginScope(string context)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        AbbotwareILoggerV2 AbbotwareILoggerV2.Create(string name)
        {
            return this.factory.Create(this.logger.Name + "." + name);
        }

        /// <inheritdoc />
        public void Fatal<T>(Func<T, string> function, T parameter)
        {
            if (this.IsFatalEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Fatal, function?.Invoke(parameter), null);
            }
        }

        /// <inheritdoc />
        public void Fatal([Localizable(false)] string message, params object?[]? args)
        {
            if (this.IsFatalEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Fatal, new SystemStringFormat(CultureInfo.InvariantCulture, message, args), null);
            }
        }

        /// <inheritdoc />
        public void Fatal(Exception exception, [Localizable(false)] string message, params object?[]? args)
        {
            if (this.IsFatalEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Fatal, new SystemStringFormat(CultureInfo.InvariantCulture, message, args), exception);
            }
        }

        /// <inheritdoc />
        public void Trace<T>(Func<T, string> function, T parameter)
        {
            if (this.IsErrorEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Trace, function?.Invoke(parameter), null);
            }
        }

        /// <inheritdoc />
        public void Trace([Localizable(false)] string message, params object?[]? args)
        {
            if (this.IsTraceEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Trace, new SystemStringFormat(CultureInfo.InvariantCulture, message, args), null);
            }
        }

        /// <inheritdoc />
        public void Trace(Exception exception, [Localizable(false)] string message, params object?[]? args)
        {
            if (this.IsTraceEnabled)
            {
                this.logger.Log(AbbotwareLogger.DeclaringType, Level.Trace, new SystemStringFormat(CultureInfo.InvariantCulture, message, args), exception);
            }
        }

        /// <summary>
        ///     coverts the object (or null) to a string
        /// </summary>
        /// <param name="objectForMessage">object to convert</param>
        /// <returns>string message</returns>
        private static string ObjectToString(object objectForMessage)
        {
            return objectForMessage?.ToString() ?? string.Empty;
        }
    }
}