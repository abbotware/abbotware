// -----------------------------------------------------------------------
// <copyright file="InterceptorBase.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Castle.ExtensionPoints.Aspects
{
    using Abbotware.Core;
    using global::Castle.DynamicProxy;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Base class for creating Castle AOP-like Interceptors
    /// </summary>
    public abstract class InterceptorBase : IInterceptor
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InterceptorBase" /> class.
        /// </summary>
        /// <param name="logger">injected logger for the class</param>
        protected InterceptorBase(ILogger logger)
        {
            Arguments.NotNull(logger, nameof(logger));

            this.Logger = logger;
        }

        /// <summary>
        ///     Gets the logger for the class
        /// </summary>
        protected ILogger Logger { get; }

        /// <inheritdoc />
        public void Intercept(IInvocation invocation)
        {
            this.OnIntercept(invocation);
        }

        /// <summary>
        ///     Callback to do the interception logic
        /// </summary>
        /// <param name="invocation">invocation details</param>
        protected abstract void OnIntercept(IInvocation invocation);
    }
}