namespace Abbotware.UnitTests.Quant.Finance
{
    using Abbotware.Quant.Finance;
    using Abbotware.Quant.Finance.Equations;
    using NUnit.Framework;

    public class EquivalentInterestRateTests
    {
        [Test]
        public void Lecture_02_Slide12()
        {
            var r1 = InterestRate.PeriodicToContinuous(.1, 2);
            Assert.That(r1, Is.EqualTo(.097580328338864084).Within(DoublePrecision.VeryHigh));

            var r2 = InterestRate.ContinousToPeriodic(.08, 4);
            Assert.That(r2, Is.EqualTo(.080805360107023105d).Within(DoublePrecision.VeryHigh));
        }

        [TestCase(.1, CompoundingFrequency.Yearly, CompoundingFrequency.Weekly)]
        [TestCase(.6, CompoundingFrequency.SemiAnnually, CompoundingFrequency.Daily)]
        public void ConversionSanityChecks(double rateA, TimePeriod periodA, TimePeriod periodB)
        {
            var r1 = InterestRate.PeriodicToPeriodic(rateA, (ushort)periodA, (ushort)periodB);
            var r2 = InterestRate.PeriodicToPeriodicAlt(rateA, (ushort)periodA, (ushort)periodB);

            Assert.That(r1, Is.EqualTo(r2));
        }

        [Test]
        public void ContinousToPeriodic()
        {
            // 12 * (e^(.07/12) - 1) = 0.07020456423702833051077058574828
            var r = InterestRate.ContinousToPeriodic(.07, 12);

            Assert.That(r, Is.EqualTo(0.07020456423702833051077058574828).Within(.00000000000001));

            // inverse of second test
            Assert.That(InterestRate.ContinousToPeriodic(0.06979662385727787975956113414264, 12), Is.EqualTo(.07).Within(.00000000000001));
        }

        [Test]
        public void PeriodicToContinuous()
        {
            var r = InterestRate.PeriodicToContinuous(.07, 12);

            // ln( (1 + .07/12)^12 ) = 0.06979662385727787975956113414264
            Assert.That(r, Is.EqualTo(0.06979662385727787975956113414264).Within(.00000000000001));

            // inverse of first test
            Assert.That(InterestRate.PeriodicToContinuous(0.07020456423702833051077058574828, 12), Is.EqualTo(.07).Within(.00000000000001));
        }

        [Test]
        public void PeriodicToPeriodic()
        {
            var r = InterestRate.PeriodicToPeriodic(.08, 12, 4);

            // 4 ( (1 + (.08/12))^(12/4)) - 1) = 0.06979662385727787975956113414264
            Assert.That(r, Is.EqualTo(0.08053451851851851851851851851852).Within(.00000000000001));

            // verify via ConvertPeriodicToContinuous then ConvertContinousToPeriodic
            Assert.That(r, Is.EqualTo(InterestRate.PeriodicToPeriodicAlt(.08, 12, 4)).Within(.00000000000001));
        }
    }
}