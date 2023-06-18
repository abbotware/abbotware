//-----------------------------------------------------------------------
// <copyright file="TimestreamBasicTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.IntegrationTests.Interop.Amazon
{
    using System.Linq;
    using System.Threading.Tasks;
    using Abbotware.Interop.Aws.Timestream;
    using Abbotware.Interop.Aws.Timestream.Protocol.Plugins;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using global::Amazon.TimestreamQuery.Model;
    using Microsoft.Extensions.Logging;
    using NUnit.Framework;

    [TestFixture]
    [Category("Interop.Amazon")]
    [Category("Interop.Amazon.Integration")]
    [Category("IntegrationTests")]
    public class TimestreamQueryTests : BaseNUnitTest
    {
        [Test]
        public async Task POCO_TimestreamBasic_SingleMeasureTest()
        {
            var p = new QueryProtocol<Row>();
            using var client = new TimestreamReader<Row>(new(), p, this.LoggerFactory.CreateLogger<TimestreamReader<Row>>());

            var rows = await client.QueryAsync("select * from test-database.test-table limit 10", default).ToListAsync();
        }
    }
}