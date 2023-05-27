// -----------------------------------------------------------------------
// <copyright file="NominalRate.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Finance.Rates
{
    using Abbotware.Quant.Finance.Equations;

    /// <summary>
    /// Nominal Annual Rate
    /// </summary>
    /// <param name="Rate">r = R/100</param>
    public record class NominalRate(double Rate) : CompoundingRate(Rate, 1)
    {
        /// <inheritdoc/>
        public override double RatePerPeriod => this.Rate;

        /// <inheritdoc/>
        public override double PeriodsPerYear => 1;

        /// <inheritdoc/>
        public override ContinuousRate AsContinuous() => new(InterestRate.PeriodicToContinuous(this.Rate, 1));
    }
}
