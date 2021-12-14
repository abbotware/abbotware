// -----------------------------------------------------------------------
// <copyright file="CurrencyMetadata.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Iso
{
    using Abbotware.Core.Metadata;

    /// <summary>
    /// Metadata class for ISO 4217 Currency
    /// </summary>
    /// <param name="Id">Currency</param>
    /// <param name="Name">country name</param>
    /// <param name="Alpha">ISO Alphabetic Code</param>
    /// <param name="MinorUnit">minor unit</param>
    public record class CurrencyMetadata(Currency Id, string Name, string Alpha, byte MinorUnit)
        : BaseMetadataRecord<Currency>(Id, Name)
    {
    }
}