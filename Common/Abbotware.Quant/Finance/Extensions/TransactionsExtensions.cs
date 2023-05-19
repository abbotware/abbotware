// -----------------------------------------------------------------------
// <copyright file="TransactionsExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Extensions
{
    using System.Diagnostics;
    using System;
    using System.Linq;
    using Abbotware.Quant.Cashflows;
    using Abbotware.Quant.Finance.Equations;
    using Abbotware.Quant.InterestRates;
    using Abbotware.Quant.Rates.Plugins;
    using Abbotware.Quant.Solvers;
    using Abbotware.Core.Math;

    /// <summary>
    /// Transactions extension methods
    /// </summary>
    public static class TransactionsExtensions
    {
        /// <summary>
        /// Discounts the Transactions according to the risk free rate function
        /// </summary>
        /// <param name="transactions">transactions</param>
        /// <param name="riskFreeRate">risk-free rate</param>
        /// <returns>discounted cash flow</returns>
        public static Transactions<double> AsDiscounted(this Transactions<double> transactions, IRiskFreeRate<double> riskFreeRate)
        {
            var t = transactions.Select(x => x with { Amount = x.Amount * (decimal)DiscountFactor.Continuous(riskFreeRate.Nearest(x.Date), x.Date) });

            return new Transactions<double>(t);
        }


        public static decimal NetPresentValue(this Transactions<double> transactions, IRiskFreeRate<double> riskFreeRate)
        {
            var price = 0m;

            foreach (var c in transactions)
            {
                var zeroRate = riskFreeRate.Nearest(c.Date);
                price += c.Amount * (decimal)DiscountFactor.Continuous(zeroRate, c.Date);
            }

            return price;
        }

        public static double InternalRateOfReturn(this Transactions<double> transactions, decimal target, double t0)
        {
            var range = new Interval<double>(-10, 10);

            var rate = Bisection.Solve(x => (double)transactions.NetPresentValue(new ConstantRiskFreeRate<double>(x)), range, (double)target, .0001);

            if (rate is null)
            {
                throw new InvalidOperationException();
            }

            return rate.Value;
        }
    }
}
