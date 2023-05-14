// -----------------------------------------------------------------------
// <copyright file="LongPayoff.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Payoffs.Plugins
{
    /// <summary>
    /// Long Position Payoff
    /// </summary>
    /// <param name="Strike">strike price</param>
    public record class LongPayoff(decimal Strike) : StrikePayoff(Strike)
    {
        /// <inheritdoc/>
        public override decimal Compute(decimal spot)
        {
            return spot - this.Strike;
        }
    }
}
