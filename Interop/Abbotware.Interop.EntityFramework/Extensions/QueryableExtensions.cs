// -----------------------------------------------------------------------
// <copyright file="QueryableExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EntityFramework.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abbotware.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Metadata;

    /// <summary>
    /// IQueryable Extensions
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Ensures that all navigation properties (up to a certain depth) are eagerly loaded when entities are resolved from this DbSet.
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <param name="dbSet">entity dbSet </param>
        /// <param name="maxDepth">max depth</param>
        /// <returns>a queryable representation of this DbSet with all includes</returns>
        public static IQueryable<TEntity> IncludeAll<TEntity>(this DbSet<TEntity> dbSet, uint maxDepth = 50)
            where TEntity : class
        {
            IQueryable<TEntity> result = dbSet;
            var context = dbSet.GetService<ICurrentDbContext>().Context;
            var includePaths = GetIncludePaths(context, typeof(TEntity), maxDepth);

            return result.Include(includePaths);
        }

        /// <summary>
        /// Adds all include paths to the query
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <param name="source">queryable</param>
        /// <param name="navigationPropertyPaths">list of navigation paths</param>
        /// <returns>queryable with all includes</returns>
        /// <remarks>https://stackoverflow.com/questions/49593482/entity-framework-core-2-0-1-eager-loading-on-all-nested-related-entities</remarks>
        public static IQueryable<TEntity> Include<TEntity>(this IQueryable<TEntity> source, IEnumerable<string> navigationPropertyPaths)
            where TEntity : class
        {
            return navigationPropertyPaths.Aggregate(source, (query, path) => query.Include(path));
        }

        /// <summary>
        /// All Include paths for the entity
        /// </summary>
        /// <param name="context">db context</param>
        /// <param name="clrEntityType">entity type</param>
        /// <param name="maxDepth">max depth</param>
        /// <returns>list of all include paths</returns>
        /// <remarks>https://stackoverflow.com/questions/49593482/entity-framework-core-2-0-1-eager-loading-on-all-nested-related-entities</remarks>
        private static IEnumerable<string> GetIncludePaths(this DbContext context, Type clrEntityType, uint maxDepth)
        {
            context = Arguments.EnsureNotNull(context, nameof(context));
            clrEntityType = Arguments.EnsureNotNull(clrEntityType, nameof(clrEntityType));

#if NET6_0_OR_GREATER
            IReadOnlyEntityType? entityType = context.Model.FindEntityType(clrEntityType);
            var includedNavigations = new HashSet<IReadOnlyNavigation>();
            var stack = new Stack<IEnumerator<IReadOnlyNavigation>>();
#else
            var entityType = context.Model.FindEntityType(clrEntityType);
            var includedNavigations = new HashSet<INavigation>();
            var stack = new Stack<IEnumerator<INavigation>>();
#endif

            Arguments.NotNull(entityType, nameof(entityType));

            while (true)
            {
#if NET6_0_OR_GREATER
                var entityNavigations = new List<IReadOnlyNavigation>();
#else
                var entityNavigations = new List<INavigation>();
#endif

                if (stack.Count <= maxDepth)
                {
                    foreach (var navigation in entityType.GetNavigations())
                    {
                        if (includedNavigations.Add(navigation))
                        {
                            entityNavigations.Add(navigation);
                        }
                    }
                }

                if (entityNavigations.Count == 0)
                {
                    if (stack.Count > 0)
                    {
                        yield return string.Join(".", stack.Reverse().Select(e => e.Current.Name));
                    }
                }
                else
                {
                    foreach (var navigation in entityNavigations)
                    {
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
                        var inverseNavigation = navigation.Inverse;
#else
                        var inverseNavigation = navigation.FindInverse();
#endif
                        if (inverseNavigation != null)
                        {
                            includedNavigations.Add(inverseNavigation);
                        }
                    }

                    stack.Push(entityNavigations.GetEnumerator());
                }

                while (stack.Count > 0 && !stack.Peek().MoveNext())
                {
                    stack.Pop();
                }

                if (stack.Count == 0)
                {
                    break;
                }

#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
                entityType = stack.Peek().Current.TargetEntityType;
#else
                entityType = stack.Peek().Current.GetTargetType();
#endif
            }
        }
    }
}
