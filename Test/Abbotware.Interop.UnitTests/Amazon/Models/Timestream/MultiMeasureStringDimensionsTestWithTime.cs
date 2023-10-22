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

        [Measure]
        public int ValueA { get; set; }

        [Measure]
        public long? ValueB { get; set; }

        [Measure]
        public long ValueC { get; set; }

        [Measure]
        public string ValueD { get; set; }

        [Measure]
        public double ValueE { get; set; }

        [Measure]
        public decimal ValueF { get; set; }

        [Measure]
        public DateTime ValueG { get; set; }

        [Measure]
        public bool ValueH { get; set; }

        [Time]
        public DateTimeOffset Time { get; set; }
    }
}
