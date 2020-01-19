// -----------------------------------------------------------------------
// <copyright file="TableMetadata.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Data.Schema
{
    using System;
    using System.Collections.Generic;
    using Abbotware.Core;
    using Microsoft.Data.SqlClient.Server;

    /// <summary>
    /// class that contains sql table metadta
    /// </summary>
    public class TableMetadata
    {
        /// <summary>
        /// mapping of column names to sql metadata
        /// </summary>
        private readonly Dictionary<string, SqlMetaData> nameToMetadata = new Dictionary<string, SqlMetaData>();

        /// <summary>
        /// mapping of column ordinal to sql metadata
        /// </summary>
        private readonly IReadOnlyDictionary<uint, SqlMetaData> ordinalToMeta;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableMetadata"/> class.
        /// </summary>
        /// <param name="schema">name of schema</param>
        /// <param name="table">name of table</param>
        /// <param name="sqlMetadata">sql metadata</param>
        public TableMetadata(string schema, string table, IReadOnlyDictionary<uint, SqlMetaData> sqlMetadata)
        {
            schema = Arguments.EnsureNotNullOrWhitespace(schema, nameof(schema));
            table = Arguments.EnsureNotNullOrWhitespace(table, nameof(table));
            sqlMetadata = Arguments.EnsureNotNull(sqlMetadata, nameof(sqlMetadata));

            this.Schema = schema;
            this.Table = table;
            this.ordinalToMeta = sqlMetadata;

            foreach (var kvp in sqlMetadata)
            {
                if (this.nameToMetadata.ContainsKey(kvp.Value.Name))
                {
                    throw new InvalidOperationException(FormattableString.Invariant($"duplicate key:{kvp.Value.Name}"));
                }

                this.nameToMetadata[kvp.Value.Name] = kvp.Value;
            }
        }

        /// <summary>
        /// Gets the schema name
        /// </summary>
        public string Schema { get; }

        /// <summary>
        /// Gets the table name
        /// </summary>
        public string Table { get; }

        /// <summary>
        /// Gets the sql metadata columns
        /// </summary>
        public IEnumerable<SqlMetaData> Columns => this.nameToMetadata.Values;

        /// <summary>
        /// Gets the sql metadata via column name
        /// </summary>
        /// <param name="columnName">column name</param>
        /// <returns>sql metadata</returns>
        public SqlMetaData? Column(string columnName)
        {
            columnName = Arguments.EnsureNotNullOrWhitespace(columnName, nameof(columnName));

            if (this.nameToMetadata.ContainsKey(columnName))
            {
                return this.nameToMetadata[columnName];
            }

            return null;
        }

        /// <summary>
        /// Gets the sql metadata via ordinal
        /// </summary>
        /// <param name="ordinal">column ordinal</param>
        /// <returns>sql metadata</returns>
        public SqlMetaData Column(uint ordinal)
        {
            return this.ordinalToMeta[ordinal];
        }
    }
}