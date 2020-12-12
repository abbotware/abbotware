// -----------------------------------------------------------------------
// <copyright file="IStorageResult{TStorageMetadata,TStorageContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.ExtensionPoints.Storage
{
    /// <summary>
    /// inteface for storage result
    /// </summary>
    /// <typeparam name="TStorageMetadata">type of the storage metadata</typeparam>
    /// <typeparam name="TStorageContext">type of the storage context</typeparam>
    public interface IStorageResult<out TStorageMetadata, out TStorageContext>
        where TStorageMetadata : IStorageMetadata
    {
        /// <summary>
        ///     Gets Metadata about the storage process
        /// </summary>
        TStorageMetadata Metadata { get; }

        /// <summary>
        ///     Gets contextual information about the storage process
        /// </summary>
        TStorageContext Context { get; }
    }
}