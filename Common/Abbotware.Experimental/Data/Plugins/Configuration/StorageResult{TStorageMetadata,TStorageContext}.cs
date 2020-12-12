// -----------------------------------------------------------------------
// <copyright file="StorageResult{TStorageMetadata,TStorageContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.Plugins.Configuration
{
    using Abbotware.Core.Data.ExtensionPoints.Storage;

    /// <summary>
    /// storage result class
    /// </summary>
    /// <typeparam name="TStorageMetadata">class for storage metadata</typeparam>
    /// <typeparam name="TStorageContext">class for storage context</typeparam>
    public class StorageResult<TStorageMetadata, TStorageContext> : IStorageResult<TStorageMetadata, TStorageContext>
        where TStorageMetadata : IStorageMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageResult{TStorageMetadata, TStorageContext}"/> class.
        /// </summary>
        /// <param name="metadata">storage metadata</param>
        /// <param name="context">storage contextr</param>
        public StorageResult(TStorageMetadata metadata, TStorageContext context)
        {
            Arguments.NotNull(metadata, nameof(metadata));
            Arguments.NotNull(context, nameof(context));

            this.Metadata = metadata;
            this.Context = context;
        }

        /// <inheritdoc />
        public TStorageMetadata Metadata { get; }

        /// <inheritdoc />
        public TStorageContext Context { get; }
    }
}