// -----------------------------------------------------------------------
// <copyright file="InMemorySqlConnectionOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Data.Entity.Models
{
    using System;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///     sql configuration class for in memory database
    /// </summary>
    public class InMemorySqlConnectionOptions : SqlConnectionOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InMemorySqlConnectionOptions"/> class.
        /// </summary>
        public InMemorySqlConnectionOptions()
            : base(Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemorySqlConnectionOptions"/> class.
        /// </summary>
        /// <param name="databaseId">unique id for database</param>
        public InMemorySqlConnectionOptions(string databaseId)
            : base(databaseId)
        {
            this.ValidateSchema = false;
        }

        /// <inheritdoc/>
        public override bool SupportsMetadata => false;

        /// <inheritdoc/>
        public override string ConnectionString => this.NameOrConnectionString;

        /// <inheritdoc/>
        public override DbContextOptions<TContext> ToDbContextOptions<TContext>()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            optionsBuilder.UseInMemoryDatabase(this.ConnectionString);

            var options = optionsBuilder.Options;

            return options;
        }
    }
}