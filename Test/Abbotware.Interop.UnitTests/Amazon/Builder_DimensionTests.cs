namespace Abbotware.UnitTests.Interop.Amazon
{
    using System;
    using System.Linq;
    using Abbotware.IntegrationTests.Interop.Amazon.TestClasses.Timestream;
    using Abbotware.Interop.Aws.Timestream.Configuration;
    using Abbotware.Interop.Aws.Timestream.Protocol;
    using Abbotware.Interop.Aws.Timestream.Protocol.Plugins;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Interop.Amazon")]
    [Category("Interop.Amazon.Timestream")]
    internal class Builder_DimensionTests : BaseNUnitTest
    {
        [Test]
        public void Expression_AddTwice()
        {
            var pb = new ProtocolBuilder<SingleMeasureTest>();
            pb.AddDimension(x => x.Name);

            Assert.Throws<ArgumentException>(() => pb.AddDimension(x => x.Name));
        }

        [Test]
        public void Expression_AddTwice_Name()
        {
            var pb = new ProtocolBuilder<SingleMeasureTest>();
            pb.AddDimension(x => x.Name);
            Assert.Throws<ArgumentException>(() => pb.AddDimension("Name", x => x.Value));
        }

        [Test]
        public void Expression_AddTwice_OverrideName()
        {
            var pb = new ProtocolBuilder<SingleMeasureTest>();
            pb.AddDimension(x => x.Name);
            Assert.Throws<ArgumentException>(() => pb.AddDimension(x => x.Value, x => x.Name = "Name"));
        }

        [Test]
        public void Function_AddTwice()
        {
            var pb = new ProtocolBuilder<SingleMeasureTest>();
            pb.AddDimension("test", x => x.Name);

            Assert.Throws<ArgumentException>(() => pb.AddDimension("test", x => x.Name));
        }

        [Test]
        public void Function_AddTwice_OverrideName()
        {
            var pb = new ProtocolBuilder<SingleMeasureTest>();
            pb.AddDimension("test2", x => x.Name);

            Assert.Throws<ArgumentException>(() => pb.AddDimension("test3", x => x.Name, x => x.Name = "test2"));
        }

        [Test]
        public void Function_AddWithDifferentNames()
        {
            var pb = new ProtocolBuilder<SingleMeasureTest>();
            pb.AddDimension("test1", x => x.Name);
            pb.AddDimension("test2", x => x.Name);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void Encode_Empty_NullableDimension(string? value)
        {
            var pb = CommonBuilder();

            var options = new TimestreamOptions() { Database = "db", Table = "table" };
            var protocol = pb.Build();

            var m = new MultiMeasureNonStringDimensionsTestWithTime { Name = "abc", Optional = value, LongNullable = 123 };

            var w = protocol.Encode(m, options);

            Assert.That(w.Records.Single().Dimensions.Single(x => x.Name == "Name").Value, Is.EqualTo("abc"));
            Assert.That(w.Records.Single().Dimensions.SingleOrDefault(x => x.Name == "Optional"), Is.Null);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void Encode_Bad_Dimensions(string? value)
        {
            var pb = CommonBuilder();

            var options = new TimestreamOptions() { Database = "db", Table = "table" };
            var protocol = pb.Build();

            var m = new MultiMeasureNonStringDimensionsTestWithTime { Name = value!, Optional = "test", LongNullable = 123 };

            var ex = Assert.Catch<Exception>(() => protocol.Encode(m, options));

            StringAssert.StartsWith("Dimension:Name is null/empty string even after converter function was called. if this is expected, use a nullable dimension instead (Parameter 'Name')", ex!.Message);
        }

        [Test]
        public void Encode_Missing_Dimensions()
        {
            var pb = new ProtocolBuilder<MultiMeasureNonStringDimensionsTestWithTime>("metrics");
            pb.AddNullableDimension(x => x.Optional);
            pb.AddNullableMeasure(x => x.LongNullable);

            var options = new TimestreamOptions() { Database = "db", Table = "table" };
            var protocol = pb.Build();

            var m = new MultiMeasureNonStringDimensionsTestWithTime { LongNullable = 123 };

            var ex = Assert.Catch<Exception>(() => protocol.Encode(m, options));

            StringAssert.StartsWith("Record is missing dimension values (they might all be null?)", ex!.Message);
        }

        [Test]
        public void Encode_TooLong_Dimensions()
        {
            var pb = CommonBuilder();

            var options = new TimestreamOptions() { Database = "db", Table = "table" };
            var protocol = pb.Build();

            var m = new MultiMeasureNonStringDimensionsTestWithTime { Name = new string('a', 3000), LongNullable = 123 };

            var ex = Assert.Catch<Exception>(() => protocol.Encode(m, options));

            StringAssert.StartsWith("Dimension:Name length(3000) is too long - max length allowed is 2048 (Parameter 'Name')", ex!.Message);
        }

        private static ProtocolBuilder<MultiMeasureNonStringDimensionsTestWithTime> CommonBuilder()
        {
            var pb = new ProtocolBuilder<MultiMeasureNonStringDimensionsTestWithTime>();
            pb.AddDimension(x => x.Name);
            pb.AddNullableDimension(x => x.Optional);
            pb.AddNullableMeasure(x => x.LongNullable);
            return pb;
        }
    }
}
