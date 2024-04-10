namespace Abbotware.Quant.MonteCarlo.Plugins
{
    using System;

    /// <summary>
    /// Black-Scholes - Euler Discretization Simulation using Gemoetric Brownian Motion
    /// </summary>
    /// <param name="r">risk free rate</param>
    /// <param name="σ">volatility</param>
    /// <param name="Δt">time increment</param>
    public readonly struct BlackScholesEuler(double r, double σ, double Δt) : IStockMovement
    {
        /// <inheridoc/>
        public double Next(double Si, double Zi)
        {
            var a = r * Δt * Si;
            var b = σ * Si * Math.Sqrt(Δt) * Zi;
            return Si + a + b;
        }
    }

}
