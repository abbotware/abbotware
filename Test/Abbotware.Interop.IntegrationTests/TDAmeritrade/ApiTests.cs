﻿//-----------------------------------------------------------------------
// <copyright file="ApiTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.IntegrationTests.Interop.TDAmeritrade
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Abbotware.Interop.TDAmeritrade;
    using Abbotware.Interop.TDAmeritrade.Configuration.Models;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Interop")]
    [Category("Interop.TDAmeritrade")]
    public class ApiTests : BaseNUnitTest
    {
        [Test]
        public async Task SearchAsync_SymbolRegex()
        {
            var settings = InitSettings();

            using var client = new TDAmeritradeClient(settings, this.Logger);

            var res = await client.SearchAsync("a.*", SearchType.SymbolRegex, default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
            Assert.Less(100, res.Response.Count);
        }

        [Test]
        public async Task FundamentalDataAsync_Exists()
        {
            var settings = InitSettings();

            using var client = new TDAmeritradeClient(settings, this.Logger);

            var res = await client.FundamentalDataAsync("AMDUF", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);

            Assert.AreEqual("AMDUF", res.Response.Symbol);
            Assert.IsNotNull(res.Response.Fundamental);
        }

        [Test]
        public async Task FundamentalDataAsync_DoesNotExist()
        {
            var settings = InitSettings();

            using var client = new TDAmeritradeClient(settings, this.Logger);

            var res = await client.FundamentalDataAsync("fdafasdfasd", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNull(res.Response);
            Assert.IsNotNull(res.Error);
            Assert.AreEqual(HttpStatusCode.NotFound, res.StatusCode);
            Assert.AreEqual("Not Found", res.Error.Error);
        }

        private static ApiSettings InitSettings()
        {
            var apiKey = Environment.GetEnvironmentVariable("UNITTEST_TDAMERITRADE_APIKEY");

            var settings = new ApiSettings
            {
                ApiKey = apiKey,
            };

            return settings;
        }
    }
}