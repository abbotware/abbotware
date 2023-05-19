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
    using Abbotware.Quant.Rates;
    using Abbotware.Quant.Rates.Plugins;
    using Abbotware.Quant.Solvers;

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
        /// gets the coupon payment
        /// </summary>
        public decimal CouponAmount => CouponPayment(this.Notional, this.CouponRate, this.CouponFrequency);

        /// <summary>
        /// Determines price given a ZeroRateCurve
        /// </summary>
        /// <param name="zeroRateCurve">zero rate curve</param>
        /// <param name="t0">start time to use other than 0</param>
        /// <returns>price</returns>
        public decimal Price(IRiskFreeRate<double> zeroRateCurve, double t0 = 0)
        {
            return this.Cashflow(t0).NetPresentValue(zeroRateCurve);
        }

        /// <summary>
        /// Determines yield for a given price
        /// </summary>
        /// <param name="price">target price</param>
        /// <param name="t0">start time to use other than 0</param>
        /// <returns>yield</returns>
        public Yield<double> Yield(decimal price, double t0 = 0)
        {
            if (CouponRate.Rate == 0)
            {

                //Functions.Root()  
            }

            var rate = this.Cashflow().InternalRateOfReturn(price, t0);

            return new(rate, t0, this.Maturity);

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Compute the par yield
        /// </summary>
        /// <param name="curve">zero rate curve</param>
        /// <param name="t0">start time to use other than 0</param>
        /// <returns>yield</returns>
        public Yield<double> ParYield(IRiskFreeRate<double> curve, double t0 = 0)
        {
            return ParYield(t0, this.Maturity, this.CouponFrequency, curve);
        }

        /// <summary>
        /// Gets the bond cashflow
        /// </summary>
        /// <returns>cashflow for bond</returns>
        public Transactions<double> Cashflow()
        {
            return this.Cashflow(0);
        }

        /// <summary>
        /// Gets the bond cashflow
        /// </summary>
        /// <param name="t0">start time to use other than 0</param>
        /// <returns>cashflow for bond</returns>
        public Transactions<double> Cashflow(double t0)
        {
            return this.Cashflow(t0, this.Maturity);
        }

        /// <summary>
        /// Gets the bond cashflow
        /// </summary>
        /// <param name="t0">start time to use other than 0</param>
        /// <param name="tM">end time to use other than Maturity</param>
        /// <returns>cashflow for bond</returns>
        public Transactions<double> Cashflow(double t0, double tM)
        {
            return Cashflow(this.Notional, t0, tM, this.CouponRate, this.CouponFrequency);
        }

        /// <summary>
        /// Generates a cashflow for a bond
        /// </summary>
        /// <param name="t0">initial time</param>
        /// <param name="tM">matuity time in years</param>
        /// <param name="couponFrequency">coupon frequency</param>
        /// <param name="zeroRateCurve">zero rate curve</param>
        /// <returns>transactions</returns>
        public static Yield<double> ParYield(double t0, double tM, BaseTimePeriod<double> couponFrequency, IRiskFreeRate<double> zeroRateCurve)
        {
            var timePoints = couponFrequency.GetPeriods(t0, tM).ToList();

            var discountFactors = timePoints.Select(x => (t: x, df: DiscountFactor.Continuous(zeroRateCurve.Nearest(x), x))).ToList();

            var (t, df) = discountFactors.OrderByDescending(x => x.t).First();

            Debug.Assert(t == tM, "last time point is not maturity");

            var rightSide = 1m - (decimal)df;

            rightSide /= discountFactors.Sum(x => (decimal)x.df);

            rightSide *= (decimal)couponFrequency.UnitsPerPeriod();

            return new((double)rightSide, t0, tM);
        }

        /// <summary>
        /// Generates a cashflow for a bond
        /// </summary>
        /// <param name="notional">notional/face value of bond</param>
        /// <param name="t0">initial time</param>
        /// <param name="tM">matuity time in years</param>
        /// <param name="couponRate">coupon rate</param>
        /// <param name="couponFrequency">coupon frequency</param>
        /// <returns>transactions</returns>
        public static Transactions<double> Cashflow(decimal notional, double t0, double tM, NominalRate couponRate, BaseTimePeriod<double> couponFrequency)
        {
            var periods = couponFrequency.GetPeriods(t0, tM).ToList();

            var cashflow = new List<Transaction<double>>(periods.Count);
            var coupon = CouponPayment(notional, couponRate, couponFrequency);

            foreach (var i in periods)
            {
                if (i == tM)
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
