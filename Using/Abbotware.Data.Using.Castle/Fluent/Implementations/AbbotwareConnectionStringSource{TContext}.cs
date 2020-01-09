// -----------------------------------------------------------------------
// <copyright file="AbbotwareConnectionStringSource{TContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Using.Castle.Fluent.Implementation
{
    using Abbotware.Data.Configuration.Models;
    using Abbotware.Interop.EntityFramework;
    using Abbotware.Interop.EntityFramework.Adapters;
    using Abbotware.Using.Castle;
    using global::Castle.Windsor;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Provides a connection string source for registering a BaseContext
    /// </summary>
    /// <typeparam name="TContext">DbContext type</typeparam>
    public class AbbotwareConnectionStringSource<TContext> : ConnectionStringSource<TContext>
        where TContext : BaseContext<TContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbbotwareConnectionStringSource{TContext}"/> class.
        /// </summary>
        /// <param name="container">di container</param>
        /// <param name="connectionString">connection string</param>
        public AbbotwareConnectionStringSource(IWindsorContainer container, string connectionString)
            : base(container, connectionString)
        {
        }

        /// <inheritdoc/>
        public override void UseMicrosoft(IConfiguration configuration)
        {
            var connectionString = configuration
             .GetConnectionString(this.ConnectionString);

            var connection = new SqlConnectionOptions(connectionString) { ValidateSchema = true };

            this.Container.RegisterAbbotwareContext<TContext>(connection, new SqlOptionsAdapter<TContext>());
        }
    }
}