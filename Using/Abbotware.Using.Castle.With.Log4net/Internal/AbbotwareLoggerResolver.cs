// -----------------------------------------------------------------------
// <copyright file="AbbotwareLoggerResolver.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Using.Castle.Internal
{
    using global::Castle.Core;
    using global::Castle.MicroKernel;
    using global::Castle.MicroKernel.Context;
    using AbbotwareILoggerV2 = Abbotware.Core.Logging.ILogger;
    using CastleILogger = global::Castle.Core.Logging.ILogger;

    /// <summary>
    ///     Logger Resolver for Abbotware
    /// </summary>
    public class AbbotwareLoggerResolver : ISubDependencyResolver
    {
        /// <summary>
        ///     injected logger Factory
        /// </summary>
        private readonly AbbotwareLoggerFactory loggerFactory;

        /// <summary>
        ///     name of logger to resolve
        /// </summary>
        private readonly string logName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbbotwareLoggerResolver" /> class.
        /// </summary>
        /// <param name="loggerFactory">logger factory</param>
        public AbbotwareLoggerResolver(AbbotwareLoggerFactory loggerFactory)
            : this(loggerFactory, string.Empty)
        {
            Abbotware.Core.Arguments.NotNull(loggerFactory, nameof(loggerFactory));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AbbotwareLoggerResolver" /> class.
        /// </summary>
        /// <param name="loggerFactory">logger factory</param>
        /// <param name="name">name of logger</param>
        public AbbotwareLoggerResolver(AbbotwareLoggerFactory loggerFactory, string name)
        {
            Abbotware.Core.Arguments.NotNull(loggerFactory, nameof(loggerFactory));
            Abbotware.Core.Arguments.NotNull(name, nameof(name));

            this.loggerFactory = loggerFactory;
            this.logName = name;
        }

        /// <inheritdoc />
        public bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            dependency = Abbotware.Core.Arguments.EnsureNotNull(dependency, nameof(dependency));

            if (dependency.TargetType == typeof(CastleILogger))
            {
                return true;
            }

            if (dependency.TargetType == typeof(AbbotwareILoggerV2))
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            model = Abbotware.Core.Arguments.EnsureNotNull(model, nameof(model));

            return string.IsNullOrEmpty(this.logName)
                ? this.loggerFactory.Create(model.Implementation)
                : this.loggerFactory.Create(this.logName).Create(model.Implementation.Name);
        }
    }
}