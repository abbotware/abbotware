// -----------------------------------------------------------------------
// <copyright file="Technicals.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using global::Newtonsoft.Json;

    /// <summary>
    /// Fundalmental\Technicals POCO
    /// </summary>
    public record Technicals(
        double? Beta,
        [property: JsonProperty(PropertyName = "52WeekHigh")] decimal? Week52High,
        [property: JsonProperty(PropertyName = "52WeekLow")] decimal? Week52Low,
        [property: JsonProperty(PropertyName = "50DayMA")] decimal? Day50MA,
        [property: JsonProperty(PropertyName = "200DayMA")] decimal? Day200MA,
        long? SharesShort,
        long? SharesShortPriorMonth,
        double? ShortRatio,
        double? ShortPercent)
    {
    }
}