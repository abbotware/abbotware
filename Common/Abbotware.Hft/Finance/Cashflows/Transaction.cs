// -----------------------------------------------------------------------
// <copyright file="Transaction.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Cashflows
{
    using System;

    /// <summary>
    /// Represents a cashflow transaction
    /// </summary>
    /// <typeparam name="TDate">type for date</typeparam>
    /// <param name="Date">date value</param>
    /// <param name="Amount">transaction amount</param>
    public record Transaction<TDate>(TDate Date, decimal Amount);

    /// <summary>
    /// Represents a cashflow transaction
    /// </summary>
    /// <param name="Date">date</param>
    /// <param name="Amount">amount</param>
    public record TransactionActual(DateTimeOffset Date, decimal Amount) : Transaction<DateTimeOffset>(Date, Amount);

    /// <summary>
    /// Represents a modeled cashflow transaction
    /// </summary>
    /// <param name="Date">date</param>
    /// <param name="Amount">amount</param>
    public record TransactionModel(double Date, decimal Amount) : Transaction<double>(Date, Amount);
}
