// -----------------------------------------------------------------------
// <copyright file="BalanceSheet.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Balance Sheet POCO
    /// </summary>
    public record BalanceSheet(
           DateTimeOffset? Date,
           [property: JsonProperty(PropertyName = "filing_date")]
           DateTimeOffset? FilingDate,
           [property: JsonProperty(PropertyName = "currency_symbol")]
           [property: MaxLength(3)]
           string? CurrencySymbol,
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
}