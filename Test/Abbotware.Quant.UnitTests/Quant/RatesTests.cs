namespace Abbotware.UnitTests.Quant
{
    using Abbotware.Quant;
    using Abbotware.Quant.Equations;
    using Abbotware.Quant.Finance;
    using Abbotware.Quant.Rates;
    using Abbotware.Quant.UnitTests;
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
            }
            else
            {
                return InterestRateEquations.Compound(amount, new InterestRate(rate, frequency), (ushort)frequency);
            }
        }

        [Test]
        public void Lecture_02_Slide12()
        {
            var r1 = InterestRateEquations.ConvertPeriodicToContinuous(.1, 2);
            Assert.That(r1, Is.EqualTo(.097580328338864084).Within(Precision.VeryHigh));

            var r2 = InterestRateEquations.ConvertContinousToPeriodic(.08, 4);
            Assert.That(r2, Is.EqualTo(.080805360107023105d).Within(Precision.VeryHigh));
        }

        [Test]
        public void DiscountFactor_Continuous()
        {
            var r1 = DiscountFactor.Continuous(.12, 2);
            Assert.That(r1, Is.EqualTo(0.7866).Within(Precision.Low));
        }

        [Test]
        public void DiscountFactor_Discrete()
        {
            var r1 = DiscountFactor.Discrete(.12, 365, 2);
            Assert.That(r1, Is.EqualTo(0.7867).Within(Precision.Low));
            var r2 = DiscountFactor.Discrete(.12, 12, 2);
            Assert.That(r2, Is.EqualTo(0.7876).Within(Precision.Low));
            var r3 = DiscountFactor.Discrete(.12, 4, 2);
            Assert.That(r3, Is.EqualTo(0.7894).Within(Precision.Low));
            var r4 = DiscountFactor.Discrete(.12, 2, 2);
            Assert.That(r4, Is.EqualTo(0.7921).Within(Precision.Low));
            var r5 = DiscountFactor.Discrete(.12, 1, 2);
            Assert.That(r5, Is.EqualTo(0.7972).Within(Precision.Low));
        }

        [TestCase(.1, CompoundingFrequency.Yearly, CompoundingFrequency.Weekly)]
        [TestCase(.6, CompoundingFrequency.SemiAnnually, CompoundingFrequency.Daily)]
        public void ConversionSanityChecks(double rateA, TimePeriod periodA, TimePeriod periodB)
        {
            var r1 = InterestRateEquations.ConvertPeriodicToPeriodic(rateA, (ushort)periodA, (ushort)periodB);
            var r2 = InterestRateEquations.ConvertPeriodicToPeriodicAlt(rateA, (ushort)periodA, (ushort)periodB);

            Assert.That(r1, Is.EqualTo(r2));
        }

        //[TestCase(.1, TimePeriod.Annually, CompoundingFrequency.Continuous, ExpectedResult = .9)]
        //public double YearlyConversion(double rateA, CompoundingFrequency periodA, CompoundingFrequency periodB)
        //{
        //    Assert.Inconclusive();
        //    var rate = new InterestRate(rateA, periodA);
        //    var converted = rate.Convert(periodB);
        //    return converted.AnnualPercentageRate;
        //}
    }
}
