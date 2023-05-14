namespace Abbotware.Quant.Payoffs
{
    /// <summary>
    /// Base class for computing a payoff for a given Spot
    /// </summary>
    public abstract record class BasePayoff
    {
        /// <summary>
        /// Calculates the payoff for the given spot price
        /// </summary>
        /// <param name="spot">spot price</param>
        /// <returns>computed payoff</returns>
        public abstract decimal Compute(decimal spot);
    }
}
