// -----------------------------------------------------------------------
// <copyright file="Country.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Iso
{
    using Abbotware.Core.Helpers;

    /// <summary>
    /// Metadata class for ISO Country data
    /// </summary>
    /// <param name="Id">Country</param>
    /// <param name="Alpha3">ISO Alpha3 Code</param>
    /// <param name="Alpha2">ISO Alpha2 Code</param>
    /// <param name="Name">country name</param>
    public record class Country(CountryCode Id, string Alpha3, string Alpha2, string Name)
        : ReadOnlyMetadata<CountryCode>(Id)
    {
    }
}