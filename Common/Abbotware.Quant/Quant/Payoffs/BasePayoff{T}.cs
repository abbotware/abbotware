// -----------------------------------------------------------------------
// <copyright file="BasePayoff{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Payoffs
{
    using System.Numerics;

    /// <summary>
    /// Base class for computing a payoff for a given spot price
    /// </summary>
    /// <typeparam name="T">number type</typeparam>
    public abstract record class BasePayoff<T>
        where T : INumber<T>
    {
        /// <summary>
        /// Calculates the payoff for the given spot price
        /// </summary>
        /// <param name="spot">spot price</param>
        /// <returns>computed payoff</returns>
        public abstract T Compute(T spot);
    }
}
