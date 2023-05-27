// -----------------------------------------------------------------------
// <copyright file="ContinuousRate.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

using Abbotware.Quant.Finance.Equations;

namespace Abbotware.Quant.Finance.Rates
{
    /// <summary>
    /// Annual Rate that is continuously compounded
    /// </summary>
    /// <param name="Rate">r = R/100</param>
    public record class ContinuousRate(double Rate) : BaseRate(Rate)
    {
        /// <inheritdoc/>
        public override double RatePerPeriod => this.Rate;

        /// <inheritdoc/>
        public override double PeriodsPerYear => 1;

        /// <inheritdoc/>
        public override bool IsContinuous => true;

        /// <inheritdoc/>
        public override ContinuousRate AsContinuous() => this;

        /// <inheritdoc/>
        public override CompoundingRate AsPeriodic(double periodsPerYear)
        {
            var newR = InterestRate.ContinousToPeriodic(this.Rate, periodsPerYear);

            return new(newR, periodsPerYear);
        }
    }
}
