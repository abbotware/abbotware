// -----------------------------------------------------------------------
// <copyright file="IRetrievalMetadata.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.ExtensionPoints.Retrieval
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for retrieval metadata
    /// </summary>
    public interface IRetrievalMetadata : IDataStageMetadata
    {
        /// <summary>
        /// Gets the number of rows retrieved
        /// </summary>
        int? Rows { get; }

        /// <summary>
        /// Gets the endpoint for the retrieval
        /// </summary>
        string Endpoint { get; }

        /// <summary>
        /// Gets the raw files retrieved
        /// </summary>
        IReadOnlyCollection<Uri> RawFiles { get; }

        /// <summary>
        /// Gets the fields in the raw file
        /// </summary>
        IReadOnlyCollection<string> Fields { get; }
    }
}