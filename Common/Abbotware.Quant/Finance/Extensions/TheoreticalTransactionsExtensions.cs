// -----------------------------------------------------------------------
// <copyright file="TheoreticalTransactionsExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Abbotware.Quant.Cashflows;

    /// <summary>
    /// Collection of Cashflow Transactions to be used in Computational work
    /// </summary>
    public static class TheoreticalTransactionsExtensions
    {
        /// <summary>
        /// convert to computational transactions
        /// </summary>
        /// <param name="t">transactions</param>
        /// <returns>computational transactions </returns>
        public static IEnumerable<ComputationalTransaction> ForComputation(this IEnumerable<TheoreticalTransaction> t)
        {
            return t.Select(x => x.AsComputational());
        }
    }
}