// -----------------------------------------------------------------------
// <copyright file="SqlBulkCopyStorage{TRecord,TContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.Plugins.Storage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Abbotware.Core.Data.ExtensionPoints.Storage;
    using Abbotware.Core.Data.Plugins.Configuration;
    using Abbotware.Core.Diagnostics;
    using Abbotware.Core.Logging;
    using Abbotware.Data.BulkInsert;
    using Abbotware.Data.BulkInsert.Plugins;
    using Abbotware.Data.Configuration;
    using Abbotware.Data.Configuration.Models;

    /// <summary>
    ///     Class for Sql Bulk Copy / Insert
    /// </summary>
    /// <typeparam name="TRecord">type of the class being inserted</typeparam>
    /// <typeparam name="TContext">type of the context used during the operation</typeparam>
    public class SqlBulkCopyStorage<TRecord, TContext> : IRecordListStorer<TRecord, IStorageMetadata, TContext>
        where TRecord : class
    {
        /// <summary>
        ///     configuration for the sql bulk copy / insert operation
        /// </summary>
        private readonly BulkInsertOptions configuration;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SqlBulkCopyStorage{TRecord, TContext}" /> class.
        /// </summary>
        /// <param name="sqlConnectionConfiguration">sql connection information</param>
        /// <param name="logger">injected logger</param>
        public SqlBulkCopyStorage(ISqlConnectionOptions sqlConnectionConfiguration, ILogger logger)
            : this(new BulkInsertOptions(ReflectionHelper.SingleOrDefaultAttribute<TableAttribute>(typeof(TRecord))!, sqlConnectionConfiguration), logger)
        {
            Arguments.NotNull(sqlConnectionConfiguration, nameof(sqlConnectionConfiguration));
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SqlBulkCopyStorage{TRecord, TContext}" /> class.
        /// </summary>
        /// <param name="bulkInsertConfiguration">bulk insert configuration</param>
        /// <param name="logger">injected logger</param>
        public SqlBulkCopyStorage(BulkInsertOptions bulkInsertConfiguration, ILogger logger)
        {
            Arguments.NotNull(bulkInsertConfiguration, nameof(bulkInsertConfiguration));
            Arguments.NotNull(logger, nameof(logger));

            this.configuration = bulkInsertConfiguration;
            this.Logger = logger;
        }

        /// <summary>
        ///     Gets the injected logger for the class
        /// </summary>
        protected ILogger Logger { get; }

        /// <inheritdoc />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification ="reviewed")]
        public IStorageResult<IStorageMetadata, TContext> Store(IEnumerable<TRecord> record, TContext context)
        {
            var list = record.ToList();

            this.Logger.Info("Attempting:{0} rows", list.Count);
            this.Logger.Info("bulk insert connection: {0}", this.configuration.SqlConnection);

            var meta = new StorageMetadata();

            try
            {
                this.OnPreInsert(list, context);

                using (var bulk = new BulkInsertClient(this.configuration, this.Logger))
                {
                    using var reader = new ClassDataReader<TRecord>(list, this.Logger);
                    bulk.Insert(reader, new PropertyNamesToColumnNames<TRecord>());
                }

                this.OnPostInsert(list, context);
            }
            catch (Exception ex)
            {
                meta.Exception = ex;
            }

            meta.EndTime = DateTimeOffset.Now;

            var result = new StorageResult<IStorageMetadata, TContext>(meta, context);

            return result;
        }

        /// <summary>
        ///     Callback for custom logic before insert
        /// </summary>
        /// <param name="records">list of records</param>
        /// <param name="context">context object</param>
        protected virtual void OnPreInsert(IEnumerable<TRecord> records, TContext context)
        {
        }

        /// <summary>
        ///     Callback for custom logic after insert
        /// </summary>
        /// <param name="records">list of records</param>
        /// <param name="context">context object</param>
        protected virtual void OnPostInsert(IEnumerable<TRecord> records, TContext context)
        {
        }
    }
}