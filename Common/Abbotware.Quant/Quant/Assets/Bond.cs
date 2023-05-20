// -----------------------------------------------------------------------
// <copyright file="Bond.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Assets
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Abbotware.Core.Math;
    using Abbotware.Quant.Cashflows;
    using Abbotware.Quant.Extensions;
    using Abbotware.Quant.Finance.Equations;
    using Abbotware.Quant.Finance.Rates;
    using Abbotware.Quant.InterestRates;
    using Abbotware.Quant.Periodic;

    /// <summary>
    /// Bond Model
    /// </summary>
    /// <param name="Maturity">maturity in years</param>
    /// <param name="CouponRate">coupon rate</param>
    /// <param name="CouponFrequency">coupon frequency</param>
    public record Bond(double Maturity, NominalRate CouponRate, BaseTimePeriod<double> CouponFrequency) : Asset<double>(Maturity)
    {
        /// <summary>
        /// place holder used a par value
        /// </summary>
        public const decimal Par = 100m;

        /// <summary>
        /// gets the Notional (Face Value)
        /// </summary>
        public decimal Notional { get; init; } = Par;

        /// <summary>
        /// Gets the range of t0 to tM
        /// </summary>
        public Interval<double> TZeroToMaturity => new Interval<double>(0, this.Maturity);

        /// <summary>
        /// gets the coupon payment
        /// </summary>
        public decimal CouponAmount => CouponPayment(this.Notional, this.CouponRate, this.CouponFrequency);

        /// <summary>
        /// Computes the time range to maturity
        /// </summary>
        /// <param name="t0">start time to use</param>
        /// <returns>t0 - Maturity range</returns>
        public Interval<double> ToMaturity(double t0) => new Interval<double>(t0, this.Maturity);

        /// <summary>
        /// Determines price given a ZeroRateCurve
        /// </summary>
        /// <param name="zeroRateCurve">zero rate curve</param>
        /// <returns>price</returns>
        public decimal Price(IRiskFreeRate<double> zeroRateCurve)
            => this.Price(zeroRateCurve, this.TZeroToMaturity);

        /// <summary>
        /// Determines price given a ZeroRateCurve
        /// </summary>
        /// <param name="zeroRateCurve">zero rate curve</param>
        /// <param name="t0">start time to use</param>
        /// <returns>price</returns>
        public decimal Price(IRiskFreeRate<double> zeroRateCurve, double t0)
            => this.Price(zeroRateCurve, this.ToMaturity(t0));

        /// <summary>
        /// Determines price given a ZeroRateCurve
        /// </summary>
        /// <param name="zeroRateCurve">zero rate curve</param>
        /// <param name="t">start-end time range</param>
        /// <returns>price</returns>
        public decimal Price(IRiskFreeRate<double> zeroRateCurve, Interval<double> t)
            => (decimal)this.Cashflow(t).ForComputation().NetPresentValue(zeroRateCurve);

        /// <summary>
        /// Determines yield for a given price
        /// </summary>
        /// <param name="price">target price</param>
        /// <returns>yield</returns>
        public Yield<double> YieldToMaturity(decimal price)
            => this.Yield(price, this.TZeroToMaturity);

        /// <summary>
        /// Determines yield for a given price
        /// </summary>
        /// <param name="price">target price</param>
        /// <param name="t0">start time to use other than 0</param>
        /// <returns>yield</returns>
        public Yield<double> Yield(decimal price, double t0)
            => this.Yield(price, this.ToMaturity(t0));

        /// <summary>
        /// Determines yield for a given price
        /// </summary>
        /// <param name="price">target price</param>
        /// <param name="t">start time to use other than 0</param>
        /// <returns>yield</returns>
        public Yield<double> Yield(decimal price, Interval<double> t)
        {
            if (this.CouponRate.Rate == 0)
            {
                ////Functions.Root()  
            }

            var rate = this.Cashflow(t).ForComputation().InternalRateOfReturn(price);

            return new(new ContinuousRate(rate), t);

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Compute the par yield
        /// </summary>
        /// <param name="curve">zero rate curve</param>
        /// <returns>yield</returns>
        public Yield<double> ParYield(IRiskFreeRate<double> curve)
        {
            return this.ParYield(curve, this.TZeroToMaturity);
        }

        /// <summary>
        /// Determines yield for a given price
        /// </summary>
        /// <param name="curve">zero rate curve</param>>
        /// <param name="t0">start time to use other than 0</param>
        /// <returns>yield</returns>
        public Yield<double> ParYield(IRiskFreeRate<double> curve, double t0)
            => this.ParYield(curve, this.ToMaturity(t0));

        /// <summary>
        /// Determines yield for a given price
        /// </summary>
        /// <param name="curve">zero rate curve</param>>
        /// <param name="t">start time to use other than 0</param>
        /// <returns>yield</returns>
        public Yield<double> ParYield(IRiskFreeRate<double> curve, Interval<double> t)
            => ParYield(t, this.CouponFrequency, curve);

        /// <summary>
        /// Gets the bond cashflow
        /// </summary>
        /// <returns>cashflow for bond</returns>
        public TheoreticalTransactions Cashflow()
            => this.Cashflow(this.TZeroToMaturity);

        /// <summary>
        /// Gets the bond cashflow
        /// </summary>
        /// <param name="t0">start time to use other than 0</param>
        /// <returns>cashflow for bond</returns>
        public TheoreticalTransactions Cashflow(double t0)
            => this.Cashflow(this.ToMaturity(t0));

        /// <summary>
        /// Gets the bond cashflow
        /// </summary>
        /// <param name="t">start-end time range</param>
        /// <returns>cashflow for bond</returns>
        public TheoreticalTransactions Cashflow(Interval<double> t)
            => Cashflow(t, this.Notional, this.CouponRate, this.CouponFrequency);

        /// <summary>
        /// calculates the Macaulay duration of the bond
        /// </summary>
        /// <param name="price">price to use</param>
        /// <returns>duration for bond</returns>
        public double MacaulayDuration(decimal price)
            => this.MacaulayDuration(price, this.TZeroToMaturity);

        /// <summary>
        /// calculates the Macaulay duration of the bond
        /// </summary>
        /// <param name="price">price to use</param>
        /// <param name="yieldToMaturity">yield to maturity to use</param>
        /// <returns>duration for bond</returns>
        public double MacaulayDuration(decimal price, Yield<double> yieldToMaturity)
            => this.MacaulayDuration(price, yieldToMaturity, this.TZeroToMaturity);

        /// <summary>
        /// calculates the Macaulay duration of the bond
        /// </summary>
        /// <param name="price">price to use</param>
        /// <param name="t0">start time to use other than 0</param>
        /// <returns>cashflow for bond</returns>
        public double MacaulayDuration(decimal price, double t0)
            => this.MacaulayDuration(price, this.ToMaturity(t0));

        /// <summary>
        /// calculates the Macaulay duration of the bond
        /// </summary>
        /// <param name="price">price to use</param>
        /// <param name="t">start-end time range</param>
        /// <returns>cashflow for bond</returns>
        public double MacaulayDuration(decimal price, Interval<double> t)
             => this.MacaulayDuration(price, this.Yield(price, t), t);

        /// <summary>
        /// calculates the Macaulay duration of the bond
        /// </summary>
        /// <param name="price">price to use</param>
        /// <param name="yield">yield to use</param>
        /// <param name="t">start-end time range</param>
        /// <returns>cashflow for bond</returns>
        public double MacaulayDuration(decimal price, Yield<double> yield, Interval<double> t)
        {
            if (yield.TimeRange != t)
            {
                throw new ArgumentException("Yield time range does not match");
            }

            var df = this.Cashflow(t).ForComputation().AsDiscounted(yield.Rate.AsContinuous()).ToList();

            var tw = df.AsTimeWeighted();

            var d = tw.Sum(x=> x.Amount) / (double)price;

            return d;
        }

        /// <summary>
        /// Generates a cashflow for a bond
        /// </summary>
        /// <param name="t">start-end time range</param>
        /// <param name="couponFrequency">coupon frequency</param>
        /// <param name="zeroRateCurve">zero rate curve</param>
        /// <returns>transactions</returns>
        public static Yield<double> ParYield(Interval<double> t, BaseTimePeriod<double> couponFrequency, IRiskFreeRate<double> zeroRateCurve)
        {
            var timePoints = couponFrequency.GetPeriods(t).ToList();

            var discountFactors = timePoints.Select(x => (t: x, df: DiscountFactor.Continuous(zeroRateCurve.Nearest(x), x))).ToList();

            var (tM, df) = discountFactors.OrderByDescending(x => x.t).First();

            Debug.Assert(tM == t.Upper, "last time point is not maturity");

            var rightSide = 1d - df;

            rightSide /= discountFactors.Sum(x => x.df);

            rightSide *= couponFrequency.UnitsPerPeriod();

            return new(new ContinuousRate(rightSide), t);
        }

        /// <summary>
        /// Generates a cashflow for a bond
        /// </summary>
        /// <param name="t">start-end time range</param>
        /// <param name="notional">notional/face value of bond</param>
        /// <param name="couponRate">coupon rate</param>
        /// <param name="couponFrequency">coupon frequency</param>
        /// <returns>transactions</returns>
        public static TheoreticalTransactions Cashflow(Interval<double> t, decimal notional, NominalRate couponRate, BaseTimePeriod<double> couponFrequency)
        {
            var periods = couponFrequency.GetPeriods(t).ToList();

            var cashflow = new List<TheoreticalTransaction>(periods.Count);
            var coupon = CouponPayment(notional, couponRate, couponFrequency);

            foreach (var i in periods)
            {
                if (i == t.Upper)
                {
                    cashflow.Add(new(i, notional + coupon));
                }
                else
                {
                    cashflow.Add(new(i, coupon));
                }
            }

            return new(cashflow);
        }

        /// <summary>
        /// Computes a coupon payment
        /// </summary>
        /// <param name="notional">notional/face value of bond</param>
        /// <param name="couponRate">coupon rate</param>
        /// <param name="couponFrequency">coupon frequency</param>
        /// <returns>coupon payment</returns>
        public static decimal CouponPayment(decimal notional, NominalRate couponRate, BaseTimePeriod<double> couponFrequency)
        {
            var ratePerPeroid = couponFrequency.RateForPeriod(couponRate);
            var payment = (decimal)ratePerPeroid * notional;
            return payment;
        }
    }
}
