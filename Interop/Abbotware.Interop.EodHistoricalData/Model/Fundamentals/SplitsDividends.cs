// -----------------------------------------------------------------------
// <copyright file="SplitsDividends.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Abbotware.Interop.Newtonsoft.Plugins;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Fundalmental\SplitsDividends POCO
    /// </summary>
    public record SplitsDividends(
        double? ForwardAnnualDividendRate,
        double? ForwardAnnualDividendYield,
        double? PayoutRatio,
        [property: JsonConverter(typeof(BetterDateTimeConverter))] DateTimeOffset? DividendDate,
        [property: JsonConverter(typeof(BetterDateTimeConverter))] DateTimeOffset? ExDividendDate,
        [property: MaxLength(10)] string? LastSplitFactor,
        [property: JsonConverter(typeof(BetterDateTimeConverter))] DateTimeOffset? LastSplitDate,
        [property: JsonProperty(PropertyName = "Change_p")] double? ChangePercent)
    {
        /// <summary>
        /// Gets the DividendsByYear
        /// </summary>
        [JsonConverter(typeof(DictionaryFlattener<int, DividendsByYear>))]
        public IReadOnlyCollection<DividendsByYear>? NumberDividendsByYear { get; init; }
    }
}