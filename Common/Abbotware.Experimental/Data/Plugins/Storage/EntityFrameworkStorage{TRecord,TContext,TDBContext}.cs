// -----------------------------------------------------------------------
// <copyright file="EntityFrameworkStorage{TRecord,TContext,TDBContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.Plugins.Storage
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Abbotware.Core.Data.ExtensionPoints.Storage;
    using Abbotware.Core.Data.Plugins.Configuration;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.EntityFramework;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Storage framework using entity framework
    /// </summary>
    /// <typeparam name="TRecord">storage record class type</typeparam>
    /// <typeparam name="TContext">storage context</typeparam>
    /// <typeparam name="TDbContext">entity framework context</typeparam>
    public class EntityFrameworkStorage<TRecord, TContext, TDbContext> : BaseComponent, IRecordStorer<TRecord, IStorageMetadata, TContext>
        where TRecord : class
        where TDbContext : DbContext
    {
        /// <summary>
        /// factory for creating ef context dynamically
        /// </summary>
        private readonly IDbContextFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrameworkStorage{TRecord, TContext, TDbContext}"/> class.
        /// </summary>
        /// <param name="factory">injected ef context factory</param>
        /// <param name="logger">injected logger</param>
        protected EntityFrameworkStorage(IDbContextFactory factory, ILogger logger)
            : base(logger)
        {
            Arguments.NotNull(logger, nameof(logger));
            Arguments.NotNull(factory, nameof(factory));

            this.factory = factory;
        }

        /// <inheritdoc/>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification ="reviewed")]
        public IStorageResult<IStorageMetadata, TContext> Store(TRecord record, TContext context)
        {
            var metadata = new StorageMetadata();

            try
            {
                this.OnPreInsert(record, context);

                using (var db = this.factory.Create<TDbContext>())
                {
                    db.Set<TRecord>().Add(record);

                    db.SaveChanges();
                }

                this.OnPostInsert(record, context);
            }
            catch (Exception ex)
            {
                metadata.Exception = ex;
            }

            metadata.EndTime = DateTimeOffset.Now;

            return new StorageResult<IStorageMetadata, TContext>(metadata, context);
        }

        /// <summary>
        /// Hook for custom logic before insert
        /// </summary>
        /// <param name="record">record being inserted</param>
        /// <param name="context">current storage context</param>
        protected virtual void OnPreInsert(TRecord record, TContext context)
        {
        }

        /// <summary>
        /// Hook for custom logic after insert
        /// </summary>
        /// <param name="record">record being inserted</param>
        /// <param name="context">current storage context</param>
        protected virtual void OnPostInsert(TRecord record, TContext context)
        {
        }
    }
}