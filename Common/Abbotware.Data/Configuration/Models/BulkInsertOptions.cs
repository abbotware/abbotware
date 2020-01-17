// -----------------------------------------------------------------------
// <copyright file="BulkInsertOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Data.Configuration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using Abbotware.Core;
    using Abbotware.Core.Configuration;
    using Abbotware.Data.Configuration;
    using Microsoft.Data.SqlClient;

    /// <summary>
    ///     Configuration class for BulkInsert
    /// </summary>
    public class BulkInsertOptions : BaseOptions, IBulkInsertOptions
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BulkInsertOptions" /> class.
        /// </summary>
        /// <param name="tableInfo"> Destination schema name</param>
        /// <param name="sqlConnectionConfiguration">sql database configuration</param>
        public BulkInsertOptions(TableAttribute tableInfo, ISqlConnectionOptions sqlConnectionConfiguration)
        {
            tableInfo = Arguments.EnsureNotNull(tableInfo, nameof(tableInfo));
            Arguments.NotNull(sqlConnectionConfiguration, nameof(sqlConnectionConfiguration));

            this.SqlConnection = sqlConnectionConfiguration;
            this.DestinationSchemaName = tableInfo.Schema;
            this.DestinationTableName = tableInfo.Name;
            this.LogOptions = true;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BulkInsertOptions" /> class.
        /// </summary>
        /// <param name="schemaName"> Destination schema name</param>
        /// <param name="tableName"> Destination tchema name</param>
        /// <param name="sqlConnectionConfiguration">sql database configuration</param>
        public BulkInsertOptions(string schemaName, string tableName, ISqlConnectionOptions sqlConnectionConfiguration)
        {
            this.SqlConnection = sqlConnectionConfiguration;
            this.DestinationSchemaName = schemaName;
            this.DestinationTableName = tableName;
        }

        /// <inheritdoc/>
        public SqlBulkCopyOptions SqlBulkCopyOptions { get; set; } = SqlBulkCopyOptions.CheckConstraints;

        /// <inheritdoc/>
        public string DestinationSchemaName { get; }

        /// <inheritdoc/>
        public string DestinationTableName { get; }

        /// <inheritdoc/>
        public string Destination => FormattableString.Invariant($"[{this.DestinationSchemaName}].[{this.DestinationTableName}]");

        /// <inheritdoc/>
        public int BatchSize { get; set; } = 500;

        /// <inheritdoc/>
        public int NotifyAfter { get; set; } = 100;

        /// <inheritdoc/>
        public bool EnableStreaming { get; set; } = true;

        /// <inheritdoc/>
        public TimeSpan BulkInsertTimeout
        {
            get { return this.SqlConnection.BulkCommandTimeout; }
        }

        /// <inheritdoc/>
        public ISqlConnectionOptions SqlConnection { get; }
    }
}