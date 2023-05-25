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
    public record ComputationalTransactions(IEnumerable<Transaction<double, double>> Entries) : IEnumerable<Transaction<double, double>>
    {
        /// <inheritdoc/>
        public IEnumerator<Transaction<double, double>> GetEnumerator() => this.Entries.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this.Entries).GetEnumerator();
    }
}
