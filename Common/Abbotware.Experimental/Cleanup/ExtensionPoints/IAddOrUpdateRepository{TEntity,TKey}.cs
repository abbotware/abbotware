// -----------------------------------------------------------------------
// <copyright file="IAddOrUpdateRepository{TEntity,TKey}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.ExtensionPoints
{
    /// <summary>
    ///     Basic CRUD repository interface
    /// </summary>
    /// <typeparam name="TEntity">Entity type for this repository</typeparam>
    /// <typeparam name="TKey">Key type for this repository</typeparam>
    public interface IAddOrUpdateRepository<TEntity, in TKey> : IReadOnlyRepository<TEntity, TKey>
        where TEntity : class, new()
    {
        /// <summary>
        ///     Adds (if it does not exist) or updates an entity
        /// </summary>
        /// <param name="entity">entity to add/update</param>
        void AddOrUpdate(TEntity entity);
    }
}