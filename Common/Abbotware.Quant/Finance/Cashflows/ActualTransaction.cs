// -----------------------------------------------------------------------
// <copyright file="ActualTransaction.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Cashflows
{
    using System;

    /// <summary>
    /// Represents a realworld cashflow transaction with a specific date and amount
    /// </summary>
    /// <param name="Date">date</param>
    /// <param name="Amount">amount</param>
    public record ActualTransaction(DateTimeOffset Date, decimal Amount) : Transaction<DateTimeOffset, decimal>(Date, Amount);
}
