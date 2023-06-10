// -----------------------------------------------------------------------
// <copyright file="CastleExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Using.Castle
{
    using Abbotware.Core;
    using Abbotware.Using.Castle.Internal;
    using global::Castle.MicroKernel.Registration;
    using global::Castle.Windsor;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    /// <summary>
    /// Extension methods for castle registraion
    /// </summary>
    public static class CastleExtensions
    {
        /// <summary>
        /// Adds and configures Log4net for the container
        /// </summary>
        /// <param name="container">container</param>
        /// <returns>contianer for fluent api</returns>
        public static IWindsorContainer AddMicrosoftNullLogger(this IWindsorContainer container)
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            container.Register(Component.For<ILoggerFactory>().Instance(NullLoggerFactory.Instance));

            return container.AddFacility<MicrosoftLoggerFacility>();
        }

        /// <summary>
        /// Adds and configures Log4net for the container
        /// </summary>
        /// <param name="container">container</param>
        /// <param name="factory">external logger factory</param>
        /// <returns>contianer for fluent api</returns>
        public static IWindsorContainer AddMicrosoftLogger(this IWindsorContainer container, ILoggerFactory factory)
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            container.Register(Component.For<ILoggerFactory>().Instance(factory));

            return container.AddFacility<MicrosoftLoggerFacility>();
        }
    }
}
