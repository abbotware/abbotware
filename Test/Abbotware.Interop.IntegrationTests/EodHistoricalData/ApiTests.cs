//-----------------------------------------------------------------------
// <copyright file="ApiTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.IntegrationTests.Interop.EodHistoricalData
{
    using System;
    using System.Threading.Tasks;
    using Abbotware.Interop.EodHistoricalData;
    using Abbotware.Interop.EodHistoricalData.Configuration.Models;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Interop")]
    [Category("Interop.TDAmeritrade")]
    public class ApiTests : BaseNUnitTest
    {
        [Test]
        public async Task MarketHoursAsync_Weekend()
        {
            var settings = InitSettings();

            using var client = new EodHistoricalDataClient(settings, this.Logger);

            //var res = await client.MarketHours(new MarketType[] { MarketType.Bond }, new DateTime(2021, 8, 22), default)
            //    .ConfigureAwait(false);

            //Assert.IsNotNull(res);
            //Assert.IsNotNull(res.Response);
            //Assert.IsNull(res.Error);
            //Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        }

        private static EodHistoricalDataSettings InitSettings()
        {
            var apiKey = Environment.GetEnvironmentVariable("UNITTEST_EODHISTORICALDATA_APIKEY");

            var settings = new EodHistoricalDataSettings
            {
                ApiKey = apiKey,
            };

            return settings;
        }
    }
}