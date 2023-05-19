// -----------------------------------------------------------------------
// <copyright file="Yield.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.InterestRates
{
    using Abbotware.Quant.Finance.Rates;

    /// <summary>
    /// Yield over a given period
    /// </summary>
    /// <typeparam name="TDate">date type</typeparam>
    /// <param name="Rate">rate</param>
    /// <param name="Start">start date</param>
    /// <param name="End">end date</param>
    public record class Yield<TDate>(double Rate, TDate Start, TDate End) : NominalRate(Rate);
}
