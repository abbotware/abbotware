// -----------------------------------------------------------------------
// <copyright file="NullInstaller.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Castle.Plugins.Installers
{
    using Abbotware.Core;
    using global::Castle.MicroKernel.Registration;
    using global::Castle.MicroKernel.SubSystems.Configuration;
    using global::Castle.Windsor;

    /// <summary>
    ///     Do nothing Installer
    /// </summary>
    public class NullInstaller : IWindsorInstaller
    {
        /// <summary>
        ///     global static instance
        /// </summary>
        private static readonly NullInstaller TheInstance = new();

        /// <summary>
        ///     Initializes a new instance of the <see cref="NullInstaller" /> class.
        /// </summary>
        private NullInstaller()
        {
        }

        /// <summary>
        ///     Gets the global static instance
        /// </summary>
        public static NullInstaller Instance
        {
            get
            {
                return NullInstaller.TheInstance;
            }
        }

        /// <inheritdoc />
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
        }
    }
}