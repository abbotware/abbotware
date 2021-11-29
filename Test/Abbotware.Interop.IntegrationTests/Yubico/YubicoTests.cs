//-----------------------------------------------------------------------
// <copyright file="YubicoTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.IntegrationTests.Interop.Yubico
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Abbotware.Interop.Yubico.Plugins;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using global::NUnit.Framework;

    [TestFixture]
    [Category("Interop")]
    [Category("Interop.Yubico")]
    public class YubicoTests : BaseNUnitTest
    {
        [Test]
        public async Task Verify_NotOK()
        {
            var user = Environment.GetEnvironmentVariable("UNITTEST_YUBICO_USERNAME");
            var pass = Environment.GetEnvironmentVariable("UNITTEST_YUBICO_PASSWORD");

            var credential = new NetworkCredential(user, pass);
            using var client = new YubicoClient(credential, this.Logger);

            var res = await client.VerifyAsync("ccscccggkthbfkghciijdkvvldebnevilllbufrrftek", default);
            Assert.IsFalse(res);

            res = await client.VerifyAsync(null, default).ConfigureAwait(false);
            Assert.IsFalse(res);

            res = await client.VerifyAsync(string.Empty, default).ConfigureAwait(false);
            Assert.IsFalse(res);

            res = await client.VerifyAsync("asdf", default).ConfigureAwait(false);
            Assert.IsFalse(res);
        }
    }
}