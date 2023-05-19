namespace Abbotware.Quant.Finance.Interest
{
    using System;
    using Abbotware.Quant.InterestRates;

    /// <summary>
    /// Continuous Interest Calculator
    /// </summary>
    /// <param name="Rate">nominal (annual) interest rate</param>
    /// <param name="Periods">periods per year</param>
    public record class Discrete(NominalRate Rate, double Periods)
        : BaseCompounding(Rate)
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
            return principal * (decimal)Math.Pow(1 + (this.Rate.Rate / this.Periods), this.Periods * t);
        }
    }
}
