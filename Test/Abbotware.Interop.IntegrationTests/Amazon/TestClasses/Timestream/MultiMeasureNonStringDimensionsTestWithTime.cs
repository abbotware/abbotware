namespace Abbotware.IntegrationTests.Interop.Amazon.TestClasses.Timestream
{
    using System;

    public class MultiMeasureNonStringDimensionsTestWithTime
    {
        public string Name { get; set; }

        public string Company { get; set; }

        public string? Optional { get; set; }

        public string? SetOptional { get; set; }

        public long IdDimension { get; set; }

        public int Int { get; set; }

        public long? LongNullable { get; set; }

        public long Long { get; set; }

        public string String { get; set; }

        public double Double { get; set; }

        public decimal Decimal { get; set; }

        public DateTime DateTime { get; set; }

        public DateTime? DateTimeNullable { get; set; }

        public DateOnly DateOnly { get; set; }

        public DateOnly? DateOnlyNullable { get; set; }

        public bool Boolean { get; set; }

        public bool? BooleanNullable { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
