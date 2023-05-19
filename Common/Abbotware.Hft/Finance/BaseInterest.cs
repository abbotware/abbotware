namespace Abbotware.Quant.Finance
{
    using Abbotware.Quant.InterestRates;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Rate">nominal (annual) interest rate</param>
    public abstract record class BaseInterest(NominalRate Rate) : IInterestCalculator
    {
        /// <inheritdoc/>
        public abstract decimal Interest(decimal principal, double t);

        /// <inheritdoc/>
        public abstract decimal AccruedAmount(decimal principal, double t);
    }

}
