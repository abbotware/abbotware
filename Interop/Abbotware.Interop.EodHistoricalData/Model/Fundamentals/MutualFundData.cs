// -----------------------------------------------------------------------
// <copyright file="MutualFundData.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Abbotware.Core.Metadata;
    using Abbotware.Interop.EodHistoricalData.Serialization;
    using Abbotware.Interop.Newtonsoft.Plugins;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Fundamental\Mutual Fund Data POCO
    /// </summary>
    public record MutualFundData(
          [property: JsonProperty("Fund_Category"), MaxLength(50)] string? FundCategory,
          [property: JsonProperty("Fund_Style"), MaxLength(50)] string? FundStyle,
          decimal? Nav,
          [property: JsonProperty("Prev_Close_Price")] decimal? PrevClosePrice,
          [property: JsonProperty("Update_Date"), JsonConverter(typeof(BetterDateTimeConverter))] DateTimeOffset? UpdateDate,
          [property: JsonProperty("Portfolio_Net_Assets")] decimal? PortfolioNetAssets,
          [property: JsonProperty("Share_Class_Net_Assets")] decimal? ShareClassNetAssets,
          [property: JsonProperty("Morning_Star_Rating"), MaxLength(50)] string? MorningStarRating,
          [property: JsonProperty("Morning_Star_Risk_Rating"), MaxLength(50)] string? MorningStarRiskRating,
          [property: JsonProperty("Morning_Star_Category"), MaxLength(50)] string? MorningStarCategory,
          [property: JsonProperty("Inception_Date"), JsonConverter(typeof(BetterDateTimeConverter))] DateTimeOffset? InceptionDate,
          CurrencyType? Currency,
          [property: JsonProperty("Domicile"), MaxLength(100)] string? Domicile,
          [property: JsonProperty("Yield")] double? Yield,
          [property: JsonProperty("Yield_YTD")] double? YieldYtd,
          [property: JsonProperty("Yield_1Year_YTD")] double? Yield1YearYtd,
          [property: JsonProperty("Yield_3Year_YTD")] double? Yield3YearYtd,
          [property: JsonProperty("Yield_5Year_YTD")] double? Yield5YearYtd,
          [property: JsonProperty("Expense_Ratio")] double? ExpenseRatio,
          [property: JsonProperty("Expense_Ratio_Date"), JsonConverter(typeof(BetterDateTimeConverter))] DateTimeOffset? ExpenseRatioDate,
          [property: JsonProperty("market_capitalization")] string? DuplicateMarketCapitalization,
          [property: JsonProperty("world_regions")] string? DuplicateWorldRegions,
          [property: JsonProperty("sector_weights")] string? DuplicateSectorWeights,
          [property: JsonProperty("asset_allocation")] string? DuplicateAssetAllocation)
    {
        /// <summary>
        /// Gets the Asset_Allocation data
        /// </summary>
        [JsonProperty("Asset_Allocation")]
        [JsonConverter(typeof(DictionaryFlattener<string, MfAssetAllocation>))]
        public IReadOnlyCollection<MfAssetAllocation>? AssetAllocations { get; init; }

        /// <summary>
        /// Gets the Value_Growth data
        /// </summary>
        [JsonProperty("Value_Growth")]
        [JsonConverter(typeof(DictionaryFlattener<string, MfValueGrowth>))]
        public IReadOnlyCollection<MfValueGrowth>? ValueGrowth { get; init; }

        /// <summary>
        /// Gets the Top_Holdings data
        /// </summary>
        [JsonProperty("Top_Holdings")]
        [JsonConverter(typeof(DictionaryFlattener<string, MfHolding>))]
        [NotMapped]
        public IReadOnlyCollection<MfHolding>? TopHoldings { get; init; }

        /// <summary>
        /// Gets the Market_Capitalization data
        /// </summary>
        [JsonProperty("Market_Capitalization")]
        [JsonConverter(typeof(DictionaryFlattener<string, MfMarketCapitalization>))]
        public IReadOnlyCollection<MfMarketCapitalization>? MarketCapitalizations { get; init; }

        /// <summary>
        /// Gets the Sector Weights data
        /// </summary>
        [JsonProperty("Sector_Weights")]
        [JsonConverter(typeof(DictionaryFlattener<string, MfSectorWeight>), typeof(MfSectorWeightKeyValue))]
        public IReadOnlyCollection<MfSectorWeight>? SectorWeights { get; init; }

        /// <summary>
        /// Gets the WorldRegions data
        /// </summary>
        [JsonProperty("World_Regions")]
        [JsonConverter(typeof(DictionaryFlattener<string, MfWorldRegion>), typeof(MfWorldRegionKeyValue))]
        public IReadOnlyCollection<MfWorldRegion>? WorldRegions { get; init; }

        /// <summary>
        /// Gets the Top_Countries data
        /// </summary>
        [JsonProperty("Top_Countries")]
        [JsonConverter(typeof(DictionaryFlattener<string, MfTopCountry>))]
        public IReadOnlyCollection<MfTopCountry>? TopCountries { get; init; }
    }

    /// <summary>
    /// Fundamentals\MutualFund_Data\Asset_Allocation
    /// </summary>
    public record MfAssetAllocation(
      [property: MaxLength(25)] string Type,
#pragma warning disable CA1720 // Identifier contains type name
      [property: JsonProperty("Long_%")] double? Long,
      [property: JsonProperty("Short_%")] double? Short,
#pragma warning restore CA1720 // Identifier contains type name
      [property: JsonProperty("Net_%")] double? Net,
      [property: JsonProperty("Category_Average")] double? CategoryAverage,
      [property: JsonProperty("Benchmark")] double? Benchmark)
    {
    }

    /// <summary>
    /// Fundamentals\MutualFund_Data\Value_Growth
    /// </summary>
    public record MfValueGrowth(
      [property: MaxLength(100)] string Name,
      [property: JsonProperty("Category_Average")] double? CategoryAverage,
      double? Benchmark,
      [property: JsonProperty("Stock_Portfolio")] double? StockPortfolio)
    {
    }

    /// <summary>
    /// Fundamentals\MutualFund_Data\Holding
    /// </summary>
    public record MfHolding(
      [property: MaxLength(Length.CompanyName)] string? Name,
      double? Owned,
      double? Change,
      double? Weight)
    {
    }

    /// <summary>
    /// Fundamentals\MutualFund_Data\Market_Capitalization
    /// </summary>
    public record MfMarketCapitalization(
     [property: MaxLength(25)] string? Size,
     [property: JsonProperty("Category_Average")] double? CategoryAverage,
     double? Benchmark,
     [property: JsonProperty("Portfolio_%")] double? PortfolioPercentage)
    {
    }

    /// <summary>
    /// Fundamentals\MutualFund_Data\World_Region
    /// </summary>
    public record MfWorldRegion(
      [property: MaxLength(Length.Country)] string Name,
      [property: MaxLength(50)] string Region,
      [property: JsonProperty("Category_Average")] double? CategoryAverage,
      double? Benchmark,
      [property: JsonProperty("Stocks_%")] double? PortfolioPercentage)
    {
    }

    /// <summary>
    /// Fundamentals\MutualFund_Data\Sector_Weights
    /// </summary>
    public record MfSectorWeight(
      [property: MaxLength(50)] string Type,
      [property: MaxLength(50)] string Category,
      [property: JsonProperty("Category_Average")] double? CategoryAverage,
      double? Benchmark,
      [property: JsonProperty("Amount_%")] double? PortfolioPercentage)
    {
    }

    /// <summary>
    /// Fundamentals\MutualFund_Data\Top_Countries
    /// </summary>
    public record MfTopCountry(
      [property: MaxLength(Length.Country)] string Country,
      [property: JsonProperty("Category_Average")] double? CategoryAverage,
      double? Benchmark,
      [property: JsonProperty("Amount_%")] double? PortfolioPercentage)
    {
    }
}
