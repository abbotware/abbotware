// -----------------------------------------------------------------------
// <copyright file="DiscountFactor.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Finance.Equations
{
    using System;
    using Abbotware.Quant.Rates;

    /// <summary>
    /// Computes the Discount Factor
    /// </summary>
    public static class DiscountFactor
    {
        /// <summary>
        /// Computes the continous discount factor
        /// </summary>
        /// <param name="rate">rate</param>
        /// <param name="t">time period</param>
        /// <returns>discount factor</returns>
        public static double Continuous(double rate, double t)
        {
            return Math.Exp(-rate * t);
        }

        /// <summary>
        /// Computes the discretized discount factor
        /// </summary>
        /// <param name="rate">rate</param>
        /// <param name="periods">number of time periods</param>
        /// <param name="t">time period</param>
        /// <returns>discount factor</returns>
        public static double Discrete(double rate, double periods, double t)
        {
            return Math.Pow(1 + (rate / periods), -periods * t);
        }
    }
}