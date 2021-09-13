// -----------------------------------------------------------------------
// <copyright file="AnalystRatings.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    /// <summary>
    /// Fundalmental\AnalystRatings POCO
    /// </summary>
    public record AnalystRatings(
        double? Rating,
        decimal? TargetPrice,
        int? StrongBuy,
        int? Buy,
        int? Hold,
        int? Sell,
        int? StrongSell)
    {
    }
}