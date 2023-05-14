// -----------------------------------------------------------------------
// <copyright file="Bond.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Assets
{
    using System.Collections.Generic;
    using Abbotware.Core.Math;
    using Abbotware.Quant.Cashflows;
    using Abbotware.Quant.Enums;
    using Abbotware.Quant.InterestRates;
    using Abbotware.Quant.Solvers;

    /// <summary>
    /// Bond Model
    /// </summary>
    /// <param name="Maturity">maturity in years</param>
    /// <param name="CouponRate">coupon rate</param>
    /// <param name="CouponFrequency">coupon frequency</param>
    public record Bond(double Maturity, NominalRate CouponRate, TimePeriod CouponFrequency) : Asset<double>(Maturity)
    {
        /// <summary>
        /// gets the Notional / Face Value
        /// </summary>
        public decimal Notional { get; init; } = 100;

        /// <summary>
        /// gets the coupon payment
        /// </summary>
        public decimal CouponAmount
        {
            get
            {
                return CouponPayment(this.Notional, this.CouponRate, this.CouponFrequency);
            }
        }

        /// <summary>
        /// Determines price given a ZeroRateCurve
        /// </summary>
        /// <param name="curve">zero rate curve</param>
        /// <returns>price</returns>
        public decimal Price(ZeroRateCurve<double> curve)
        {
            var cashflow = this.Cashflow();
            var price = 0m;

            foreach (var c in cashflow)
            {
                var zeroRate = curve.Lookup(c.Date);
                price += c.Amount * (decimal)InterestRateEquations.DiscountFactor(zeroRate, c.Date);
            }

            return price;
        }

        /// <summary>
        /// Determines price for a given yield
        /// </summary>
        /// <param name="yield">target yield</param>
        /// <returns>price</returns>
        public decimal PriceFromYield(InterestRate yield)
        {
            var cashflow = this.Cashflow();
            var price = 0m;

            foreach (var c in cashflow)
            {
                price += c.Amount * (decimal)InterestRateEquations.DiscountFactor(yield, c.Date);
            }

            return price;
        }

        /// <summary>
        /// Determines yield for a given price
        /// </summary>
        /// <param name="price">target price</param>
        /// <returns>yield</returns>
        public InterestRate? YieldFromPrice(decimal price)
        {
            var range = new Interval<double>(-10, 10);

            var rate = Bisection.Solve(x => (double)this.PriceFromYield(InterestRate.Continuous(x)), range, (double)price, .0001);

            if (rate is not null)
            {
                return InterestRate.Continuous(rate.Value);
            }

            return null;
        }

        /// <summary>
        /// Compute the par yield
        /// </summary>
        /// <param name="curve">zero rate curve</param>
        /// <returns>yield</returns>
        public InterestRate? ParYield(ZeroRateCurve<double> curve)
        {
            return this.YieldFromPrice(100);
        }

        /// <summary>
        /// Gets the bond cashflow
        /// </summary>
        /// <returns>cashflow for bond</returns>
        public Transactions<double> Cashflow()
        {
            return Cashflow(this.Notional, this.Maturity, this.CouponRate, this.CouponFrequency);
        }

        /// <summary>
        /// Generates a cashflow for a bond
        /// </summary>
        /// <param name="notional">notional/face value of bond</param>
        /// <param name="maturity">matuity time in years</param>
        /// <param name="couponRate">coupon rate</param>
        /// <param name="couponFrequency">coupon frequency</param>
        /// <returns>transactions</returns>
        public static Transactions<double> Cashflow(decimal notional, double maturity, NominalRate couponRate, TimePeriod couponFrequency)
        {
            var coupon = CouponPayment(notional, couponRate, couponFrequency);
            var increment = 1d / (ushort)couponFrequency;

            var cashflow = new List<Transaction<double>>();

            for (var i = increment; i < maturity; i += increment)
            {
                cashflow.Add(new(i, coupon));
            }

            cashflow.Add(new(maturity, notional + coupon));

            return new(cashflow);
        }

        /// <summary>
        /// Computes a coupon payment
        /// </summary>
        /// <param name="notional">notional/face value of bond</param>
        /// <param name="couponRate">coupon rate</param>
        /// <param name="couponFrequency">coupon frequency</param>
        /// <returns>coupon payment</returns>
        public static decimal CouponPayment(decimal notional, NominalRate couponRate, TimePeriod couponFrequency)
        {
            var ratePerPeroid = couponRate.RateForPeriod(couponFrequency);
            var payment = (decimal)ratePerPeroid * notional;
            return payment;
        }
    }
}
