// -----------------------------------------------------------------------
// <copyright file="Accrual.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Rates
{
    /// <summary>
    /// Accrual information
    /// </summary>
    public abstract record Accrual
    {
        /// <summary>
        /// Compute the rate per period
        /// </summary>
        /// <param name="rate">rate</param>
        /// <returns>rate per period</returns>
        public abstract double RatePerPeriod(double rate);
    }
}
