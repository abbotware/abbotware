// -----------------------------------------------------------------------
// <copyright file="EqualProbabilityBinomial.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.Pricers.Trees.Plugins
{
    using System;

    /// <summary>
    /// Binomial Calculations for a 50/50 Equal Probability Tree
    /// </summary>
    public record class EqualProbabilityBinomial : IBinomialCalculations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EqualProbabilityBinomial"/> class.
        /// </summary>
        /// <param name="variables">variables</param>
        public EqualProbabilityBinomial(TreeVariables variables)
        {
            var factor1 = (variables.RiskFreeRate - variables.DividendYield) - (variables.σ_squared * .5);
            var factor2 = variables.σ * Math.Sqrt(variables.Δt);

            this.UpShift = Math.Exp(factor1 + factor2);
            this.DownShift = Math.Exp(factor1 - factor2);

            this.UpProbability = .5;
            this.DownProbability = .5;
        }

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