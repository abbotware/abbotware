namespace Abbotware.UnitTests.Core.Quant
{
    using Abbotware.Quant;
    using Abbotware.Quant.Enums;
    using Abbotware.Quant.InterestRates;
    using NUnit.Framework;

    public class RatesTests
    {
        [TestCase(100, .1, AccrualPeriods.Yearly, ExpectedResult = 110)]
        [TestCase(100, .1, AccrualPeriods.SemiAnnually, ExpectedResult = 110.250)]
        [TestCase(100, .1, AccrualPeriods.Quarterly, ExpectedResult = 110.3812890625)]
        [TestCase(100, .1, AccrualPeriods.Monthly, ExpectedResult = 110.47130674413)]
        [TestCase(100, .1, AccrualPeriods.Weekly, ExpectedResult = 110.506479277977)]
        [TestCase(100, .1, AccrualPeriods.Daily_365, ExpectedResult = 110.515578161623)]
        [TestCase(100, .1, AccrualPeriods.Continuous, ExpectedResult = 110.517091807565)]
        [TestCase(1000, .231, AccrualPeriods.BiMonthly, ExpectedResult = 1254.40854925457)]
        public decimal Compound(decimal amount, double rate, AccrualPeriods frequency)
        {
            return Equations.InterestRates.Compound(amount, new InterestRate(rate, frequency), (ushort)frequency);
        }

        [TestCase(.1, AccrualPeriods.Yearly, ExpectedResult = .9)]
        public decimal DiscountFactor(double rate, AccrualPeriods frequency)
        {
            return Equations.InterestRates.DiscountFactor(new InterestRate(rate, frequency), 1);
        }

        [TestCase(.1, AccrualPeriods.Yearly, AccrualPeriods.Weekly)]
        [TestCase(.6, AccrualPeriods.SemiAnnually, AccrualPeriods.Daily_365)]
        public void ConversionSanityChecks(double rateA, AccrualPeriods periodA, AccrualPeriods periodB)
        {
            var r1 = Equations.InterestRates.ConvertPeriodicToPeriodic(rateA, (ushort)periodA, (ushort)periodB);
            var r2 = Equations.InterestRates.ConvertPeriodicToPeriodicAlt(rateA, (ushort)periodA, (ushort)periodB);

            Assert.That(r1, Is.EqualTo(r2));
        }

        [TestCase(.1, AccrualPeriods.Yearly, AccrualPeriods.Continuous, ExpectedResult = .9)]
        public double YearlyConversion(double rateA, AccrualPeriods periodA, AccrualPeriods periodB)
        {
            var rate = new InterestRate(rateA, periodA);
            var converted = rate.Convert(periodB);
            return converted.Rate;
        }
    }
}
