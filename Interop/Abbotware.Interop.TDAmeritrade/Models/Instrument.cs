// -----------------------------------------------------------------------
// <copyright file="Instrument.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade
{
    /// <summary>
    /// Instrument Poco
    /// </summary>
    /// <param name="Cusip">cusip</param>
    /// <param name="Symbol">symbol</param>
    /// <param name="Description">description</param>
    /// <param name="Exchange">exchange</param>
    /// <param name="AssetType">asset type</param>
    /// <param name="Fundamental">Fundamental Data</param>
    public record Instrument(string? Cusip, string Symbol, string? Description, string Exchange, AssetType AssetType, Fundamental? Fundamental)
    {
    }
}