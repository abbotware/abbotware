// -----------------------------------------------------------------------
// <copyright file="BaseRate.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.InterestRates
{
    /// <summary>
    /// Yield over a given period
    /// </summary>
    /// <typeparam name="TDate">date type</typeparam>
    /// <param name="Rate">rate</param>
    /// <param name="Start">start date</param>
    /// <param name="End">end date</param>
    public record class Yield<TDate>(double Rate, TDate Start, TDate End) : BaseRate(Rate);
}
