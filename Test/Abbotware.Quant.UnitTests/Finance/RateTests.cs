namespace Abbotware.UnitTests.Quant.Finance
{
    using Abbotware.Quant.Finance.Equations;
    using Abbotware.Quant.Finance.Rates;
    using NUnit.Framework;

    public class RateTests
    {
        [Test]
        public void Yield_Basic()
        {
            var y = new Yield(.12,  1);
            Assert.AreEqual(y.Rate, .12);
            Assert.AreEqual(y.TimePeriod.Lower, 0);
            Assert.AreEqual(y.TimePeriod.Upper, 1);
            Assert.AreEqual(y.RatePerPeriod, .12);
            Assert.AreEqual(y.PeriodsPerYear, 1);
            Assert.AreEqual(y.PeriodLength, 1);
            Assert.AreEqual(y.IsContinuous, true);
        }

        [Test]
        public void ZeroRate_Basic()
        {
            var zr = new ZeroRate(.12, 1.25);
            Assert.AreEqual(zr.Rate, .12);
            Assert.AreEqual(zr.TimePeriod.Lower, 0);
            Assert.AreEqual(zr.TimePeriod.Upper, 1.25);
            Assert.AreEqual(zr.RatePerPeriod, .12);
            Assert.AreEqual(zr.PeriodsPerYear, 0.8);
            Assert.AreEqual(zr.PeriodLength, 1.25);
            Assert.AreEqual(zr.IsContinuous, true);
        }
    }
}