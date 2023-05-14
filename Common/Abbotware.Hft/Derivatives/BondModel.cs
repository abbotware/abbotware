// -----------------------------------------------------------------------
// <copyright file="BondModel.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Derivatives
{
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
    public record BondModel(double Maturity, InterestRate CouponRate, AccrualPeriods CouponFrequency) : DerivativeModel
    {
        /// <summary>
        /// gets the Notional / Face Value
        /// </summary>
        public decimal Notional { get; init; } = 100;

        /// <summary>
        /// gets the coupon payment
        /// </summary>
        public decimal CouponPayment
        {
            get
            {
                return Bonds.CouponPayment(this.Notional, this.CouponRate, this.CouponFrequency);
            }
        }

        /// <summary>
        /// Determines price given a ZeroRateCurve
        /// </summary>
        /// <param name="curve">zero rate curve</param>
        /// <returns>price</returns>
        public decimal Price(ZeroRateCurve curve)
        {
            var cashflow = this.Cashflow();
            var price = 0m;

            foreach (var c in cashflow)
            {
                var zeroRate = curve.Lookup(c.Date);
                price += c.Amount * Equations.InterestRates.DiscountFactor(zeroRate, c.Date);
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
                price += c.Amount * Equations.InterestRates.DiscountFactor(yield, c.Date);
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
        public InterestRate? ParYield(ZeroRateCurve curve)
        {
            return this.YieldFromPrice(100);
        }

        /// <summary>
        /// Gets the bond cashflow
        /// </summary>
        /// <returns>cashflow for bond</returns>
        public Transactions<double> Cashflow()
        {
            return Bonds.Cashflow(this.Notional, this.Maturity,  this.CouponRate, this.CouponFrequency);
        }
    }
}
