// -----------------------------------------------------------------------
// <copyright file="CompoundingRate.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Finance.Rates
{
    using Abbotware.Quant.Finance;
    using Abbotware.Quant.Finance.Equations;

    /// <summary>
    /// Annual Rate that is periodically compounded
    /// </summary>
    /// <param name="Rate">r = R/100</param>
    /// <param name="PeriodsPerYear">periods per year</param>
    public record class CompoundingRate(double Rate, double PeriodsPerYear) : BaseRate(Rate)
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompoundingRate"/> class.
        /// </summary>
        /// <param name="rate">annual rate r = R/100</param>
        /// <param name="periods">periods per year frequency</param>
        public CompoundingRate(double rate, TimePeriod periods)
            : this(rate, (int)periods)
        {
        }

        /// <inheritdoc/>
        public override ContinuousRate AsYearlyContinuous() => new(InterestRate.PeriodicToContinuous(this.Rate, this.PeriodsPerYear));

        /// <inheritdoc/>
        public override CompoundingRate AsYearlyPeriodic(double periodsPerYear)
        {
            if (this.PeriodsPerYear == periodsPerYear)
            {
                return this;
            }

            var r = InterestRate.PeriodicToPeriodic(this.Rate, this.RatePerPeriod, periodsPerYear);

            return new(r, periodsPerYear);
        }
    }
}