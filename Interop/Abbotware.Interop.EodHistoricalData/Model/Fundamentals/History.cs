// -----------------------------------------------------------------------
// <copyright file="History.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System;
    using Abbotware.Interop.EodHistoricalData.Serialization;
    using Abbotware.Interop.Iso;
    using Abbotware.Interop.Newtonsoft.Plugins;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Officer POCO
    /// </summary>
    public record History(
        [property: JsonConverter(typeof(BetterDateTimeConverter))] DateTimeOffset? Date,
        [property: JsonConverter(typeof(BetterDateTimeConverter))] DateTimeOffset? ReportDate,
        BeforeAfterMarket? BeforeAfterMarket,
        [property: JsonConverter(typeof(CurrencyTypeConverter))] Currency Currency,
        decimal? EpsActual,
        decimal? EpsEstimate,
        decimal? EpsDifference,
        double? SurprisePercent)
    {
    }
}