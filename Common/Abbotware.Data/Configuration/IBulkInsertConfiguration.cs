// -----------------------------------------------------------------------
// <copyright file="IBulkInsertConfiguration.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Data.Configuration
{
    using System;
    using Microsoft.Data.SqlClient;

    /// <summary>
    /// Bulk Insert configuration
    /// </summary>
    public interface IBulkInsertConfiguration
    {
        /// <summary>
        ///     Gets the Flags that specify SqlBulkCopyOptions
        /// </summary>
        SqlBulkCopyOptions SqlBulkCopyOptions { get; }

        /// <summary>
        ///     Gets the Destination Schema name
        /// </summary>
        string DestinationSchemaName { get; }

        /// <summary>
        ///     Gets the Destination Table name
        /// </summary>
        string DestinationTableName { get; }

        /// <summary>
        ///     Gets the Destination [Schema].[Table] for the SqlBulkCopy
        /// </summary>
        string Destination { get; }

        /// <summary>
        ///     Gets the insert batch size
        /// </summary>
        int BatchSize { get; }

        /// <summary>
        ///     Gets  how many records before the notification callback
        /// </summary>
        int NotifyAfter { get; }

        /// <summary>
        ///     Gets a value indicating whether or not to stream from the DataReader
        /// </summary>
        bool EnableStreaming { get; }

        /// <summary>
        ///     Gets the bulk insert operation timeout
        /// </summary>
        TimeSpan BulkInsertTimeout { get; }

        /// <summary>
        ///     Gets the sql connection connection information
        /// </summary>
        ISqlConnectionOptions SqlConnection { get; }
    }
}