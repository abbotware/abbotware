// -----------------------------------------------------------------------
// <copyright file="Transaction.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Cashflows
{
    using System;

    /// <summary>
    /// Represents a cashflow transaction
    /// </summary>
    /// <typeparam name="TDate">type for date</typeparam>
    /// <typeparam name="TAmount">type for amount</typeparam>
    /// <param name="Date">date value</param>
    /// <param name="Amount">transaction amount</param>
    public record Transaction<TDate, TAmount>(TDate Date, TAmount Amount);

    /// <summary>
    /// Represents a cashflow transaction
    /// </summary>
    /// <param name="Date">date value</param>
    /// <param name="Amount">transaction amount</param>
    public record ComputationalTransaction(double Date, double Amount) : Transaction<double, double>(Date, Amount);

    /// <summary>
    /// Represents a cashflow transaction
    /// </summary>
    /// <param name="Date">date</param>
    /// <param name="Amount">amount</param>
    public record ActualTransaction(DateTimeOffset Date, decimal Amount) : Transaction<DateTimeOffset, decimal>(Date, Amount);

    /// <summary>
    /// Represents a modeled cashflow transaction
    /// </summary>
    /// <param name="Date">date</param>
    /// <param name="Amount">amount</param>
    public record TheoreticalTransaction(double Date, decimal Amount) : Transaction<double, decimal>(Date, Amount)
    {
        /// <summary>
        /// convert to a computational transaction
        /// </summary>
        /// <returns>ComputationalTransaction</returns>
        public ComputationalTransaction AsComputationTransaction() => new ComputationalTransaction(this.Date, (double)this.Amount);
    }
}
