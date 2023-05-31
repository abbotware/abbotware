namespace Abbotware.UnitTests.Quant.Hull
{
    using Abbotware.Quant.Finance;
    using Abbotware.Quant.Finance.Equations;
    using Abbotware.Quant.Finance.Rates;
    using Abbotware.UnitTests.Quant;
    using NUnit.Framework;

    public class Examples
    {
        [Test]
        public void Example_04_01()
        {
            var equationResult = InterestRate.PeriodicToContinuous(.1, 2);

            Assert.That(equationResult, Is.EqualTo(.09758).Within(Precision.Medium));

            var r = new CompoundingRate(.1, TimePeriod.SemiAnnually);
            Assert.That(r.AsYearlyContinuous().Rate, Is.EqualTo(equationResult));
        }

        [Test]
        public void Example_04_02()
        {
            var equationResult = InterestRate.ContinousToPeriodic(.08, 4);

            Assert.That(equationResult, Is.EqualTo(.0808).Within(Precision.Medium));

            var r = new ContinuousRate(.08);
            Assert.That(r.AsYearlyPeriodic(4).Rate, Is.EqualTo(equationResult));
        }

        [Test]
        public void Example_04_06()
        {
            var equationResult = InterestRate.ContinousToPeriodic(.08, 4);

            Assert.That(equationResult, Is.EqualTo(.0808).Within(Precision.Medium));

            var r = new ContinuousRate(.08);
            Assert.That(r.AsYearlyPeriodic(4).Rate, Is.EqualTo(equationResult));
        }
    }
}