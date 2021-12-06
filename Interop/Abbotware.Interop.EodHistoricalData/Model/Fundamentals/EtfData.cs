// -----------------------------------------------------------------------
// <copyright file="EtfData.cs" company="Abbotware, LLC">
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
    /// Fundamental\ETF Data POCO
    /// </summary>
    public record EtfData(
          [property: MaxLength(50)] string? Isin,
          [property: MaxLength(Length.CompanyName), JsonProperty("Company_Name")] string? CompanyName,
          [property: MaxLength(Length.DomainName), JsonProperty("Company_URL")] string? CompanyUrl,
          [property: MaxLength(Length.Url), JsonProperty("ETF_URL")] string? EtfUrl,
          double? Yield,
          [property: JsonProperty("Dividend_Paying_Frequency")] string? DividendPayingFrequency,
          [property: JsonProperty("Inception_Date"), JsonConverter(typeof(BetterDateTimeConverter))] DateTimeOffset? InceptionDate,
          [property: JsonProperty("Max_Annual_Mgmt_Charge")] double? MaxAnnualMgmtCharge,
          [property: JsonProperty("Ongoing_Charge")] double? OngoingCharge,
          [property: JsonProperty("Date_Ongoing_Charge"), JsonConverter(typeof(BetterDateTimeConverter))] DateTimeOffset? DateOngoingCharge,
          double? NetExpenseRatio,
          decimal? AnnualHoldingsTurnover,
          decimal? TotalAssets,
          [property: MaxLength(20), JsonProperty("Average_Mkt_Cap_Mil")] string? AverageMktCapMil,
          [property: JsonProperty("Holdings_Count")] int? HoldingsCount,
          [property: JsonConverter(typeof(BetterDateTimeConverter))] DateTimeOffset? UpdatedAt,
          [property: MaxLength(200)] string? Domicile,
          [property: MaxLength(200), JsonProperty("Index_Name")] string? IndexName)
    {
        /// <summary>
        /// Gets the Market Capitalisation data
        /// </summary>
        [JsonProperty("Market_Capitalisation")]
        public MarketCapitalisation? MarketCapitalisation { get; init; }

        /// <summary>
        /// Gets the AssetAllocation data
        /// </summary>
        [JsonProperty("Asset_Allocation")]
        [JsonConverter(typeof(DictionaryFlattener<string, AssetAllocation>), typeof(AssetAllocationKeyValue))]
        public IReadOnlyCollection<AssetAllocation>? AssetAllocations { get; init; }

        /// <summary>
        /// Gets the WorldRegions data
        /// </summary>
        [JsonProperty("World_Regions")]
        [JsonConverter(typeof(DictionaryFlattener<string, WorldRegion>), typeof(WorldRegionKeyValue))]
        public IReadOnlyCollection<WorldRegion>? WorldRegions { get; init; }

        /// <summary>
        /// Gets the Sector Weights data
        /// </summary>
        [JsonProperty("Sector_Weights")]
        [JsonConverter(typeof(DictionaryFlattener<string, SectorWeight>), typeof(SectorWeightKeyValue))]
        public IReadOnlyCollection<SectorWeight>? SectorWeights { get; init; }

        /// <summary>
        /// Gets the Market Fixed Income data
        /// </summary>
        [JsonProperty("Fixed_Income")]
        [JsonConverter(typeof(DictionaryFlattener<string, FixedIncome>), typeof(FixedIncomeKeyValue))]
        public IReadOnlyCollection<FixedIncome>? FixedIncome { get; init; }

        /// <summary>
        /// Gets the Top10Holdings data
        /// </summary>
        [JsonProperty("Top_10_Holdings")]
        [JsonConverter(typeof(DictionaryFlattener<string, Holding>))]
        [NotMapped]
        public IReadOnlyCollection<Holding>? Top10Holdings { get; init; }

        /// <summary>
        /// Gets the Holdings data
        /// </summary>
        [JsonConverter(typeof(DictionaryFlattener<string, Holding>))]
        public IReadOnlyCollection<Holding>? Holdings { get; init; }

        /// <summary>
        /// Gets the Valuations Growth data
        /// </summary>
        [JsonProperty("Valuations_Growth")]
        public ValuationsGrowth? ValuationsGrowth { get; init; }

        /// <summary>
        /// Gets the MorningStar data
        /// </summary>
        public MorningStar? MorningStar { get; init; }

        /// <summary>
        /// Gets the Performance data
        /// </summary>
        public Performance? Performance { get; init; }
    }

    /// <summary>
    /// Fundamentals\ETF_Data\Market_Capitalisation
    /// </summary>
    public record MarketCapitalisation(
     [property: MaxLength(25)] string? Mega,
     [property: MaxLength(25)] string? Big,
     [property: MaxLength(25)] string? Medium,
     [property: MaxLength(25)] string? Small,
     [property: MaxLength(25)] string? Micro)
    {
        /// <summary>
        /// Gets the Alias for Mega
        /// </summary>
        [JsonProperty("Géantes")]
        [NotMapped]
        [Obsolete("Do not use, only exists for alias mapping")]
        public string? Giantes { get => this.Mega; init => this.Mega = value; }

        /// <summary>
        /// Gets the Alias for Big
        /// </summary>
        [NotMapped]
        [Obsolete("Do not use, only exists for alias mapping")]
        public string? Grandes { get => this.Big; init => this.Big = value; }

        /// <summary>
        /// Gets the Alias for Medium
        /// </summary>
        [NotMapped]
        [Obsolete("Do not use, only exists for alias mapping")]
        public string? Moyennes { get => this.Medium; init => this.Medium = value; }

        /// <summary>
        /// Gets the Alias for Mega
        /// </summary>
        [NotMapped]
        [Obsolete("Do not use, only exists for alias mapping")]
        public string? Petites { get => this.Small; init => this.Small = value; }
    }

    /// <summary>
    /// Fundamentals\ETF_Data\MorningStar
    /// </summary>
    public record MorningStar(
     double? Ratio,
     [property: MaxLength(50), JsonProperty("Category_Benchmark")] string? CategoryBenchmark,
     [property: MaxLength(50), JsonProperty("Sustainability_Ratio")] string? SustainabilityRatio)
    {
    }

    /// <summary>
    /// Fundamentals\ETF_Data\Performance
    /// </summary>
    public record Performance(
     [property: JsonProperty("1y_Volatility")] double? Volatility1Year,
     [property: JsonProperty("3y_Volatility")] double? Volatility3Year,
     [property: JsonProperty("3y_ExpReturn")] double? ExpReturn3Year,
     [property: JsonProperty("3y_SharpRatio")] double? SharpRatio3Year,
     [property: JsonProperty("Returns_YTD")] double? ReturnsYtd,
     [property: JsonProperty("Returns_1Y")] double? Returns1Year,
     [property: JsonProperty("Returns_3Y")] double? Returns3Year,
     [property: JsonProperty("Returns_5Y")] double? Returns5Year,
     [property: JsonProperty("Returns_10Y")] double? Returns10Year)
    {
    }

    /// <summary>
    /// Fundamentals\ETF_Data\Asset_Allocation
    /// </summary>
    public record AssetAllocation(
      [property: MaxLength(25)] string Type,
      [property: JsonProperty("Long_%")] double? Long,
      [property: JsonProperty("Short_%")] double? Short,
      [property: JsonProperty("Net_Assets_%")] double? NetAssets)
    {
    }

    /// <summary>
    /// Fundamentals\ETF_Data\World_Region
    /// </summary>
    public record WorldRegion(
      [property: MaxLength(50)] string Type,
      [property: JsonProperty("Equity_%")] double? EquityPercentage,
      string? RelativeToCategory)
        : EtfRelativeToCategory(RelativeToCategory)
    {
    }

    /// <summary>
    /// Fundamentals\ETF_Data\Sector_Weights
    /// </summary>
    public record SectorWeight(
      [property: MaxLength(50)] string Type,
      [property: JsonProperty("Equity_%")] double? EquityPercentage,
      string? RelativeToCategory)
        : EtfRelativeToCategory(RelativeToCategory)
    {
    }

    /// <summary>
    /// Fundamentals\ETF_Data\Holding
    /// </summary>
    public record Holding(
      [property: MaxLength(50)] string? Code,
      [property: MaxLength(50)] string? Exchange,
      [property: MaxLength(Length.CompanyName)] string? Name,
      [property: MaxLength(50)] string? Sector,
      [property: MaxLength(50)] string? Industry,
      [property: MaxLength(Length.Country)] string? Country,
      [property: MaxLength(50)] string? Region,
      [property: JsonProperty("Assets_%")] double? Assets)
    {
    }

    /// <summary>
    /// Fundamentals\ETF_Data\Fixed_Income
    /// </summary>
    public record FixedIncome(
      [property: MaxLength(50)] string Type,
      [property: JsonProperty("Fund_%")] double? FundPercentage,
      string? RelativeToCategory)
        : EtfRelativeToCategory(RelativeToCategory)
    {
    }

    /// <summary>
    /// Fundamentals\ETF_Data\Fixed_Income
    /// </summary>
    public record EtfRelativeToCategory(
        [property: MaxLength(50), JsonProperty("Relative_to_Category")] string? RelativeToCategory)
    {
    }

    /// <summary>
    /// Fundamentals\ETF_Data\ValuationsGrowth
    /// </summary>
    public record ValuationsGrowth()
    {
        /// <summary>
        /// Gets the Valuations_Rates_Portfolio data
        /// </summary>
        [JsonProperty("Valuations_Rates_Portfolio")]
        public ValuationsRates? ValuationsRatesPortfolio { get; init; }

        /// <summary>
        /// Gets the Valuations_Rates_To_Category data
        /// </summary>
        [JsonProperty("Valuations_Rates_To_Category")]
        public ValuationsRates? ValuationsRatesCategory { get; init; }

        /// <summary>
        /// Gets the Growth_Rates_Portfolio data
        /// </summary>
        [JsonProperty("Growth_Rates_Portfolio")]
        public GrowthRates? GrowthRatesPortfolio { get; init; }

        /// <summary>
        /// Gets the Growth_Rates_To_Category data
        /// </summary>
        [JsonProperty("Growth_Rates_To_Category")]
        public GrowthRates? GrowthRatesCategory { get; init; }
    }

    /// <summary>
    /// Fundamentals\ETF_Data\Valuations
    /// </summary>
    public record ValuationsRates(
      [property: JsonProperty("Price/Prospective Earnings")] double? PriceToProspectiveEarnings,
      [property: JsonProperty("Price/Book")] double? PriceToBook,
      [property: JsonProperty("Price/Sales")] double? PriceToSales,
      [property: JsonProperty("Price/Cash Flow")] double? PriceToCashFlow,
      [property: JsonProperty("Dividend-Yield Factor")] double? DividendYieldFactor)
    {
    }

    /// <summary>
    /// Fundamentals\ETF_Data\Valuations
    /// </summary>
    public record GrowthRates(
      [property: JsonProperty("Long-Term Projected Earnings Growth")] double? LongTermProjectedEarnings,
      [property: JsonProperty("Historical Earnings Growth")] double? HistoricalEarnings,
      [property: JsonProperty("Sales Growth")] double? Sales,
      [property: JsonProperty("Cash-Flow Growth")] double? CashFlow,
      [property: JsonProperty("Book-Value Growth")] double? BookValue)
    {
    }
}
