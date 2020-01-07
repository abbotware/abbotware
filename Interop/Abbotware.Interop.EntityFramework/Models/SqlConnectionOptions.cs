// -----------------------------------------------------------------------
// <copyright file="SqlConnectionOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Data.Entity.Models
{
    using System;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using Abbotware.Core.Data.Configuration;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///     configuration class containg SQL Connection properties
    /// </summary>
    public class SqlConnectionOptions : ISqlConnectionOptions
    {
        /// <summary>
        ///     internal sql connection string builder
        /// </summary>
        private readonly SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

        /// <summary>
        ///     Initializes a new instance of the <see cref="SqlConnectionOptions" /> class.
        /// </summary>
        /// <param name="nameOrConnectionString">
        ///     full connection string or 'name={enties}' identifying the name of the connection string
        /// </param>
        public SqlConnectionOptions(string nameOrConnectionString)
        {
            this.NameOrConnectionString = nameOrConnectionString;
        }

        /// <inheritdoc/>
        public string NameOrConnectionString { get; }

        /// <summary>
        ///     Gets the Connection extracted from app config file or supplied via constructor
        /// </summary>
        public virtual string ConnectionString
        {
            get
            {
                var actualConnectionString = this.NameOrConnectionString;

                this.builder.ConnectionString = actualConnectionString;

                var constructed = this.builder.ConnectionString;

                return constructed;
            }
        }

        /// <inheritdoc/>
        public TimeSpan ConnectionTimeout { get; set; } = TimeSpan.FromSeconds(20);

        /// <inheritdoc/>
        public TimeSpan CommandTimeout { get; set; } = TimeSpan.FromMinutes(10);

        /// <inheritdoc/>
        public TimeSpan TransactionTimeout { get; set; } = TimeSpan.FromMinutes(5);

        /// <inheritdoc/>
        public TimeSpan BulkCommandTimeout { get; set; } = TimeSpan.FromMinutes(30);

        /// <inheritdoc/>
        public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.Snapshot;

        /// <inheritdoc/>
        public bool ValidateSchema { get; set; } = true;

        /// <inheritdoc/>
        public virtual bool SupportsMetadata => true;

        /// <summary>
        /// Creates a DbContextOptions class from the configured options
        /// </summary>
        /// <typeparam name="TContext">class type of the EF Context</typeparam>
        /// <returns>configuration object for EF Core</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "requried to pass type into EF Core")]
        public virtual DbContextOptions<TContext> ToDbContextOptions<TContext>()
            where TContext : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            optionsBuilder.UseSqlServer(this.ConnectionString);

            var options = optionsBuilder.Options;

            return options;
        }
    }
}