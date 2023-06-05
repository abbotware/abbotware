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
    using Abbotware.Interop.Aws.Timestream;
    using Abbotware.Interop.Aws.Timestream.Attributes;
    using Abbotware.Interop.Aws.Timestream.Configuration;
    using Abbotware.Interop.Microsoft;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using global::Amazon.TimestreamWrite;
    using NUnit.Framework;

    [TestFixture]
    [Category("Interop.Amazon")]
    [Category("Interop.Amazon.Integration")]
    [Category("IntegrationTests")]
    public class TimestreamBasicTests : BaseNUnitTest
    {
        [Test]
        public async Task TimestreamBasic_SingleMeasureTest()
        {
            var options = ConfigurationHelper.AppSettingsJson(UnitTestSettingsFile).BindSection<TimestreamOptions>(TimestreamOptions.DefaultSection);
            using var c = new PocoPublisher<SingleMeasureTest>(options, this.Logger);

            var p = await c.PublishAsync(new SingleMeasureTest { Name = "asdf", Value = 123 }, default);

            Assert.That(p, Is.EqualTo(PublishStatus.Confirmed));
        }

        [Test]
        public async Task TimestreamBasic_MultiMeasureTest()
        {
            var options = ConfigurationHelper.AppSettingsJson(UnitTestSettingsFile).BindSection<TimestreamOptions>(TimestreamOptions.DefaultSection);
            using var c = new PocoPublisher<MultiMeasureTest>(options, this.Logger);

            var p = await c.PublishAsync(new MultiMeasureTest { Name = "asdf", Company = "asdfads", ValueA = 123, ValueB = 345, ValueC = 789, ValueD = "testing", ValueE = 123.23, ValueF = 12.345m, ValueG = DateTime.UtcNow, ValueH = false }, default);

            Assert.That(p, Is.EqualTo(PublishStatus.Confirmed));
        }

        [Test]
        public async Task TimestreamBasic_BatchMultiMeasureTest()
        {
            var options = ConfigurationHelper.AppSettingsJson(UnitTestSettingsFile).BindSection<TimestreamOptions>(TimestreamOptions.DefaultSection);
            using var c = new PocoPublisher<MultiMeasureTestWithTime>(options, this.Logger);

            var list = new List<MultiMeasureTestWithTime>();

            var t = DateTimeOffset.UtcNow;

            for (int i = 0; i < 100; ++i)
            {
                t = t.AddMilliseconds(1);
                list.Add(new MultiMeasureTestWithTime { Name = "asdf", Company = "asdfads", ValueA = 123 + i, ValueB = 345 + i, ValueC = 789 + i, ValueD = "testing", ValueE = 123.23 + i, ValueF = 12.345m + i, ValueG = DateTime.UtcNow, ValueH = false, Time = t });
            }

            var p = await c.PublishAsync(list, default);

            Assert.That(p, Is.EqualTo(PublishStatus.Confirmed));
        }

        public class SingleMeasureTest
        {
            [Dimension]
            public string Name { get; set; }

            [MeasureValue]
            public int Value { get; set; }
        }

        [MeasureName("Data")]
        public class MultiMeasureTest
        {
            [Dimension]
            public string Name { get; set; }

            [Dimension]
            public string Company { get; set; }

            [MeasureValue]
            public int ValueA { get; set; }

            [MeasureValue]
            public long? ValueB { get; set; }

            [MeasureValue]
            public long ValueC { get; set; }

            [MeasureValue]
            public string ValueD { get; set; }

            [MeasureValue]
            public double ValueE { get; set; }

            [MeasureValue]
            public decimal ValueF { get; set; }

            [MeasureValue]
            public DateTime ValueG { get; set; }

            [MeasureValue]
            public bool ValueH { get; set; }
        }

        [MeasureName("Data")]
        public class MultiMeasureTestWithTime
        {
            [Dimension]
            public string Name { get; set; }

            [Dimension]
            public string Company { get; set; }

            [MeasureValue]
            public int ValueA { get; set; }

            [MeasureValue]
            public long? ValueB { get; set; }

            [MeasureValue]
            public long ValueC { get; set; }

            [MeasureValue]
            public string ValueD { get; set; }

            [MeasureValue]
            public double ValueE { get; set; }

            [MeasureValue]
            public decimal ValueF { get; set; }

            [MeasureValue]
            public DateTime ValueG { get; set; }

            [MeasureValue]
            public bool ValueH { get; set; }

            [Time]
            public DateTimeOffset Time { get; set; }
        }
    }
}