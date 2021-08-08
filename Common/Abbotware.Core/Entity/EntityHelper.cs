// -----------------------------------------------------------------------
// <copyright file="EntityHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Helper functions related to entity model classes
    /// </summary>
    public static class EntityHelper
    {
        /// <summary>
        /// Creates an IQueryable compatible expression for searching by supplied primary keys
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <param name="names">key names</param>
        /// <returns>expression function for searching by primary key</returns>
        public static Func<TEntity, object[], bool> PrimaryKeyExpression<TEntity>(IEnumerable<string> names)
        {
            names = Arguments.EnsureNotNull(names, nameof(names));

            var keyValues = Expression.Parameter(typeof(object[]));

            var entity = Expression.Parameter(typeof(TEntity));

            BinaryExpression? expression = null;

            var i = 0;
            foreach (var n in names)
            {
                var property = Expression.PropertyOrField(entity, n);
                var index = Expression.Constant(i);
                var arrayIndex = Expression.ArrayIndex(keyValues, index);
                var casted = Expression.Convert(arrayIndex, property.Type);
                var equals = Expression.Equal(property, casted);

                if (expression == null)
                {
                    expression = equals;
                }
                else
                {
                    expression = Expression.And(expression, equals);
                }

                ++i;
            }

            var block = Expression.Lambda<Func<TEntity, object[], bool>>(expression!, entity, keyValues);

            return block.Compile();
        }

        /// <summary>
        /// Creates an IQueryable compatible expression for searching by supplied primary keys
        /// </summary>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <param name="names">key names</param>
        /// <param name="keys">key</param>
        /// <returns>expression function for searching by primary key</returns>
        public static Expression<Func<TEntity, bool>> FindExpression<TEntity>(IEnumerable<string> names, object[] keys)
        {
            names = Arguments.EnsureNotNull(names, nameof(names));
            keys = Arguments.EnsureNotNull(keys, nameof(keys));

            var entity = Expression.Parameter(typeof(TEntity));

            BinaryExpression? expression = null;

            var i = 0;
            foreach (var n in names)
            {
                var property = Expression.PropertyOrField(entity, n);
                var value = Expression.Constant(keys[i]);
                var casted = Expression.Convert(value, property.Type);
                var equals = Expression.Equal(property, casted);

                if (expression == null)
                {
                    expression = equals;
                }
                else
                {
                    expression = Expression.And(expression, equals);
                }

                ++i;
            }

            var block = Expression.Lambda<Func<TEntity, bool>>(expression!, entity);

            return block;
        }
    }
}