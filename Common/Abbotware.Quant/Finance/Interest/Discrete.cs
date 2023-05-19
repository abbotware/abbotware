// -----------------------------------------------------------------------
// <copyright file="Discrete.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Finance.Interest
{
    using Abbotware.Quant.Finance.Equations;
    using Abbotware.Quant.Finance.Rates;

    /// <summary>
    /// Continuous Interest Calculator
    /// </summary>
    /// <param name="Rate">nominal (annual) interest rate</param>
    /// <param name="Periods">periods per year</param>
    public record class Discrete(NominalRate Rate, double Periods)
        : CompoundingInterest(Rate)
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Discrete"/> class.
        /// </summary>
        /// <param name="rate">nominal (annual) interest rate</param>
        /// <param name="frequency">compounding frequency</param>
        public Discrete(NominalRate rate, CompoundingFrequency frequency)
            : this(rate, (int)frequency)
        {
        }

        /// <inheritdoc/>
        public override decimal AccruedAmount(decimal principal, double t)
        {
            return principal * (decimal)CompoundingFactor.Discrete(this.Rate.Rate, this.Periods, t);
        }
    }
}
