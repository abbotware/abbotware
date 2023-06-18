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
    }
}
