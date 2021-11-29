// -----------------------------------------------------------------------
// <copyright file="CountryMetadata.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Iso
{
    using Abbotware.Core.Helpers;

    /// <summary>
    /// Metadata for country enum
    /// </summary>
    public class CountryMetadata : MetadataLookup<CountryCode, Country>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryMetadata"/> class.
        /// </summary>
        public CountryMetadata()
        {
            var values = EnumHelper.GetValues<CountryCode>();

            foreach (var v in values)
            {
                var m = new Country(v, v.ToString(), ((Country2Code)(int)v).ToString(), v.ToString());
                this.Lookup.Add(v, m);
            }
        }
    }
}