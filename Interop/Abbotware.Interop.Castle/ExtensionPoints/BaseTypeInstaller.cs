// -----------------------------------------------------------------------
// <copyright file="BaseTypeInstaller.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Castle.ExtensionPoints
{
    using System;
    using Abbotware.Core;
    using global::Castle.MicroKernel.Registration;
    using global::Castle.MicroKernel.SubSystems.Configuration;
    using global::Castle.Windsor;

    /// <summary>
    ///     Base class for creating a type installer
    /// </summary>
    public abstract class BaseTypeInstaller : BaseInstaller
    {
        /// <inheritdoc />
        protected override void OnInstall(IWindsorContainer container, IConfigurationStore store)
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            container.Register(this.OnFindTypes()
                .Configure(this.OnConfigureTypes()));
        }

        /// <summary>
        ///     Hook to implement custom type config logic
        /// </summary>
        /// <returns>configuration delegate for configuring types</returns>
        protected abstract Action<ComponentRegistration> OnConfigureTypes();

        /// <summary>
        ///     Hook to implement custom type finding
        /// </summary>
        /// <returns>type descriptor for searching for types</returns>
        protected abstract BasedOnDescriptor OnFindTypes();
    }
}