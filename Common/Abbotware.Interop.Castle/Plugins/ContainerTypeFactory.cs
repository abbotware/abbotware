// -----------------------------------------------------------------------
// <copyright file="ContainerTypeFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>
namespace Abbotware.Interop.Castle.Plugins
{
    using System;
    using Abbotware.Interop.Castle.ExtensionPoints;
    using global::Castle.Windsor;

    /// <summary>
    ///     plugin for creating types via Type name or Generics
    /// </summary>
    public class ContainerTypeFactory : IContainerTypeFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerTypeFactory"/> class.
        /// </summary>
        /// <param name="container">injected reference to the dependency injection container</param>
        public ContainerTypeFactory(IWindsorContainer container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Gets the dependency injection container
        /// </summary>
        protected IWindsorContainer Container { get; }

        /// <inheritdoc />
        public object Create(Type type)
        {
            return this.Container.Resolve(type);
        }

        /// <inheritdoc />
        public TObject Create<TObject>()
        {
            var type = typeof(TObject);

            return (TObject)this.Container.Resolve(type);
        }
    }
}