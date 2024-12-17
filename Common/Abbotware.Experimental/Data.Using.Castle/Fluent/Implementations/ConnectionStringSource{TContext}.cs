// -----------------------------------------------------------------------
// <copyright file="ConnectionStringSource{TContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Using.Castle.Fluent.Implementation
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.Data.Configuration.Models;
    using Abbotware.Interop.EntityFramework.Adapters;
    using Abbotware.Using.Castle;
    using global::Castle.Windsor;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Provides a connection string source for registering the DbContext
    /// </summary>
    /// <typeparam name="TContext">DbContext type</typeparam>
    public class ConnectionStringSource<TContext> : IConnectionStringSource<TContext>
        where TContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionStringSource{TContext}"/> class.
        /// </summary>
        /// <param name="container">di container</param>
        /// <param name="connectionString">connection string</param>
        public ConnectionStringSource(IWindsorContainer container, string connectionString)
        {
            this.Container = container;
            this.ConnectionString = connectionString;
        }

        /// <summary>
        /// Gets the container
        /// </summary>
        protected IWindsorContainer Container { get; }

        /// <summary>
        /// Gets the connection string
        /// </summary>
        protected string ConnectionString { get; }

        /// <summary>
        /// Use the app config for configuration source
        /// </summary>
        public virtual void UseAppConfig()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Use the Microsoft.Extensions.Configuration as a source for connection string
        /// </summary>
        /// <param name="configuration">configuration</param>
        public virtual void UseMicrosoft(IConfiguration configuration)
        {
            configuration = Arguments.EnsureNotNull(configuration, nameof(configuration));

            var connectionString = configuration
                .GetConnectionString(this.ConnectionString);

            if (connectionString.IsBlank())
            {
                throw new ArgumentException($"Can not find connection string:{this.ConnectionString}");
            }

            var connection = new SqlConnectionOptions(connectionString) { ValidateSchema = true };

            this.Container.RegisterDbContext<TContext>(connection, new SqlOptionsAdapter<TContext>());
        }
    }
}