// -----------------------------------------------------------------------
// <copyright file="Highlights.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System;
    using Abbotware.Interop.Newtonsoft.Plugins;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Fundamental\Highlights POCO
    /// </summary>
    public record Highlights(
        decimal? MarketCapitalization,
        decimal? MarketCapitalizationMln,
        decimal? Ebitda,
        double? PeRatio,
        double? PegRatio,
        decimal? WallStreetTargetPrice,
        decimal? BookValue,
        decimal? DividendShare,
        double? DividendYield,
        decimal? EarningsShare,
        decimal? EpsEstimateCurrentYear,
        decimal? EpsEstimateNextYear,
        decimal? EpsEstimateNextQuarter,
        decimal? EpsEstimateCurrentQuarter,
        [property: JsonConverter(typeof(BetterDateTimeConverter))] DateTimeOffset? MostRecentQuarter,
        double? ProfitMargin,
        double? OperatingMarginTTM,
        double? ReturnOnAssetsTTM,
        double? ReturnOnEquityTTM,
        decimal? RevenueTTM,
        decimal? RevenuePerShareTTM,
        double? QuarterlyRevenueGrowthYOY,
        decimal? GrossProfitTTM,
        decimal? DilutedEpsTTM,
        double? QuarterlyEarningsGrowthYOY)
    {
    }
}