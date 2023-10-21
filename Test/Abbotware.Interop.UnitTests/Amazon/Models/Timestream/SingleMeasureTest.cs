namespace Abbotware.IntegrationTests.Interop.Amazon.TestClasses.Timestream
{
    using System;
    using Abbotware.Interop.Aws.Timestream.Attributes;

    public class SingleMeasureTest
    {
        [Dimension]
        public string Name { get; set; }

        [MeasureValue]
        public int Value { get; set; }

        [Time]
        public DateTimeOffset Time { get; set; }
    }
}
