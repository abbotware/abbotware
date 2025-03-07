﻿// -----------------------------------------------------------------------
// <copyright file="BaseInstaller.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Castle.ExtensionPoints
{
    using System.Globalization;
    using System.IO;
    using Abbotware.Core;
    using global::Castle.MicroKernel.Registration;
    using global::Castle.MicroKernel.SubSystems.Configuration;
    using global::Castle.Windsor;
    using global::Castle.Windsor.Installer;

    /// <summary>
    ///     abstract base class for a strongly typed/fluent Windsor Installer.  Has config file that can override it if needed
    /// </summary>
    public abstract class BaseInstaller : IWindsorInstaller
    {
        /// <inheritdoc />
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            var fileName = string.Format(CultureInfo.InvariantCulture, "{0}.castle.config", this.GetType().Name);

            if (File.Exists(fileName))
            {
                container.Install(Configuration.FromXmlFile(fileName));
            }
            else
            {
                this.OnPreInstall(container, store);

                this.OnInstall(container, store);

                this.OnPostInstall(container, store);
            }
        }

        /// <summary>
        ///     Hook to install custom installer
        /// </summary>
        /// <param name="container">The container</param>
        /// <param name="store"> The configuration store</param>
        protected virtual void OnPreInstall(IWindsorContainer container, IConfigurationStore store)
        {
        }

        /// <summary>
        ///     Hook to install custom installer
        /// </summary>
        /// <param name="container">The container</param>
        /// <param name="store"> The configuration store</param>
        protected abstract void OnInstall(IWindsorContainer container, IConfigurationStore store);

        /// <summary>
        ///     Hook to install custom installer
        /// </summary>
        /// <param name="container">The container</param>
        /// <param name="store"> The configuration store</param>
        protected virtual void OnPostInstall(IWindsorContainer container, IConfigurationStore store)
        {
        }
    }
}