// -----------------------------------------------------------------------
// <copyright file="Fundamental.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Fundamental POCO
    /// </summary>
    public record Fundamental()
    {
        /// <summary>
        /// Gets the general data for the instrument
        /// </summary>
        public General? General { get; init; }

        /// <summary>
        /// Gets the highlights for the instrument
        /// </summary>
        public Highlights? Highlights { get; init; }
    }

    /// <summary>
    /// Fundamental\General POCO
    /// </summary>
    public record General(
        [property: MaxLength(10)] string Code,
        [property: MaxLength(10)] string Type,
        [property: MaxLength(25)] string Name,
        [property: MaxLength(25)] string Exchange,
        [property: MaxLength(5)] string CurrencyCode,
        [property: MaxLength(10)] string CurrencyName,
        [property: MaxLength(2)] string CurrencySymbol,
        [property: MaxLength(50)] string CountryName,
        [property: MaxLength(5)] string CountryISO,
        [property: MaxLength(15)] string ISIN,
        [property: MaxLength(15)] string CUSIP,
        [property: MaxLength(10)] string CIK,
        [property: MaxLength(10)] string EmployerIdNumber,
        [property: MaxLength(10)] string FiscalYearEnd,
        [property: MaxLength(10)] DateTimeOffset? IPODate,
        [property: MaxLength(25)] string InternationalDomestic,
        [property: MaxLength(25)] string Sector,
        [property: MaxLength(25)] string Industry,
        [property: MaxLength(25)] string GicSector,
        [property: MaxLength(25)] string GicGroup,
        [property: MaxLength(50)] string GicIndustry,
        [property: MaxLength(50)] string GicSubIndustry,
        [property: MaxLength(10)] string HomeCategory,
        bool? IsDelisted,
        string Description,
        [property: MaxLength(250)] string Address,
        [property: MaxLength(50)] string Phone,
        [property: MaxLength(100)] string WebUrl,
        [property: MaxLength(100)] string LogoUrl,
        int? FullTimeEmployees,
        DateTimeOffset? UpdatedAt)
    {
        /// <summary>
        /// Gets the Address data
        /// </summary>
        public AddressData? AddressData { get; init; }

        /// <summary>
        /// Gets the Listings
        /// </summary>
        public Dictionary<string, Listing>? Listings { get; init; }

        /// <summary>
        /// Gets the Officers
        /// </summary>
        public Dictionary<string, Officer>? Officers { get; init; }

        /// <summary>
        /// Gets the Earnings data
        /// </summary>
        public Earnings? Earnings { get; init; }

        /// <summary>
        /// Gets the Financials data
        /// </summary>
        public Financials? Financials { get; init; }
    }

    /// <summary>
    /// Address Data POCO
    /// </summary>
    public record AddressData(
        [property: MaxLength(25)] string Street,
        [property: MaxLength(25)] string City,
        [property: MaxLength(50)] string State,
        [property: MaxLength(50)] string Country,
        [property: MaxLength(10)] string Zip)
    {
    }

    /// <summary>
    /// Listing POCO
    /// </summary>
    public record Listing(
        [property: MaxLength(10)] string Code,
        [property: MaxLength(10)] string Exchange,
        [property: MaxLength(10)] string Name)
    {
    }

    /// <summary>
    /// Officer POCO
    /// </summary>
    public record Officer(
        [property: MaxLength(50)] string Name,
        [property: MaxLength(25)] string Title,
        [property: MaxLength(4)] string YearBorn)
    {
    }

    /// <summary>
    /// Fundamental\Highlights POCO
    /// </summary>
    public record Highlights(
        decimal MarketCapitalization,
        decimal MarketCapitalizationMln,
        decimal Ebitda,
        double PeRatio,
        double PegRatio,
        decimal WallStreetTargetPrice,
        decimal BookValue,
        decimal DividendShare,
        double DividendYield,
        decimal EarningsShare,
        decimal EpsEstimateCurrentYear,
        decimal EpsEstimateNextYear,
        decimal EpsEstimateNextQuarter,
        decimal EpsEstimateCurrentQuarter,
        DateTimeOffset? MostRecentQuarter,
        double ProfitMargin,
        double OperatingMarginTTM,
        double ReturnOnAssetsTTM,
        double ReturnOnEquityTTM,
        decimal RevenueTTM,
        decimal RevenuePerShareTTM,
        double QuarterlyRevenueGrowthYOY,
        decimal GrossProfitTTM,
        decimal DilutedEpsTTM,
        double QuarterlyEarningsGrowthYOY)
    {
    }

    /// <summary>
    /// Fundamental\Earnings POCO
    /// </summary>
    public record Earnings()
    {
        /// <summary>
        /// Gets the Earnings History data
        /// </summary>
        public Dictionary<string, History>? History { get; init; }

        /// <summary>
        /// Gets the Earnings Trend data
        /// </summary>
        public Dictionary<string, Trend>? Trend { get; init; }

        /// <summary>
        /// Gets the Annual Earnings data
        /// </summary>
        public Dictionary<string, Annual>? Annual { get; init; }
    }

    /// <summary>
    /// Officer POCO
    /// </summary>
    public record History(
        DateTimeOffset? ReportDate,
        DateTimeOffset? Date,
        BeforeAfterMarket? BeforeAfterMarket,
        [property: MaxLength(4)] string Currency,
        decimal? EpsActual,
        decimal? EpsEstimate,
        decimal? EpsDifference,
        double? SurprisePercent)
    {
    }

    /// <summary>
    /// Officer POCO
    /// </summary>
    public record Trend(
        DateTimeOffset? Date,
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
        double? EpsRevisionsDownLast90days)
    {
    }

    /// <summary>
    /// Officer POCO
    /// </summary>
    public record Annual(
        DateTimeOffset Date,
        decimal? EpsActual)
    {
    }

    public record Financials()
    { 
    }
}