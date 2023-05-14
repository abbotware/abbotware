// -----------------------------------------------------------------------
// <copyright file="PutPayoff.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Payoffs.Plugins
{
    using System;

    /// <summary>
    /// Put Option Payoff
    /// </summary>
    /// <param name="Strike">strike price</param>
    public record class PutPayoff(decimal Strike) : ShortPayoff(Strike)
    {
        /// <inheritdoc/>
        public override decimal Compute(decimal spot)
        {
            return Math.Max(base.Compute(spot), 0);
        }
    }
}
