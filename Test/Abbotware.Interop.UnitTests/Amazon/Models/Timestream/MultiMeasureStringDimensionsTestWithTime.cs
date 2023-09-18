namespace Abbotware.IntegrationTests.Interop.Amazon.TestClasses.Timestream
{
    using System;
    using Abbotware.Interop.Aws.Timestream.Attributes;

    [MeasureName("Data")]
    public class MultiMeasureStringDimensionsTestWithTime
    {
        [Dimension]
        public string Name { get; set; }

        [Dimension]
        public string Company { get; set; }

        [Dimension]
        public string? Optional { get; set; }

        [Dimension]
        public string? SetOptional { get; set; }

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
