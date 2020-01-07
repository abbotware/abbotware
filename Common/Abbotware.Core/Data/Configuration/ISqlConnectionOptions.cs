// -----------------------------------------------------------------------
// <copyright file="ISqlConnectionOptions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Data.Configuration
{
    using System;
    using System.Data;

    /// <summary>
    /// SQL connection options
    /// </summary>
    public interface ISqlConnectionOptions
    {
        /// <summary>
        ///     Gets the timeout for bulk operations
        /// </summary>
        TimeSpan BulkCommandTimeout { get; }

        /// <summary>
        ///     Gets the timeout for an SQL Command
        /// </summary>
        TimeSpan CommandTimeout { get; }

        /// <summary>
        ///     Gets the timeout for starting a new connection to the SQL Server
        /// </summary>
        TimeSpan ConnectionTimeout { get; }

        /// <summary>
        ///     Gets the isolation level for the connection
        /// </summary>
        IsolationLevel IsolationLevel { get; }

        /// <summary>
        ///     Gets the Connection extracted from app config file or supplied via constructor
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        ///     Gets the full connection string or 'name={enties}' identifying the name of the connection string in the app.config
        /// </summary>
        string NameOrConnectionString { get; }

        /// <summary>
        ///     Gets a value indicating whether or not the database has metadata support
        /// </summary>
        bool SupportsMetadata { get; }

        /// <summary>
        ///     Gets the timeout for a transaction
        /// </summary>
        TimeSpan TransactionTimeout { get; }

        /// <summary>
        ///     Gets a value indicating whether or not schema validation should be performed
        /// </summary>
        bool ValidateSchema { get; }
    }
}