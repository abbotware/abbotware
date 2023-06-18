//-----------------------------------------------------------------------
// <copyright file="TimestreamBasicTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.IntegrationTests.Interop.Amazon
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Abbotware.Interop.Aws.Timestream;
    using Abbotware.Interop.Aws.Timestream.Protocol.Plugins;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using global::Amazon.TimestreamQuery.Model;
    using Microsoft.Extensions.Logging;
    using NUnit.Framework;
    using NUnit.Framework.Internal;
    using StackExchange.Redis;

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

            var query = @"SELECT* FROM ""test-database"".""test-table"" ORDER BY time DESC LIMIT 10";

            var rows = await client.QueryAsync(query, default).ToListAsync();

            Assert.That(rows, Has.Count.EqualTo(10));
        }
    }
}