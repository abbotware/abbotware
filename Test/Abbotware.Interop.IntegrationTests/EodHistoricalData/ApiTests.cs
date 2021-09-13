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
        public async Task Fundamental_VTI_US()
        {
            var settings = InitSettings("OeAFFmMliFG5orCUuwAKQ8l4WWFQ67YX");

            using var client = new EodHistoricalDataClient(settings, this.Logger);

            var res = await client.Fundamentals.GetAsync("VTI", "US", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        }

        [Test]
        public async Task Fundamental_AAPL_US()
        {
            var settings = InitSettings("OeAFFmMliFG5orCUuwAKQ8l4WWFQ67YX");

            using var client = new EodHistoricalDataClient(settings, this.Logger);

            var res = await client.Fundamentals.GetAsync("AAPL", "US", default)
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

            var res = await client.Fundamentals.GeneralAsync("AAPL", "US", default)
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

            var res = await client.Fundamentals.HighlightsAsync("AAPL", "US", default)
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

            var res = await client.Fundamentals.EarningsAsync("AAPL", "US", default)
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

            var res = await client.Fundamentals.FinancialsAsync("AAPL", "US", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        }

        [Test]
        public async Task Fundamental_Valuation_AAPL_US()
        {
            var settings = InitSettings("OeAFFmMliFG5orCUuwAKQ8l4WWFQ67YX");

            using var client = new EodHistoricalDataClient(settings, this.Logger);

            var res = await client.Fundamentals.ValuationAsync("AAPL", "US", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        }

        [Test]
        public async Task Fundamental_SharesStats_AAPL_US()
        {
            var settings = InitSettings("OeAFFmMliFG5orCUuwAKQ8l4WWFQ67YX");

            using var client = new EodHistoricalDataClient(settings, this.Logger);

            var res = await client.Fundamentals.SharesStatsAsync("AAPL", "US", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        }

        [Test]
        public async Task Fundamental_Technicals_AAPL_US()
        {
            var settings = InitSettings("OeAFFmMliFG5orCUuwAKQ8l4WWFQ67YX");

            using var client = new EodHistoricalDataClient(settings, this.Logger);

            var res = await client.Fundamentals.TechnicalsAsync("AAPL", "US", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        }

        [Test]
        public async Task Fundamental_SplitsDividends_AAPL_US()
        {
            var settings = InitSettings("OeAFFmMliFG5orCUuwAKQ8l4WWFQ67YX");

            using var client = new EodHistoricalDataClient(settings, this.Logger);

            var res = await client.Fundamentals.SplitsDividendsAsync("AAPL", "US", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        }

        [Test]
        public async Task Fundamental_AnalystRatings_AAPL_US()
        {
            var settings = InitSettings("OeAFFmMliFG5orCUuwAKQ8l4WWFQ67YX");

            using var client = new EodHistoricalDataClient(settings, this.Logger);

            var res = await client.Fundamentals.AnalystRatingsAsync("AAPL", "US", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        }

        [Test]
        public async Task Fundamental_InsiderTransactions_AAPL_US()
        {
            var settings = InitSettings("OeAFFmMliFG5orCUuwAKQ8l4WWFQ67YX");

            using var client = new EodHistoricalDataClient(settings, this.Logger);

            var res = await client.Fundamentals.InsiderTransactionsAsync("AAPL", "US", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        }

        [Test]
        public async Task Fundamental_Holders_AAPL_US()
        {
            var settings = InitSettings("OeAFFmMliFG5orCUuwAKQ8l4WWFQ67YX");

            using var client = new EodHistoricalDataClient(settings, this.Logger);

            var res = await client.Fundamentals.HoldersAsync("AAPL", "US", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        }

        [Test]
        public async Task Fundamental_EsgScores_AAPL_US()
        {
            var settings = InitSettings("OeAFFmMliFG5orCUuwAKQ8l4WWFQ67YX");

            using var client = new EodHistoricalDataClient(settings, this.Logger);

            var res = await client.Fundamentals.EsgScoresAsync("AAPL", "US", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        }

        [Test]
        public async Task Fundamental_OutstandingShares_AAPL_US()
        {
            var settings = InitSettings("OeAFFmMliFG5orCUuwAKQ8l4WWFQ67YX");

            using var client = new EodHistoricalDataClient(settings, this.Logger);

            var res = await client.Fundamentals.OutstandingSharesAsync("AAPL", "US", default)
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