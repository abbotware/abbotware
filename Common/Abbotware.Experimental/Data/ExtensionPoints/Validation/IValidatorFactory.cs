// -----------------------------------------------------------------------
// <copyright file="IValidatorFactory.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.ExtensionPoints.Validation
{
    /// <summary>
    /// Creates a validator object for the supplied type
    /// </summary>
    public interface IValidatorFactory
    {
        /// <summary>
        /// Creates a validator
        /// </summary>
        /// <typeparam name="TEntity">entity type to create validator for</typeparam>
        /// <returns>validator</returns>
        IValidator<TEntity> Create<TEntity>()
            where TEntity : class;
    }
}