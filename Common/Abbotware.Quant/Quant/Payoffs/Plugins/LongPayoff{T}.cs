// -----------------------------------------------------------------------
// <copyright file="LongPayoff{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Payoffs.Plugins
{
    using System.Numerics;

    /// <summary>
    /// Long Position Payoff
    /// </summary>
    /// <param name="Strike">strike price</param>
    /// <typeparam name="T">number type</typeparam>
    public record class LongPayoff<T>(T Strike) : StrikePayoff<T>(Strike)
        where T : INumber<T>
    {
        /// <inheritdoc/>
        public override T Compute(T spot)
        {
            return spot - this.Strike;
        }
    }
}
