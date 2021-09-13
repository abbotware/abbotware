// -----------------------------------------------------------------------
// <copyright file="Financials.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using global::Newtonsoft.Json;

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
}