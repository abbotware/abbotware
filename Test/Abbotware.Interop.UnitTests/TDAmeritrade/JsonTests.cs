//-----------------------------------------------------------------------
// <copyright file="JsonTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.UnitTests.Interop.TDAmeritrade
{
    using System.IO;
    using Abbotware.Interop.Newtonsoft;
    using Abbotware.Interop.TDAmeritrade.Models;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    public class JsonTests : BaseNUnitTest
    {
        [Test]
        [Category("Interop")]
        [Category("Interop.TDAmeritrade")]
        public void TeslaOptionChain()
        {
            var text = File.ReadAllText(Path.Combine("TDAmeritrade", "Samples", "tsla.option.json"));

            var oc = JsonHelper.FromString<OptionChain>(text);

            Assert.IsNotNull(oc);

            Assert.That(oc!.CallExpDateMap, Has.Count.EqualTo(2));
            Assert.That(oc!.PutExpDateMap, Has.Count.EqualTo(2));

            Assert.That(oc!.CallExpDateMap!["2023-06-23:4"], Has.Count.EqualTo(4));
            Assert.That(oc!.CallExpDateMap!["2023-06-30:11"], Has.Count.EqualTo(4));

            Assert.That(oc!.PutExpDateMap!["2023-06-23:4"], Has.Count.EqualTo(9));
            Assert.That(oc!.PutExpDateMap!["2023-06-30:11"], Has.Count.EqualTo(3));
        }
    }
}