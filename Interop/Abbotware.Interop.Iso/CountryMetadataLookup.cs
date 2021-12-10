// -----------------------------------------------------------------------
// <copyright file="CountryMetadataLookup.cs" company="Abbotware, LLC">
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
    using Abbotware.Core.Metadata;

    /// <summary>
    /// Metadata lookup for ISO 3166 Country
    /// </summary>
    public partial class CountryMetadataLookup : MetadataLookup<Country, CountryMetadata>
    {
        private readonly Dictionary<string, CountryMetadata> alpha2;
        private readonly Dictionary<string, CountryMetadata> alpha3;
        private readonly Dictionary<ushort, CountryMetadata> id;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryMetadataLookup"/> class.
        /// </summary>
        public CountryMetadataLookup()
        {
            this.Init();

            this.alpha2 = this.Data.ToDictionary(kvp => kvp.Value.Alpha2, kvp => kvp.Value, StringComparer.InvariantCultureIgnoreCase);
            this.alpha3 = this.Data.ToDictionary(kvp => kvp.Value.Alpha3, kvp => kvp.Value, StringComparer.InvariantCultureIgnoreCase);
            this.id = this.Data.ToDictionary(kvp => (ushort)kvp.Value.Id, kvp => kvp.Value);
        }

        /// <summary>
        /// Gets the singleton instance
        /// </summary>
        public static CountryMetadataLookup Instance => new();

        /// <summary>
        /// Lookup Country by Alpha2 Code
        /// </summary>
        /// <param name="alpha2">ISO Alpha2 Code</param>
        /// <returns>country metadata</returns>
        public CountryMetadata LookupAlpha2(string alpha2)
        {
            return this.alpha2[alpha2];
        }

        /// <summary>
        /// Try and parse Alpha2 Code into country enum
        /// </summary>
        /// <param name="alpha2">ISO Alpha2 Code</param>
        /// <param name="country">parsed country type</param>
        /// <returns>true/false based on parse status</returns>
#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
        public bool TryParseAlpha2(string? alpha2, [NotNullWhen(true)] out Country? country)
#else
        public bool TryParseAlpha2(string alpha2, out Country? country)
#endif
        {
            country = null;

            if (alpha2 == null)
            {
                return false;
            }

            alpha2 = alpha2.Trim();

            if (!this.alpha2.TryGetValue(alpha2, out var meta))
            {
                return false;
            }

            country = meta.Id;
            return true;
        }

        /// <summary>
        /// Lookup Country by Alpha3 Code
        /// </summary>
        /// <param name="alpha3">ISO Alpha3 Code</param>
        /// <returns>country metadata</returns>
        public CountryMetadata LookupAlpha3(string alpha3)
        {
            return this.alpha3[alpha3];
        }

        /// <summary>
        /// Try and parse Alpha2 Code into country enum
        /// </summary>
        /// <param name="alpha3">ISO Alpha3 Code</param>
        /// <param name="country">parsed country type</param>
        /// <returns>true/false based on parse status</returns>
#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
        public bool TryParseAlpha3(string? alpha3, [NotNullWhen(true)] out Country? country)
#else
        public bool TryParseAlpha3(string alpha3, out Country? country)
#endif
        {
            country = null;

            if (alpha3 == null)
            {
                return false;
            }

            alpha3 = alpha3.Trim();

            if (!this.alpha3.TryGetValue(alpha3, out var meta))
            {
                return false;
            }

            country = meta.Id;
            return true;
        }

        /// <summary>
        /// Lookup Country by Id
        /// </summary>
        /// <param name="id">ISO Country Id</param>
        /// <returns>country metadata</returns>
        public CountryMetadata Lookup(ushort id)
        {
            return this.id[id];
        }

        private partial void Init();
    }
}