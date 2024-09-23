namespace Abbotware.Quant.Pricers
{
    public record class OptionVariables(
        decimal InitialPrice,
        decimal StrikePrice,
        double Maturity,
        double Volatility,
        double RiskFreeRate,
        double DividendYield);
}