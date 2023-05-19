// -----------------------------------------------------------------------
// <copyright file="AddDbContext{TContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Using.Castle.Fluent.Implementation
{
    using Abbotware.Core;
    using Abbotware.Data.Configuration;
    using Abbotware.Data.Configuration.Models;
    using Abbotware.Interop.EntityFramework;
    using Abbotware.Interop.EntityFramework.Adapters;
    using Abbotware.Using.Castle;
    using global::Castle.Windsor;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Fluent API implementation for IAddDbContext
    /// </summary>
    /// <typeparam name="TContext">context type</typeparam>
    public class AddDbContext<TContext> : IAddDbContext<TContext>
        where TContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddDbContext{TContext}"/> class.
        /// </summary>
        /// <param name="container">container to configure</param>
        public AddDbContext(IWindsorContainer container) => this.Container = container;

        /// <summary>
        /// Gets the container
        /// </summary>
        protected IWindsorContainer Container { get; }

        /// <inheritdoc/>
        public ICreateInMemory<TContext> UseInMemory()
        {
            return this.UseInMemory(new InMemoryOptions());
        }

        /// <inheritdoc/>
        public ICreateInMemory<TContext> UseInMemory(string databaseId)
        {
            return this.UseInMemory(new InMemoryOptions(databaseId));
        }

        /// <inheritdoc/>
        public virtual ICreateInMemory<TContext> UseInMemory(IInMemoryOptions inMemory)
        {
            inMemory = Arguments.EnsureNotNull(inMemory, nameof(inMemory));

            var configuration = new InMemorySqlConnectionOptions(inMemory.DatabaseId);

            this.OnRegister(configuration, new InMemoryAdapter<TContext>());

            return new CreateInMemory<TContext>(this.Container);
        }

        /// <inheritdoc/>
        public virtual IConnectionStringSource<TContext> ConnectionStringName(string name)
        {
            return new ConnectionStringSource<TContext>(this.Container, name);
        }

        /// <inheritdoc/>
        public virtual void ConnectionStringValue(string connectionString)
        {
            var configuration = new SqlConnectionOptions(connectionString);

            this.OnRegister(configuration, new SqlOptionsAdapter<TContext>());
        }

        /// <summary>
        /// Registration function
        /// </summary>
        /// <param name="configuration">sql connection configuration</param>
        /// <param name="adapter">sql options adapter</param>
        protected virtual void OnRegister(ISqlConnectionOptions configuration, IDbContextOptionsAdapter<TContext> adapter)
        {
            this.Container.RegisterDbContext<TContext>(configuration, adapter);
        }
    }
}
