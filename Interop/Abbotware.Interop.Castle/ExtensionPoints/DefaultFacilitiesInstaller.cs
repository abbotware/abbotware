// -----------------------------------------------------------------------
// <copyright file="DefaultFacilitiesInstaller.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Castle.Plugins.Installers
{
    using Abbotware.Core;
    using Abbotware.Core.Configuration;
    using Abbotware.Interop.Castle.ExtensionPoints;
    using global::Castle.Facilities.Startable;
    using global::Castle.Facilities.TypedFactory;
    using global::Castle.MicroKernel.Registration;
    using global::Castle.MicroKernel.Resolvers;
    using global::Castle.MicroKernel.Resolvers.SpecializedResolvers;
    using global::Castle.MicroKernel.SubSystems.Configuration;
    using global::Castle.Windsor;

    /// <summary>
    ///     Installs the default facilities for the Windsor Container
    /// </summary>
    public class DefaultFacilitiesInstaller : BaseInstaller
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DefaultFacilitiesInstaller" /> class.
        /// </summary>
        /// <param name="options">container options</param>
        public DefaultFacilitiesInstaller(IContainerOptions options)
        {
            Arguments.NotNull(options, nameof(options));

            this.Options = options;
        }

        /// <summary>
        ///     Gets the options
        /// </summary>
        public IContainerOptions Options { get; }

        /// <inheritdoc />
        protected override void OnInstall(IWindsorContainer container, IConfigurationStore store)
        {
            container = Arguments.EnsureNotNull(container, nameof(container));

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel, true));
            container.Kernel.Resolver.AddSubResolver(new ListResolver(container.Kernel, true));

            container.Register(Component.For<ILazyComponentLoader>()
                .ImplementedBy<LazyOfTComponentLoader>());

            container.AddFacility<TypedFactoryFacility>();

            if (!this.Options.DisableStartable)
            {
                container.AddFacility<StartableFacility>(f => f.DeferredStart());
            }
        }
    }
}