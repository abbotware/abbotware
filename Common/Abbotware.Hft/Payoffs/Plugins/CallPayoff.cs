
namespace Abbotware.Quant.Payoffs.Plugins
{
    using System;

    /// <summary>
    /// Call Option Payoff
    /// </summary>
    /// <param name="Strike">strike price</param>
    public record class CallPayoff(decimal Strike) : LongPayoff(Strike)
    {
        /// <inheritdoc/>
        public override decimal Compute(decimal spot)
        {
            return Math.Max(base.Compute(spot), 0);
        }
    }
}
