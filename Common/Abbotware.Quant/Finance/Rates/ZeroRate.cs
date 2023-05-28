// -----------------------------------------------------------------------
// <copyright file="ZeroRate.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Finance.Rates
{
    using Abbotware.Core.Math;

    /// <summary>
    /// Yield over a given period
    /// </summary>
    /// <param name="Rate">rate</param>
    /// <param name="Maturity">start-end time range</param>
    public record class ZeroRate(double Rate, double Maturity) : Yield(Rate, new Interval<double>(0, Maturity))
    {
    }
}
