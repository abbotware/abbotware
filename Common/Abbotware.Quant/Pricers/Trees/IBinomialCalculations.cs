// -----------------------------------------------------------------------
// <copyright file="IBinomialCalculations.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Pricers.Trees
{
    /// <summary>
    /// Interface for Binomial Tree Calculations
    /// </summary>
    public interface IBinomialCalculations
    {
        /// <summary>
        /// Gets the up movement probabilty u
        /// </summary>
        double UpProbability { get; }

        /// <summary>
        /// Gets the down movement probability d  (d = 1 - u)
        /// </summary>
        double DownProbability { get; }

        /// <summary>
        /// Gets the shift in price for the up movement
        /// </summary>
        double UpShift { get; }

        /// <summary>
        /// Gets the shift in price for the down movement
        /// </summary>
        double DownShift { get; }
    }
}