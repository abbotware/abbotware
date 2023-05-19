namespace Abbotware.Quant.Finance.Interest
{
    using Abbotware.Quant.Equations;
    using Abbotware.Quant.InterestRates;

    /// <summary>
    /// Continuous Interest Calculator
    /// </summary>
    /// <param name="Rate">nominal (annual) interest rate</param>
    public record class Continuous(NominalRate Rate) : BaseCompounding(Rate)
    {
        /// <inheritdoc/>
        public override decimal AccruedAmount(decimal principal, double t)
        {
            return principal * (decimal)CompoundingFactor.Continuous(this.Rate.Rate, t);
        }
    }
}
