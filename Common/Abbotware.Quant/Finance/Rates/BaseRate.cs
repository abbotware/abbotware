// -----------------------------------------------------------------------
// <copyright file="BaseRate.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Finance.Rates
{
    /// <summary>
    /// base for rate
    /// </summary>
    /// <param name="Rate">Rate in Percentage</param>
    public abstract record class BaseRate(double Rate)
    {
        /// <summary>
        /// Gets the normalized rate per period
        /// </summary>
        /// <returns>rate adjusted per period</returns>
        public abstract double RatePerPeriod { get; }

        /// <summary>
        /// Gets the number of periods per year
        /// </summary>
        /// <returns>rate adjusted per period</returns>
        public abstract double PeriodsPerYear { get; }

        /// <summary>
        /// Gets the equivalent continuous compounded rate
        /// </summary>
        /// <returns>rate adjusted per period</returns>
        public abstract ContinuousRate AsContinuous();
    }
}
