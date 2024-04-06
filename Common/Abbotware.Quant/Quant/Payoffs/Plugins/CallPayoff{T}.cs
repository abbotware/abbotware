// -----------------------------------------------------------------------
// <copyright file="CallPayoff{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Payoffs.Plugins
{
    using System.Numerics;

    /// <summary>
    /// Call Option Payoff
    /// </summary>
    /// <param name="Strike">strike price</param>
    /// <typeparam name="T">number type</typeparam>
    public record class CallPayoff<T>(T Strike) : LongPayoff<T>(Strike)
        where T : INumber<T>
    {
        /// <inheritdoc/>
        public override T Compute(T spot)
        {
            return T.Max(base.Compute(spot), T.Zero);
        }
    }
}
