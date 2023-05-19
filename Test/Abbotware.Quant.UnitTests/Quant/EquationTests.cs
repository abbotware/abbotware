namespace Abbotware.UnitTests.Quant
{
    using Abbotware.Quant;
    using Abbotware.Quant.Rates;
    using NUnit.Framework;

    public class EquationTests
    {
        [Test]
        public void InterestRate_ConvertContinousToPeriodic()
        {
            // 12 * (e^(.07/12) - 1) = 0.07020456423702833051077058574828
            var r = InterestRateEquations.ConvertContinousToPeriodic(.07, 12);

            Assert.That(r, Is.EqualTo(0.07020456423702833051077058574828).Within(.00000000000001));

            // inverse of second test
            Assert.That(InterestRateEquations.ConvertContinousToPeriodic(0.06979662385727787975956113414264, 12), Is.EqualTo(.07).Within(.00000000000001));
        }

        [Test]
        public void InterestRate_ConvertPeriodicToContinuous()
        {
            var r = InterestRateEquations.ConvertPeriodicToContinuous(.07, 12);

            // ln( (1 + .07/12)^12 ) = 0.06979662385727787975956113414264
            Assert.That(r, Is.EqualTo(0.06979662385727787975956113414264).Within(.00000000000001));

            // inverse of first test
            Assert.That(InterestRateEquations.ConvertPeriodicToContinuous(0.07020456423702833051077058574828, 12), Is.EqualTo(.07).Within(.00000000000001));
        }

        [Test]
        public void InterestRate_ConvertPeriodicToPeriodic()
        {
            var r = InterestRateEquations.ConvertPeriodicToPeriodic(.08, 12, 4);

            // 4 ( (1 + (.08/12))^(12/4)) - 1) = 0.06979662385727787975956113414264
            Assert.That(r, Is.EqualTo(0.08053451851851851851851851851852).Within(.00000000000001));

            // verify via ConvertPeriodicToContinuous then ConvertContinousToPeriodic
            Assert.That(r, Is.EqualTo(InterestRateEquations.ConvertPeriodicToPeriodicAlt(.08, 12, 4)).Within(.00000000000001));
        }

        [Test]
        public void ForwardRate_BasicTests()
        {
            var r1 = InterestRateEquations.ForwardRate(InterestRate.Continuous(.03), 1, InterestRate.Continuous(.04), 2);
            Assert.That(r1, Is.EqualTo(0.05).Within(.00000000000001));

            var r2 = InterestRateEquations.ForwardRate(InterestRate.Continuous(.04), 2, InterestRate.Continuous(.046), 3);
            Assert.That(r2, Is.EqualTo(0.058).Within(.00000000000001));
        }
    }
}
