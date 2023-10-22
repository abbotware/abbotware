namespace Abbotware.IntegrationTests.Interop.Amazon.TestClasses.Timestream
{
    using System;
    using Abbotware.Interop.Aws.Timestream.Attributes;

    [MeasureName("Data")]
    public class MultiMeasureTest
    {
        [Dimension]
        public string Name { get; set; }

        [Dimension(Name = "Company2")]
        public string Company { get; set; }

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
    }
}
