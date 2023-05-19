// -----------------------------------------------------------------------
// <copyright file="IRecordListRetriever{TRecord,TRetrievalMetadata,TRetrievalContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.ExtensionPoints.Retrieval
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     Encapulates a way to retrieve a data list
    /// </summary>
    /// <typeparam name="TRecord">type of data to retrieve</typeparam>
    /// <typeparam name="TRetrievalMetadata">type of retrevial metadata</typeparam>
    /// <typeparam name="TRetrievalContext">type of retrevial context</typeparam>
    public interface IRecordListRetriever<TRecord, out TRetrievalMetadata, TRetrievalContext> : IRecordRetriever<IEnumerable<TRecord>, TRetrievalMetadata, TRetrievalContext>
        where TRetrievalMetadata : IRetrievalMetadata
    {
    }
}