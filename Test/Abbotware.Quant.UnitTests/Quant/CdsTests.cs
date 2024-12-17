namespace Abbotware.UnitTests.Quant
{
    using System.Collections.Generic;
    using Abbotware.Quant.Assets;
    using Abbotware.Quant.Finance;
    using Abbotware.Quant.Finance.Rates;
    using Abbotware.Quant.Periodic;
    using Abbotware.Quant.Rates.Plugins;
    using NUnit.Framework;

    public class CdsTests
    {
        [TestCase(.015, 1, 98.5112214597145d)]
        [TestCase(.025, 2, 98.0353890082365d)]
        [TestCase(.03, 5, 95.3791749943793d)]
        [TestCase(.04, 10, 97.7993896923126d)]
        public void Price_Lecture02_BondPricing_xls(double couponRate, double maturity, double xls)
        {
            Assert.Inconclusive("To Implement");

            var zeroRateCurve = new ZeroRateCurve<double>(
            KeyValuePair.Create(1d, .03),
            KeyValuePair.Create(2d, .035),
            KeyValuePair.Create(5d, .04),
            KeyValuePair.Create(10d, .0425));

            var bond = new Bond(maturity, new(new NominalRate(couponRate), new SimplePeriodic<double>(TimePeriod.SemiAnnually)));

            var price = bond.Price(zeroRateCurve);

            Assert.That(price, Is.EqualTo(xls).Within(DoublePrecision.High));
        }
    }
}
