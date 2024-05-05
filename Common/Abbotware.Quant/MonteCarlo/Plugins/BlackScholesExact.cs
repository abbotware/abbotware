namespace Abbotware.Quant.MonteCarlo.Plugins
{
    using System;

    /// <summary>
    /// Black-Scholes - Exact Simulation using Gemoetric Brownian Motion
    /// </summary>
    /// <param name="μ">rate of return/drift</param>
    /// <param name="σ">volatility</param>
    /// <param name="Δt">time increment</param>
    public readonly struct BlackScholesExact(double μ, double σ, double Δt) : IStockMovement
    {
        /// <inheridoc/>
        public double Next(double Si, double Zi)
        {
            var a = (μ - (σ * σ * .5)) * Δt;
            var b = σ * Math.Sqrt(Δt) * Zi;
            return Si * Math.Exp(a + b);
        }
    }

}
