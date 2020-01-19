//-----------------------------------------------------------------------
// <copyright file="MacVendorsTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2016. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.IntegrationTests.Interop.MacVendors
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Interop.MacVendors.Plugins;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Interop")]
    [Category("Interop.MacVendors")]
    public class MacVendorsTests : BaseNUnitTest
    {
        [SetUp]
        public void Delay()
        {
            Thread.Sleep(1030);
        }

        [Test]
        public void Lookup_NonValid()
        {
            using var client = new MacVendorsClient(this.Logger);

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await client.LookupAsync("asdf", default));
        }

        [Test]
        public async Task Lookup_Valid()
        {
            using var client = new MacVendorsClient(this.Logger);

            var res = await client.LookupAsync("00-00-EA-79-C0-2D", default);

            Assert.AreEqual("UPNOD AB", res);
        }
    }
}