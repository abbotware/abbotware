// -----------------------------------------------------------------------
// <copyright file="CountryMetadata.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Iso
{
    using Abbotware.Core.Metadata;

    /// <summary>
    /// Metadata class for ISO 3166 Country
    /// </summary>
    /// <param name="Id">Country</param>
    /// <param name="Alpha3">ISO Alpha3 Code</param>
    /// <param name="Alpha2">ISO Alpha2 Code</param>
    /// <param name="Name">country name</param>
    /// <param name="NameFrench">country name in French</param>
    public record class CountryMetadata(Country Id, string Alpha3, string Alpha2, string Name, string NameFrench)
        : BaseMetadataRecord<Country>(Id, Name)
    {
    }
}