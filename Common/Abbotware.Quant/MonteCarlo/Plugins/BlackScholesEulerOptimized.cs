// -----------------------------------------------------------------------
// <copyright file="BlackScholesEulerOptimized.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.MonteCarlo.Plugins
{
    using System;

    /// <summary>
    /// Black-Scholes - Euler Discretization Simulation using Gemoetric Brownian Motion
    /// </summary>
    /// <param name="μ">rate of return/drift</param>
    /// <param name="σ">volatility</param>
    /// <param name="Δt">time increment</param>
    public readonly struct BlackScholesEulerOptimized(double μ, double σ, double Δt) : IStockMovement
    {
        /// <summary>
        /// Gets constant C1 for this set of parameters
        /// </summary>
        public double C1 { get; } = 1 + (μ * Δt);

        /// <summary>
        /// Gets constant C2 for this set of parameters
        /// </summary>
        public double C2 { get; } = σ * Math.Sqrt(Δt);

        /// <inheridoc/>
        public double Next(double Si, double Zi)
        {
            return Si * (this.C1 + (this.C2 * Zi));
        }
    }

}
