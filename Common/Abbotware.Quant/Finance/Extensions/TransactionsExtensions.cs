// -----------------------------------------------------------------------
// <copyright file="TransactionsExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Extensions
{
    using System.Linq;
    using Abbotware.Quant.Cashflows;
    using Abbotware.Quant.Finance.Equations;
    using Abbotware.Quant.InterestRates;

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
        public static Transactions<double> Discounted(this Transactions<double> transactions, IRiskFreeRate<double> riskFreeRate)
        {
            var t = transactions.Select(x => x with { Amount = x.Amount * (decimal)DiscountFactor.Continuous(riskFreeRate.Nearest(x.Date), x.Date) });

            return new Transactions<double>(t);
        }
    }
}
