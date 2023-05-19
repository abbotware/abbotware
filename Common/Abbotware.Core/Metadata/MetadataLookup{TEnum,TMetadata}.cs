// -----------------------------------------------------------------------
// <copyright file="MetadataLookup{TEnum,TMetadata}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Metadata
{
    using System;
    using System.Collections.Generic;
    using Abbotware.Core.Collections;

    /// <summary>
    /// Enum-based lookup for a metadata
    /// </summary>
    /// <typeparam name="TEnum">enum type</typeparam>
    /// <typeparam name="TMetadata">metadata type</typeparam>
    public class MetadataLookup<TEnum, TMetadata> : ILookup<TEnum, TMetadata>
        where TEnum : Enum
        where TMetadata : BaseMetadataRecord<TEnum>
    {
        /// <inheritdoc/>
        public long Count => this.Data.Count;

        /// <summary>
        /// Gets the values
        /// </summary>
        public IEnumerable<TMetadata> Values => this.Data.Values;

        /// <summary>
        /// Gets the lookup
        /// </summary>
        protected Dictionary<TEnum, TMetadata> Data { get; } = new Dictionary<TEnum, TMetadata>();

        /// <summary>
        /// Gets the metadata for the provided enum
        /// </summary>
        /// <param name="key">enum key</param>
        /// <returns>meta data</returns>
        public TMetadata this[TEnum key] => this.Data[key];

        /// <inheritdoc/>
        public TMetadata Lookup(TEnum key1) => this[key1];

        /// <inheritdoc/>
        public IEnumerable<TEnum> Level1() => this.Data.Keys;
    }
}