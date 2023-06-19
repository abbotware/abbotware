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
    using NUnit.Framework.Internal;

    [TestFixture]
    [Category("Interop.Amazon")]
    [Category("Interop.Amazon.Integration")]
    [Category("IntegrationTests")]
    public class TimestreamQueryTests : BaseNUnitTest
    {
        [Test]
        public async Task TimestreamReader_Row()
        {
            var p = new QueryProtocol<Row>();
            using var client = new TimestreamReader<Row>(new(), p, this.LoggerFactory.CreateLogger<TimestreamReader<Row>>());

            var query = @"SELECT* FROM ""test-database"".""test-table"" ORDER BY time DESC LIMIT 10";

            var rows = await client.QueryAsync(query, default).ToListAsync();

            Assert.That(rows, Has.Count.EqualTo(10));
        }

        [Test]
        public async Task TimestreamReader_Metric()
        {
            var p = new QueryProtocol<Metric>();
            using var client = new TimestreamReader<Metric>(new(), p, this.LoggerFactory.CreateLogger<TimestreamReader<Metric>>());

            var query = @"SELECT* FROM ""test-database"".""test-table"" ORDER BY time DESC LIMIT 10";

            var rows = await client.QueryAsync(query, default).ToListAsync();

            Assert.That(rows, Has.Count.EqualTo(10));
        }
    }
}