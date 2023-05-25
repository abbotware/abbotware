// -----------------------------------------------------------------------
// <copyright file="Transactions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Cashflows
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Collection of Cashflow Transactions to be used in Computational work
    /// </summary>
    /// <param name="Entries">cashflow entries</param>
    public record TheoreticalTransactions(IEnumerable<TheoreticalTransaction> Entries) : IEnumerable<TheoreticalTransaction>
    {
        /// <inheritdoc/>
        public IEnumerator<TheoreticalTransaction> GetEnumerator() => this.Entries.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this.Entries).GetEnumerator();
    }
}
