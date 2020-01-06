// -----------------------------------------------------------------------
// <copyright file="TimingInterceptor.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Castle.Plugins.Aspects
{
    using System.Diagnostics;
    using System.Globalization;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Interop.Castle.ExtensionPoints.Aspects;
    using global::Castle.DynamicProxy;

    /// <summary>
    /// Interceptor that can be used for timing method calls
    /// </summary>
    public class TimingInterceptor : InterceptorBase
    {
        /// <summary>
        /// stopwatch timer for the class
        /// </summary>
        private readonly Stopwatch timer = Stopwatch.StartNew();

        /// <summary>
        /// Initializes a new instance of the <see cref="TimingInterceptor"/> class.
        /// </summary>
        /// <param name="logger">injected logger for the class</param>
        public TimingInterceptor(ILogger logger)
            : base(logger)
        {
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <summary>
        /// Intercepts and performs timing logic
        /// </summary>
        /// <param name="invocation">current invocation site</param>
        protected override sealed void OnIntercept(IInvocation invocation)
        {
            Arguments.NotNull(invocation, nameof(invocation));

            var start = this.timer.ElapsedTicks;

            try
            {
#pragma warning disable CA1062 // Validate arguments of public methods
                invocation.Proceed();
#pragma warning restore CA1062 // Validate arguments of public methods
            }
            finally
            {
                var end = this.timer.ElapsedTicks;

                var duration = start - end;

                var concreteClass = invocation.GetConcreteMethodInvocationTarget();

                var concreteMethod = invocation.GetConcreteMethod();

                this.Logger.Debug("Class:{0} Method:{1} Duration:{2}", concreteClass.Name, concreteMethod.Name, duration);
            }
        }
    }
}