// -----------------------------------------------------------------------
// <copyright file="ITrinomialCalculations.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Pricers.Trees
{
    /// <summary>
    /// Interface for Trinomial Tree Calculations
    /// </summary>
    public interface ITrinomialCalculations : IBinomialCalculations
    {
        /// <summary>
        /// Gets the probability of the middle path
        /// </summary>
        double MiddleProbability { get; }

        /// <summary>
        /// Gets the shift of the middle path
        /// </summary>
        double MiddleShift { get; }
    }
}