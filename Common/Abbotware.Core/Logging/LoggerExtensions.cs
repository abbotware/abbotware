﻿// -----------------------------------------------------------------------
// <copyright file="LoggerExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
#if NET5_0_OR_GREATER

namespace Abbotware.Core.Logging
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using Microsoft.Extensions.Logging;
    using ILogger2 = Microsoft.Extensions.Logging.ILogger;

    /// <summary>
    /// ILogger Extension Methods
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// Log a message at Critical level
        /// </summary>
        /// <param name="logger">extended logger</param>
        /// <param name="message">message</param>
        /// <param name="line">compiler injected line number</param>
        /// <param name="member">compiler injected class member</param>
        /// <param name="file">compiler injected file</param>
        public static void Critical([NotNull] this ILogger2 logger, string message, [CallerLineNumber] int? line = null, [CallerMemberName] string? member = null, [CallerFilePath] string? file = null)
        {
            logger.Critical(null, message, line, member, file);
        }

        /// <summary>
        /// Log a message at Critical level
        /// </summary>
        /// <param name="logger">extended logger</param>
        /// <param name="exception">exception</param>
        /// <param name="message">message</param>
        /// <param name="line">compiler injected line number</param>
        /// <param name="member">compiler injected class member</param>
        /// <param name="file">compiler injected file</param>
        public static void Critical([NotNull] this ILogger2 logger, Exception? exception, string message, [CallerLineNumber] int? line = null, [CallerMemberName] string? member = null, [CallerFilePath] string? file = null)
        {
            Log(logger!, LogLevel.Critical, exception, message, line, member, file);
        }

        /// <summary>
        /// Log a message at Error level
        /// </summary>
        /// <param name="logger">extended logger</param>
        /// <param name="message">message</param>
        /// <param name="line">compiler injected line number</param>
        /// <param name="member">compiler injected class member</param>
        /// <param name="file">compiler injected file</param>
        public static void Error([NotNull] this ILogger2 logger, string message, [CallerLineNumber] int? line = null, [CallerMemberName] string? member = null, [CallerFilePath] string? file = null)
        {
            logger.Error(null, message, line, member, file);
        }

        /// <summary>
        /// Log a message at Error level
        /// </summary>
        /// <param name="logger">extended logger</param>
        /// <param name="exception">exception</param>
        /// <param name="message">message</param>
        /// <param name="line">compiler injected line number</param>
        /// <param name="member">compiler injected class member</param>
        /// <param name="file">compiler injected file</param>
        public static void Error([NotNull] this ILogger2 logger, Exception? exception, string message, [CallerLineNumber] int? line = null, [CallerMemberName] string? member = null, [CallerFilePath] string? file = null)
        {
            Log(logger, LogLevel.Error, exception, message, line, member, file);
        }

        /// <summary>
        /// Log a message at Warning level
        /// </summary>
        /// <param name="logger">extended logger</param>
        /// <param name="message">message</param>
        /// <param name="line">compiler injected line number</param>
        /// <param name="member">compiler injected class member</param>
        /// <param name="file">compiler injected file</param>
        public static void Warn([NotNull] this ILogger2 logger, string message, [CallerLineNumber] int? line = null, [CallerMemberName] string? member = null, [CallerFilePath] string? file = null)
        {
            logger.Warn(null, message, line, member, file);
        }

        /// <summary>
        /// Log a message at Warning level
        /// </summary>
        /// <param name="logger">extended logger</param>
        /// <param name="exception">exception</param>
        /// <param name="message">message</param>
        /// <param name="line">compiler injected line number</param>
        /// <param name="member">compiler injected class member</param>
        /// <param name="file">compiler injected file</param>
        public static void Warn([NotNull] this ILogger2 logger, Exception? exception, string message, [CallerLineNumber] int? line = null, [CallerMemberName] string? member = null, [CallerFilePath] string? file = null)
        {
            Log(logger, LogLevel.Warning, exception, message, line, member, file);
        }

        /// <summary>
        /// Log a message at Information level
        /// </summary>
        /// <param name="logger">extended logger</param>
        /// <param name="message">message</param>
        /// <param name="line">compiler injected line number</param>
        /// <param name="member">compiler injected class member</param>
        /// <param name="file">compiler injected file</param>
        public static void Info([NotNull] this ILogger2 logger, string message, [CallerLineNumber] int? line = null, [CallerMemberName] string? member = null, [CallerFilePath] string? file = null)
        {
            logger.Error(null, message, line, member, file);
        }

        /// <summary>
        /// Log a message at Information level
        /// </summary>
        /// <param name="logger">extended logger</param>
        /// <param name="exception">exception</param>
        /// <param name="message">message</param>
        /// <param name="line">compiler injected line number</param>
        /// <param name="member">compiler injected class member</param>
        /// <param name="file">compiler injected file</param>
        public static void Info([NotNull] this ILogger2 logger, Exception? exception, string message, [CallerLineNumber] int? line = null, [CallerMemberName] string? member = null, [CallerFilePath] string? file = null)
        {
            Log(logger, LogLevel.Information, exception, message, line, member, file);
        }

        /// <summary>
        /// Log a message at Debug level
        /// </summary>
        /// <param name="logger">extended logger</param>
        /// <param name="message">message</param>
        /// <param name="line">compiler injected line number</param>
        /// <param name="member">compiler injected class member</param>
        /// <param name="file">compiler injected file</param>
        public static void Debug([NotNull] this ILogger2 logger, string message, [CallerLineNumber] int? line = null, [CallerMemberName] string? member = null, [CallerFilePath] string? file = null)
        {
            logger.Debug(null, message, line, member, file);
        }

        /// <summary>
        /// Log a message at Debug level
        /// </summary>
        /// <param name="logger">extended logger</param>
        /// <param name="exception">exception</param>
        /// <param name="message">message</param>
        /// <param name="line">compiler injected line number</param>
        /// <param name="member">compiler injected class member</param>
        /// <param name="file">compiler injected file</param>
        public static void Debug([NotNull] this ILogger2 logger, Exception? exception, string message, [CallerLineNumber] int? line = null, [CallerMemberName] string? member = null, [CallerFilePath] string? file = null)
        {
            Log(logger, LogLevel.Debug, exception, message, line, member, file);
        }

        /// <summary>
        /// Log a message at Trace level
        /// </summary>
        /// <param name="logger">extended logger</param>
        /// <param name="message">message</param>
        /// <param name="line">compiler injected line number</param>
        /// <param name="member">compiler injected class member</param>
        /// <param name="file">compiler injected file</param>
        [Conditional("TRACE")]
        public static void Trace([NotNull] this ILogger2 logger, string message, [CallerLineNumber] int? line = null, [CallerMemberName] string? member = null, [CallerFilePath] string? file = null)
        {
            logger.Trace(null, message, line, member, file);
        }

        /// <summary>
        /// Log a message at Debug level
        /// </summary>
        /// <param name="logger">extended logger</param>
        /// <param name="exception">exception</param>
        /// <param name="message">message</param>
        /// <param name="line">compiler injected line number</param>
        /// <param name="member">compiler injected class member</param>
        /// <param name="file">compiler injected file</param>
        [Conditional("TRACE")]
        public static void Trace([NotNull] this ILogger2 logger, Exception? exception, string message, [CallerLineNumber] int? line = null, [CallerMemberName] string? member = null, [CallerFilePath] string? file = null)
        {
            Log(logger, LogLevel.Trace, exception, message, line, member, file);
        }

        /// <summary>
        /// Log a message at critical level
        /// </summary>
        /// <param name="logger">extended logger</param>
        /// <param name="level">log level</param>
        /// <param name="exception">exception</param>
        /// <param name="message">message</param>
        /// <param name="line">compiler injected line number</param>
        /// <param name="member">compiler injected class member</param>
        /// <param name="file">compiler injected file</param>
        private static void Log([NotNull] ILogger2 logger, LogLevel level, Exception? exception, string message, [CallerLineNumber] int? line = null, [CallerMemberName] string? member = null, [CallerFilePath] string? file = null)
        {
            var lld = new LogLocationDetail(line, member, file);

            logger.Log(level, default, lld, exception, (s, e) => message);
        }
    }
}

#endif