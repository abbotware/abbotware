// -----------------------------------------------------------------------
// <copyright file="DefaultFacilitiesInstaller.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Castle.Plugins.Installers
{
    using Abbotware.Core;
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
        ///     name of the default component
        /// </summary>
        public const string DefaultComponent = "Default";

        /// <summary>
        ///     flag to indicate whether or not IStartable components should be enabled.
        /// </summary>
        private readonly bool enableStartable;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DefaultFacilitiesInstaller" /> class.
        /// </summary>
        public DefaultFacilitiesInstaller()
            : this(true)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DefaultFacilitiesInstaller" /> class.
        /// </summary>
        /// <param name="enableStartable">flag to enable / disable the IStartable facility. (set to false for unit tests)</param>
        public DefaultFacilitiesInstaller(bool enableStartable)
            : this(DefaultComponent, enableStartable)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DefaultFacilitiesInstaller" /> class.
        /// </summary>
        /// <param name="component">name of component</param>
        /// <param name="enableStartable">flag to enable / disable the IStartable facility. (set to false for unit tests)</param>
        public DefaultFacilitiesInstaller(string component, bool enableStartable)
        {
            Arguments.NotNull(component, nameof(component));

            this.ComponentName = component;
            this.enableStartable = enableStartable;
        }

        /// <summary>
        ///     Gets the name of the component
        /// </summary>
        public string ComponentName { get; }

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

            if (this.enableStartable)
            {
                container.AddFacility<StartableFacility>(f => f.DeferredStart());
            }
        }
    }
}