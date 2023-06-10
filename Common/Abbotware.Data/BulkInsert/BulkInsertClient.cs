// -----------------------------------------------------------------------
// <copyright file="BulkInsertClient.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Data.BulkInsert
{
    using System;
    using System.Data;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using Abbotware.Data.Configuration;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Class that perform SQL bulk inserts
    /// </summary>
    public class BulkInsertClient : BaseComponent<IBulkInsertOptions>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BulkInsertClient" /> class.
        /// </summary>
        /// <param name="configuration">configuration for class</param>
        /// <param name="logger">injected logger</param>
        public BulkInsertClient(IBulkInsertOptions configuration, ILogger logger)
            : base(configuration, logger)
        {
            Arguments.NotNull(configuration, nameof(configuration));
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <summary>
        ///     Inserts the records from the supplied data reader
        /// </summary>
        /// <param name="reader">records to insert via data reader</param>
        /// <param name="mappingStrategy">column mapping for bulk insert</param>
        public void Insert(IDataReader reader, BulkInsertMappingStrategy mappingStrategy)
        {
            Arguments.NotNull(reader, nameof(reader));
            Arguments.NotNull(mappingStrategy, nameof(mappingStrategy));

            this.ThrowIfDisposed();

            using var sqlConnection = new SqlConnection
            {
                ConnectionString = this.Configuration.SqlConnection.ConnectionString,
            };

            sqlConnection.InfoMessage += (sender, args) => { this.Logger.Info($"InfoMessage - Sender:{sender} Args.Message:{args.Message} Args.Source:{args.Source} Args.Errors:{args.Errors}"); };

            sqlConnection.Disposed += (sender, args) => { this.Logger.Info($"Disposed - Sender:{sender}"); };

            sqlConnection.StateChange += (sender, args) => { this.Logger.Info($"StateChange - Sender:{sender} Args.OriginalState:{args.OriginalState} => Args.CurrentState:{args.CurrentState}"); };

            sqlConnection.Open();

            this.Insert(reader, mappingStrategy, sqlConnection);
        }

        /// <summary>
        ///     Inserts the records from the supplied data reader using supplied connection
        /// </summary>
        /// <param name="reader">records to insert via data reader</param>
        /// <param name="mappingStrategy">column mapping for bulk insert</param>
        /// <param name="sqlConnection">existing sql connection to use</param>
        private void Insert(IDataReader reader, BulkInsertMappingStrategy mappingStrategy, SqlConnection sqlConnection)
        {
            Arguments.NotNull(reader, nameof(reader));
            Arguments.NotNull(mappingStrategy, nameof(mappingStrategy));
            Arguments.NotNull(sqlConnection, nameof(sqlConnection));

            var transactionId = FormattableString.Invariant($"{this.Configuration.Destination} - {Guid.NewGuid()}");

            if (transactionId.Length > 32)
            {
#if NETSTANDARD2_0
                transactionId = transactionId.Substring(0, 32);
#else
                transactionId = transactionId[..32];
#endif
            }

            this.Logger.Debug($"Bulk Copy Transaction Id:{transactionId}");

            using var transaction = sqlConnection.BeginTransaction(this.Configuration.SqlConnection.IsolationLevel, transactionId);

            this.Insert(reader, mappingStrategy, sqlConnection, transaction);

            transaction.Commit();
        }

        /// <summary>
        ///     Inserts the records from the supplied data reader using supplied connection and transaction
        /// </summary>
        /// <param name="reader">records to insert via data reader</param>
        /// <param name="mappingStrategy">column mapping for bulk insert</param>
        /// <param name="sqlConnection">existing sql connection to use</param>
        /// <param name="transaction">existing transaction to use</param>
        private void Insert(IDataReader reader, BulkInsertMappingStrategy mappingStrategy, SqlConnection sqlConnection, SqlTransaction transaction)
        {
            Arguments.NotNull(reader, nameof(reader));
            Arguments.NotNull(mappingStrategy, nameof(mappingStrategy));
            Arguments.NotNull(sqlConnection, nameof(sqlConnection));
            Arguments.NotNull(transaction, nameof(transaction));

            using var s = new SqlBulkCopy(sqlConnection, this.Configuration.SqlBulkCopyOptions, transaction)
            {
                DestinationTableName = this.Configuration.Destination,
            };

            foreach (var map in mappingStrategy.Mappings)
            {
                s.ColumnMappings.Add(map);
            }

            s.BatchSize = this.Configuration.BatchSize;

            s.BulkCopyTimeout = (int)this.Configuration.BulkInsertTimeout.TotalSeconds;

            s.EnableStreaming = this.Configuration.EnableStreaming;

            s.NotifyAfter = this.Configuration.NotifyAfter;

            s.SqlRowsCopied += (sender, args) => { this.Logger.Info($"SqlRowsCopied Sender:{sender} Args.Abort:{args.Abort} Args.RowsCopied{args.RowsCopied}"); };

            s.WriteToServer(reader);
        }
    }
}