// -----------------------------------------------------------------------
// <copyright file="Holder.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Abbotware.Core.Metadata;
    using Abbotware.Interop.Newtonsoft.Plugins;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Fundalmental\AnalystRatings POCO
    /// </summary>
    public record Holder(
        [property: MaxLength(Length.CombinedName)]
        string Name,
        [property: JsonConverter(typeof(BetterDateTimeConverter))]
        DateTimeOffset? Date,
        double? TotalShares,
        double? TotalAssets,
        int? CurrentShares,
        int? Change,
        [property: JsonProperty(PropertyName = "Change_p")]
        double? ChangePercent)
    {
    }
}