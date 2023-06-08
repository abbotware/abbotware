//-----------------------------------------------------------------------
// <copyright file="TimestreamBasicTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.IntegrationTests.Interop.Amazon
{
    using System;
    using System.Linq;
    using Abbotware.IntegrationTests.Interop.Amazon.TestClasses.Timestream;
    using Abbotware.Interop.Aws.Timestream;
    using Abbotware.Interop.Aws.Timestream.Configuration;
    using Abbotware.Interop.Aws.Timestream.Protocol;
    using Abbotware.Interop.Aws.Timestream.Protocol.Plugins;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Interop.Amazon")]
    [Category("Interop.Amazon.Integration")]
    [Category("IntegrationTests")]
    public class TimestreamProtocolBuilderTests : BaseNUnitTest
    {
        [Test]
        public void TimestreamBasic_AddMeasureTwice()
        {
            var pb = new ProtocolBuilder<SingleMeasureTest>();
            pb.AddMeasure(x => x.Name);

            Assert.Throws<ArgumentException>(() => pb.AddMeasure(x => x.Name));
        }

        [Test]
        public void TimestreamBasic_AddMeasureTwice_Configure()
        {
            var pb = new ProtocolBuilder<SingleMeasureTest>();
            pb.AddMeasure(x => x.Name);

            Assert.Throws<ArgumentException>(() => pb.AddMeasure(x => x.Value, x => x.Name = "Name"));
        }

        [Test]
        public void TimestreamBasic_AddDimensionTwice()
        {
            var pb = new ProtocolBuilder<SingleMeasureTest>();
            pb.AddDimension(x => x.Name);

            Assert.Throws<ArgumentException>(() => pb.AddDimension(x => x.Name));
        }

        [Test]
        public void TimestreamBasic_AddDuplicate()
        {
            var pb = new ProtocolBuilder<SingleMeasureTest>();
            pb.AddMeasure(x => x.Name);

            Assert.Throws<ArgumentException>(() => pb.AddDimension(x => x.Name));
        }

        [Test]
        public void TimestreamBasic_AddTimeDuplicate()
        {
            var pb = new ProtocolBuilder<MultiMeasureNonStringDimensionsTestWithTime>();
            pb.AddTime(x => x.Time, TimeUnitType.Milliseconds);

            Assert.Throws<ArgumentException>(() => pb.AddTime(x => x.Time, TimeUnitType.Milliseconds));
        }

        [Test]
        public void TimestreamBasic_MultiMeasureBuilder_NoMeasureName()
        {
            var pb = new ProtocolBuilder<MultiMeasureNonStringDimensionsTestWithTime>();
            pb.AddMeasure(x => x.ValueA);

            Assert.Throws<ArgumentException>(() => pb.AddMeasure(x => x.ValueB));
        }

        [Test]
        public void TimestreamBasic_MultiMeasureBuilder()
        {
            var pb = new ProtocolBuilder<MultiMeasureNonStringDimensionsTestWithTime>("metrics");
            pb.AddDimension(x => x.Name);
            pb.AddDimension(x => x.Company, x => x.Converter = y => y);
            pb.AddNullableDimension(x => x.Optional);
            pb.AddNullableDimension(x => x.SetOptional);
            pb.AddDimension(x => x.IdDimension, x => x.Converter = y => y.ToString());
            pb.AddMeasure(x => x.ValueA);
            pb.AddMeasure(x => x.ValueB);
            pb.AddMeasure(x => x.ValueC);
            pb.AddMeasure(x => x.ValueD);
            pb.AddMeasure(x => x.ValueE);
            pb.AddMeasure(x => x.ValueF);
            pb.AddMeasure(x => x.ValueG);
            pb.AddMeasure(x => x.ValueH);
            pb.AddTime(x => x.Time, TimeUnitType.Milliseconds);

            var options = new TimestreamOptions() { Database = "db", Table = "table" };
            var protocol = pb.Build();

            var record_time = DateTime.UtcNow;
            var write_time = DateTimeOffset.UtcNow.AddDays(-1);

            var m = new MultiMeasureNonStringDimensionsTestWithTime { Name = "asdf", Company = "asdfads", SetOptional = "SetOptional",  ValueA = 123, ValueB = 345, ValueC = 789, ValueD = "testing", ValueE = 123.23, ValueF = 12.345m, ValueG = record_time, ValueH = false, Time = write_time };

            var encoded = protocol.Encode(m, options);

            // Check Dimensions
            Assert.That(encoded.Records.Single().Dimensions.Single(x => x.Name == "Name").Value, Is.EqualTo(m.Name));
            Assert.That(encoded.Records.Single().Dimensions.Single(x => x.Name == "Company").Value, Is.EqualTo(m.Company));
            Assert.That(encoded.Records.Single().Dimensions.SingleOrDefault(x => x.Name == "Optional"), Is.Null);
            Assert.That(encoded.Records.Single().Dimensions.SingleOrDefault(x => x.Name == "SetOptional"), Is.Not.Null);
            Assert.That(encoded.Records.Single().Dimensions.Single(x => x.Name == "SetOptional").Value, Is.EqualTo(m.SetOptional));

            // Check Measures
            Assert.That(encoded.CommonAttributes.MeasureName, Is.EqualTo("metrics"));
            Assert.That(encoded.Records.Single().MeasureValueType.Value, Is.EqualTo("MULTI"));

            Assert.That(encoded.Records.Single().MeasureValues.Single(x => x.Name == "ValueA").Value, Is.EqualTo("123"));
            Assert.That(encoded.Records.Single().MeasureValues.Single(x => x.Name == "ValueB").Value, Is.EqualTo("345"));
            Assert.That(encoded.Records.Single().MeasureValues.Single(x => x.Name == "ValueC").Value, Is.EqualTo("789"));
            Assert.That(encoded.Records.Single().MeasureValues.Single(x => x.Name == "ValueD").Value, Is.EqualTo("testing"));
            Assert.That(encoded.Records.Single().MeasureValues.Single(x => x.Name == "ValueE").Value, Is.EqualTo("123.23"));
            Assert.That(encoded.Records.Single().MeasureValues.Single(x => x.Name == "ValueF").Value, Is.EqualTo("12.345"));
            var gValue = new DateTimeOffset(record_time).ToUnixTimeMilliseconds().ToString();
            Assert.That(encoded.Records.Single().MeasureValues.Single(x => x.Name == "ValueG").Value, Is.EqualTo(gValue));
            Assert.That(encoded.Records.Single().MeasureValues.Single(x => x.Name == "ValueH").Value, Is.EqualTo("False"));

            // Check Time
            var tValue = write_time.ToUnixTimeMilliseconds().ToString();
            Assert.That(encoded.Records.Single().Time, Is.EqualTo(tValue));
        }
    }
}