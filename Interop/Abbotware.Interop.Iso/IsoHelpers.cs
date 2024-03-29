﻿// -----------------------------------------------------------------------
// <copyright file="IsoHelpers.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Iso
{
    /// <summary>
    /// Static ISO Helper
    /// </summary>
    public static class IsoHelpers
    {
        /// <summary>
        /// Gets a singleton instance of the CurrencyMetadataLookup
        /// </summary>
        public static CurrencyMetadataLookup Currency { get; } = new();

        /// <summary>
        /// Gets a singleton instance of the CountryMetadataLookup
        /// </summary>
        public static CountryMetadataLookup Country { get; } = new();
    }
}
