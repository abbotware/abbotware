// -----------------------------------------------------------------------
// <copyright file="ValidationService.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.Plugins.Validation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Abbotware.Core.Data.ExtensionPoints.Validation;

    /// <summary>
    /// class that implements the validation service
    /// </summary>
    public class ValidationService : IValidationService
    {
        /// <summary>
        /// injected factory to create validators
        /// </summary>
        private readonly IValidatorFactory validatorFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationService"/> class.
        /// </summary>
        /// <param name="validatorFactory">injected factory to create validators</param>
        public ValidationService(IValidatorFactory validatorFactory)
        {
            this.validatorFactory = validatorFactory;
        }

        /// <inheritdoc/>
        public void Validate<TEntity>(TEntity entity)
            where TEntity : class
        {
            this.Validate<TEntity>(new List<TEntity> { entity });
        }

        /// <inheritdoc/>
        public void Validate<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {
            Arguments.NotNull(entities, nameof(entities));

            var validator = this.validatorFactory.Create<TEntity>();

            var results = new List<ValidationResult>();

            foreach (var entity in entities)
            {
                results.AddRange(validator.Validate(entity));
            }

            if (results.Count > 0)
            {
                // throw new ValidationException(results);
            }
        }
    }
}