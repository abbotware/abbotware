// -----------------------------------------------------------------------
// <copyright file="IDbContextOptionsAdapter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.EntityFramework
{
    using Abbotware.Core;
    using Abbotware.Core.Data.Configuration;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// interface for converting ISqlConnectionOptions to DbContextOptions
    /// </summary>
    /// <typeparam name="TContext">class type of the EF Context</typeparam>
    public interface IDbContextOptionsAdapter<TContext> : IConverter<ISqlConnectionOptions, DbContextOptions<TContext>>
        where TContext : DbContext
    {
    }
}