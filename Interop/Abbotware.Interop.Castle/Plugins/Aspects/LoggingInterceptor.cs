// -----------------------------------------------------------------------
// <copyright file="LoggingInterceptor.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Castle.Plugins.Aspects
{
    using System;
    using System.Collections.Concurrent;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Interop.Castle.ExtensionPoints.Aspects;
    using global::Castle.DynamicProxy;

    /// <summary>
    ///     Interceptor that can be used to log a method call's entry, exit and parameter
    /// </summary>
    public class LoggingInterceptor : InterceptorBase
    {
        /// <summary>
        ///     collection of loggers
        /// </summary>
        private readonly ConcurrentDictionary<Type, ILogger> loggers = new ConcurrentDictionary<Type, ILogger>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoggingInterceptor" /> class.
        /// </summary>
        /// <param name="logger">injected logger</param>
        public LoggingInterceptor(ILogger logger)
            : this(true, true, logger)
        {
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoggingInterceptor" /> class.
        /// </summary>
        /// <param name="shouldLogExit">indicates to log exit</param>
        /// <param name="shouldLogParameters">indicates to log parameters</param>
        /// <param name="logger">logger factory</param>
        public LoggingInterceptor(bool shouldLogExit, bool shouldLogParameters, ILogger logger)
            : base(logger)
        {
            Arguments.NotNull(logger, nameof(logger));

            this.ShouldLogParameters = shouldLogParameters;
            this.ShouldLogExit = shouldLogExit;
        }

        /// <summary>
        ///     Gets a value indicating whether or not to log the function invocation parameters
        /// </summary>
        public bool ShouldLogParameters { get; }

        /// <summary>
        ///     Gets a value indicating whether or not to log the function exit
        /// </summary>
        public bool ShouldLogExit { get; }

        /// <inheritdoc />
        protected override void OnIntercept(IInvocation invocation)
        {
            invocation = Arguments.EnsureNotNull(invocation, nameof(invocation));

            var currentLogger = this.loggers.GetOrAdd(invocation.TargetType, s => this.Logger.Create(s.Name));
            var parameters = !this.ShouldLogParameters ? "..." : LoggingInterceptor.CreateParametersMessage(invocation);

            currentLogger.Debug(LoggingInterceptor.CreateEntryMessage(invocation, parameters));

            try
            {
                invocation.Proceed();

                if (this.ShouldLogExit)
                {
                    currentLogger.Debug(LoggingInterceptor.CreateExitMessage(invocation, parameters));
                }
            }
            catch (Exception ex)
            {
                currentLogger.Error(ex, "EXCEPTION: {0}{1}", invocation.Method.Name, parameters);

                throw;
            }
        }

        /// <summary>
        ///     Creates a log message for entry
        /// </summary>
        /// <param name="invocation">intercepted invocation</param>
        /// <param name="parameters">intercepted invocation parameters</param>
        /// <returns>message for logging</returns>
        private static string CreateEntryMessage(IInvocation invocation, string parameters)
        {
            var message = string.Format(CultureInfo.InvariantCulture, "ENTRY: {0}({1})", invocation.Method.Name, parameters);

            return message;
        }

        /// <summary>
        ///     Creates a log message for exit
        /// </summary>
        /// <param name="invocation">intercepted invocation</param>
        /// <param name="parameters">intercepted invocation parameters</param>
        /// <returns>message for logging</returns>
        private static string CreateExitMessage(IInvocation invocation, string parameters)
        {
            invocation = Arguments.EnsureNotNull(invocation, nameof(invocation));

            var retValue = invocation.Method.ReturnType.Name.Equals("Void", StringComparison.OrdinalIgnoreCase) ? "void" : invocation.ReturnValue;

            var message = string.Format(CultureInfo.InvariantCulture, "EXIT: {0}({1}) ret:({2})", invocation.Method.Name, parameters, retValue);

            return message;
        }

        /// <summary>
        ///     Creates a log message for exit
        /// </summary>
        /// <param name="invocation">intercepted invocation</param>
        /// <returns>message for logging</returns>
        private static string CreateParametersMessage(IInvocation invocation)
        {
            Arguments.NotNull(invocation, nameof(invocation));

            var sb = new StringBuilder(100);
            sb.Append('(');

            foreach (var argument in invocation.Arguments)
            {
                var argumentDescription = argument == null ? "null" : argument.ToString();
                sb.Append(argumentDescription).Append(',');
            }

            if (invocation.Arguments.Any())
            {
                sb.Length--;
            }

            sb.Append(')');

            return sb.ToString();
        }
    }
}