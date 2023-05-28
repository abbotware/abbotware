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
        /// Gets the >number of periods per year
        /// </summary>
        /// <returns>>number of periods per year</returns>
        public abstract double PeriodsPerYear { get; init; }

        /// <summary>
        /// Gets the normalized rate per period
        /// </summary>
        /// <returns>rate adjusted per period</returns>
        public virtual double RatePerPeriod => this.Rate / this.PeriodsPerYear;

        /// <summary>
        /// Gets the number of periods per year
        /// </summary>
        /// <returns>rate adjusted per period</returns>
        public virtual double PeriodLength => 1 / this.PeriodsPerYear;

        /// <summary>
        /// Gets a value indicating whether or not this is a continous rate or not
        /// </summary>
        public virtual bool IsContinuous { get; }

        /// <summary>
        /// Gets the equivalent continuous compounded rate
        /// </summary>
        /// <returns>rate as its equivalent continuous rate</returns>
        public abstract ContinuousRate AsYearlyContinuous();

        /// <summary>
        /// Gets the equivalent periodic compounded rate
        /// </summary>
        /// <param name="periodsPerYear">the number of periods per year</param>
        /// <returns>rate as its equivalent periodic compunding rate</returns>
        public abstract CompoundingRate AsYearlyPeriodic(double periodsPerYear);
    }
}
