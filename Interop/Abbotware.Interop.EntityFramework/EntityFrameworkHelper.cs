// -----------------------------------------------------------------------
// <copyright file="EntityFrameworkHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Linq.Expressions;
    using Abbotware.Core;
    using Abbotware.Core.Entity;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Helper functions related to entity model classes
    /// </summary>
    public static class EntityFrameworkHelper
    {
        /// <summary>
        /// Gets a list of property names that are used for keys.
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns>list of key fields</returns>
        public static IEnumerable<string> PrimaryKeyNames<TEntity>()
        {
            var properties = typeof(TEntity).GetProperties();

            var d = new Dictionary<int, string>();

            foreach (var p in properties)
            {
                var hasKey = p.GetCustomAttributes(true).OfType<KeyAttribute>()
                    .Any();

                if (!hasKey)
                {
                    continue;
                }

                var hasOrder = p.GetCustomAttributes(true).OfType<ColumnAttribute>()
                    .SingleOrDefault();

                if (hasOrder == null)
                {
                    d.Add(0, p.Name);
                }
                else
                {
                    d.Add(hasOrder.Order, p.Name);
                }
            }

            return d.ToList()
                .OrderBy(x => x.Key)
                .Select(x => x.Value)
                .ToList();
        }

        /// <summary>
        /// Gets a list of property names that are used for keys using the context's model metadata
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <param name="context">context instance</param>
        /// <returns>list of key fields</returns>
        public static IEnumerable<string> PrimaryKeyNames<TEntity>(DbContext context)
        {
            context = Arguments.EnsureNotNull(context, nameof(context));

            var meta = context.Model.FindEntityType(typeof(TEntity).FullName);

            if (meta == null)
            {
                throw new InvalidOperationException("Missing Primary Key");
            }

            var pk = meta.FindPrimaryKey() ?? throw new InvalidOperationException("Missing Primary Key");

            var names = new List<string>();

            foreach (var p in pk.Properties)
            {
                names.Add(p.Name);
            }

            return names;
        }

        /// <summary>
        /// Creates an IQueryable compatible expression for searching by primay key
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <param name="context">context instance</param>
        /// <returns>expression function for searching by primary key</returns>
        public static Func<TEntity, object[], bool> PrimaryKeyExpression<TEntity>(DbContext context)
        {
            var names = PrimaryKeyNames<TEntity>(context);

            return EntityHelper.PrimaryKeyExpression<TEntity>(names);
        }

        /// <summary>
        /// Creates an IQueryable compatible expression for searching by primay key
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns>expression function for searching by primary key</returns>
        public static Func<TEntity, object[], bool> PrimaryKeyExpression<TEntity>()
        {
            var names = PrimaryKeyNames<TEntity>();

            return EntityHelper.PrimaryKeyExpression<TEntity>(names);
        }

        /// <summary>
        /// Creates an IQueryable compatible expression for searching by supplied primary keys
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <param name="key">key</param>
        /// <returns>expression function for searching by primary key</returns>
        public static Expression<Func<TEntity, bool>> FindExpression<TEntity>(object key)
        {
            return FindExpression<TEntity>(new object[] { key });
        }

        /// <summary>
        /// Creates an IQueryable compatible expression for searching by supplied primary keys
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <param name="keys">keys</param>
        /// <returns>expression function for searching by primary key</returns>
        public static Expression<Func<TEntity, bool>> FindExpression<TEntity>(object[] keys)
        {
            var names = PrimaryKeyNames<TEntity>();

            return EntityHelper.FindExpression<TEntity>(names, keys);
        }
    }
}