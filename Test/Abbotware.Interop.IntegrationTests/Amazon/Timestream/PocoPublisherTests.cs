namespace Abbotware.IntegrationTests.Interop.Amazon.Timestream
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Abbotware.Core.Messaging.Integration;
    using Abbotware.IntegrationTests.Interop.Amazon.TestClasses.Timestream;
    using Abbotware.Interop.Aws.Timestream;
    using Abbotware.Interop.Aws.Timestream.Configuration;
    using Abbotware.Interop.Microsoft;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using Microsoft.Extensions.Logging;
    using NUnit.Framework;

    internal class PocoPublisherTests : BaseNUnitTest
    {
        [Test]
        public async Task SingleMeasureWithoutTime()
        {
            var options = ConfigurationHelper.AppSettingsJson(UnitTestSettingsFile).BindSection<TimestreamOptions>(TimestreamOptions.DefaultSection);
            using var c = new PocoPublisher<SingleMeasureWithoutTime>(options, this.LoggerFactory.CreateLogger<PocoPublisher<SingleMeasureWithoutTime>>());

            var p = await c.PublishAsync(new SingleMeasureWithoutTime { Name = Guid.NewGuid().ToString(), Value = 123 }, default);

            Assert.That(p, Is.EqualTo(PublishStatus.Confirmed));
        }

        [Test]
        public async Task NullDimension()
        {
            var options = ConfigurationHelper.AppSettingsJson(UnitTestSettingsFile).BindSection<TimestreamOptions>(TimestreamOptions.DefaultSection);
            using var c = new PocoPublisher<NullDimension>(options, this.LoggerFactory.CreateLogger<PocoPublisher<NullDimension>>());

            var p = await c.PublishAsync(new NullDimension { Name = Guid.NewGuid().ToString(), Value = 123, Time = DateTimeOffset.Now }, default);

            Assert.That(p, Is.EqualTo(PublishStatus.Confirmed));
        }

        [Test]
        public async Task MultiMeasureTest()
        {
            var options = ConfigurationHelper.AppSettingsJson(UnitTestSettingsFile).BindSection<TimestreamOptions>(TimestreamOptions.DefaultSection);
            using var c = new PocoPublisher<MultiMeasureTest>(options, this.LoggerFactory.CreateLogger<PocoPublisher<MultiMeasureTest>>());

            var p = await c.PublishAsync(new MultiMeasureTest { Name = Guid.NewGuid().ToString(), Company = "asdfads", ValueA = 123, ValueB = 345, ValueC = 789, ValueD = "testing", ValueE = 123.23, ValueF = 12.345m, ValueG = DateTime.UtcNow, ValueH = false }, default);

            Assert.That(p, Is.EqualTo(PublishStatus.Confirmed));
        }

        [Test]
        public async Task MultiMeasureStringDimensionsTestWithTime()
        {
            var options = ConfigurationHelper.AppSettingsJson(UnitTestSettingsFile).BindSection<TimestreamOptions>(TimestreamOptions.DefaultSection);
            using var c = new PocoPublisher<MultiMeasureStringDimensionsTestWithTime>(options, this.LoggerFactory.CreateLogger<PocoPublisher<MultiMeasureStringDimensionsTestWithTime>>());

            var list = new List<MultiMeasureStringDimensionsTestWithTime>();

            var t = DateTimeOffset.UtcNow;

            for (int i = 0; i < 100; ++i)
            {
                t = t.AddMilliseconds(1);
                list.Add(new MultiMeasureStringDimensionsTestWithTime { Name = Guid.NewGuid().ToString(), Company = "asdfads", ValueA = 123 + i, ValueB = 345 + i, ValueC = 789 + i, ValueD = "testing", ValueE = 123.23 + i, ValueF = 12.345m + i, ValueG = DateTime.UtcNow, ValueH = false, Time = t });
            }

            var p = await c.PublishAsync(list, default);

            Assert.That(p, Is.EqualTo(PublishStatus.Confirmed));
        }
    }
}
