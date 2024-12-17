namespace Abbotware.UnitTests.Quant.Finance
{
    using Abbotware.Quant.Finance.Equations;
    using NUnit.Framework;

    public class TimeValueTests
    {
        [Test]
        public void FutureValue_BaseCases()
        {
            Assert.That(TimeValue.Discrete.FutureValue(0, 0, 0), Is.EqualTo(0));
            Assert.That(TimeValue.Discrete.FutureValue(0, 123, 0), Is.EqualTo(0));
            Assert.That(TimeValue.Discrete.FutureValue(0, 0, 123), Is.EqualTo(0));

            Assert.That(TimeValue.Discrete.FutureValue(100, 0, 0), Is.EqualTo(100));
            Assert.That(TimeValue.Discrete.FutureValue(100, 0, 1), Is.EqualTo(100));
            Assert.That(TimeValue.Discrete.FutureValue(100, 1, 0), Is.EqualTo(100));
            Assert.That(TimeValue.Discrete.FutureValue(100, 0, 4), Is.EqualTo(100));
            Assert.That(TimeValue.Discrete.FutureValue(100, 4, 0), Is.EqualTo(100));
        }

        [Test]
        public void PresentValue_BaseCases()
        {
            Assert.That(TimeValue.Discrete.PresentValue(0, 0, 0), Is.EqualTo(0));
            Assert.That(TimeValue.Discrete.PresentValue(0, 123, 0), Is.EqualTo(0));
            Assert.That(TimeValue.Discrete.PresentValue(0, 0, 123), Is.EqualTo(0));

            Assert.That(TimeValue.Discrete.PresentValue(100, 0, 0), Is.EqualTo(100));
            Assert.That(TimeValue.Discrete.PresentValue(100, 0, 1), Is.EqualTo(100));
            Assert.That(TimeValue.Discrete.PresentValue(100, 1, 0), Is.EqualTo(100));
            Assert.That(TimeValue.Discrete.PresentValue(100, 0, 4), Is.EqualTo(100));
            Assert.That(TimeValue.Discrete.PresentValue(100, 4, 0), Is.EqualTo(100));
        }

        [Test]
        [TestCase(1500000, .0634, 10, 2773689.35)]
        public void FutureValue_BaseCases(decimal pv, double i, double n, double expected)
        {
            Assert.That(TimeValue.Discrete.FutureValue(pv, i, n), Is.EqualTo(expected).Within(DoublePrecision.VeryLow));
        }
    }
}