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
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Abbotware.Core;
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
        /// Lookup Currency by Alphabetic Code
        /// </summary>
        /// <param name="alpha">ISO Alphabetic Code</param>
        /// <returns>country metadata</returns>
        public CurrencyMetadata LookupAlpha(string alpha)
        {
            return this.alpha[alpha];
        }

        /// <summary>
        /// Try and parse Alphabetic Code in enum
        /// </summary>
        /// <param name="alpha">ISO Alphabetic Code</param>
        /// <param name="currency">parsed currency type</param>
        /// <returns>true/false based on parse status</returns>
#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
        public bool TryParseAlpha(string? alpha, [NotNullWhen(true)] out Currency? currency)
#else
        public bool TryParseAlpha(string alpha, out Currency? currency)
#endif
        {
            currency = null;

            if (alpha == null)
            {
                return false;
            }

            alpha = alpha.Trim();

            if (!this.alpha.TryGetValue(alpha, out var meta))
            {
                return false;
            }

            currency = meta.Id;
            return true;
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