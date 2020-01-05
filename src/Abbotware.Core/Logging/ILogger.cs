// -----------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Logging
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Generic Interface for logging
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Gets the logger name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Lazily log an Info string using a function with parameters
        /// </summary>
        /// <typeparam name="T">parameter type</typeparam>
        /// <param name="function">function that returns a log</param>
        /// <param name="parameter">function's parameter</param>
        void Info<T>(Func<T, string> function, T parameter);

        /// <summary>
        /// Log an Info level formatted message
        /// </summary>
        /// <param name="message">format string</param>
        /// <param name="args">format parameters</param>
        void Info([Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Log an Info level string using a function with parameters with an exception (first parameter)
        /// </summary>
        /// <param name="exception">exception to log</param>
        /// <param name="message">format string</param>
        /// <param name="args">format parameters</param>
        void Info(Exception exception, [Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Lazily log a Debug string using a function with parameters
        /// </summary>
        /// <typeparam name="T">parameter type</typeparam>
        /// <param name="function">function that returns a log</param>
        /// <param name="parameter">function's parameter</param>
        void Debug<T>(Func<T, string> function, T parameter);

        /// <summary>
        /// Log a Debug level formatted message
        /// </summary>
        /// <param name="message">format string</param>
        /// <param name="args">format parameters</param>
        void Debug([Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Log a Debug level string using a function with parameters with an exception (first parameter)
        /// </summary>
        /// <param name="exception">exception to log</param>
        /// <param name="message">format string</param>
        /// <param name="args">format parameters</param>
        void Debug(Exception exception, [Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Lazily log a Warn string using a function with parameters
        /// </summary>
        /// <typeparam name="T">parameter type</typeparam>
        /// <param name="function">function that returns a log</param>
        /// <param name="parameter">function's parameter</param>
        void Warn<T>(Func<T, string> function, T parameter);

        /// <summary>
        /// Log a Warn level formatted message
        /// </summary>
        /// <param name="message">format string</param>
        /// <param name="args">format parameters</param>
        void Warn([Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Log a Warn level string using a function with parameters with an exception (first parameter)
        /// </summary>
        /// <param name="exception">exception to log</param>
        /// <param name="message">format string</param>
        /// <param name="args">format parameters</param>
        void Warn(Exception exception, [Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Lazily log an Error string using a function with parameters
        /// </summary>
        /// <typeparam name="T">parameter type</typeparam>
        /// <param name="function">function that returns a log</param>
        /// <param name="parameter">function's parameter</param>
        void Error<T>(Func<T, string> function, T parameter);

        /// <summary>
        /// Log an Error level formatted message
        /// </summary>
        /// <param name="message">format string</param>
        /// <param name="args">format parameters</param>
        void Error([Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Log an Error level string using a function with parameters with an exception (first parameter)
        /// </summary>
        /// <param name="exception">exception to log</param>
        /// <param name="message">format string</param>
        /// <param name="args">format parameters</param>
        void Error(Exception exception, [Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Lazily log a Fatal string using a function with parameters
        /// </summary>
        /// <typeparam name="T">parameter type</typeparam>
        /// <param name="function">function that returns a log</param>
        /// <param name="parameter">function's parameter</param>
        void Fatal<T>(Func<T, string> function, T parameter);

        /// <summary>
        /// Log a Fatal level formatted message
        /// </summary>
        /// <param name="message">format string</param>
        /// <param name="args">format parameters</param>
        void Fatal([Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Log a Fatal level string using a function with parameters with an exception (first parameter)
        /// </summary>
        /// <param name="exception">exception to log</param>
        /// <param name="message">format string</param>
        /// <param name="args">format parameters</param>
        void Fatal(Exception exception, [Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Lazily log a Trace string using a function with parameters
        /// </summary>
        /// <typeparam name="T">parameter type</typeparam>
        /// <param name="function">function that returns a log</param>
        /// <param name="parameter">function's parameter</param>
        void Trace<T>(Func<T, string> function, T parameter);

        /// <summary>
        /// Log a Fatal Trace formatted message
        /// </summary>
        /// <param name="message">format string</param>
        /// <param name="args">format parameters</param>
        void Trace([Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Log a Fatal Trace string using a function with parameters with an exception (first parameter)
        /// </summary>
        /// <param name="exception">exception to log</param>
        /// <param name="message">format string</param>
        /// <param name="args">format parameters</param>
        void Trace(Exception exception, [Localizable(false)] string message, params object[] args);

        /// <summary>
        /// creates a logger scope
        /// </summary>
        /// <param name="context">context to push to logger stack</param>
        /// <returns>disposable handle</returns>
        IDisposable BeginScope(string context);

        /// <summary>
        /// creates a sub logger
        /// </summary>
        /// <param name="name">name of the sub logger</param>
        /// <returns>sub logger</returns>
        ILogger Create(string name);
    }
}