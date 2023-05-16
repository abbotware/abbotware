// -----------------------------------------------------------------------
// <copyright file="InterestRateEquations.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant
{
    using System;
    using Abbotware.Quant.Enums;
    using Abbotware.Quant.InterestRates;
    using Abbotware.Quant.Rates;

    /// <summary>
    /// Interest Rate related equations
    /// </summary>
    public static class InterestRateEquations
    {
        /// <summary>
        /// Convert a Continuous Rate to a periodic
        /// </summary>
        /// <param name="yearlyRate">continuous yearly rate</param>
        /// <param name="periodsPerYear">periods per year</param>
        /// <returns>perodic rate</returns>
        public static double ConvertContinousToPeriodic(double yearlyRate, double periodsPerYear)
        {
            return periodsPerYear * (Math.Exp(yearlyRate / periodsPerYear) - 1);
        }

        /// <summary>
        /// Convert a periodic Rate to a Continuous Rate
        /// </summary>
        /// <param name="yearlyRate">peroidic yearly rate</param>
        /// <param name="periodsPerYear">periods per year</param>
        /// <returns>continuous rate</returns>
        public static double ConvertPeriodicToContinuous(double yearlyRate, double periodsPerYear)
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
        public static double ConvertPeriodicToPeriodic(double yearlyRate, double periodsPerYearSource, double periodsPerYearTarget)
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
        public static double ConvertPeriodicToPeriodicAlt(double yearlyRate, double periodsPerYearSource, double periodsPerYearTarget)
        {
            return ConvertContinousToPeriodic(ConvertPeriodicToContinuous(yearlyRate, periodsPerYearSource), periodsPerYearTarget);
        }

        /// <summary>
        /// Compound Interest
        /// </summary>
        /// <param name="principal">principal</param>
        /// <param name="rate">Interest Rate</param>
        /// <param name="periods">periods to apply interest</param>
        /// <returns>compounded interest</returns>
        public static decimal Compound(decimal principal, InterestRate rate, double periods)
        {
            if (rate.CompoundingFrequency == CompoundingFrequency.Continuous)
            {
                return principal * (decimal)Math.Exp(rate.AnnualPercentageRate * periods);
            }
            else
            {
                return principal * (decimal)Math.Pow(1 + (rate.AnnualPercentageRate / (double)rate.CompoundingFrequency), periods);
            }
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
        public static double ForwardRate(InterestRate firstRate, double firstPeriod, InterestRate secondRate, double secondPeriod)
        {
            if (firstPeriod >= secondPeriod)
            {
                throw new ArgumentException($"firstPeriod >= secondPeriod");
            }

            return ((secondRate.AnnualPercentageRate * secondPeriod) - (firstRate.AnnualPercentageRate * firstPeriod)) / (secondPeriod - firstPeriod);
        }
    }
}
