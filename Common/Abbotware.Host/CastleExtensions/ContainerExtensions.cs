// -----------------------------------------------------------------------
// <copyright file="ContainerExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Host.Extensions
{
    using Abbotware.Core;
    using Abbotware.Core.Runtime;
    using Abbotware.Host.Helpers;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    /// <summary>
    /// Castle Windsor container extensions
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Adds the operating system interfafce
        /// </summary>
        /// <param name="container">co</param>
        public static void AddOperatingSystem(this IWindsorContainer container)
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            container.Register(Component.For<IEnvironmentInformation, IOperatingSystemInformation, IOperatingSystem>()
                .ImplementedBy(OperatingSystemHelper.GetOperatingSystemType()).LifestyleSingleton());
        }
    }
}