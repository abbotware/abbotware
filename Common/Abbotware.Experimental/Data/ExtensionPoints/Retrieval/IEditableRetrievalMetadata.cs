// -----------------------------------------------------------------------
// <copyright file="IEditableRetrievalMetadata.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.ExtensionPoints.Retrieval
{
    using System;

    /// <summary>
    ///     interface for editing the retrieval meta data
    /// </summary>
    public interface IEditableRetrievalMetadata : IRetrievalMetadata
    {
        /// <summary>
        ///     Adds the field to the list of fields/headers parsed
        /// </summary>
        /// <param name="field">field name</param>
        void AddField(string field);

        /// <summary>
        ///     adds the file to the list of raw files created
        /// </summary>
        /// <param name="file">file path</param>
        void AddRawFile(Uri file);
    }
}