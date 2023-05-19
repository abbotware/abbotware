// -----------------------------------------------------------------------
// <copyright file="RetrievalResult{TData,TRetrievalMetadata,TRetrievalContext}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.Plugins.Configuration
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Abbotware.Core.Data.ExtensionPoints.Retrieval;

    /// <summary>
    ///    class for the results of the Data Retriever
    /// </summary>
    /// <typeparam name="TData">type of data to retrieve</typeparam>
    /// <typeparam name="TRetrievalMetadata">type of retrevial metadata</typeparam>
    /// <typeparam name="TRetrievalContext">type of retrevial context</typeparam>
    [SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "Used to enforce type inheritance")]
    public class RetrievalResult<TData, TRetrievalMetadata, TRetrievalContext> : IRetrievalResult<TData, TRetrievalMetadata, TRetrievalContext>
        where TRetrievalMetadata : IRetrievalMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RetrievalResult{TData, TRetrievalMetadata, TRetrievalContext}"/> class.
        /// </summary>
        /// <param name="data">the result data</param>
        /// <param name="metadata">retrevial metadata</param>
        /// <param name="context">retrevial context</param>
        public RetrievalResult(TData data, TRetrievalMetadata metadata, TRetrievalContext context)
        {
            Arguments.NotNull(metadata, nameof(metadata));
            Arguments.NotNull(context, nameof(context));

            this.Data = data;
            this.Metadata = metadata;
            this.Context = context;
        }

        /// <inheritdoc/>
        public TRetrievalContext Context { get; }

        /// <inheritdoc/>
        public TRetrievalMetadata Metadata { get; }

        /// <inheritdoc/>
        public TData Data { get; }
    }
}