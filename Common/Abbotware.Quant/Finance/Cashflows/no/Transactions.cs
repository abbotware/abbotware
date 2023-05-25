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
    /// Collection of Cashflow Transactions
    /// </summary>
    /// <typeparam name="TDate">type for date</typeparam>
    /// <typeparam name="TAmount">type for amount</typeparam>
    /// <param name="Entries">cashflow entries</param>
    public record Transactions<TDate, TAmount>(IEnumerable<Transaction<TDate, TAmount>> Entries) : IEnumerable<Transaction<TDate, TAmount>>
    {
        /// <inheritdoc/>
        public IEnumerator<Transaction<TDate, TAmount>> GetEnumerator() => this.Entries.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this.Entries).GetEnumerator();
    }
}
