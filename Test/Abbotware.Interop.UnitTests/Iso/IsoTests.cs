//-----------------------------------------------------------------------
// <copyright file="IsoTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.UnitTests.Interop.Iso
{
    using Abbotware.Interop.Iso;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using global::NUnit.Framework;

    [TestFixture]
    public class IsoTests : BaseNUnitTest
    {
        [Test]
        [Category("Interop")]
        [Category("Interop.Iso")]
        public void CountryLookup()
        {
            var us = IsoHelpers.Country.Lookup(Country.USA);

            Assert.AreSame(us, IsoHelpers.Country[us.Id]);
            Assert.AreSame(us, IsoHelpers.Country.Lookup((ushort)us.Id));
            Assert.AreSame(us, IsoHelpers.Country.LookupAlpha3("USA"));
            Assert.AreSame(us, IsoHelpers.Country.LookupAlpha2("US"));
        }

        [Test]
        [Category("Interop")]
        [Category("Interop.Iso")]
        public void CurrencyLookup()
        {
            var us = IsoHelpers.Currency.Lookup(Currency.USD);

            Assert.AreSame(us, IsoHelpers.Currency[us.Id]);
            Assert.AreSame(us, IsoHelpers.Currency.Lookup((ushort)us.Id));
            Assert.AreSame(us, IsoHelpers.Currency.LookupAlpha("USD"));
        }

        [Test]
        [Category("Interop")]
        [Category("Interop.Iso")]
        public void CurrencyLookup_None()
        {
            var c = IsoHelpers.Currency.Lookup(Currency.None);

            Assert.AreSame(c, IsoHelpers.Currency[c.Id]);
            Assert.AreSame(c, IsoHelpers.Currency.Lookup((ushort)c.Id));
            Assert.AreSame(c, IsoHelpers.Currency.LookupAlpha("None"));
        }

        [Test]
        [Category("Interop")]
        [Category("Interop.Iso")]
        public void CurrencyParse_Good()
        {
            Assert.IsTrue(IsoHelpers.Currency.TryParseAlpha("uSd", out var c));
            Assert.IsNotNull(c);
            Assert.AreEqual(Currency.USD, c);
        }

        [Test]
        [Category("Interop")]
        [Category("Interop.Iso")]
        public void CurrencyParse_Bad()
        {
            Assert.IsFalse(IsoHelpers.Currency.TryParseAlpha("USDd", out var c));
            Assert.IsNull(c);
        }

        [Test]
        [Category("Interop")]
        [Category("Interop.Iso")]
        public void CountryParseAlpha3_Good()
        {
            Assert.IsTrue(IsoHelpers.Country.TryParseAlpha3("uSa", out var c));
            Assert.IsNotNull(c);
            Assert.AreEqual(Country.USA, c);
        }

        [Test]
        [Category("Interop")]
        [Category("Interop.Iso")]
        public void CountryParseAlpha3_Bad()
        {
            Assert.IsFalse(IsoHelpers.Country.TryParseAlpha3("asdf", out var c));
            Assert.IsNull(c);
        }

        [Test]
        [Category("Interop")]
        [Category("Interop.Iso")]
        public void CountryParseAlpha2_Good()
        {
            Assert.IsTrue(IsoHelpers.Country.TryParseAlpha2("uS", out var c));
            Assert.IsNotNull(c);
            Assert.AreEqual(Country.USA, c);
        }

        [Test]
        [Category("Interop")]
        [Category("Interop.Iso")]
        public void CountryParseAlpha2_Bad()
        {
            Assert.IsFalse(IsoHelpers.Country.TryParseAlpha2("asdf", out var c));
            Assert.IsNull(c);
        }
    }
}