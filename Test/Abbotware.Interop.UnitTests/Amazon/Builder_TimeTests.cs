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
            var pb = new ProtocolBuilder<SingleMeasureTest>();
            pb.AddTime(x => x.Time, TimeUnitType.Seconds);
            var e = Assert.Throws<ArgumentException>(() => pb.AddTime(x => x.Time, TimeUnitType.Seconds));
        }

        [Test]
        public void Encode_DateTimeOffset_Seconds()
        {
            var pb = CommonBuilder();
            pb.AddTime(x => x.Time, TimeUnitType.Seconds);
            var p = pb.Build();

            var w = p.Encode(new SingleMeasureTest() { Name = "a", Time = DateTimeOffset.Now });
        }

        [Test]
        public void Encode_DateTimeOffset_Milliseconds()
        {
            var pb = CommonBuilder();
            pb.AddTime(x => x.Time, TimeUnitType.Milliseconds);
            var p = pb.Build();

            var w = p.Encode(new SingleMeasureTest() { Name = "a", Time = DateTimeOffset.Now });
        }

        [Test]
        [Ignore("incomplete")]
        public void Encode_DateTimeOffset_Microseconds()
        {
            var pb = CommonBuilder();
            pb.AddTime(x => x.Time, TimeUnitType.Microseconds);
            var p = pb.Build();

            var w = p.Encode(new SingleMeasureTest() { Name = "a", Time = DateTimeOffset.Now });
        }

        [Test]
        [Ignore("incomplete")]
        public void Encode_DateTimeOffset_Nanoseconds()
        {
            var pb = CommonBuilder();
            pb.AddTime(x => x.Time, TimeUnitType.Nanoseconds);
            var p = pb.Build();

            var w = p.Encode(new SingleMeasureTest() { Name = "a", Time = DateTimeOffset.Now });
        }

        private static ProtocolBuilder<SingleMeasureTest> CommonBuilder()
        {
            var pb = new ProtocolBuilder<SingleMeasureTest>();
            pb.AddDimension(x => x.Name);
            pb.AddMeasure(x => x.Value);
            return pb;
        }
    }
}
