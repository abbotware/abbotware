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
    using System.ComponentModel.DataAnnotations.Schema;
    using global::Newtonsoft.Json;

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

        /// <summary>
        /// Gets the Valuation for the instrument
        /// </summary>
        public Valuation? Valuation { get; init; }

        /// <summary>
        /// Gets the earnings for the instrument
        /// </summary>
        public SharesStats? SharesStats { get; init; }

        /// <summary>
        /// Gets the technicals for the instrument
        /// </summary>
        public Technicals? Technicals { get; init; }

        /// <summary>
        /// Gets the splits and dividends for the instrument
        /// </summary>
        public SplitsDividends? SplitsDividends { get; init; }

        /// <summary>
        /// Gets the analyst ratings for the instrument
        /// </summary>
        public AnalystRatings? AnalystRatings { get; init; }

        /// <summary>
        /// Gets the holders for the instrument
        /// </summary>
        public Holders? Holders { get; init; }

        /// <summary>
        /// Gets the Insider Transaction
        /// </summary>
        public Dictionary<int, InsiderTransaction>? InsiderTransactions { get; init; }

        /// <summary>
        /// Gets the ESG Scores for the instrument
        /// </summary>
        [property: JsonProperty(PropertyName = "ESGScores")]
        public EsgScores? EsgScores { get; init; }

        /// <summary>
        /// Gets the Outstanding Shares for the instrument
        /// </summary>
        public OutstandingShares? OutstandingShares { get; init; }

        /// <summary>
        /// Gets the earnings for the instrument
        /// </summary>
        public Earnings? Earnings { get; init; }

        /// <summary>
        /// Gets the financials for the instrument
        /// </summary>
        public Financials? Financials { get; init; }
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
        DateTimeOffset? MostRecentQuarter,
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
        [property: Key]
        DateTimeOffset? Date,
        DateTimeOffset? ReportDate,
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
        [property: Key]
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
        double? EpsRevisionsDownLast90days)
    {
    }

    /// <summary>
    /// Officer POCO
    /// </summary>
    public record Annual(
        [property: Key]
        DateTimeOffset Date,
        decimal? EpsActual)
    {
    }

    /// <summary>
    /// Fundamental\Financials POCO
    /// </summary>
    public record Financials()
    {
        /// <summary>
        /// Gets the Balance Sheet data
        /// </summary>
        [JsonProperty(PropertyName = "balance_sheet")]
        public BalanceSheets? BalanceSheets { get; init; }

        /// <summary>
        /// Gets the Cash Flow data
        /// </summary>
        [JsonProperty(PropertyName = "cash_flow")]
        public CashFlows? CashFlows { get; init; }

        /// <summary>
        /// Gets the Income Statement data
        /// </summary>
        [JsonProperty(PropertyName = "income_statement")]
        public IncomeStatements? IncomeStatements { get; init; }
    }

    /// <summary>
    /// Balance Sheet POCO
    /// </summary>
    public record BalanceSheets(
        [property: MaxLength(3)]
        [property: JsonProperty(PropertyName = "currency_symbol")] string CurrencySymbol)
    {
        /// <summary>
        /// Gets the quarterly balance sheet data
        /// </summary>
        public Dictionary<string, BalanceSheet>? Quarterly { get; init; }

        /// <summary>
        /// Gets the yearly balance sheet data
        /// </summary>
        public Dictionary<string, BalanceSheet>? Yearly { get; init; }
    }

    /// <summary>
    /// Balance Sheet POCO
    /// </summary>
    public record BalanceSheet(
           DateTimeOffset? Date,
           [property: JsonProperty(PropertyName = "filing_date")] DateTimeOffset? FilingDate,
           [property: MaxLength(3)]
           [property: JsonProperty(PropertyName = "currency_symbol")] string CurrencySymbol,
           decimal? TotalAssets,
           decimal? IntangibleAssets,
           decimal? EarningAssets,
           decimal? OtherCurrentAssets,
           decimal? TotalLiab,
           decimal? TotalStockholderEquity,
           decimal? DeferredLongTermLiab,
           decimal? OtherCurrentLiab,
           decimal? CommonStock,
           decimal? RetainedEarnings,
           decimal? OtherLiab,
           decimal? GoodWill,
           decimal? OtherAssets,
           decimal? Cash,
           decimal? TotalCurrentLiabilities,
           decimal? NetDebt,
           decimal? ShortTermDebt,
           decimal? ShortLongTermDebt,
           decimal? ShortLongTermDebtTotal,
           decimal? OtherStockholderEquity,
           decimal? PropertyPlantEquipment,
           decimal? TotalCurrentAssets,
           decimal? LongTermInvestments,
           decimal? NetTangibleAssets,
           decimal? ShortTermInvestments,
           decimal? NetReceivables,
           decimal? LongTermDebt,
           decimal? Inventory,
           decimal? AccountsPayable,
           decimal? TotalPermanentEquity,
           decimal? NoncontrollingInterestInConsolidatedEntity,
           decimal? TemporaryEquityRedeemableNoncontrollingInterests,
           decimal? AccumulatedOtherComprehensiveIncome,
           decimal? AdditionalPaidInCapital,
           decimal? CommonStockTotalEquity,
           decimal? PreferredStockTotalEquity,
           decimal? RetainedEarningsTotalEquity,
           decimal? TreasuryStock,
           decimal? AccumulatedAmortization,
           decimal? NonCurrrentAssetsOther,
           decimal? DeferredLongTermAssetCharges,
           decimal? NonCurrentAssetsTotal,
           decimal? CapitalLeaseObligations,
           decimal? LongTermDebtTotal,
           decimal? NonCurrentLiabilitiesOther,
           decimal? NonCurrentLiabilitiesTotal,
           decimal? NegativeGoodwill,
           decimal? Warrants,
           decimal? PreferredStockRedeemable,
           decimal? CapitalSurpluse,
           decimal? LiabilitiesAndStockholdersEquity,
           decimal? CashAndShortTermInvestments,
           decimal? PropertyPlantAndEquipmentGross,
           decimal? AccumulatedDepreciation,
           decimal? NetWorkingCapital,
           decimal? NetInvestedCapital,
           decimal? CommonStockSharesOutstanding)
    {
    }

    /// <summary>
    /// Cash Flow POCO
    /// </summary>
    public record CashFlows(
        [property: MaxLength(3)]
        [property: JsonProperty(PropertyName = "currency_symbol")] string CurrencySymbol)
    {
        /// <summary>
        /// Gets the quarterly cash flow data
        /// </summary>
        public Dictionary<string, CashFlow>? Quarterly { get; init; }

        /// <summary>
        /// Gets the yearly cash flow data
        /// </summary>
        public Dictionary<string, CashFlow>? Yearly { get; init; }
    }

    /// <summary>
    /// Balance Sheet POCO
    /// </summary>
    public record CashFlow(
           DateTimeOffset? Date,
           [property: JsonProperty(PropertyName = "filing_date")] DateTimeOffset? FilingDate,
           [property: MaxLength(3)]
           [property: JsonProperty(PropertyName = "currency_symbol")] string CurrencySymbol,
           decimal? Investments,
           decimal? ChangeToLiabilities,
           decimal? TotalCashflowsFromInvestingActivities,
           decimal? NetBorrowings,
           decimal? TotalCashFromFinancingActivities,
           decimal? ChangeToOperatingActivities,
           decimal? NetIncome,
           decimal? ChangeInCash,
           decimal? BeginPeriodCashFlow,
           decimal? EndPeriodCashFlow,
           decimal? TotalCashFromOperatingActivities,
           decimal? Depreciation,
           decimal? OtherCashflowsFromInvestingActivities,
           decimal? DividendsPaid,
           decimal? ChangeToInventory,
           decimal? ChangeToAccountReceivables,
           decimal? SalePurchaseOfStock,
           decimal? OtherCashflowsFromFinancingActivities,
           decimal? ChangeToNetincome,
           decimal? CapitalExpenditures,
           decimal? ChangeReceivables,
           decimal? CashFlowsOtherOperating,
           decimal? ExchangeRateChanges,
           decimal? CashAndCashEquivalentsChanges,
           decimal? ChangeInWorkingCapital,
           decimal? OtherNonCashItems,
           decimal? FreeCashFlow)
    {
    }

    /// <summary>
    /// Income Statement POCO
    /// </summary>
    public record IncomeStatements(
        [property: MaxLength(3)]
        [property: JsonProperty(PropertyName = "currency_symbol")] string CurrencySymbol)
    {
        /// <summary>
        /// Gets the income statement data
        /// </summary>
        public Dictionary<string, IncomeStatement>? Quarterly { get; init; }

        /// <summary>
        /// Gets the yearly income statement data
        /// </summary>
        public Dictionary<string, IncomeStatement>? Yearly { get; init; }
    }

    /// <summary>
    /// Income Statement POCO
    /// </summary>
    public record IncomeStatement(
            DateTimeOffset? Date,
            [property: JsonProperty(PropertyName = "filing_date")] DateTimeOffset? FilingDate,
            [property: MaxLength(3)]
            [property: JsonProperty(PropertyName = "currency_symbol")] string CurrencySymbol,
            decimal? ResearchDevelopment,
            decimal? EffectOfAccountingCharges,
            decimal? IncomeBeforeTax,
            decimal? MinorityInterest,
            decimal? NetIncome,
            decimal? SellingGeneralAdministrative,
            decimal? SellingAndMarketingExpenses,
            decimal? GrossProfit,
            decimal? ReconciledDepreciation,
            decimal? Ebit,
            decimal? Ebitda,
            decimal? DepreciationAndAmortization,
            decimal? NonOperatingIncomeNetOther,
            decimal? OperatingIncome,
            decimal? OtherOperatingExpenses,
            decimal? InterestExpense,
            decimal? TaxProvision,
            decimal? InterestIncome,
            decimal? NetInterestIncome,
            decimal? ExtraordinaryItems,
            decimal? NonRecurring,
            decimal? OtherItems,
            decimal? IncomeTaxExpense,
            decimal? TotalRevenue,
            decimal? TotalOperatingExpenses,
            decimal? CostOfRevenue,
            decimal? TotalOtherIncomeExpenseNet,
            decimal? DiscontinuedOperations,
            decimal? NetIncomeFromContinuingOps,
            decimal? NetIncomeApplicableToCommonShares,
            decimal? PreferredStockAndOtherAdjustments)
    {
    }

    /// <summary>
    /// Fundalmental\Valuation POCO
    /// </summary>
    public record Valuation(
        double? TrailingPE,
        double? ForwardPE,
        double? PriceSalesTTM,
        double? PriceBookMRQ,
        decimal? EnterpriseValue,
        decimal? EnterpriseValueRevenue,
        decimal? EnterpriseValueEbitda)
    {
    }

    /// <summary>
    /// Fundalmental\SharesStats POCO
    /// </summary>
    public record SharesStats(
        double? SharesOutstanding,
        double? SharesFloat,
        double? PercentInsiders,
        double? PercentInstitutions,
        double? SharesShort,
        double? SharesShortPriorMonth,
        double? ShortRatio,
        double? ShortPercentOutstanding,
        double? ShortPercentFloat)
    {
    }

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

    /// <summary>
    /// Fundalmental\Holders POCO
    /// </summary>
    public record Holders()
    {
        /// <summary>
        /// Gets the Institution Holders
        /// </summary>
        public Dictionary<int, Holder>? Institutions { get; init; }

        /// <summary>
        /// Gets the Fund Holders
        /// </summary>
        public Dictionary<int, Holder>? Funds { get; init; }
    }

    /// <summary>
    /// Fundalmental\AnalystRatings POCO
    /// </summary>
    public record Holder(
        [property: MaxLength(100)] string Name,
        DateTimeOffset? Date,
        double? TotalShares,
        double? TotalAssets,
        int? CurrentShares,
        int? Change,
        [property: JsonProperty(PropertyName = "Change_p")] double? ChangePercent)
    {
    }

    /// <summary>
    /// Fundalmental\SplitsDividends POCO
    /// </summary>
    public record SplitsDividends(
        double? ForwardAnnualDividendRate,
        double? ForwardAnnualDividendYield,
        double? PayoutRatio,
        DateTimeOffset? DividendDate,
        DateTimeOffset? ExDividendDate,
        [property: MaxLength(10)] string LastSplitFactor,
        DateTimeOffset? LastSplitDate,
        [property: JsonProperty(PropertyName = "Change_p")] double? ChangePercent)
    {
        /// <summary>
        /// Gets the DividendsByYear
        /// </summary>
        public Dictionary<int, DividendsByYear>? NumberDividendsByYear { get; init; }
    }

    /// <summary>
    /// DividendsByYear POCO
    /// </summary>
    public record DividendsByYear(
        short? Year,
        short? Count)
    {
    }

    /// <summary>
    /// Insider Transaction POCO
    /// </summary>
    public record InsiderTransaction(
        DateTimeOffset? Date,
        [property: MaxLength(100)] string OwnerCik,
        [property: MaxLength(100)] string OwnerName,
        DateTimeOffset? TransactionDate,
        char? TransactionCode,
        decimal? TransactionAmount,
        decimal? TransactionPrice,
        char? TransactionAcquiredDisposed,
        decimal? PostTransactionAmount,
        [property: MaxLength(500)] string SecLink)
    {
    }

    /// <summary>
    /// Fundalmental\ESGScores POCO
    /// </summary>
    public record EsgScores(
        [property: NotMapped]
        string Disclaimer,
        DateTimeOffset? RatingDate,
        double? TotalEsg,
        double? TotalEsgPercentile,
        double? EnvironmentScore,
        double? EnvironmentScorePercentile,
        double? SocialScore,
        double? SocialScorePercentile,
        double? GovernanceScore,
        double? GovernanceScorePercentile,
        double? ControversyLevel)
    {
        /// <summary>
        /// Gets the Activities Involvement
        /// </summary>
        public Dictionary<int, ActivitiesInvolvement>? ActivitiesInvolvement { get; init; }
    }

    /// <summary>
    /// ActivitiesInvolvement POCO
    /// </summary>
    public record ActivitiesInvolvement(
        ActivityType Activity,
        [property: MaxLength(3)] string Involvement)
    {
    }

    /// <summary>
    /// Fundalmental\OutstandingShares POCO
    /// </summary>
    public record OutstandingShares()
    {
        /// <summary>
        /// Gets the quarterly cash flow data
        /// </summary>
        public Dictionary<int, Outstanding>? Annual { get; init; }

        /// <summary>
        /// Gets the yearly cash flow data
        /// </summary>
        public Dictionary<int, Outstanding>? Quarterly { get; init; }
    }

    /// <summary>
    /// Outstanding POCO
    /// </summary>
    public record Outstanding(
        [property: MaxLength(7)] string Date,
        [property: MaxLength(10)] string DateFormatted,
        double? SharesMln,
        long Shares)
    {
    }
}