// -----------------------------------------------------------------------
// <copyright file="IocContainer.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Using.Castle
{
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
        /// <param name="component">name of component</param>
        /// <param name="enableStartable">flag to enable / disable the IStartable facility. (set to false for unit tests)</param>
        /// <returns>new container</returns>
        public static IWindsorContainer Create(string component, bool enableStartable)
        {
            var c = new WindsorContainer();

            c.AddDefaultFacilities(component, enableStartable);

            return c;
        }
    }
}