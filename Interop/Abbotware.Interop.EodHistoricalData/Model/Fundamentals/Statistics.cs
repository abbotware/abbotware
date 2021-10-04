// -----------------------------------------------------------------------
// <copyright file="Statistics.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System.ComponentModel.DataAnnotations;
    using Abbotware.Core.Metadata;

    /// <summary>
    /// Fundamental\Statistics POCO
    /// </summary>
    public record Statistics(
        decimal? MarketCapitalization,
        decimal? MarketCapitalizationDiluted,
        decimal? CirculatingSupply,
        decimal? TotalSupply,
        decimal? MaxSupply,
        decimal? MarketCapDominance,
        [property: MaxLength(Length.Url)] string? TechnicalDoc,
        [property: MaxLength(Length.Url)] string? Explorer,
        [property: MaxLength(Length.Url)] string? SourceCode,
        [property: MaxLength(Length.Url)] string? MessageBoard,
        decimal? LowAllTime,
        decimal? HighAllTime)
    {
    }
}