// -----------------------------------------------------------------------
// <copyright file="MicrosoftLoggerResolver.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Using.Castle.Internal
{
    using global::Castle.Core;
    using global::Castle.MicroKernel;
    using global::Castle.MicroKernel.Context;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Logger Resolver for Abbotware
    /// </summary>
    public class MicrosoftLoggerResolver : ISubDependencyResolver
    {
        /// <summary>
        ///     injected logger Factory
        /// </summary>
        private readonly ILoggerFactory loggerFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MicrosoftLoggerResolver" /> class.
        /// </summary>
        /// <param name="loggerFactory">logger factory</param>
        public MicrosoftLoggerResolver(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = Abbotware.Core.Arguments.EnsureNotNull(loggerFactory, nameof(loggerFactory));
        }

        /// <inheritdoc />
        public bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            dependency = Abbotware.Core.Arguments.EnsureNotNull(dependency, nameof(dependency));

            if (dependency.TargetType == typeof(ILogger))
            {
                return true;
            }

            if (dependency.TargetType == typeof(ILogger<>))
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            model = Abbotware.Core.Arguments.EnsureNotNull(model, nameof(model));

            return this.loggerFactory.CreateLogger(model.Implementation);
        }
    }
}