//-----------------------------------------------------------------------
// <copyright file="IsoTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
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

            Assert.That(us, Is.SameAs(IsoHelpers.Country[us.Id]));
            Assert.That(us, Is.SameAs(IsoHelpers.Country.Lookup((ushort)us.Id)));
            Assert.That(us, Is.SameAs(IsoHelpers.Country.LookupAlpha3("USA")));
            Assert.That(us, Is.SameAs(IsoHelpers.Country.LookupAlpha2("US")));
        }

        [Test]
        [Category("Interop")]
        [Category("Interop.Iso")]
        public void CurrencyLookup()
        {
            var us = IsoHelpers.Currency.Lookup(Currency.USD);

            Assert.That(us, Is.SameAs(IsoHelpers.Currency[us.Id]));
            Assert.That(us, Is.SameAs(IsoHelpers.Currency.Lookup((ushort)us.Id)));
            Assert.That(us, Is.SameAs(IsoHelpers.Currency.LookupAlpha("USD")));
        }

        [Test]
        [Category("Interop")]
        [Category("Interop.Iso")]
        public void CurrencyLookup_None()
        {
            var c = IsoHelpers.Currency.Lookup(Currency.None);

            Assert.That(c, Is.SameAs(IsoHelpers.Currency[c.Id]));
            Assert.That(c, Is.SameAs(IsoHelpers.Currency.Lookup((ushort)c.Id)));
            Assert.That(c, Is.SameAs(IsoHelpers.Currency.LookupAlpha("None")));
        }

        [Test]
        [Category("Interop")]
        [Category("Interop.Iso")]
        public void CurrencyParse_Good()
        {
            Assert.That(IsoHelpers.Currency.TryParseAlpha("uSd", out var c), Is.True);
            Assert.That(c, Is.Not.Null);
            Assert.That(Currency.USD, Is.EqualTo(c));
        }

        [Test]
        [Category("Interop")]
        [Category("Interop.Iso")]
        public void CurrencyParse_Bad()
        {
            Assert.That(IsoHelpers.Currency.TryParseAlpha("USDd", out var c), Is.False);
            Assert.That(c, Is.Null);
        }

        [Test]
        [Category("Interop")]
        [Category("Interop.Iso")]
        public void CountryParseAlpha3_Good()
        {
            Assert.That(IsoHelpers.Country.TryParseAlpha3("uSa", out var c), Is.True);
            Assert.That(c, Is.Not.Null);

            Assert.That(Country.USA, Is.EqualTo(c));
        }

        [Test]
        [Category("Interop")]
        [Category("Interop.Iso")]
        public void CountryParseAlpha3_Bad()
        {
            Assert.That(IsoHelpers.Country.TryParseAlpha3("asdf", out var c), Is.False);
            Assert.That(c, Is.Null);
        }

        [Test]
        [Category("Interop")]
        [Category("Interop.Iso")]
        public void CountryParseAlpha2_Good()
        {
            Assert.That(IsoHelpers.Country.TryParseAlpha2("uS", out var c), Is.True);
            Assert.That(c, Is.Not.Null);

            Assert.That(Country.USA, Is.EqualTo(c));
        }

        [Test]
        [Category("Interop")]
        [Category("Interop.Iso")]
        public void CountryParseAlpha2_Bad()
        {
            Assert.That(IsoHelpers.Country.TryParseAlpha2("asdf", out var c), Is.False);
            Assert.That(c, Is.Null);
        }
    }
}