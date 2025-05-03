// -----------------------------------------------------------------------
// <copyright file="BlackScholesExactOptimized.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.MonteCarlo.Plugins
{
    using System;

    /// <summary>
    /// Black-Scholes - Exact Simulation using Gemoetric Brownian Motion
    /// </summary>
    /// <param name="μ">rate of return/drift</param>
    /// <param name="σ">volatility</param>
    /// <param name="Δt">time increment</param>
    public readonly record struct BlackScholesExactOptimized(double μ, double σ, double Δt) : IStockMovement
    {
        /// <summary>
        /// Gets constant C1 for this set of parameters
        /// </summary>
        public double C1 { get; } = Math.Exp((μ - (σ * σ * .5)) * Δt);

        /// <summary>
        /// Gets constant C2 for this set of parameters
        /// </summary>
        public double C2 { get; } = Math.Exp(σ * Math.Sqrt(Δt));

        /// <inheritdoc/>
        public double Next(double Si, double Zi)
        {
            return Si * this.C1 * Math.Pow(this.C2, Zi);
        }
    }
}
