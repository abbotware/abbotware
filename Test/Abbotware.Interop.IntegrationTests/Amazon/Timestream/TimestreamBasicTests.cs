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
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using NUnit.Framework;

    [TestFixture]
    [Category("Interop.Amazon")]
    [Category("Interop.Amazon.Integration")]
    [Category("IntegrationTests")]
    public class TimestreamBasicTests : BaseNUnitTest
    {
       

        [Test]
        public async Task Builder_NullDimension()
        {
            var pb = new ProtocolBuilder<NullDimension>("metrics");
            pb.AddDimension(x => x.Name);
            pb.AddNullableDimension(x => x.Optional);
            pb.AddMeasure(x => x.Value);
            pb.AddNullableMeasure(x => x.ValueB);

            var options = ConfigurationHelper.AppSettingsJson(UnitTestSettingsFile).BindSection<TimestreamOptions>(TimestreamOptions.DefaultSection);
            using var c = new TimestreamPublisher<NullDimension>(options, pb.Build(), this.LoggerFactory.CreateLogger<PocoPublisher<NullDimension>>());

            var p = await c.PublishAsync(new NullDimension { Name = Guid.NewGuid().ToString(), Value = 1 }, default);

            Assert.That(p, Is.EqualTo(PublishStatus.Confirmed));
        }

        [Test]
        public async Task Builder_Batch_NullDimensionNullMeasure()
        {
            var pb = new ProtocolBuilder<NullDimension>("metrics");
            pb.AddDimension(x => x.Name);
            pb.AddNullableDimension(x => x.Optional);
            pb.AddMeasure(x => x.Value);
            pb.AddNullableMeasure(x => x.ValueB);
            pb.AddTime(x => x.Time, TimeUnitType.Milliseconds);

            var options = ConfigurationHelper.AppSettingsJson(UnitTestSettingsFile).BindSection<TimestreamOptions>(TimestreamOptions.DefaultSection);
            using var c = new TimestreamPublisher<NullDimension>(options, pb.Build(), this.LoggerFactory.CreateLogger<PocoPublisher<NullDimension>>());

            var list = new List<NullDimension>();

            var t = DateTimeOffset.UtcNow;

            for (int i = 0; i < 100; ++i)
            {
                list.Add(new NullDimension { Name = Guid.NewGuid().ToString(), Value = i, Time = t });
            }

            var p = await c.PublishAsync(list, default);

            Assert.That(p, Is.EqualTo(PublishStatus.Confirmed));
        }

        [Test]
        public async Task Builder_Batch_MultiMeasureStringDimensionsTestWithTime()
        {
            var pb = new ProtocolBuilder<MultiMeasureStringDimensionsTestWithTime>("metrics");
            pb.AddDimension(x => x.Name);
            pb.AddDimension(x => x.Company, x => x.Converter = y => y);
            pb.AddNullableDimension(x => x.Optional);
            pb.AddNullableDimension(x => x.SetOptional);
            pb.AddMeasure(x => x.ValueA);
            pb.AddNullableMeasure(x => x.ValueB);
            pb.AddMeasure(x => x.ValueC);
            pb.AddMeasure(x => x.ValueD);
            pb.AddMeasure(x => x.ValueE);
            pb.AddMeasure(x => x.ValueF);
            pb.AddMeasure(x => x.ValueG);
            pb.AddMeasure(x => x.ValueH);
            pb.AddTime(x => x.Time, TimeUnitType.Milliseconds);

            var options = ConfigurationHelper.AppSettingsJson(UnitTestSettingsFile).BindSection<TimestreamOptions>(TimestreamOptions.DefaultSection);
            using var c = new TimestreamPublisher<MultiMeasureStringDimensionsTestWithTime>(options, pb.Build(), NullLogger<TimestreamPublisher<MultiMeasureStringDimensionsTestWithTime>>.Instance);

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

        [Test]
        public async Task Builder_Batch_MultiMeasureNonStringDimensionsTestWithTime()
        {
            var pb = new ProtocolBuilder<MultiMeasureNonStringDimensionsTestWithTime>("metrics");
            pb.AddDimension(x => x.Name);
            pb.AddDimension(x => x.Company, x => x.Converter = y => y);
            pb.AddNullableDimension(x => x.Optional);
            pb.AddNullableDimension(x => x.SetOptional);
            pb.AddDimension(x => x.IdDimension, x => x.Converter = y => y.ToString());
            pb.AddMeasure(x => x.Int);
            pb.AddNullableMeasure(x => x.LongNullable);
            pb.AddMeasure(x => x.Long);
            pb.AddMeasure(x => x.String);
            pb.AddMeasure(x => x.Double);
            pb.AddMeasure(x => x.Decimal);
            pb.AddMeasure(x => x.DateTime);
            pb.AddMeasure(x => x.Boolean);
            pb.AddTime(x => x.Time, TimeUnitType.Milliseconds);

            var options = ConfigurationHelper.AppSettingsJson(UnitTestSettingsFile).BindSection<TimestreamOptions>(TimestreamOptions.DefaultSection);
            using var c = new TimestreamPublisher<MultiMeasureNonStringDimensionsTestWithTime>(options, pb.Build(), NullLogger<TimestreamPublisher<MultiMeasureNonStringDimensionsTestWithTime>>.Instance);

            var list = new List<MultiMeasureNonStringDimensionsTestWithTime>();

            var t = DateTimeOffset.UtcNow;

            for (int i = 0; i < 100; ++i)
            {
                t = t.AddMilliseconds(1);
                list.Add(new MultiMeasureNonStringDimensionsTestWithTime { Name = Guid.NewGuid().ToString(), Company = "asdfads", Int = 123 + i, LongNullable = 345 + i, Long = 789 + i, String = "testing", Double = 123.23 + i, Decimal = 12.345m + i, DateTime = DateTime.UtcNow, Boolean = false, Time = t });
            }

            var p = await c.PublishAsync(list, default);

            Assert.That(p, Is.EqualTo(PublishStatus.Confirmed));
        }

        [Test]
        public async Task Builder_LargeData_MultiMeasureNonStringDimensionsTestWithTime()
        {
            var pb = new ProtocolBuilder<MultiMeasureNonStringDimensionsTestWithTime>("metrics");
            pb.AddDimension(x => x.Name);
            pb.AddDimension(x => x.Company, x => x.Converter = y => y);
            pb.AddNullableDimension(x => x.Optional);
            pb.AddNullableDimension(x => x.SetOptional);
            pb.AddDimension(x => x.IdDimension, x => x.Converter = y => y.ToString());
            pb.AddMeasure(x => x.Int);
            pb.AddNullableMeasure(x => x.LongNullable);
            pb.AddMeasure(x => x.Long);
            pb.AddMeasure(x => x.String);
            pb.AddMeasure(x => x.String2);
            pb.AddMeasure(x => x.String3);
            pb.AddMeasure(x => x.Double);
            pb.AddMeasure(x => x.Decimal);
            pb.AddMeasure(x => x.DateTime);
            pb.AddMeasure(x => x.Boolean);
            pb.AddTime(x => x.Time, TimeUnitType.Milliseconds);

            var options = ConfigurationHelper.AppSettingsJson(UnitTestSettingsFile).BindSection<TimestreamOptions>(TimestreamOptions.DefaultSection);
            using var c = new TimestreamPublisher<MultiMeasureNonStringDimensionsTestWithTime>(options, pb.Build(), NullLogger<TimestreamPublisher<MultiMeasureNonStringDimensionsTestWithTime>>.Instance);

            var list = new List<MultiMeasureNonStringDimensionsTestWithTime>();

            var t = DateTimeOffset.UtcNow;

            var a = new string('a', 1000);
            var b = new string('a', 5000);

            var r = new MultiMeasureNonStringDimensionsTestWithTime { Name = a, Company = a, Int = 123, LongNullable = 345, Long = 789, String = b, String2 = b, String3 = b, Double = 123.23, Decimal = 12.345m, DateTime = DateTime.UtcNow, Boolean = false, Time = t };

            var p = await c.PublishAsync(r, default);

            Assert.That(p, Is.EqualTo(PublishStatus.Confirmed));
        }

        [Test]
        public async Task Buffered_Builder_Batch_MultiMeasureNonStringDimensionsTestWithTime()
        {
            var pb = new ProtocolBuilder<MultiMeasureNonStringDimensionsTestWithTime>("metrics");
            pb.AddDimension(x => x.Name);
            pb.AddDimension(x => x.Company, x => x.Converter = y => y);
            pb.AddNullableDimension(x => x.Optional);
            pb.AddNullableDimension(x => x.SetOptional);
            pb.AddDimension(x => x.IdDimension, x => x.Converter = y => y.ToString());
            pb.AddMeasure(x => x.Int);
            pb.AddNullableMeasure(x => x.LongNullable);
            pb.AddMeasure(x => x.Long);
            pb.AddMeasure(x => x.String);
            pb.AddMeasure(x => x.Double);
            pb.AddMeasure(x => x.Decimal);
            pb.AddMeasure(x => x.DateTime);
            pb.AddMeasure(x => x.Boolean);
            pb.AddTime(x => x.Time, TimeUnitType.Milliseconds);

            var options = ConfigurationHelper.AppSettingsJson(UnitTestSettingsFile).BindSection<TimestreamOptions>(TimestreamOptions.DefaultSection);
            using var c = new BufferedTimestreamPublisher<MultiMeasureNonStringDimensionsTestWithTime>(options, pb.Build(), NullLoggerFactory.Instance, NullLogger<TimestreamPublisher<MultiMeasureNonStringDimensionsTestWithTime>>.Instance);

            var list = new List<MultiMeasureNonStringDimensionsTestWithTime>();

            var t = DateTimeOffset.UtcNow;

            for (int i = 0; i < 101; ++i)
            {
                t = t.AddMilliseconds(1);
                list.Add(new MultiMeasureNonStringDimensionsTestWithTime { Name = Guid.NewGuid().ToString(), Company = "asdfads", Int = 123 + i, LongNullable = 345 + i, Long = 789 + i, String = "testing", Double = 123.23 + i, Decimal = 12.345m + i, DateTime = DateTime.UtcNow, Boolean = false, Time = t });
            }

            var p = await c.PublishAsync(list, default);

            await Task.Delay(TimeSpan.FromSeconds(.5));
            Assert.That(c.RecordsPublished, Is.EqualTo(100));
            Assert.That(c.RecordsNotIngested, Is.EqualTo(0));

            await Task.Delay(TimeSpan.FromSeconds(.5));
            Assert.That(c.RecordsPublished, Is.EqualTo(101));
            Assert.That(c.RecordsNotIngested, Is.EqualTo(101));

            this.BlockIfDebugging();
        }
    }
}