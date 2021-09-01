//-----------------------------------------------------------------------
// <copyright file="ApiTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.IntegrationTests.Interop.EodHistoricalData
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Abbotware.Interop.EodHistoricalData;
    using Abbotware.Interop.EodHistoricalData.Configuration.Models;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Interop")]
    [Category("Interop.EodHistoricalData")]
    public class ApiTests : BaseNUnitTest
    {
        [Test]
        public async Task Fundamental_AAPL_US()
        {
            var settings = InitSettings("OeAFFmMliFG5orCUuwAKQ8l4WWFQ67YX");

            using var client = new EodHistoricalDataClient(settings, this.Logger);

            var res = await client.FundamentalAsync("AAPL", "US", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        }

        [Test]
        public async Task Fundamental_General_AAPL_US()
        {
            var settings = InitSettings("OeAFFmMliFG5orCUuwAKQ8l4WWFQ67YX");

            using var client = new EodHistoricalDataClient(settings, this.Logger);

            var res = await client.FundamentalGeneralAsync("AAPL", "US", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        }

        [Test]
        public async Task Fundamental_Highlights_AAPL_US()
        {
            var settings = InitSettings("OeAFFmMliFG5orCUuwAKQ8l4WWFQ67YX");

            using var client = new EodHistoricalDataClient(settings, this.Logger);

            var res = await client.FundamentalHighlightsAsync("AAPL", "US", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        }

        [Test]
        public async Task Fundamental_Earnings_AAPL_US()
        {
            var settings = InitSettings("OeAFFmMliFG5orCUuwAKQ8l4WWFQ67YX");

            using var client = new EodHistoricalDataClient(settings, this.Logger);

            var res = await client.FundamentalEarningsAsync("AAPL", "US", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        }

        [Test]
        public async Task Fundamental_Financials_AAPL_US()
        {
            var settings = InitSettings("OeAFFmMliFG5orCUuwAKQ8l4WWFQ67YX");

            using var client = new EodHistoricalDataClient(settings, this.Logger);

            var res = await client.FundamentalFinancialsAsync("AAPL", "US", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        }

        private static EodHistoricalDataSettings InitSettings(string overrideApikey)
        {
            var apiKey = Environment.GetEnvironmentVariable("UNITTEST_EODHISTORICALDATA_APIKEY");

            var settings = new EodHistoricalDataSettings
            {
                ApiKey = overrideApikey ?? apiKey,
            };

            return settings;
        }
    }
}