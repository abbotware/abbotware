// -----------------------------------------------------------------------
// <copyright file="CompoundingFactor.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Finance.Equations
{
    using System;

    /// <summary>
    /// Computes the Compounding Factor
    /// </summary>
    public static class CompoundingFactor
    {
        /// <summary>
        /// Computes the continous compounding factor
        /// </summary>
        /// <param name="rate">rate</param>
        /// <param name="t">time period</param>
        /// <returns>discount factor</returns>
        public static double Continuous(double rate, double t)
        {
            return Math.Exp(rate * t);
        }

        /// <summary>
        /// Computes the discretized compounding factor
        /// </summary>
        /// <param name="rate">rate</param>
        /// <param name="periods">number of time periods</param>
        /// <param name="t">time period</param>
        /// <returns>discount factor</returns>
        public static double Discrete(double rate, double periods, double t)
        {
            return Math.Pow(1 + (rate / periods), periods * t);
        }
    }
}