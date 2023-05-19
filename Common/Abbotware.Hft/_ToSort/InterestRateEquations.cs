// -----------------------------------------------------------------------
// <copyright file="InterestRateEquations.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant
{
    using System;
    using Abbotware.Quant.Enums;
    using Abbotware.Quant.Finance;
    using Abbotware.Quant.Rates;

    /// <summary>
    /// Interest Rate related equations
    /// </summary>
    public static class ForwardRateEquations
    {
        /// <summary>
        /// Compute the forward rate between 2 rates
        /// </summary>
        /// <param name="firstRate">First Interest Rate</param>
        /// <param name="firstPeriod">periods in first rate</param>
        /// <param name="secondRate">Second Interest Rate</param>
        /// <param name="secondPeriod">periods in second rate</param>
        /// <returns>computed forward rate</returns>
        /// <exception cref="ArgumentException">argument was invalid</exception>
        //public static double ForwardRate(InterestRate firstRate, double firstPeriod, InterestRate secondRate, double secondPeriod)
        //{
        //    if (firstPeriod >= secondPeriod)
        //    {
        //        throw new ArgumentException($"firstPeriod >= secondPeriod");
        //    }

        //    return ((secondRate.AnnualPercentageRate * secondPeriod) - (firstRate.AnnualPercentageRate * firstPeriod)) / (secondPeriod - firstPeriod);
        //}
    }
}
