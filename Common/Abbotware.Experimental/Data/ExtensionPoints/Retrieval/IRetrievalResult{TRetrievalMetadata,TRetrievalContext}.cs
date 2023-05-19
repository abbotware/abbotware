// -----------------------------------------------------------------------
// <copyright file="IRetrievalResult{TRetrievalMetadata,TRetrievalContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.ExtensionPoints.Retrieval
{
    /// <summary>
    ///     results of the Data Retriever
    /// </summary>
    /// <typeparam name="TRetrievalMetadata">type of retrevial metadata</typeparam>
    /// <typeparam name="TRetrievalContext">type of retrevial context</typeparam>
    public interface IRetrievalResult<out TRetrievalMetadata, out TRetrievalContext>
        where TRetrievalMetadata : IRetrievalMetadata
    {
        /// <summary>
        ///     Gets the context for the retrieval process
        /// </summary>
        TRetrievalContext Context { get; }

        /// <summary>
        ///     Gets the metadata about the retrieval process
        /// </summary>
        TRetrievalMetadata Metadata { get; }
    }
}