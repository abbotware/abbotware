// -----------------------------------------------------------------------
// <copyright file="ExpressionHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Helpers
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    ///     Helper class that performs Expression related tasks
    /// </summary>
    public static class ExpressionHelper
    {
        /// <summary>
        /// Gets the property info and value via a function
        /// </summary>
        /// <typeparam name="TObject">object type</typeparam>
        /// <typeparam name="TProperty">property type</typeparam>
        /// <param name="expression">property acessor expression</param>
        /// <returns>property info and acessor function</returns>
        /// <exception cref="ArgumentException">invalid expression</exception>
        /// <remarks>Improved version based off https://stackoverflow.com/questions/671968/retrieving-property-name-from-lambda-expression</remarks>
        public static (PropertyInfo PropertyInfo, Func<TObject, TProperty> Compiled) GetPropertyExpression<TObject, TProperty>(Expression<Func<TObject, TProperty>> expression)
        {
            if (expression.Body is not MemberExpression member)
            {
                throw new ArgumentException($"Expression '{expression}' does not refer to a property.");
            }

            if (member.Member is not PropertyInfo propInfo)
            {
                throw new ArgumentException($"Expression '{expression}' does not refer to a property.");
            }

            return (propInfo, expression.Compile());
        }
    }
}
