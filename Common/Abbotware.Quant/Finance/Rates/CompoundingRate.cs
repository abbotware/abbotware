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
    /// <param name="Periods">periods per year</param>
    public record class CompoundingRate(double Rate, double Periods) : BaseRate(Rate)
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
        public override double RatePerPeriod => this.Rate / this.Periods;

        /// <inheritdoc/>
        public override double PeriodsPerYear => this.Periods;

        /// <inheritdoc/>
        public override double AsContinuous => InterestRate.PeriodicToContinuous(this.Rate, this.Periods);
    }
}
