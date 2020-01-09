// -----------------------------------------------------------------------
// <copyright file="LoadInstallersInAssembly.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Castle.Plugins.Installers
{
    using System;
    using System.Linq;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Logging;
    using Abbotware.Interop.Castle.ExtensionPoints;
    using global::Castle.MicroKernel.SubSystems.Configuration;
    using global::Castle.Windsor;
    using global::Castle.Windsor.Installer;

    /// <summary>
    ///     Installer that loads Installer classes in the specified assemblies
    /// </summary>
    public class LoadInstallersInAssembly : BaseInstaller
    {
        /// <summary>
        /// injected logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// list of assemblies
        /// </summary>
        private readonly Uri[] assemblies;

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoadInstallersInAssembly" /> class.
        /// </summary>
        /// <param name="assemblies">assembly files</param>
        /// <param name="logger">logger</param>
        public LoadInstallersInAssembly(Uri[] assemblies, ILogger logger)
        {
            Arguments.NotNull(assemblies, nameof(assemblies));
            Arguments.NotNull(logger, nameof(logger));

            this.assemblies = assemblies;
            this.logger = logger;
        }

        /// <inheritdoc />
        protected override void OnInstall(IWindsorContainer container, IConfigurationStore store)
        {
            Arguments.NotNull(container, nameof(container));

            var sorted = this.assemblies.OrderByDescending(x => x.LocalPath.ContainsCaseInsensitive("plugin"));

            foreach (var assembly in sorted)
            {
                if (assembly == null)
                {
                    continue;
                }

                this.logger.Debug("Running Installers in:{0}", assembly.LocalPath);

                try
                {
                    var installers = FromAssembly.Named(assembly.LocalPath);
                }
                catch (Exception ex)
                {
                    this.logger.Error(ex, "installer:{0}", assembly.LocalPath);
                }
            }
        }
    }
}