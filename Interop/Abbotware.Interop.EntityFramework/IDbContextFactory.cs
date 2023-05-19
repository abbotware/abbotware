// -----------------------------------------------------------------------
// <copyright file="IDbContextFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.EntityFramework
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///     Typed factory for creating DB Context / Entities classes
    /// </summary>
    public interface IDbContextFactory
    {
        /// <summary>
        ///     Creates a specific type of DBContext with dependencies injected
        /// </summary>
        /// <typeparam name="TDbContext">type of DB Context</typeparam>
        /// <returns>constructed context with dependencies injected</returns>
        /// <remarks>The context is expected to be configured appropriately via the dependency injection framework</remarks>
        TDbContext Create<TDbContext>()
            where TDbContext : DbContext;
    }
}