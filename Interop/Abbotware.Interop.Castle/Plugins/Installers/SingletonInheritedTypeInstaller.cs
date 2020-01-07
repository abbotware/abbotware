// -----------------------------------------------------------------------
// <copyright file="SingletonInheritedTypeInstaller.cs" company="Abbotware, LLC">
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
    ///     Installs all inherited types of T in the assembly containing T as a singleton
    /// </summary>
    /// <typeparam name="T">Base type to install</typeparam>
    public class SingletonInheritedTypeInstaller<T> : InheritedTypeInstaller<T>
    {
        /// <inheritdoc />
        protected override Action<ComponentRegistration> OnConfigureTypes()
        {
            return c => c.LifestyleSingleton();
        }
    }
}