namespace Abbotware.UnitTests.Quant.Finance
{
    using Abbotware.Quant.Finance.Rates;
    using NUnit.Framework;

    public class RateTests
    {
        [Test]
        public void Yield_Basic()
        {
            var y = new Yield(.12, 1);
            Assert.That(y.Rate, Is.EqualTo(.12));
            Assert.That(y.TimePeriod.Lower, Is.EqualTo(0));
            Assert.That(y.TimePeriod.Upper, Is.EqualTo(1));
            Assert.That(y.RatePerPeriod, Is.EqualTo(.12));
            Assert.That(y.PeriodsPerYear, Is.EqualTo(1));
            Assert.That(y.PeriodLength, Is.EqualTo(1));
            Assert.That(y.IsContinuous, Is.EqualTo(true));
        }

        [Test]
        public void ZeroRate_Basic()
        {
            var zr = new ZeroRate(.12, 1.25);
            Assert.That(zr.Rate, Is.EqualTo(.12));
            Assert.That(zr.TimePeriod.Lower, Is.EqualTo(0));
            Assert.That(zr.TimePeriod.Upper, Is.EqualTo(1.25));
            Assert.That(zr.RatePerPeriod, Is.EqualTo(.12));
            Assert.That(zr.PeriodsPerYear, Is.EqualTo(0.8));
            Assert.That(zr.PeriodLength, Is.EqualTo(1.25));
            Assert.That(zr.IsContinuous, Is.EqualTo(true));
        }
    }
}