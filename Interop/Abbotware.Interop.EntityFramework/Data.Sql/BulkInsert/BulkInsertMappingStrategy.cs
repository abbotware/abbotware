// -----------------------------------------------------------------------
// <copyright file="BulkInsertMappingStrategy.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Data.BulkInsert
{
    using System.Collections.Generic;
    using Abbotware.Core;
    using Microsoft.Data.SqlClient;

    /// <summary>
    ///     Class that creates a soure to destination mapping strategy for the BulkInsert operation
    /// </summary>
    public abstract class BulkInsertMappingStrategy
    {
        /// <summary>
        ///     internal list of mappings
        /// </summary>
        private readonly List<SqlBulkCopyColumnMapping> columns = [];

        /// <summary>
        ///     Gets the SqlBulkCopyColumnMappings
        /// </summary>
        public IEnumerable<SqlBulkCopyColumnMapping> Mappings => this.columns;

        /// <summary>
        ///     Allows derived classes to add mappings to the internal list
        /// </summary>
        /// <param name="mapping">mapping to add</param>
        protected void AddMapping(SqlBulkCopyColumnMapping mapping)
        {
            Arguments.NotNull(mapping, nameof(mapping));

            this.columns.Add(mapping);
        }
    }
}