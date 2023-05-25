// -----------------------------------------------------------------------
// <copyright file="ComputationalTransaction.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Cashflows
{
    /// <summary>
    /// Represents a cashflow transaction
    /// </summary>
    /// <param name="Date">date value</param>
    /// <param name="Amount">transaction amount</param>
    public record ComputationalTransaction(double Date, double Amount) : Transaction<double, double>(Date, Amount);
}
