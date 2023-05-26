// -----------------------------------------------------------------------
// <copyright file="DiscountFactor.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Finance.Equations
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Computes the Discount Factor
    /// </summary>
    public static class DiscountFactor
    {
        /// <summary>
        /// Computes the continuous discount factor
        /// </summary>
        /// <param name="r">rate per unit of time</param>
        /// <param name="t">unit(s) of time</param>
        /// <returns>discount factor</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Continuous(double r, double t)
            => Math.Exp(-r * t);

        /// <summary>
        /// Computes the discretized discount factor
        /// </summary>
        /// <param name="r">rate for the peroid</param>
        /// <param name="n">number of periods</param>
        /// <returns>discount factor</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Discrete(double r, double n) 
            => Math.Pow(1 + r, -n);

        /// <summary>
        /// Computes the discretized discount factor
        /// </summary>
        /// <param name="r">rate per unit of time</param>
        /// <param name="n">number of compounding periods per unit of time</param>
        /// <param name="t">unit(s) of time</param>
        /// <returns>discount factor</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Discrete(double r, double n, double t)
            => Discrete(r / n, n * t);
    }
}