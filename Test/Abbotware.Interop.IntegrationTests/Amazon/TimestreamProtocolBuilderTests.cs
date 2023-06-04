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
    using Abbotware.Interop.Aws.Timestream;
    using Abbotware.Interop.Aws.Timestream.Attributes;
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

            Assert.Throws<InvalidOperationException>(() => pb.AddMeasure(x => x.Name));
        }

        [Test]
        public void TimestreamBasic_AddDimensionTwice()
        {
            var pb = new ProtocolBuilder<SingleMeasureTest>();
            pb.AddDimension(x => x.Name);

            Assert.Throws<InvalidOperationException>(() => pb.AddDimension(x => x.Name));
        }

        [Test]
        public void TimestreamBasic_AddDuplicate()
        {
            var pb = new ProtocolBuilder<SingleMeasureTest>();
            pb.AddMeasure(x => x.Name);

            Assert.Throws<InvalidOperationException>(() => pb.AddDimension(x => x.Name));
        }

        [Test]
        public void TimestreamBasic_AddTimeDuplicate()
        {
            var pb = new ProtocolBuilder<MultiMeasureTestWithTime>();
            pb.AddTime(x => x.Time, TimeUnitType.Milliseconds);

            Assert.Throws<InvalidOperationException>(() => pb.AddTime(x => x.Time, TimeUnitType.Milliseconds));
        }

        [Test]
        public void TimestreamBasic_MultiMeasureBuilder()
        {
            var pb = new ProtocolBuilder<MultiMeasureTestWithTime>();
            pb.AddDimension(x => x.Name);
            pb.AddDimension(x => x.Company);
            pb.AddMeasure(x => x.ValueA);
            pb.AddMeasure(x => x.ValueB);
            pb.AddMeasure(x => x.ValueC);
            pb.AddMeasure(x => x.ValueD);
            pb.AddMeasure(x => x.ValueE);
            pb.AddMeasure(x => x.ValueF);
            pb.AddMeasure(x => x.ValueG);
            pb.AddMeasure(x => x.ValueH);
            pb.AddTime(x => x.Time, TimeUnitType.Milliseconds);

            var options = new TimestreamOptions() {Database = "db", Table = "table" };

            var protocol = pb.Build();

            var m = new MultiMeasureTestWithTime { Name = "asdf", Company = "asdfads", ValueA = 123, ValueB = 345, ValueC = 789, ValueD = "testing", ValueE = 123.23, ValueF = 12.345m, ValueG = DateTime.UtcNow, ValueH = false, Time = DateTime.UtcNow };

            var encoded = protocol.Encode(m, options);

            Assert.That(encoded.Records.Single().Dimensions.Single(x => x.Name == "Name").Value, Is.EqualTo(m.Name));
            Assert.That(encoded.Records.Single().Dimensions.Single(x => x.Name == "Company").Value, Is.EqualTo(m.Company));
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