// -----------------------------------------------------------------------
// <copyright file="CurrencyMetadataLookup.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Iso
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abbotware.Core.Metadata;

    /// <summary>
    /// Metadata lookup for ISO 4217 Currency
    /// </summary>
    public partial class CurrencyMetadataLookup : MetadataLookup<Currency, CurrencyMetadata>
    {
        private readonly Dictionary<string, CurrencyMetadata> alpha;
        private readonly Dictionary<ushort, CurrencyMetadata> id;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyMetadataLookup"/> class.
        /// </summary>
        public CurrencyMetadataLookup()
        {
            this.Init();

            this.alpha = this.Data.ToDictionary(kvp => kvp.Value.Alpha, kvp => kvp.Value, StringComparer.InvariantCultureIgnoreCase);
            this.id = this.Data.ToDictionary(kvp => (ushort)kvp.Value.Id, kvp => kvp.Value);
        }

        /// <summary>
        /// Gets the singleton instance
        /// </summary>
        public static CurrencyMetadataLookup Instance => new();

        /// <summary>
        /// Lookup Currency by Alphabetic Code
        /// </summary>
        /// <param name="alpha">ISO Alphabetic Code</param>
        /// <returns>country metadata</returns>
        public CurrencyMetadata LookupAlpha(string alpha)
        {
            return this.alpha[alpha];
        }

        /// <summary>
        /// Lookup Currency by Numeric Code
        /// </summary>
        /// <param name="id">ISO Numeric Code</param>
        /// <returns>country metadata</returns>
        public CurrencyMetadata Lookup(ushort id)
        {
            return this.id[id];
        }

        private partial void Init();
    }
}