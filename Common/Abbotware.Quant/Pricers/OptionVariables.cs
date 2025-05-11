// -----------------------------------------------------------------------
// <copyright file="OptionVariables.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Pricers
{
    public record class OptionVariables(
        decimal InitialPrice,
        decimal StrikePrice,
        double Maturity,
        double Volatility,
        double RiskFreeRate,
        double DividendYield);
}