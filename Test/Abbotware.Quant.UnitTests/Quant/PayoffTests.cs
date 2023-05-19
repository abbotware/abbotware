namespace Abbotware.UnitTests.Quant
{
    using Abbotware.Quant.Payoffs.Plugins;
    using NUnit.Framework;

    public class PayoffTests
    {
        [Test]
        [TestCase(56.2, 100.3, ExpectedResult = 44.1)]
        [TestCase(100.3, 56.2, ExpectedResult = -44.1)]
        [TestCase(5, 10, ExpectedResult = 5)]
        [TestCase(10, 5, ExpectedResult = -5)]
        [TestCase(1, 1, ExpectedResult = 0)]
        [TestCase(1, 0, ExpectedResult = -1)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(0, 1, ExpectedResult = 1)]
        public decimal LongTests(decimal strike, decimal spot)
        {
            var p = new LongPayoff(strike);

            return p.Compute(spot);
        }

        [Test]
        [TestCase(56.2, 100.3, ExpectedResult = -44.1)]
        [TestCase(100.3, 56.2, ExpectedResult = 44.1)]
        [TestCase(5, 10, ExpectedResult = -5)]
        [TestCase(10, 5, ExpectedResult = 5)]
        [TestCase(1, 1, ExpectedResult = 0)]
        [TestCase(1, 0, ExpectedResult = 1)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(0, 1, ExpectedResult = -1)]
        public decimal ShortTests(decimal strike, decimal spot)
        {
            var p = new ShortPayoff(strike);

            return p.Compute(spot);
        }

        [Test]
        [TestCase(56.2, 100.3, ExpectedResult = 44.1)]
        [TestCase(100.3, 56.2, ExpectedResult = 0)]
        [TestCase(5, 10, ExpectedResult = 5)]
        [TestCase(10, 5, ExpectedResult = 0)]
        [TestCase(1, 1, ExpectedResult = 0)]
        [TestCase(1, 0, ExpectedResult = 0)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(0, 1, ExpectedResult = 1)]
        public decimal CallTests(decimal strike, decimal spot)
        {
            var p = new CallPayoff(strike);

            return p.Compute(spot);
        }

        [Test]
        [TestCase(56.2, 100.3, ExpectedResult = 0)]
        [TestCase(100.3, 56.2, ExpectedResult = 44.1)]
        [TestCase(5, 10, ExpectedResult = 0)]
        [TestCase(10, 5, ExpectedResult = 5)]
        [TestCase(1, 1, ExpectedResult = 0)]
        [TestCase(1, 0, ExpectedResult = 1)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(0, 1, ExpectedResult = 0)]
        public decimal PutTests(decimal strike, decimal spot)
        {
            var p = new PutPayoff(strike);

            return p.Compute(spot);
        }
    }
}
