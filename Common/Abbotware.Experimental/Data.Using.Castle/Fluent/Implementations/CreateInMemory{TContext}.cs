// -----------------------------------------------------------------------
// <copyright file="CreateInMemory{TContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Using.Castle.Fluent.Implementation
{
    using System;
    using Abbotware.Core;
    using Abbotware.Interop.EntityFramework;
    using global::Castle.Windsor;

    /// <summary>
    /// Fluent API implementation for ICreateInMemory
    /// </summary>
    /// <typeparam name="TContext">context type</typeparam>
    public class CreateInMemory<TContext> : ICreateInMemory<TContext>
    {
        private readonly IWindsorContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInMemory{TContext}"/> class.
        /// </summary>
        /// <param name="container">container to configure</param>
        public CreateInMemory(IWindsorContainer container)
        {
            this.container = container;
        }

        /// <inheritdoc/>
        public void Create(Action<IDbContextFactory> action)
        {
            action = Arguments.EnsureNotNull(action, nameof(action));

            action(this.container.Resolve<IDbContextFactory>());
        }
    }
}