// -----------------------------------------------------------------------
// <copyright file="InterestRate.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Finance.Equations
{
    using System;
    using Abbotware.Quant.Finance.Rates;

    /// <summary>
    /// Interest Rate related equations
    /// </summary>
    public static class InterestRate
    {
        /// <summary>
        /// Convert a Continuous Rate to a periodic
        /// </summary>
        /// <param name="yearlyRate">continuous yearly rate</param>
        /// <param name="periodsPerYear">periods per year</param>
        /// <returns>perodic rate</returns>
        public static double ContinousToPeriodic(double yearlyRate, double periodsPerYear)
        {
            return periodsPerYear * (Math.Exp(yearlyRate / periodsPerYear) - 1);
        }

        /// <summary>
        /// Convert a periodic Rate to a Continuous Rate
        /// </summary>
        /// <param name="yearlyRate">peroidic yearly rate</param>
        /// <param name="periodsPerYear">periods per year</param>
        /// <returns>continuous rate</returns>
        public static double PeriodicToContinuous(double yearlyRate, double periodsPerYear)
        {
            return periodsPerYear * Math.Log(1 + (yearlyRate / periodsPerYear));
        }

        /// <summary>
        /// Convert a periodic Rate to another per Continuous Rate
        /// </summary>
        /// <param name="yearlyRate">source peroidic yearly rate</param>
        /// <param name="periodsPerYearSource">source periods per year</param>
        /// <param name="periodsPerYearTarget">target periods per year</param>
        /// <returns>new perodic rate</returns>
        public static double PeriodicToPeriodic(double yearlyRate, double periodsPerYearSource, double periodsPerYearTarget)
        {
            return periodsPerYearTarget * (Math.Pow(1 + (yearlyRate / periodsPerYearSource), periodsPerYearSource / periodsPerYearTarget) - 1);
        }

        /// <summary>
        /// Convert a periodic Rate to another per Continuous Rate (Alt Method)
        /// </summary>
        /// <param name="yearlyRate">source peroidic yearly rate</param>
        /// <param name="periodsPerYearSource">source periods per year</param>
        /// <param name="periodsPerYearTarget">target periods per year</param>
        /// <returns>new perodic rate</returns>
        public static double PeriodicToPeriodicAlt(double yearlyRate, double periodsPerYearSource, double periodsPerYearTarget)
        {
            return ContinousToPeriodic(PeriodicToContinuous(yearlyRate, periodsPerYearSource), periodsPerYearTarget);
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
        public static double ForwardRate(NominalRate firstRate, double firstPeriod, NominalRate secondRate, double secondPeriod)
        {
            if (firstPeriod >= secondPeriod)
            {
                throw new ArgumentException($"firstPeriod >= secondPeriod");
            }

            return ((secondRate.Rate * secondPeriod) - (firstRate.Rate * firstPeriod)) / (secondPeriod - firstPeriod);
        }
    }
}