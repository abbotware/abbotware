//-----------------------------------------------------------------------
// <copyright file="YubicoTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.ManualTests.Interop.Yubico
{
    using Abbotware.Interop.Yubico;
    using Abbotware.Interop.Yubico.Plugins;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("ManualTests")]
    [Category("Interop.Yubico")]
    [Category("Interop.Yubico.ManualTests")]
    public class YubicoTests : BaseNUnitTest
    {
        [Test]
        public void Verify_OK()
        {
            Assert.Inconclusive();

            ////var client = new ApiClient(this.Logger);

            ////var res = client.VerifyAsync("ccccccggkthbuuijnluffkkritbenjkjkkrglnrfuhir").Result;

            ////Assert.IsTrue(res);
        }
    }
}