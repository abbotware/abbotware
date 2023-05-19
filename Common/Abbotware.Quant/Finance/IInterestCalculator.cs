// -----------------------------------------------------------------------
// <copyright file="IInterestCalculator.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Finance
{
    /// <summary>
    /// Interface for calculating interest
    /// </summary>
    public interface IInterestCalculator
    {
        /// <summary>
        /// Computes the interest on the principal over the period of time t
        /// </summary>
        /// <param name="principal">principal</param>
        /// <param name="t">time period</param>
        /// <returns>just the interest</returns>
        decimal Interest(decimal principal, double t);

        /// <summary>
        /// Computes the accrued amount (principal + interest) over the period of time t
        /// </summary>
        /// <param name="principal">principal</param>
        /// <param name="t">time period</param>
        /// <returns>principal + interest</returns>
        decimal AccruedAmount(decimal principal, double t);
    }
}