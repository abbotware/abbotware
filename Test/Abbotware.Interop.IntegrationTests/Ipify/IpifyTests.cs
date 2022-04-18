//-----------------------------------------------------------------------
// <copyright file="IpifyTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.IntegrationTests.Interop.Ipify
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Abbotware.Core.Net;
    using Abbotware.Interop.Ipify.Plugins;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Interop")]
    [Category("Interop.Ipify")]
    public class IpifyTests : BaseNUnitTest
    {
        [Test]
        public async Task Ipify_Verify_OK()
        {
            var address = Environment.GetEnvironmentVariable("UNITTEST_IPIFY_ADDRESS");
            var ip = IPAddress.Parse(address!);

            using IGetInternetAddress client = new IpifyClient(this.Logger);

            var res = await client.GetInternetAddressAsync(default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);

            if (ip != res)
            {
                Assert.Inconclusive();
            }
            else
            {
                Assert.AreEqual(ip, res);
            }
        }
    }
}