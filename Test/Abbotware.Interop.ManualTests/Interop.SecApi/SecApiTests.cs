//-----------------------------------------------------------------------
// <copyright file="YubicoTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.ManualTests.Interop.SecApi
{
    using System.Net;
    using System.Threading.Tasks;
    using Abbotware.Interop.SecApi;
    using Abbotware.Interop.SecApi.Configuration.Models;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("ManualTests")]
    [Category("Interop.SecApi")]
    [Category("Interop.SecApi.ManualTests")]
    public class SecApiTests : BaseNUnitTest
    {
        [Test]
        public async Task Verify_OK()
        {
            Assert.Inconclusive();

            var settings = new SecApiSettings();
            settings.ApiKey = "";

            using var client = new SecApiClient(settings, this.Logger);
            var res = await client.Mapping.CusipAsync("375558103", default);

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(res.Response, Is.Not.Null);
            Assert.That(res.Error, Is.Null);
        }
    }
}