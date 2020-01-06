// -----------------------------------------------------------------------
// <copyright file="CastleExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Using.Castle
{
    using System.Diagnostics.CodeAnalysis;
    using Abbotware.Core;
    using Abbotware.Interop.Castle.Plugins.Installers;
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
        /// <param name="component">name of component</param>
        /// <param name="enableStartable">flag to enable / disable the IStartable facility. (set to false for unit tests)</param>
        public static void AddDefaultFacilities(this IWindsorContainer container, string component, bool enableStartable)
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            container.Install(new DefaultFacilitiesInstaller(component, enableStartable));
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
    }
}
