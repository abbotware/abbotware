// -----------------------------------------------------------------------
// <copyright file="IValidationService.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.ExtensionPoints.Validation
{
    using System.Collections.Generic;

    /// <summary>
    /// provides a service that can validate objects
    /// </summary>
    public interface IValidationService
    {
        /// <summary>
        /// Validates a single object
        /// </summary>
        /// <typeparam name="TEntity">type of entity</typeparam>
        /// <param name="entity">entity to validate</param>
        void Validate<TEntity>(TEntity entity)
            where TEntity : class;

        /// <summary>
        /// Validates a collection of objects
        /// </summary>
        /// <typeparam name="TEntity">type of entity</typeparam>
        /// <param name="entities">entities to validate</param>
        void Validate<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;
    }
}