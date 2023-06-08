using System;
using Abbotware.Interop.Aws.Timestream.Attributes;

namespace Abbotware.IntegrationTests.Interop.Amazon.TestClasses.Timestream
{

    public class MultiMeasureNonStringDimensionsTestWithTime
    {
        public string Name { get; set; }

        public string Company { get; set; }

        public string? Optional { get; set; }

        public string? SetOptional { get; set; }

        public long IdDimension { get; set; }

        public int ValueA { get; set; }

        public long? ValueB { get; set; }

        public long ValueC { get; set; }

        public string ValueD { get; set; }

        public double ValueE { get; set; }

        public decimal ValueF { get; set; }

        public DateTime ValueG { get; set; }

        public bool ValueH { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
