// -----------------------------------------------------------------------
// <copyright file="DatabaseMetadata.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Data.Schema
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlTypes;
    using System.Linq;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using Abbotware.Data.Configuration;
    using Microsoft.Data.SqlClient;
    using Microsoft.Data.SqlClient.Server;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Metadata about tables and columns within a database
    /// </summary>
    public class DatabaseMetadata : BaseComponent<ISqlConnectionOptions>
    {
        /// <summary>
        /// list of table meta data
        /// </summary>
        private readonly List<TableMetadata> tables = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseMetadata"/> class.
        /// </summary>
        /// <param name="configuration">connection configuration</param>
        /// <param name="logger">injected logger</param>
        public DatabaseMetadata(ISqlConnectionOptions configuration, ILogger logger)
            : base(configuration, logger)
        {
            Arguments.NotNull(configuration, nameof(configuration));
            Arguments.NotNull(logger, nameof(logger));

            if (!this.Configuration.SupportsMetadata)
            {
                throw new NotSupportedException("SQL Connetion does not support metadata");
            }
        }

        /// <summary>
        /// Gets the list of table metadata
        /// </summary>
        public IEnumerable<TableMetadata> Tables
        {
            get
            {
                this.InitializeIfRequired();

                return this.tables;
            }
        }

        /// <summary>
        /// Searches for the first or default schema/table metadata
        /// </summary>
        /// <param name="schemaName">sql schema name</param>
        /// <param name="tableName">name of table</param>
        /// <returns>metadata if schema/table is found</returns>
        public TableMetadata? Table(string schemaName, string tableName)
        {
            schemaName = Arguments.EnsureNotNullOrWhitespace(schemaName, nameof(schemaName));
            tableName = Arguments.EnsureNotNullOrWhitespace(tableName, nameof(tableName));

            this.InitializeIfRequired();

            return this.tables.SingleOrDefault(x => (x.Schema.ToUpperInvariant() == schemaName.ToUpperInvariant()) && (x.Table.ToUpperInvariant() == tableName.ToUpperInvariant()));
        }

        /// <inheritdoc/>
        protected override void OnInitialize()
        {
            using var con = new SqlConnection(this.Configuration.ConnectionString);

            con.Open();

            var builder = new SqlConnectionStringBuilder(con.ConnectionString);
            var catalog = builder.InitialCatalog;

            var tableInfo = con.GetSchema(SqlClientMetaDataCollectionNames.Tables, new[] { catalog });

            this.Logger.Info($"tables retrieved {tableInfo.Rows}");

            var columnInfo = con.GetSchema(SqlClientMetaDataCollectionNames.Columns, new[] { catalog });

            this.Logger.Info($"tables retrieved {columnInfo.Rows}");

            foreach (DataRow table in tableInfo.Rows)
            {
                var tableMetadata = new Dictionary<uint, SqlMetaData>();
                var tableSchema = (string)table["TABLE_SCHEMA"];
                var tableName = (string)table["TABLE_NAME"];

                foreach (DataRow column in columnInfo.Rows)
                {
                    var columnTableSchema = (string)column["TABLE_SCHEMA"];
                    var columnTableName = (string)column["TABLE_NAME"];

                    if ((tableSchema == columnTableSchema) && (tableName == columnTableName))
                    {
                        var ordinal = (int)column["ORDINAL_POSITION"];
                        var dataType = (string)column["DATA_TYPE"];

                        if (dataType.Equals("numeric", StringComparison.OrdinalIgnoreCase))
                        {
                            dataType = "decimal";
                        }

                        var columnName = (string)column["COLUMN_NAME"];

                        var length = 0;
                        if (column["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value)
                        {
                            length = (int)column["CHARACTER_MAXIMUM_LENGTH"];
                        }

                        byte precision = 0;
                        if (column["NUMERIC_PRECISION"] != DBNull.Value)
                        {
                            precision = (byte)column["NUMERIC_PRECISION"];
                        }

                        byte scale = 0;
                        if (column["NUMERIC_SCALE"] != DBNull.Value)
                        {
                            scale = (byte)(int)column["NUMERIC_SCALE"];
                        }

                        if (!Enum.TryParse(dataType, true, out SqlDbType sqlType))
                        {
                            throw new InvalidCastException(FormattableString.Invariant($"data type {dataType} can't be parsed into SqlDbType"));
                        }

                        var meta = new SqlMetaData(
                            columnName,
                            sqlType,
                            length,
                            precision,
                            scale,
                            0,
                            SqlCompareOptions.None,
                            null);

                        if (tableMetadata.ContainsKey((uint)ordinal))
                        {
                            throw new InvalidOperationException(FormattableString.Invariant($"dupliate key:{ordinal}"));
                        }

                        tableMetadata[(uint)ordinal] = meta;
                    }
                }

                this.tables.Add(new TableMetadata(tableSchema, tableName, tableMetadata));
            }
        }
    }
}