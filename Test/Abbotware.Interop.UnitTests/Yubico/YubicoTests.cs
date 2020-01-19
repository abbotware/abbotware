//-----------------------------------------------------------------------
// <copyright file="YubicoTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.UnitTests.Interop.Yubico
{
    using Abbotware.Interop.Yubico;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using global::NUnit.Framework;

    [TestFixture]
    public class YubicoTests : BaseNUnitTest
    {
        [Test]
        [Category("Interop")]
        [Category("Interop.Yubico")]
        public void GetOtpId()
        {
            Assert.AreEqual("ccccccggkthb", YubicoHelper.GetOtpId("ccccccggkthbkvrnclltrlijncurndkgvjubfkttfrre"));

            Assert.AreEqual("cccccclkcctj", YubicoHelper.GetOtpId("cccccclkcctjbvehrebjlttdlvhuehhudkbeijcbkdtf"));
        }
    }
}