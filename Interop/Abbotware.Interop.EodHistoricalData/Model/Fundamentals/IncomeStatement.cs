// -----------------------------------------------------------------------
// <copyright file="IncomeStatement.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Income Statement POCO
    /// </summary>
    public record IncomeStatement(
            DateTimeOffset? Date,
            [property: JsonProperty(PropertyName = "filing_date")]
            DateTimeOffset? FilingDate,
            [property: MaxLength(3)]
            [property: JsonProperty(PropertyName = "currency_symbol")]
            string? CurrencySymbol,
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
}