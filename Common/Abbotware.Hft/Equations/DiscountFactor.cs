namespace Abbotware.Quant.Equations
{
    using System;

    public static class DiscountFactor
    {
        public static double Continuous(double rate, double t)
        {
            return Math.Exp(-rate * t);
        }

        public static double Discrete(double rate, double periods, double t)
        {
            return Math.Pow(1 + (rate / periods), -periods * t);
        }
    }
}
