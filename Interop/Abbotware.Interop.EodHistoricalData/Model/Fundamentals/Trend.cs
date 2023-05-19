// -----------------------------------------------------------------------
// <copyright file="Trend.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Officer POCO
    /// </summary>
    public record Trend(
        DateTimeOffset Date,
        [property: MaxLength(10)] string Period,
        double? Growth,
        double? EarningsEstimateAvg,
        double? EarningsEstimateLow,
        double? EarningsEstimateHigh,
        double? EarningsEstimateYearAgoEps,
        double? EarningsEstimateNumberOfAnalysts,
        double? EarningsEstimateGrowth,
        decimal? RevenueEstimateAvg,
        decimal? RevenueEstimateLow,
        decimal? RevenueEstimateHigh,
        decimal? RevenueEstimateYearAgoEps,
        double? RevenueEstimateNumberOfAnalysts,
        double? RevenueEstimateGrowth,
        decimal? EpsTrendCurrent,
        decimal? EpsTrend7daysAgo,
        decimal? EpsTrend30daysAgo,
        decimal? EpsTrend60daysAgo,
        decimal? EpsTrend90daysAgo,
        double? EpsRevisionsUpLast7days,
        double? EpsRevisionsUpLast30days,
        double? EpsRevisionsDownLast30days,
        double? EpsRevisionsDownLast90days,
        double? EpsRevisionsDownLast7days)
    {
    }
}