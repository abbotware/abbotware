// -----------------------------------------------------------------------
// <copyright file="CoxRubenstienRossTrinomial.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Pricers.Trees.Plugins
{
    using System;
    using Abbotware.Quant.Pricers.Trees;

    /// <summary>
    /// Trinomial Calculations Cox-Rubenstien-Ross Tree
    /// </summary>
    public record class CoxRubenstienRossTrinomial : ITrinomialCalculations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoxRubenstienRossTrinomial"/> class.
        /// </summary>
        /// <param name="variables">varaibles</param>
        public CoxRubenstienRossTrinomial(TreeVariables variables)
        {
            this.UpShift = Math.Exp(variables.σ * Math.Sqrt(3 * variables.Δt));
            this.MiddleShift = 0;
            this.DownShift = 1 / this.UpShift;

            var factor1 = Math.Sqrt(variables.Δt / (12 * variables.σ_squared));
            var factor2 = (variables.RiskFreeRate - variables.DividendYield) - (variables.σ_squared / 2);
            var factor3 = 1d / 6d;

            this.UpProbability = (factor1 * factor2) + factor3;
            this.MiddleProbability = 2d / 3d;
            this.DownProbability = -this.UpProbability;
        }

        /// <inheritdoc/>
        public double UpShift { get; }

        /// <inheritdoc/>
        public double DownShift { get; }

        /// <inheritdoc/>
        public double UpProbability { get; }

        /// <inheritdoc/>
        public double DownProbability { get; }

        /// <inheritdoc/>
        public double MiddleProbability { get; }

        /// <inheritdoc/>
        public double MiddleShift { get; }
    }
}