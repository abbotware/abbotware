namespace Abbotware.UnitTests.Quant.Finance
{
    using Abbotware.Quant.Finance.Equations;
    using NUnit.Framework;

    public class DiscountFactorTests
    {
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
    }
}
