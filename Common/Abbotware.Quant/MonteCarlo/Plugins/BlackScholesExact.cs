// -----------------------------------------------------------------------
// <copyright file="BlackScholesExact.cs" company="Abbotware, LLC">
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
    public readonly record struct BlackScholesExact(double μ, double σ, double Δt) : IStockMovement
    {
        /// <inheritdoc/>
        public double Next(double Si, double Zi)
        {
            var a = (this.μ - (this.σ * this.σ * .5)) * this.Δt;
            var b = this.σ * Math.Sqrt(this.Δt) * Zi;
            return Si * Math.Exp(a + b);
        }
    }
}