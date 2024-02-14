namespace Abbotware.UnitTests.Quant.Quant
{
    using Abbotware.Quant;
    using Abbotware.Quant.Options;
    using NUnit.Framework;

    public class BlackScholesTests
    {
        [Test]
        public void D1_D2()
        {
            var (d1, d2) = BlackScholes.D(33, 34, .5, .25, .045, 0);

            Assert.Multiple(() =>
            {
                Assert.That(d1, Is.EqualTo(0.046793707).Within(Precision.High));
                Assert.That(d2, Is.EqualTo(-0.129982988).Within(Precision.High));
            });
        }

        [Test]
        public void Price()
        {
            var call = BlackScholes.Premium(ContractType.Call, 33, 34, .5, .25, .045, 0);
            var put = BlackScholes.Premium(ContractType.Put, 33, 34, .5, .25, .045, 0);

            Assert.Multiple(() =>
            {
                Assert.That(call, Is.EqualTo(2.213073289).Within(Precision.High));
                Assert.That(put, Is.EqualTo(2.456615353).Within(Precision.High));
            });
        }
    }
}