namespace Abbotware.IntegrationTests.Interop.Amazon.TestClasses.Timestream
{
    using System;
    using Abbotware.Interop.Aws.Timestream.Attributes;

    public class NullDimension
    {
        [Dimension]
        public string Name { get; set; }

        [Dimension]
        public string? Optional { get; set; }

        [MeasureValue]
        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
