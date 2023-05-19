// -----------------------------------------------------------------------
// <copyright file="RetrievalMetadata.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Data.Plugins.Configuration
{
    using System;
    using System.Collections.Generic;
    using Abbotware.Core.Data.ExtensionPoints.Retrieval;

    /// <summary>
    ///     Class for Retrieval Metadata
    /// </summary>
    public class RetrievalMetadata : DataStageMetadata, IEditableRetrievalMetadata
    {
        /// <summary>
        /// fields present in the retrieval process
        /// </summary>
        private readonly HashSet<string> fields = new();

        /// <summary>
        /// list of raw files
        /// </summary>
        private readonly List<Uri> rawFiles = new List<Uri>();

        /// <summary>
        /// Gets the list of raw files
        /// </summary>
        public IReadOnlyCollection<Uri> RawFiles => this.rawFiles;

        /// <summary>
        /// Gets the list of fields present in the retrieval process
        /// </summary>
        public IReadOnlyCollection<string> Fields => this.fields;

        /// <inheritdoc/>
        public int? Rows { get; set; }

        /// <inheritdoc/>
        public string Endpoint { get; set; } = string.Empty;

        /// <inheritdoc />
        public void AddField(string field)
        {
            this.fields.Add(field);
        }

        /// <inheritdoc />
        public void AddRawFile(Uri file)
        {
            this.rawFiles.Add(file);
        }
    }
}