// -----------------------------------------------------------------------
// <copyright file="CastleExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Using.Castle
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Configuration;
    using Abbotware.Core.Configuration.Models;
    using Abbotware.Interop.AutoMapper;
    using Abbotware.Interop.Castle.Plugins.Installers;
    using AutoMapper;
    using global::Castle.MicroKernel.Registration;
    using global::Castle.Windsor;

    /// <summary>
    /// Extension methods for castle registraion
    /// </summary>
    public static class CastleExtensions
    {
        /// <summary>
        /// Adds and configures default facilities for the container
        /// </summary>
        /// <param name="container">container</param>
        /// <param name="configure">setup options callback</param>
        /// <returns>container for builder chaining</returns>
        public static IWindsorContainer AddDefaultFacilities(this IWindsorContainer container, Action<ContainerOptions> configure)
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            var options = new ContainerOptions();

            configure?.Invoke(options);

            container.Register(Component.For<IContainerOptions>()
                .Instance(options));

            container.Install(new DefaultFacilitiesInstaller(options));

            return container;
        }

        /// <summary>
        /// Register and Resolve and object
        /// </summary>
        /// <typeparam name="T">context type</typeparam>
        /// <param name="container">container</param>
        /// <returns>resovled item</returns>
        public static T RegisterAndResolve<T>(this IWindsorContainer container)
            where T : class
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            container.Register(Component.For<T>()
                .ImplementedBy<T>());

            return container.Resolve<T>();
        }

        /// <summary>
        /// Creates and registers a mapper with the provided profile
        /// </summary>
        /// <typeparam name="TProfile">mapping profile</typeparam>
        /// <param name="container">container</param>
        /// <param name="useExpressionMapper">flag to indicate expression mapper support</param>
        /// <returns>container for builder chaining</returns>
        public static IWindsorContainer AddAutoMapper<TProfile>(this IWindsorContainer container, bool useExpressionMapper = false)
            where TProfile : Profile, new()
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            var mapper = AutoMapperHelper.Create<TProfile>(useExpressionMapper);

            container.Register(Component.For<IMapper>().Instance(mapper));

            return container;
        }

        /// <summary>
        /// Creates and registers a mapper with the provided profile by resolving the profile's dependency's from the container
        /// </summary>
        /// <typeparam name="TProfile">mapping profile</typeparam>
        /// <param name="container">container</param>
        /// <param name="useExpressionMapper">flag to indicate expression mapper support</param>
        /// <returns>container for builder chaining</returns>
        public static IWindsorContainer AddAutoMapperUsingResolve<TProfile>(this IWindsorContainer container, bool useExpressionMapper = false)
            where TProfile : Profile
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            var profile = container.RegisterAndResolve<TProfile>();

            var mapper = AutoMapperHelper.Create(useExpressionMapper, profile);

            container.Register(Component.For<IMapper>().Instance(mapper));

            return container;
        }
    }
}
