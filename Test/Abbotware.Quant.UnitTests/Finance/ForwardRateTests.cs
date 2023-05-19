namespace Abbotware.UnitTests.Quant.Finance
{
    using Abbotware.Quant.Finance.Equations;
    using NUnit.Framework;

    public class ForwardRateTests
    {
        [Test]
        public void BasicTests()
        {
            var r1 = InterestRate.ForwardRate(new(.03), 1, new(.04), 2);
            Assert.That(r1, Is.EqualTo(0.05).Within(.00000000000001));

            var r2 = InterestRate.ForwardRate(new(.04), 2, new(.046), 3);
            Assert.That(r2, Is.EqualTo(0.058).Within(.00000000000001));
        }
    }
}