//-----------------------------------------------------------------------
// <copyright file="InteropAmazonTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.IntegrationTests.Interop.Amazon
{
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.Core.Messaging.Integration.Base;
    using Abbotware.Interop.Aws.Sqs;
    using Abbotware.Interop.Aws.Sqs.Configuration.Models;
    using Abbotware.Interop.Aws.Sqs.Plugins;
    using Abbotware.Interop.ProtoBufNet.Plugins;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;
    using TimeoutAttribute = NUnit.Framework.TimeoutAttribute;

    [TestFixture]
    [Category("Interop.Amazon")]
    [Category("Interop.Amazon.Integration")]
    [Category("IntegrationTests")]
    public class SqsBasicTests : BaseNUnitTest
    {
        [OneTimeSetUp]
        public async Task Init()
        {
            await AmazonTestHelper.PurgeUnitTestQueue();
        }

        [Test]
        public void SqsBasic_CreateConnection()
        {
            using (var c = SqsHelper.CreateConnection(this.Logger, SqsSettings.DefaultSection, UnitTestSettingsFile))
            {
                Assert.IsNotNull(c);
            }

            var cfg = SqsHelper.GetSqsConfigurationFromFile(SqsSettings.DefaultSection, UnitTestSettingsFile);

            Assert.AreNotEqual("https://sqs.us-east-2.amazonaws.com/202810038399/dev-scans", cfg.Queue, "This can be ignored for local development");
        }

        [Test]
        [Timeout(20000)]
        public async Task SqsBasic_Consumer_NoMessages()
        {
            bool hitCode = false;

            using var c = SqsHelper.CreateConnection(this.Logger, SqsSettings.DefaultSection, UnitTestSettingsFile);
            using var consumer = c.CreateConsumer<SqsConsumer>();

            consumer.Initialize();

            Assert.AreEqual(ConsumerStatus.Running, consumer.Status);

            consumer.OnDelivery += (s, e) =>
            {
                    // No Messages, so should never hit this
                    hitCode = true;
            };

            await Task.Delay(15000);

            consumer.Dispose();

            await Task.Delay(500);

            Assert.AreEqual(ConsumerStatus.CancelRequested, consumer.Status);
            Assert.AreEqual(0u, consumer.Delivered);
            Assert.GreaterOrEqual(consumer.EmptyGets, 1u);
            Assert.False(hitCode);
        }

        [Test]
        [Timeout(45000)]
        public async Task SqsBasic_Consumer_WithProtocol_NoMessages()
        {
            var count = 0;
            var hitCode = false;

            using (var c = SqsHelper.CreateConnection(this.Logger, SqsSettings.DefaultSection, UnitTestSettingsFile))
            {
                using (var mq1 = new ActionConsumer<Scan>(x => { Interlocked.Increment(ref count); }, c.CreateRetriever(), new ProtoBufOverAmqp<Scan>(), c.CreateConsumer(), this.Logger))
                {
                    await Task.Delay(15000);
                }

                using (var mq2 = new ActionConsumer<Scan>(x => { Interlocked.Increment(ref count); }, c.CreateRetriever(), new ProtoBufOverAmqp<Scan>(), c.CreateConsumer(), this.Logger))
                {
                    mq2.Initialize();
                    await Task.Delay(15000);
                }

                hitCode = true;
            }

            Assert.AreEqual(0, count);
            Assert.IsTrue(hitCode);
        }
    }
}