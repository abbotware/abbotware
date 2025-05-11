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

    [Category("ManualTests")]
    [Category("Interop.SecApi")]
    [Category("Interop.SecApi.ManualTests")]
    public class SecApiTests : BaseNUnitTest
    {
        [Test]
        public async Task CusipAsync_OK()
        {
           // Assert.Inconclusive();

            var settings = new SecApiSettings();
            settings.ApiKey = "23dfbe1e40975ea165805035fde50bb9154da55d764746dde58f20a999fcf204";

            using var client = new SecApiClient(settings, this.Logger);
            var res = await client.Mapping.CusipAsync("375558103", default);

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(res.Response, Is.Not.Null);
            Assert.That(res.Error, Is.Null);
        }

        [Test]
        public async Task RawCusipAsync_OK()
        {
            // Assert.Inconclusive();

            var settings = new SecApiSettings();
            settings.ApiKey = "23dfbe1e40975ea165805035fde50bb9154da55d764746dde58f20a999fcf204";

            using var client = new SecApiClient(settings, this.Logger);
            var res = await client.Mapping.RawCusipAsync("375558103", default);

            Assert.That(res.Response, Is.EqualTo(res.RawResponse));
            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(res.Response, Is.Not.Null);
            Assert.That(res.Error, Is.Null);
        }

        [Test]
        public async Task BulkCusipToTickerAsync_OK()
        {
            // Assert.Inconclusive();

            var settings = new SecApiSettings();
            settings.ApiKey = "23dfbe1e40975ea165805035fde50bb9154da55d764746dde58f20a999fcf204";

            using var client = new SecApiClient(settings, this.Logger);
            var res = await client.Mapping.BulkCusipToTickerAsync("375558103", default);

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(res.Response, Is.Not.Null);
            Assert.That(res.Error, Is.Null);
        }
    }
}