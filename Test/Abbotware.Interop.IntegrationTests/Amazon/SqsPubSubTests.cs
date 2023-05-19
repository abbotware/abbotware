//-----------------------------------------------------------------------
// <copyright file="InteropAmazonTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.IntegrationTests.Interop.Amazon
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Messaging.Integration.Amqp.Plugins;
    using Abbotware.Core.Messaging.Integration.Base;
    using Abbotware.Interop.Aws.Sqs;
    using Abbotware.Interop.Aws.Sqs.Configuration.Models;
    using Abbotware.Interop.Aws.Sqs.Plugins;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;
    using TimeoutAttribute = NUnit.Framework.TimeoutAttribute;

    [TestFixture]
    [Category("Interop.Amazon")]
    [Category("Interop.Amazon.Integration")]
    [Category("IntegrationTests")]
    public class SqsPubSubTests : BaseNUnitTest
    {
        [SetUp]
        public async Task Init()
        {
            await AmazonTestHelper.PurgeUnitTestQueue();
        }

        [Test]
        [Obsolete("this unit test is ok for now, but 'StringPublishAsync' + 'StringRetrieveAsync' are deprecated")]
        [Timeout(180000)]
        public async Task SqsPubSub_Pub_Get_String()
        {
            using var c = SqsHelper.CreateConnection(this.Logger, SqsSettings.DefaultSection, UnitTestSettingsFile);

            var payload = Guid.NewGuid().ToString();

            using (var pub = c.CreatePublisher<SqsPublisher>())
            {
                var result = await pub.StringPublishAsync(payload);
                Assert.AreEqual(PublishStatus.Confirmed, result);
            }

            using var ret = c.CreateRetriever<SqsRetriever>();

            var res = await ret.StringRetrieveAsync();
            Assert.AreEqual(1, res.Count());
            Assert.AreEqual(payload, res.Single());
        }

        [Test]
        [Timeout(180000)]
        [Obsolete("this unit test is ok for now, but 'StringRetrieveAsync' is deprecated")]
        public async Task SqsPubSub_TypedPublisher_StringRetrieve()
        {
            using var c = SqsHelper.CreateConnection(this.Logger, SqsSettings.DefaultSection, UnitTestSettingsFile);

            var payload = Guid.NewGuid().ToString();

            using (var p = c.CreatePublisher<SqsPublisher>())
            using (var pub = new Publisher<string>(p.Configuration.Queue.ToString(), p, new Strings(), this.Logger))
            {
                var result = await pub.PublishAsync(payload);
                Assert.AreEqual(PublishStatus.Confirmed, result);
            }

            using var ret = c.CreateRetriever<SqsRetriever>();
            var res = await ret.StringRetrieveAsync();
            Assert.AreEqual(1, res.Count());
            Assert.AreEqual(payload, res.Single());
        }

        [Test]
        [Timeout(180000)]
        [Obsolete("this unit test is ok for now, but 'ProtoRetrieveAsync' + 'ProtoPublishAsync' are deprecated")]
        public async Task SqsPubSub_Pub_Get_ProtoBuf()
        {
            using var c = SqsHelper.CreateConnection(this.Logger, SqsSettings.DefaultSection, UnitTestSettingsFile);

            var testData = new Scan();
            testData.Metrics.Add(new Metric { Id = MetricTypeId.PingRoundtrip });
            testData.Metrics.Add(new Metric { Id = MetricTypeId.ProcessUptime });

            using (var p = c.CreatePublisher<SqsPublisher>())
            {
                var result = await p.ProtoPublishAsync(testData);

                Assert.AreEqual(PublishStatus.Confirmed, result);
            }

            using var ret = c.CreateRetriever<SqsRetriever>();

            var res = await ret.ProtoRetrieveAsync<Scan>();

            Assert.AreEqual(1, res.Count());
            var s = res.Single();

            Assert.AreEqual(2, s.Metrics.Count);
            Assert.IsNotNull(s.Metrics.SingleOrDefault(x => x.Id == MetricTypeId.PingRoundtrip));
            Assert.IsNotNull(s.Metrics.SingleOrDefault(x => x.Id == MetricTypeId.ProcessUptime));
        }
    }
}