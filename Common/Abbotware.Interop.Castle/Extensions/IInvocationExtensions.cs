// -----------------------------------------------------------------------
// <copyright file="IInvocationExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Castle.Extensions
{
    using System;
    using Abbotware.Core;
    using global::Castle.DynamicProxy;

    /// <summary>
    /// Extensions to the IInvocation class
    /// </summary>
    public static class IInvocationExtensions
    {
        /// <summary>
        /// Gets the method name for the current IInvocation object
        /// </summary>
        /// <param name="invocation">IInvocation being extended</param>
        /// <returns>method name</returns>
        public static string GetMethodName(this IInvocation invocation)
        {
            Arguments.NotNull(invocation, nameof(invocation));

#pragma warning disable CA1062 // Validate arguments of public methods
            var targetType = invocation.TargetType;
#pragma warning restore CA1062 // Validate arguments of public methods
            var concreteMethod = invocation.GetConcreteMethod();

            return targetType.FullName + "." + concreteMethod.Name;
        }
    }
}