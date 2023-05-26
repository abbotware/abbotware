// -----------------------------------------------------------------------
// <copyright file="TheoreticalTransaction.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Cashflows
{
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
        public ComputationalTransaction AsComputational() => new ComputationalTransaction(this.Date, (double)this.Amount);
    }
}
