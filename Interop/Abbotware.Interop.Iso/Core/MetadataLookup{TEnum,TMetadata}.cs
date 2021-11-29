// -----------------------------------------------------------------------
// <copyright file="MetadataLookup{TEnum,TMetadata}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Iso
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Enum-based lookup for a metadata
    /// </summary>
    /// <typeparam name="TEnum">enum type</typeparam>
    /// <typeparam name="TMetadata">metadata type</typeparam>
    public class MetadataLookup<TEnum, TMetadata>
        where TEnum : Enum
        where TMetadata : ReadOnlyMetadata<TEnum>
    {
        /// <summary>
        /// Gets the lookup
        /// </summary>
        protected Dictionary<TEnum, TMetadata> Lookup { get; } = new Dictionary<TEnum, TMetadata>();

        /// <summary>
        /// Gets the metadata for the provided enum
        /// </summary>
        /// <param name="key">enum key</param>
        /// <returns>meta data</returns>
        public TMetadata this[TEnum key] => this.Lookup[key];
    }
}