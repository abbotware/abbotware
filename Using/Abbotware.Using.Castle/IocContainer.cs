// -----------------------------------------------------------------------
// <copyright file="IocContainer.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Using.Castle
{
    using Abbotware.Core;
    using Abbotware.Core.Configuration;
    using global::Castle.Windsor;

    /// <summary>
    /// Ioc Container factory
    /// </summary>
    public static class IocContainer
    {
        /// <summary>
        /// creates the container
        /// </summary>
        /// <returns>new container</returns>
        public static IWindsorContainer Create()
        {
            return Create(string.Empty, false);
        }

        /// <summary>
        /// creates the container
        /// </summary>
        /// <param name="options">container options</param>
        /// <returns>new container</returns>
        public static IWindsorContainer Create(IContainerOptions options)
        {
            options = Arguments.EnsureNotNull(options, nameof(options));

            return Create(options.Component, options.DisableStartable);
        }

        /// <summary>
        /// creates the container
        /// </summary>
        /// <param name="component">name of component</param>
        /// <param name="disableStartable">flag to enable / disable the IStartable facility. (set to false for unit tests)</param>
        /// <returns>new container</returns>
        public static IWindsorContainer Create(string component, bool disableStartable)
        {
            var c = new WindsorContainer();

            c.AddDefaultFacilities(c =>
            {
                c.DisableStartable = disableStartable;
                c.Component = component;
            });

            return c;
        }
    }
}