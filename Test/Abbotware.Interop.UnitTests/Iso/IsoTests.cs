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
            var t = CountryMetadataLookup.Instance;

            var us = t.Lookup(Country.USA);

            Assert.AreSame(us, t[us.Id]);
            Assert.AreSame(us, t.Lookup((ushort)us.Id));
            Assert.AreSame(us, t.LookupAlpha3("USA"));
            Assert.AreSame(us, t.LookupAlpha2("US"));
        }

        [Test]
        [Category("Interop")]
        [Category("Interop.Iso")]
        public void CurrencyLookup()
        {
            var t = CurrencyMetadataLookup.Instance;

            var us = t.Lookup(Currency.USD);

            Assert.AreSame(us, t[us.Id]);
            Assert.AreSame(us, t.Lookup((ushort)us.Id));
            Assert.AreSame(us, t.LookupAlpha("USD"));
        }
    }
}