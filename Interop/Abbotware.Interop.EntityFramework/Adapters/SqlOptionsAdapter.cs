// -----------------------------------------------------------------------
// <copyright file="SqlOptionsAdapter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EntityFramework.Adapters
{
    using Abbotware.Core;
    using Abbotware.Data.Configuration;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Creates a DbContextOptions class from the configured options
    /// </summary>
    /// <typeparam name="TContext">class type of the EF Context</typeparam>
    public class SqlOptionsAdapter<TContext> : IDbContextOptionsAdapter<TContext>
           where TContext : DbContext
    {
        /// <inheritdoc/>
        public DbContextOptions<TContext> Convert(ISqlConnectionOptions from)
        {
            from = Arguments.EnsureNotNull(from, nameof(from));

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            optionsBuilder.UseSqlServer(from.ConnectionString);

            var options = optionsBuilder.Options;

            return options;
        }
    }
}