// -----------------------------------------------------------------------
// <copyright file="TransientTypeInstaller.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Castle.Plugins.Installers
{
    using System;
    using Abbotware.Interop.Castle.ExtensionPoints;
    using global::Castle.MicroKernel.Registration;

    /// <summary>
    ///     Installs as transient types
    /// </summary>
    public abstract class TransientTypeInstaller : BaseTypeInstaller
    {
        /// <inheritdoc />
        protected sealed override Action<ComponentRegistration> OnConfigureTypes()
        {
            return c => c.LifestyleTransient();
        }
    }
}