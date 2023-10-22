namespace Abbotware.IntegrationTests.Interop.Amazon.TestClasses.Timestream
{
    using System;
    using Abbotware.Interop.Aws.Timestream.Attributes;

    public class SingleMeasureWithTime
    {
        [Dimension]
        public string Name { get; set; }

        [Measure]
        public int Value { get; set; }

        [Time]
        public DateTimeOffset Time { get; set; }
    }
}
