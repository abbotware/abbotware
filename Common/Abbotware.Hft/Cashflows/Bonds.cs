// -----------------------------------------------------------------------
// <copyright file="Bonds.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Cashflows
{
    using System.Collections.Generic;
    using Abbotware.Quant.Enums;
    using Abbotware.Quant.InterestRates;

    /// <summary>
    /// Helper methods for Bond Cashflows
    /// </summary>
    public static class Bonds
    {
        /// <summary>
        /// Generates a cashflow for a bond
        /// </summary>
        /// <param name="notional">notional/face value of bond</param>
        /// <param name="maturity">matuity time in years</param>
        /// <param name="couponRate">coupon rate</param>
        /// <param name="couponFrequency">coupon frequency</param>
        /// <returns>transactions</returns>
        public static Transactions<double> Cashflow(decimal notional, double maturity, InterestRate couponRate, AccrualPeriods couponFrequency)
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
        public static decimal CouponPayment(decimal notional, InterestRate couponRate, AccrualPeriods couponFrequency)
        {
            var ratePerPeroid = couponRate.Rate / (ushort)couponFrequency;
            var payment = (decimal)ratePerPeroid * notional;
            return payment;
        }
    }
}
