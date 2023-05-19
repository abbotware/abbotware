namespace Abbotware.UnitTests.Quant.Finance
{
    using Abbotware.Quant.Finance;
    using Abbotware.Quant.Finance.Interest;
    using NUnit.Framework;

    public class InterestTests
    {
        [TestCase(100000000, 0.1234, 1.5, 18510000)]
        public void Simple(decimal p, double r, double t, decimal i)
        {
            var s = new Simple(new(r));
            Assert.That(s.Interest(p, t), Is.EqualTo(i).Within(Precision.VeryLow));
            Assert.That(s.AccruedAmount(p, t), Is.EqualTo(p + i).Within(Precision.VeryLow));
        }

        [TestCase(1234, 0, 1.5, 0)]
        [TestCase(1234, .1234, 0, 0)]
        [TestCase(0, .1234, 1.5, 0)]
        public void Simple_ZeroCases(decimal p, double r, double t, decimal i)
        {
            var s = new Simple(new(r));
            Assert.That(s.Interest(p, t), Is.EqualTo(i).Within(Precision.VeryLow));
            Assert.That(s.AccruedAmount(p, t), Is.EqualTo(p + i).Within(Precision.VeryLow));
        }

        [TestCase(1000, 0.08, 5, 1491.82)]
        public void Continuous(decimal p, double r, double t, decimal p_i)
        {
            var s = new Continuous(new(r));
            Assert.That(s.Interest(p, t), Is.EqualTo(p_i - p).Within(Precision.VeryLow));
            Assert.That(s.AccruedAmount(p, t), Is.EqualTo(p_i).Within(Precision.VeryLow));
        }

        [TestCase(0, 0.08, 5, 0)]
        [TestCase(1000, 0, 5, 0)]
        [TestCase(1000, 0.08, 0, 0)]
        public void Continuous_ZeroCases(decimal p, double r, double t, decimal p_i)
        {
            var s = new Continuous(new(r));
            Assert.That(s.Interest(p, t), Is.EqualTo(0).Within(Precision.VeryLow));
            Assert.That(s.AccruedAmount(p, t), Is.EqualTo(p).Within(Precision.VeryLow));
        }

        [TestCase(12345.00, .12, 7.8, CompoundingFrequency.Weekly, 31442.89)]
        [TestCase(12345.00, .12, 7.8, CompoundingFrequency.Quarterly, 31046.49)]
        public void Discrete(decimal p, double r, double t, CompoundingFrequency frequency, decimal p_i)
        {
            var s = new Discrete(new(r), frequency);
            Assert.That(s.Interest(p, t), Is.EqualTo(p_i - p).Within(Precision.VeryLow));
            Assert.That(s.AccruedAmount(p, t), Is.EqualTo(p_i).Within(Precision.VeryLow));
        }

        [TestCase(0, .12, 7.8, CompoundingFrequency.Quarterly, 0)]
        [TestCase(12345.00, 0, 7.8, CompoundingFrequency.Quarterly, 0)]
        [TestCase(12345.00, .12, 0, CompoundingFrequency.Quarterly, 0)]
        public void Discrete_ZeroCases(decimal p, double r, double t, CompoundingFrequency frequency, decimal p_i)
        {
            var s = new Discrete(new(r), frequency);
            Assert.That(s.Interest(p, t), Is.EqualTo(0).Within(Precision.VeryLow));
            Assert.That(s.AccruedAmount(p, t), Is.EqualTo(p).Within(Precision.VeryLow));
        }
    }
}