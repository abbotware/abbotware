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

        [TestCase(0, 0.08, 5)]
        [TestCase(1000, 0, 5)]
        [TestCase(1000, 0.08, 0)]
        public void Continuous_ZeroCases(decimal p, double r, double t)
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

        [TestCase(0, .12, 7.8, CompoundingFrequency.Quarterly)]
        [TestCase(12345.00, 0, 7.8, CompoundingFrequency.Quarterly)]
        [TestCase(12345.00, .12, 0, CompoundingFrequency.Quarterly)]
        public void Discrete_ZeroCases(decimal p, double r, double t, CompoundingFrequency frequency)
        {
            var s = new Discrete(new(r), frequency);
            Assert.That(s.Interest(p, t), Is.EqualTo(0).Within(Precision.VeryLow));
            Assert.That(s.AccruedAmount(p, t), Is.EqualTo(p).Within(Precision.VeryLow));
        }

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
                var c = new Continuous(new(rate));
                return c.AccruedAmount(amount, 1);
            }
            else
            {
                var c = new Discrete(new(rate), frequency);
                return c.AccruedAmount(amount, 1);
            }
        }
    }
}