// -----------------------------------------------------------------------
// <copyright file="ContinuousRate.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

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
        public override double AsContinuous => this.Rate;
    }
}
