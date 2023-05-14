// -----------------------------------------------------------------------
// <copyright file="Equations.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant
{
    using System;
    using Abbotware.Core.Math;
    using Abbotware.Quant.Enums;
    using Abbotware.Quant.InterestRates;

    /// <summary>
    /// Wrapper class for Quantitatve equations
    /// </summary>
    public static class Equations
    {
        /// <summary>
        /// Interest Rate related equations
        /// </summary>
        public static class InterestRates
        {
            public static double ConvertContinousToPeriodic(double yearlyRate, double periodsPerYear)
            {
                return periodsPerYear * (Math.Exp(yearlyRate / periodsPerYear) - 1);
            }

            public static double ConvertPeriodicToContinuous(double yearlyRate, double periodsPerYear)
            {
                return periodsPerYear * Math.Log(1 + (yearlyRate / periodsPerYear));
            }

            public static double ConvertPeriodicToPeriodic(double yearlyRate, double periodsPerYearSource, double periodsPerYearTarget)
            {
                return periodsPerYearTarget * (Math.Pow(1 + (yearlyRate / periodsPerYearSource), periodsPerYearSource / periodsPerYearTarget) - 1);
            }

            public static double ConvertPeriodicToPeriodicAlt(double yearlyRate, double periodsPerYearSource, double periodsPerYearTarget)
            {
                return ConvertContinousToPeriodic(ConvertPeriodicToContinuous(yearlyRate, periodsPerYearSource), periodsPerYearTarget);
            }

            public static decimal DiscountFactor(InterestRate rate, double peroids)
            {
                return Compound(1m, new(-rate.Rate, rate.Periods), peroids);
            }

            public static decimal Compound(decimal amount, InterestRate rate, double peroids)
            {
                if (rate.Periods == AccrualPeriods.Continuous)
                {
                    return amount * (decimal)Math.Exp(rate.Rate * peroids);
                }
                else
                {
                    return amount * (decimal)Math.Pow(1 + (rate.Rate / (double)rate.Periods), peroids);
                }
            }

            public static double ForwardRate(InterestRate firstRate, double firstPeriod, InterestRate secondRate, double secondPeriod)
            {
                if (firstPeriod >= secondPeriod)
                {
                    throw new ArgumentException();
                }

                return ((secondRate.Rate * secondPeriod) - (firstRate.Rate * firstPeriod)) / (secondPeriod - firstPeriod);
            }
        }
    }
}
