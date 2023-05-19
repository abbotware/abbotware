namespace Abbotware.Quant.Finance.Interest
{
    using Abbotware.Quant.InterestRates;

    /// <summary>
    /// Base for an InterestCalculator
    /// </summary>
    /// <param name="Rate">nominal (annual) interest rate</param>
    public abstract record class InterestCalculator(NominalRate Rate) : IInterestCalculator
    {
        /// <inheritdoc/>
        public abstract decimal Interest(decimal principal, double t);

        /// <inheritdoc/>
        public abstract decimal AccruedAmount(decimal principal, double t);
    }

}
