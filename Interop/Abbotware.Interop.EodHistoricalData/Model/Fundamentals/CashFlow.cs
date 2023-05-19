// -----------------------------------------------------------------------
// <copyright file="CashFlow.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
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
    public record CashFlow(
           DateTimeOffset? Date,
           [property: JsonProperty(PropertyName = "filing_date")]
           DateTimeOffset? FilingDate,
           [property: MaxLength(3)]
           [property: JsonProperty(PropertyName = "currency_symbol")]
           string? CurrencySymbol,
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
}