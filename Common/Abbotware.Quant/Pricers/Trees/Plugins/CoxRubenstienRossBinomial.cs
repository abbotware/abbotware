// -----------------------------------------------------------------------
// <copyright file="CoxRubenstienRossBinomial.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Pricers.Trees.Plugins
{
    using System;
    using Abbotware.Quant.Pricers.Trees;

    /// <summary>
    /// Binomial Calculations Cox-Rubenstien-Ross Tree
    /// </summary>
    public record class CoxRubenstienRossBinomial : IBinomialCalculations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoxRubenstienRossBinomial"/> class.
        /// </summary>
        /// <param name="variables">variables</param>
        public CoxRubenstienRossBinomial(TreeVariables variables)
        {
            this.UpShift = Math.Exp(variables.σ * Math.Sqrt(variables.Δt));
            this.DownShift = Math.Exp(-variables.σ * Math.Sqrt(variables.Δt));

            this.A = Math.Exp((variables.RiskFreeRate - variables.DividendYield) * variables.Δt);
            this.UpProbability = (this.A - this.DownShift) / (this.UpShift - this.DownShift);
            this.DownProbability = 1 - this.UpProbability;
        }

        /// <summary>
        /// Gets the intermediate varaiable (used for unit tests)
        /// </summary>
        public double A { get; }

        /// <inheritdoc/>
        public double UpShift { get; }

        /// <inheritdoc/>
        public double DownShift { get; }

        /// <inheritdoc/>
        public double UpProbability { get; }

        /// <inheritdoc/>
        public double DownProbability { get; }
    }
}