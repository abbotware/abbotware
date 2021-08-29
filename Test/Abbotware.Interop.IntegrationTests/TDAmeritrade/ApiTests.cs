//-----------------------------------------------------------------------
// <copyright file="ApiTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.IntegrationTests.Interop.TDAmeritrade
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Abbotware.Interop.TDAmeritrade;
    using Abbotware.Interop.TDAmeritrade.Configuration.Models;
    using Abbotware.Interop.TDAmeritrade.Models;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Interop")]
    [Category("Interop.TDAmeritrade")]
    public class ApiTests : BaseNUnitTest
    {
        [Test]
        public async Task MarketHoursAsync_BadDate()
        {
            var settings = InitSettings();

            using var client = new TDAmeritradeClient(settings, this.Logger);

            var res = await client.MarketHours(new MarketType[] { MarketType.Bond }, DateTime.UtcNow.AddDays(-5), default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNull(res.Response);
            Assert.IsNotNull(res.Error);
            Assert.AreEqual(HttpStatusCode.BadRequest, res.StatusCode);
            Assert.AreEqual("Input date is not acceptable.", res.Error.Message);
        }

        [Test]
        public async Task MarketHoursAsync_Weekday_AllMarkets()
        {
            var settings = InitSettings();

            using var client = new TDAmeritradeClient(settings, this.Logger);

            var res = await client.MarketHours(new MarketType[] { MarketType.Bond, MarketType.Equity, MarketType.Option, MarketType.Future, MarketType.Forex }, DateTime.UtcNow, default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
            Assert.AreEqual(5, res.Response.Keys.Count());
        }

        [Test]
        public async Task PriceHistoryAsync_BadSymbol()
        {
            var settings = InitSettings();

            using var client = new TDAmeritradeClient(settings, this.Logger);

            var res = await client.PriceHistoryAsync("DoesNotExist", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
            Assert.AreEqual("DoesNotExist", res.Response.Symbol);
            Assert.IsTrue(res.Response.Empty);
        }

        [Test]
        public async Task PriceHistoryAsync_IBM()
        {
            var settings = InitSettings();

            using var client = new TDAmeritradeClient(settings, this.Logger);

            var res = await client.PriceHistoryAsync("IBM", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
            Assert.AreEqual("IBM", res.Response.Symbol);
            Assert.IsFalse(res.Response.Empty);
            Assert.Less(10, res.Response.Candles.Count);
        }

        [Test]
        public async Task PriceHistoryAsync_IBM_1Day_EveryMinute()
        {
            var settings = InitSettings();

            using var client = new TDAmeritradeClient(settings, this.Logger);

            var res = await client.PriceHistoryAsync("IBM", History.Days(HowManyDays.One, Minutes.One), false, default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
            Assert.AreEqual("IBM", res.Response.Symbol);
            Assert.IsFalse(res.Response.Empty);
            Assert.GreaterOrEqual(389, res.Response.Candles.Count);
        }

        [Test]
        public async Task PriceHistoryAsync_IBM_20Years_Weekly()
        {
            var settings = InitSettings();

            using var client = new TDAmeritradeClient(settings, this.Logger);

            var res = await client.PriceHistoryAsync("IBM", History.Years(HowManyYears.Twenty, Yearly.ByWeek), false, default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
            Assert.AreEqual("IBM", res.Response.Symbol);
            Assert.IsFalse(res.Response.Empty);
            Assert.AreEqual(1043, res.Response.Candles.Count);
        }

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
        public async Task FundamentalDataAsync_000850107()
        {
            var settings = InitSettings();

            using var client = new TDAmeritradeClient(settings, this.Logger);

            var res = await client.FundamentalDataAsync("000850107", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);

            Assert.AreEqual("ABP", res.Response.Fundamental.Symbol);
            Assert.IsNotNull(res.Response.Fundamental);
        }

        [Test]
        public async Task FundamentalDataAsync_ABP()
        {
            var settings = InitSettings();

            using var client = new TDAmeritradeClient(settings, this.Logger);

            var res = await client.FundamentalDataAsync("ABP", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);

            Assert.AreEqual("ABP", res.Response.Fundamental.Symbol);
            Assert.IsNotNull(res.Response.Fundamental);
        }

        [Test]
        public async Task FundamentalDataAsync_AA()
        {
            var settings = InitSettings();

            using var client = new TDAmeritradeClient(settings, this.Logger);

            var res = await client.FundamentalDataAsync("AA", default)
                .ConfigureAwait(false);

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.Response);
            Assert.IsNull(res.Error);
            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);

            Assert.AreEqual("AA", res.Response.Symbol);
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

        private static TDAmeritradeSettings InitSettings()
        {


            var apiKey = Environment.GetEnvironmentVariable("UNITTEST_TDAMERITRADE_APIKEY");

            var settings = new TDAmeritradeSettings
            {
                ApiKey = apiKey,
            };

            return settings;
        }
    }
}