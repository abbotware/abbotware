// -----------------------------------------------------------------------
// <copyright file="Yield.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Finance.Rates
{
    using System;
    using Abbotware.Core.Math;

    /// <summary>
    /// Yield over a given time period
    /// </summary>
    /// <param name="Rate">rate</param>
    /// <param name="TimePeriod">start-end time range</param>
    public record class Yield(double Rate, Interval<double> TimePeriod) : ContinuousRate(Rate)
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Yield"/> class.
        /// </summary>
        /// <param name="rate">rate</param>
        /// <param name="maturity">maturity</param>
        public Yield(double rate, double maturity)
            : this(rate, new Interval<double>(0, maturity))
        {
        }

        /// <inheritdoc/>
        public override double PeriodLength => this.TimePeriod.Upper - this.TimePeriod.Lower;

        /// <inheritdoc/>
        public override double PeriodsPerYear => 1 / this.PeriodLength;

        /// <summary>
        /// Cconverts to a ZeroRate yield
        /// </summary>
        /// <returns>ZeroRate</returns>
        /// <exception cref="InvalidOperationException">if lower is not 0</exception>
        public ZeroRate AsZeroRate()
        {
            if (this.TimePeriod.Lower != 0)
            {
                throw new InvalidOperationException($"Yield Lower Range is not 0");
            }

            return new(this.Rate, this.TimePeriod.Upper);
        }

        /// <summary>
        /// Cconverts to a Discount Rate yield
        /// </summary>
        /// <returns>ZeroRate</returns>
        /// <exception cref="InvalidOperationException">if lower is not 0</exception>
        public DiscountRate AsDiscountRate()
        {
            if (this.TimePeriod.Lower != 0)
            {
                throw new InvalidOperationException($"Yield Lower Range is not 0");
            }

            return new(this.AsYearlyContinuous().Rate);
        }
    }
}
