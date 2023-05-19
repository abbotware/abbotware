// -----------------------------------------------------------------------
// <copyright file="BaseTimePeriod.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Periodic
{
    using System;
    using System.Collections.Generic;
    using Abbotware.Quant.Finance;
    using Abbotware.Quant.Finance.Equations;
    using Abbotware.Quant.InterestRates;

    /// <summary>
    /// base for time period
    /// </summary>
    /// <typeparam name="TDate">date type</typeparam>
    public abstract record class BaseTimePeriod<TDate>()
        where TDate : notnull
    {
        /// <summary>
        /// Gets the periods
        /// </summary>
        /// <param name="start">start point</param>
        /// <param name="end">end point</param>
        /// <returns>list of time periods</returns>
        public abstract IEnumerable<TDate> GetPeriods(TDate start, TDate end);

        ///// <summary>
        ///// Gets the periods
        ///// </summary>
        ///// <param name="start">start point</param>
        ///// <param name="end">end point</param>
        ///// <returns>list of time periods</returns>
        //public abstract double Periods(TDate start, TDate end);

        /// <summary>
        /// Convert a nominal rate in terms this time period
        /// </summary>
        /// <param name="rate">nominal rate</param>
        /// <returns>converted rate</returns>
        public abstract double RateForPeriod(NominalRate rate);

        /// <summary>
        /// Convert a nominal rate in terms this time period
        /// </summary>
        /// <returns>converted rate</returns>
        public abstract double UnitsPerPeriod();
    }

    /// <summary>
    /// Simple Periodic
    /// </summary>
    /// <param name="Period">time period</param>
    /// <typeparam name="TDate">date type</typeparam>
    public record class SimplePeriodic<TDate>(TimePeriod Period) : BaseTimePeriod<TDate>
            where TDate : notnull
    {
        /// <inheritdoc/>
        public override IEnumerable<TDate> GetPeriods(TDate start, TDate end)
        {
            if (typeof(TDate) == typeof(double))
            {
                var increment = 1d / (int)this.Period;
                var current = (double)(object)start;
                var final = (double)(object)end;

                while (current <= final)
                {
                    if (current != 0)
                    {
                        yield return (TDate)(object)current;
                    }

                    current += increment;
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override double UnitsPerPeriod()
        {
            return (double)this.Period;
        }

        /// <inheritdoc/>
        public override double RateForPeriod(NominalRate rate)
        {
            var source = (double)rate.Units;
            var target = this.UnitsPerPeriod();

            if (source == target)
            {
                return rate.RatePerPeriod;
            }

            if (source == 1)
            {
                return rate.Rate / target;
            }

            return InterestRate.PeriodicToPeriodic(rate.Rate, source, target);
        }
    }
}