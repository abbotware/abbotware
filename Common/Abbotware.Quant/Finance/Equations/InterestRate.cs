// -----------------------------------------------------------------------
// <copyright file="InterestRate.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Finance.Equations
{
    using System;
    using System.Runtime.CompilerServices;
    using Abbotware.Quant.Finance.Rates;

    /// <summary>
    /// Interest Rate related equations
    /// </summary>
    public static class InterestRate
    {
        /// <summary>
        /// Convert a Continuous Rate to a periodic
        /// </summary>
        /// <param name="source">source continuous yearly rate</param>
        /// <param name="targetN">target rate's number of periods per year</param>
        /// <returns>perodic rate</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ContinousToPeriodic(double source, double targetN)
        {
            return targetN * (Math.Exp(source / targetN) - 1);
        }

        /// <summary>
        /// Convert a periodic Rate to a Continuous Rate
        /// </summary>
        /// <param name="source">source periodic yearly rate</param>
        /// <param name="sourceN">source rate's number of periods per year</param>
        /// <returns>continuous rate</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double PeriodicToContinuous(double source, double sourceN)
        {
            return sourceN * Math.Log(1 + (source / sourceN));
        }

        /// <summary>
        /// Convert a periodic Rate to another per Continuous Rate
        /// </summary>
        /// <param name="source">source periodic yearly rate</param>
        /// <param name="sourceN">source rate's number of periods per year</param>
        /// <param name="targetN">target rate's number of periods per year</param>
        /// <returns>new perodic rate</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double PeriodicToPeriodic(double source, double sourceN, double targetN)
        {
            return targetN * (Math.Pow(1 + (source / sourceN), sourceN / targetN) - 1);
        }

        /// <summary>
        /// Convert a periodic Rate to another per Continuous Rate (Alt Method)
        /// </summary>
        /// <param name="source">source periodic yearly rate</param>
        /// <param name="sourceN">source rate's number of periods per year</param>
        /// <param name="targetN">target rate's number of periods per year</param>
        /// <returns>new perodic rate</returns>
        public static double PeriodicToPeriodicAlt(double source, double sourceN, double targetN)
        {
            return ContinousToPeriodic(PeriodicToContinuous(source, sourceN), targetN);
        }

        /// <summary>
        /// Compute the forward rate between 2 rates
        /// </summary>
        /// <param name="firstRate">First Interest Rate</param>
        /// <param name="firstPeriod">periods in first rate</param>
        /// <param name="secondRate">Second Interest Rate</param>
        /// <param name="secondPeriod">periods in second rate</param>
        /// <returns>computed forward rate</returns>
        /// <exception cref="ArgumentException">argument was invalid</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ForwardRate(ContinuousRate firstRate, double firstPeriod, ContinuousRate secondRate, double secondPeriod)
        {
            if (firstPeriod >= secondPeriod)
            {
                throw new ArgumentException($"firstPeriod >= secondPeriod");
            }

            return ((secondRate.Rate * secondPeriod) - (firstRate.Rate * firstPeriod)) / (secondPeriod - firstPeriod);
        }
    }
}