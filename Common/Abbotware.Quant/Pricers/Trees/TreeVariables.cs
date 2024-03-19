namespace Abbotware.Quant.Pricers.Trees
{
    public record class TreeVariables(double Δt, double σ, double RiskFreeRate, double DividendYield)
    {
        public double σ_squared { get; } = σ * σ;

        public double DiscountFactor { get; } = Finance.Equations.DiscountFactor.Continuous(RiskFreeRate - DividendYield, Δt);
    }
}