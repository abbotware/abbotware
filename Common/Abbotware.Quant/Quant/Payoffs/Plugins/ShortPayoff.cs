// -----------------------------------------------------------------------
// <copyright file="ShortPayoff.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Payoffs.Plugins
{
    /// <summary>
    /// Short Position Payoff
    /// </summary>
    /// <param name="Strike">strike price</param>
    public record class ShortPayoff(decimal Strike) : StrikePayoff(Strike)
    {
        /// <inheritdoc/>
        public override decimal Compute(decimal spot)
        {
            return this.Strike - spot;
        }
    }
}
