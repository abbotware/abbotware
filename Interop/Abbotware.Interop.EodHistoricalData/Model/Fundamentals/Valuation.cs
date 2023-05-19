// -----------------------------------------------------------------------
// <copyright file="Valuation.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
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
}