// -----------------------------------------------------------------------
// <copyright file="TransactionScopeInterceptor.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Castle.Plugins.Aspects
{
    using System;
    using System.Diagnostics;
    using System.Transactions;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Interop.Castle.ExtensionPoints.Aspects;
    using Abbotware.Interop.Castle.Extensions;
    using global::Castle.DynamicProxy;

    /// <summary>
    /// Interceptor that uses a transaction if required
    /// </summary>
    public class TransactionScopeInterceptor : AttributeInterceptorBase<TransactionScopeAttribute>
    {
        /// <summary>
        /// scope options for transaction
        /// </summary>
        private readonly TransactionScopeOption scopeOption;

        /// <summary>
        /// options for transaction
        /// </summary>
        private readonly TransactionOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionScopeInterceptor"/> class.
        /// </summary>
        /// <param name="scopeOption">Transaction Scope Options</param>
        /// <param name="options">Transaction Options</param>
        /// <param name="logger">injected logger for the class</param>
        public TransactionScopeInterceptor(TransactionScopeOption scopeOption, TransactionOptions options, ILogger logger)
            : base(logger)
        {
            Arguments.NotNull(logger, nameof(logger));

            this.scopeOption = scopeOption;
            this.options = options;
        }

        /// <summary>
        /// Intercepts and performs timing logic
        /// </summary>
        /// <param name="invocation">current invocation site</param>
        /// <param name="attributes">transaction attributes</param>
        protected override void OnIntercepted(IInvocation invocation, TransactionScopeAttribute[] attributes)
        {
            invocation = Arguments.EnsureNotNull(invocation, nameof(invocation));

            try
            {
                var esio = EnterpriseServicesInteropOption.None;

                using var ts = new TransactionScope(this.scopeOption, this.options, esio);

                invocation.Proceed();

                var stopwatch = Stopwatch.StartNew();

                ts.Complete();

                var timeSpan = stopwatch.ElapsedMilliseconds;

                this.Logger.Debug("COMMIT:{0} Duration(ms):{1}", invocation.GetConcreteMethod(), timeSpan);
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex, "ROLLBACK:{0} Message:{1}", invocation.GetMethodName(), ex.Message);
                throw;
            }
        }
    }
}