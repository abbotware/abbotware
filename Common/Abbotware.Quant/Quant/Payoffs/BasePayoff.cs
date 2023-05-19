// -----------------------------------------------------------------------
// <copyright file="BasePayoff.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Payoffs
{
    /// <summary>
    /// Base class for computing a payoff for a given spot price
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
