//-----------------------------------------------------------------------
// <copyright file="Builder_MeasureTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.UnitTests.Interop.Amazon
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
    public class Builder_MeasureTests : BaseNUnitTest
    {
        [Test]
        public void Expression_AddTwice()
        {
            var pb = new ProtocolBuilder<SingleMeasureWithTime>();
            pb.AddMeasure(x => x.Name);

            Assert.Throws<ArgumentException>(() => pb.AddMeasure(x => x.Name));
        }

        [Test]
        public void Expression_AddTwice_Name()
        {
            var pb = new ProtocolBuilder<SingleMeasureWithTime>();
            pb.AddMeasure(x => x.Name);

            Assert.Throws<ArgumentException>(() => pb.AddMeasure("Name", x => x.Value));
        }

        [Test]
        public void Expression_AddTwice_OverrideName()
        {
            var pb = new ProtocolBuilder<SingleMeasureWithTime>();
            pb.AddMeasure(x => x.Name);

            Assert.Throws<ArgumentException>(() => pb.AddMeasure(x => x.Value, x => x.Name = "Name"));
        }

        [Test]
        public void Function_AddTwice()
        {
            var pb = new ProtocolBuilder<SingleMeasureWithTime>();
            pb.AddMeasure("test", x => x.Name);

            Assert.Throws<ArgumentException>(() => pb.AddMeasure("test", x => x.Name));
        }

        [Test]
        public void Function_AddTwice_OverrideName()
        {
            var pb = new ProtocolBuilder<SingleMeasureWithTime>();
            pb.AddMeasure("test2", x => x.Name);

            Assert.Throws<ArgumentException>(() => pb.AddMeasure("test3", x => x.Name, x => x.Name = "test2"));
        }

        [Test]
        public void Function_AddWithDifferentNames()
        {
            var pb = new ProtocolBuilder<SingleMeasureWithTime>("metrics");
            pb.AddMeasure("test1", x => x.Name);
            pb.AddMeasure("test2", x => x.Name);
        }

        [Test]
        public void MultiMeasure_MissingMeasureName()
        {
            var pb = new ProtocolBuilder<MultiMeasureNonStringDimensionsTestWithTime>();
            pb.AddMeasure(x => x.Int);

            Assert.Throws<ArgumentException>(() => pb.AddNullableMeasure(x => x.LongNullable));
        }

        [Test]
        public void MultiMeasure_Encode()
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
            pb.AddNullableMeasure(x => x.DateTimeNullable);
            pb.AddMeasure(x => x.DateOnly);
            pb.AddNullableMeasure(x => x.DateOnlyNullable);

            pb.AddMeasure(x => x.Boolean);
            pb.AddTime(x => x.Time, TimeUnitType.Milliseconds);

            var options = new TimestreamOptions() { Database = "db", Table = "table" };
            var protocol = pb.Build();

            var record_time = DateTime.UtcNow;
            var write_time = DateTimeOffset.UtcNow.AddDays(-1);

            var m = new MultiMeasureNonStringDimensionsTestWithTime
            {
                Name = "asdf",
                Company = "asdfads",
                SetOptional = "SetOptional",
                Int = 123,
                LongNullable = 345,
                Long = 789,
                String = "testing",
                Double = 123.23,
                Decimal = 12.345m,
                DateTime = record_time,
                Boolean = false,
                DateOnly = new DateOnly(11, 12, 14),
                Time = write_time,
            };

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

            Assert.That(encoded.Records.Single().MeasureValues.Single(x => x.Name == "Int").Value, Is.EqualTo("123"));
            Assert.That(encoded.Records.Single().MeasureValues.Single(x => x.Name == "LongNullable").Value, Is.EqualTo("345"));
            Assert.That(encoded.Records.Single().MeasureValues.Single(x => x.Name == "Long").Value, Is.EqualTo("789"));
            Assert.That(encoded.Records.Single().MeasureValues.Single(x => x.Name == "String").Value, Is.EqualTo("testing"));
            Assert.That(encoded.Records.Single().MeasureValues.Single(x => x.Name == "Double").Value, Is.EqualTo("123.23"));
            Assert.That(encoded.Records.Single().MeasureValues.Single(x => x.Name == "Decimal").Value, Is.EqualTo("12.345"));
            var gValue = new DateTimeOffset(record_time).ToUnixTimeMilliseconds().ToString();
            Assert.That(encoded.Records.Single().MeasureValues.Single(x => x.Name == "DateTime").Value, Is.EqualTo(gValue));
            var dateOnlyValue = new DateTimeOffset(11, 12, 14, 0, 0, 0, 0, TimeSpan.Zero).ToUnixTimeMilliseconds().ToString();
            Assert.That(encoded.Records.Single().MeasureValues.Single(x => x.Name == "DateOnly").Value, Is.EqualTo(dateOnlyValue));

            Assert.That(encoded.Records.Single().MeasureValues.Single(x => x.Name == "Boolean").Value, Is.EqualTo("False"));

            // Check Time
            var tValue = write_time.ToUnixTimeMilliseconds().ToString();
            Assert.That(encoded.Records.Single().Time, Is.EqualTo(tValue));
        }

        [Test]
        public void Encode_AllNullMeasures()
        {
            var pb = new ProtocolBuilder<MultiMeasureNonStringDimensionsTestWithTime>("metrics");
            pb.AddDimension(x => x.Name);
            pb.AddNullableMeasure(x => x.LongNullable);

            var options = new TimestreamOptions() { Database = "db", Table = "table" };
            var protocol = pb.Build();

            var m = new MultiMeasureNonStringDimensionsTestWithTime { Name = "asdf" };

            var ex = Assert.Catch<Exception>(() => protocol.Encode(m, options));

            StringAssert.StartsWith("Record is missing measure values (they might all be null?)", ex!.Message);
        }
    }
}