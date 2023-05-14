namespace Abbotware.UnitTests.Core.Quant
{
    using Abbotware.Quant;
    using Abbotware.Quant.Enums;
    using Abbotware.Quant.InterestRates;
    using NUnit.Framework;

    public class RatesTests
    {
        [TestCase(100, .1, CompoundingFrequency.Yearly, ExpectedResult = 110)]
        [TestCase(100, .1, CompoundingFrequency.SemiAnnually, ExpectedResult = 110.250)]
        [TestCase(100, .1, CompoundingFrequency.Quarterly, ExpectedResult = 110.3812890625)]
        [TestCase(100, .1, CompoundingFrequency.Monthly, ExpectedResult = 110.47130674413)]
        [TestCase(100, .1, CompoundingFrequency.Weekly, ExpectedResult = 110.506479277977)]
        [TestCase(100, .1, CompoundingFrequency.Daily, ExpectedResult = 110.515578161623)]
        [TestCase(100, .1, CompoundingFrequency.Continuous, ExpectedResult = 110.517091807565)]
        [TestCase(1000, .231, CompoundingFrequency.BiMonthly, ExpectedResult = 1254.40854925457)]
        public decimal Compound(decimal amount, double rate, CompoundingFrequency frequency)
        {
            if (frequency == CompoundingFrequency.Continuous)
            {
                return InterestRateEquations.Compound(amount, new InterestRate(rate, frequency), 1);
            } else
            {
                return InterestRateEquations.Compound(amount, new InterestRate(rate, frequency), (ushort)frequency);
            }
        }

        [TestCase(.1, AccrualPeriods.Yearly, ExpectedResult = .9)]
        public double DiscountFactor(double rate, CompoundingFrequency frequency)
        {
            return InterestRateEquations.DiscountFactor(new InterestRate(rate, frequency), 1);
        }

        [TestCase(.1, CompoundingFrequency.Yearly, CompoundingFrequency.Weekly)]
        [TestCase(.6, CompoundingFrequency.SemiAnnually, CompoundingFrequency.Daily)]
        public void ConversionSanityChecks(double rateA, AccrualPeriods periodA, AccrualPeriods periodB)
        {
            var r1 = InterestRateEquations.ConvertPeriodicToPeriodic(rateA, (ushort)periodA, (ushort)periodB);
            var r2 = InterestRateEquations.ConvertPeriodicToPeriodicAlt(rateA, (ushort)periodA, (ushort)periodB);

            Assert.That(r1, Is.EqualTo(r2));
        }

        [TestCase(.1, AccrualPeriods.Yearly, CompoundingFrequency.Continuous, ExpectedResult = .9)]
        public double YearlyConversion(double rateA, CompoundingFrequency periodA, CompoundingFrequency periodB)
        {
            Assert.Inconclusive();
            var rate = new InterestRate(rateA, periodA);
            var converted = rate.Convert(periodB);
            return converted.AnnualPercentageRate;
        }
    }
}
