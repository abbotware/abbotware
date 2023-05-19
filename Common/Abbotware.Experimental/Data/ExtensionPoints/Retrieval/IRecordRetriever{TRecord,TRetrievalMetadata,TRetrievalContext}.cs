// -----------------------------------------------------------------------
// <copyright file="IRecordRetriever{TRecord,TRetrievalMetadata,TRetrievalContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.ExtensionPoints.Retrieval
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     Encapulates a way to retrieve data
    /// </summary>
    /// <typeparam name="TRecord">type of data to retrieve</typeparam>
    /// <typeparam name="TRetrievalMetadata">type of retrevial metadata</typeparam>
    /// <typeparam name="TRetrievalContext">type of retrevial context</typeparam>
    public interface IRecordRetriever<TRecord, out TRetrievalMetadata, TRetrievalContext>
        where TRetrievalMetadata : IRetrievalMetadata
    {
        /// <summary>
        ///     Retrieves data
        /// </summary>
        /// <param name="context">context of the retrieval process</param>
        /// <returns>the data with context</returns>
        IRetrievalResult<TRecord, TRetrievalMetadata, TRetrievalContext> Retrieve(TRetrievalContext context);
    }
}