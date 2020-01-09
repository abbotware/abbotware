// -----------------------------------------------------------------------
// <copyright file="PropertyNamesToColumnNames{TClass}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Data.BulkInsert.Plugins
{
    using Abbotware.Core.Diagnostics;
    using Microsoft.Data.SqlClient;

    /// <summary>
    ///     Maps all public properties of a class to columns names
    /// </summary>
    /// <typeparam name="TClass">Class type</typeparam>
    public class PropertyNamesToColumnNames<TClass> : BulkInsertMappingStrategy
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PropertyNamesToColumnNames{TClass}" /> class.
        /// </summary>
        public PropertyNamesToColumnNames()
        {
            var properties = ReflectionHelper.GetSimplePropertyNames<TClass>();

            foreach (var property in properties)
            {
                this.AddMapping(new SqlBulkCopyColumnMapping(property, property));
            }
        }
    }
}