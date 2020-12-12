// -----------------------------------------------------------------------
// <copyright file="IRetrievalResult{TData,TRetrievalMetadata,TRetrievalContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.ExtensionPoints.Retrieval
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///    interface for the results of the Data Retriever
    /// </summary>
    /// <typeparam name="TData">type of data to retrieve</typeparam>
    /// <typeparam name="TRetrievalMetadata">type of retrevial metadata</typeparam>
    /// <typeparam name="TRetrievalContext">type of retrevial context</typeparam>
    [SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "Used to enforce type inheritance")]
    public interface IRetrievalResult<TData, out TRetrievalMetadata, out TRetrievalContext> : IRetrievalResult<TRetrievalMetadata, TRetrievalContext>
        where TRetrievalMetadata : IRetrievalMetadata
    {
        /// <summary>
        ///     Gets the data retrieved (if any)
        /// </summary>
        TData Data { get; }
    }
}