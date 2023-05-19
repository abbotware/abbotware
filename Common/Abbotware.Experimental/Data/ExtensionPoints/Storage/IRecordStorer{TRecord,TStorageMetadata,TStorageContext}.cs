// -----------------------------------------------------------------------
// <copyright file="IRecordStorer{TRecord,TStorageMetadata,TStorageContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.ExtensionPoints.Storage
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     Encapulates a way to store data
    /// </summary>
    /// <typeparam name="TRecord">type of data</typeparam>
    /// <typeparam name="TStorageMetadata">type of the storage metadata</typeparam>
    /// <typeparam name="TStorageContext">type of storage context</typeparam>
    [SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "Used to enforce type inheritance")]
    public interface IRecordStorer<TRecord, out TStorageMetadata, TStorageContext>
        where TStorageMetadata : IStorageMetadata
    {
        /// <summary>
        /// Stores the data
        /// </summary>
        /// <param name="record">record to store</param>
        /// <param name="context">context associated with</param>
        /// <returns>storage result</returns>
        IStorageResult<TStorageMetadata, TStorageContext> Store(TRecord record, TStorageContext context);
    }
}