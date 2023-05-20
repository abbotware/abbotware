// -----------------------------------------------------------------------
// <copyright file="TransactionsExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Abbotware.Core.Math;
    using Abbotware.Quant.Cashflows;
    using Abbotware.Quant.Finance.Equations;
    using Abbotware.Quant.Finance.Rates;
    using Abbotware.Quant.InterestRates;
    using Abbotware.Quant.Rates.Plugins;
    using Abbotware.Quant.Solvers;

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
        public static ComputationalTransactions AsDiscounted(this IEnumerable<Transaction<double, double>> transactions, ContinuousRate riskFreeRate)
        {
            var t = transactions.Select(x => x with { Amount = x.Amount * DiscountFactor.Continuous(riskFreeRate, x.Date) });

            return new ComputationalTransactions(t);
        }

        /// <summary>
        /// Discounts the Transactions according to the risk free rate function
        /// </summary>
        /// <param name="transactions">transactions</param>
        /// <param name="riskFreeRate">risk-free rate</param>
        /// <returns>discounted cash flow</returns>
        public static ComputationalTransactions AsDiscounted(this IEnumerable<Transaction<double, double>> transactions, IRiskFreeRate<double> riskFreeRate)
        {
            var t = transactions.Select(x => x with { Amount = x.Amount * DiscountFactor.Continuous(riskFreeRate.Nearest(x.Date), x.Date) });

            return new ComputationalTransactions(t);
        }

        /// <summary>
        /// Discounts the Transactions according to the risk free rate function
        /// </summary>
        /// <param name="transactions">transactions</param>
        /// <returns>discounted cash flow</returns>
        public static ComputationalTransactions AsTimeWeighted(this IEnumerable<Transaction<double, double>> transactions)
        {
            var t = transactions.Select(x => x with { Amount = x.Amount * x.Date });

            return new ComputationalTransactions(t);
        }

        /// <summary>
        /// Compute the Net Present Value of the cashflow
        /// </summary>
        /// <param name="transactions">cashflow</param>
        /// <param name="riskFreeRate">risk-free rate</param>
        /// <returns>Net Present Value</returns>
        public static double NetPresentValue(this IEnumerable<Transaction<double, double>> transactions, IRiskFreeRate<double> riskFreeRate)
        {
            var price = 0d;

            foreach (var c in transactions)
            {
                var zeroRate = riskFreeRate.Nearest(c.Date);
                price += c.Amount * DiscountFactor.Continuous(zeroRate, c.Date);
            }

            return price;
        }

        /// <summary>
        /// Compute the Internal Rate of Return for the cashflow
        /// </summary>
        /// <param name="transactions">cashflow</param>
        /// <param name="target">target (if other than zero)</param>
        /// <returns>Net Present Value</returns>
        public static double InternalRateOfReturn(this IEnumerable<Transaction<double, double>> transactions, decimal target)
        {
            var range = new Interval<double>(-1, 1);

            var rate = Bisection.Solve(x => transactions.NetPresentValue(new ConstantRiskFreeRate<double>(x)), range, (double)target, .00001);

            if (rate is null)
            {
                throw new InvalidOperationException();
            }

            return rate.Value;
        }
    }
}