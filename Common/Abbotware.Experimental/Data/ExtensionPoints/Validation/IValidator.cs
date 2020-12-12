// -----------------------------------------------------------------------
// <copyright file="IValidator.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.ExtensionPoints.Validation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Validates an entity
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Validates the entity
        /// </summary>
        /// <param name="entity">object to validate</param>
        /// <returns>validation results</returns>
        IEnumerable<ValidationResult> Validate(object entity);
    }
}