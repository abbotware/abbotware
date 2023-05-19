// -----------------------------------------------------------------------
// <copyright file="DBContextFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.EntityFramework
{
    using System;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Concrete Factory class that can only create a single DbContext type
    /// </summary>
    /// <typeparam name="TContext">the DB Context</typeparam>
    public class DBContextFactory<TContext> : IDbContextFactory
        where TContext : DbContext
    {
        private readonly Func<TContext> factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DBContextFactory{TContext}"/> class.
        /// </summary>
        /// <param name="factory">factory function</param>
        public DBContextFactory(Func<TContext> factory)
        {
            this.factory = factory;
        }

        /// <inheritdoc/>
        public TDbContext Create<TDbContext>()
            where TDbContext : DbContext
        {
            if (typeof(TContext) != typeof(TDbContext))
            {
                throw new InvalidOperationException($"Factory Does Not Support type:{typeof(TContext).FullName}");
            }

            return (TDbContext)(object)this.factory();
        }
    }
}