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
    using Microsoft.Extensions.Logging;
    using NUnit.Framework;

    [TestFixture]
    [Category("Interop.Amazon")]
    [Category("Interop.Amazon.Timestream")]
    internal class Builder_TimeTests : BaseNUnitTest
    {

        [Test]
        public void AddTime_Twice()
        {
            var pb = new ProtocolBuilder<SingleMeasureWithTime>();
            pb.AddTime(x => x.Time, TimeUnitType.Seconds);
            var e = Assert.Throws<ArgumentException>(() => pb.AddTime(x => x.Time, TimeUnitType.Seconds));
        }

        [Test]
        public void Encode_DateTimeOffset_Seconds()
        {
            var pb = CommonBuilder();
            pb.AddTime(x => x.Time, TimeUnitType.Seconds);
            var p = pb.Build();

            var w = p.Encode(new SingleMeasureWithTime() { Name = "a", Time = DateTimeOffset.Now });
        }

        [Test]
        public void Encode_DateTimeOffset_Milliseconds()
        {
            var pb = CommonBuilder();
            pb.AddTime(x => x.Time, TimeUnitType.Milliseconds);
            var p = pb.Build();

            var w = p.Encode(new SingleMeasureWithTime() { Name = "a", Time = DateTimeOffset.Now });
        }

        [Test]
        [Ignore("incomplete")]
        public void Encode_DateTimeOffset_Microseconds()
        {
            var pb = CommonBuilder();
            pb.AddTime(x => x.Time, TimeUnitType.Microseconds);
            var p = pb.Build();

            var w = p.Encode(new SingleMeasureWithTime() { Name = "a", Time = DateTimeOffset.Now });
        }

        [Test]
        [Ignore("incomplete")]
        public void Encode_DateTimeOffset_Nanoseconds()
        {
            var pb = CommonBuilder();
            pb.AddTime(x => x.Time, TimeUnitType.Nanoseconds);
            var p = pb.Build();

            var w = p.Encode(new SingleMeasureWithTime() { Name = "a", Time = DateTimeOffset.Now });
        }

        [Test]
        public void Encode_NullableTime()
        {
            var pb = new ProtocolBuilder<MultiMeasureNonStringDimensionsTestWithTime>("metrics");
            pb.AddDimension(x => x.Name);
            pb.AddNullableMeasure(x => x.LongNullable);
            pb.AddNullableMeasure(x => x.NullableTime);
            pb.AddNullableTime(x => x.NullableTime, TimeUnitType.Milliseconds, x => x ?? DateTimeOffset.UtcNow);

            var options = new TimestreamOptions() { Database = "db", Table = "table" };
            var protocol = pb.Build();

            var m = new MultiMeasureNonStringDimensionsTestWithTime { Name = "asdf", LongNullable = 123 };

            var encoded = protocol.Encode(m, options);

            using var c = new TimestreamPublisher<MultiMeasureNonStringDimensionsTestWithTime>(options, pb.Build(), this.LoggerFactory.CreateLogger<PocoPublisher<MultiMeasureNonStringDimensionsTestWithTime>>());
        }

        private static ProtocolBuilder<SingleMeasureWithTime> CommonBuilder()
        {
            var pb = new ProtocolBuilder<SingleMeasureWithTime>();
            pb.AddDimension(x => x.Name);
            pb.AddMeasure(x => x.Value);
            return pb;
        }
    }
}
