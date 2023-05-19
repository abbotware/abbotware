namespace Abbotware.Quant.Finance
{
    using Abbotware.Quant.InterestRates;

    /// <summary>
    /// Base for Compounding Interest
    /// </summary>
    /// <param name="Rate">nominal (annual) interest rate</param>
    public abstract record class BaseCompounding(NominalRate Rate) : BaseInterest(Rate) {

        /// <inheritdoc/>
        public override decimal Interest(decimal principal, double t)
        {
            return this.AccruedAmount(principal, t) - principal;
        }
    }
}
