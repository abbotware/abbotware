// -----------------------------------------------------------------------
// <copyright file="AttributeInterceptorBase{TAttribute}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Castle.ExtensionPoints.Aspects
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using global::Castle.DynamicProxy;

    /// <summary>
    ///     Base class for implementing an attribute based interceptor
    /// </summary>
    /// <typeparam name="TAttribute">Type of attribute</typeparam>
    public abstract class AttributeInterceptorBase<TAttribute> : InterceptorBase
        where TAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AttributeInterceptorBase{TAttribute}" /> class.
        /// </summary>
        /// <param name="logger">Injected logger</param>
        protected AttributeInterceptorBase(ILogger logger)
            : base(logger)
        {
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <inheritdoc />
        protected override sealed void OnIntercept(IInvocation invocation)
        {
            invocation = Arguments.EnsureNotNull(invocation, nameof(invocation));

            var attributes = invocation.Method.GetCustomAttributes(typeof(TAttribute), true) as TAttribute[];

            if (attributes?.Length > 0)
            {
                this.OnIntercepted(invocation, attributes);
            }
            else
            {
                invocation.Proceed();
            }
        }

        /// <summary>
        ///     Hook for Custom logic when a method is intercepted
        /// </summary>
        /// <param name="invocation">Invocation site</param>
        /// <param name="attributes">matching attributes on the invocation</param>
        protected abstract void OnIntercepted(IInvocation invocation, TAttribute[] attributes);
    }
}