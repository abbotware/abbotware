// -----------------------------------------------------------------------
// <copyright file="Instrument.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System.ComponentModel.DataAnnotations;
    using Abbotware.Core.Metadata;

    /// <summary>
    /// Instrument Poco
    /// </summary>
    /// <param name="Cusip">cusip</param>
    /// <param name="Symbol">symbol</param>
    /// <param name="Description">description</param>
    /// <param name="Exchange">exchange</param>
    /// <param name="AssetType">asset type</param>
    public record Instrument(
        [property: MaxLength(Length.Cusip)] string? Cusip,
        [property: Key][property: MaxLength(TDAmeritradeConstants.SymbolLength)] string Symbol,
        [property: MaxLength(250)] string? Description,
        ExchangeType Exchange,
        AssetType AssetType)
    {
        /// <summary>
        /// Gets the Fundamental data for the instrument
        /// </summary>
        public Fundamental? Fundamental { get; init; }
    }
}