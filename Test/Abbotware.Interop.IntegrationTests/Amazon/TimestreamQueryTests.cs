//-----------------------------------------------------------------------
// <copyright file="TimestreamBasicTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.IntegrationTests.Interop.Amazon
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.IntegrationTests.Interop.Amazon.TestClasses.Timestream;
    using Abbotware.Interop.Aws.Timestream;
    using Abbotware.Interop.Aws.Timestream.Configuration;
    using Abbotware.Interop.Aws.Timestream.Protocol;
    using Abbotware.Interop.Aws.Timestream.Protocol.Plugins;
    using Abbotware.Interop.Microsoft;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using global::Amazon.TimestreamQuery;
    using global::Amazon.TimestreamQuery.Model;
    using Microsoft.Extensions.Logging.Abstractions;
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
            var c = new AmazonTimestreamQueryClient();
            var r = new QueryRequest();
            r.QueryString = "select * from test-database.test-table limit 10";

            var q = await c.QueryAsync(r);
        }
   }
}