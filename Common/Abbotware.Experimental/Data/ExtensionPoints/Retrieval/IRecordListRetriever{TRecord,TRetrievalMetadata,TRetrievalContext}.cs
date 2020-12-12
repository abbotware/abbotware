// -----------------------------------------------------------------------
// <copyright file="IRecordListRetriever{TRecord,TRetrievalMetadata,TRetrievalContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
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
    [SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "Used to enforce type inheritance")]
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Used to enforce type inheritance")]
    public interface IRecordListRetriever<TRecord, out TRetrievalMetadata, TRetrievalContext> : IRecordRetriever<IEnumerable<TRecord>, TRetrievalMetadata, TRetrievalContext>
        where TRetrievalMetadata : IRetrievalMetadata
    {
    }
}