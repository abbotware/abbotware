﻿namespace Abbotware.IntegrationTests.Interop.Amazon.TestClasses.Timestream
{
    using System;
    using Abbotware.Interop.Aws.Timestream.Attributes;

    [MeasureName("metrics")]
    public class NullDimension
    {
        [Dimension]
        public string Name { get; set; }

        [Dimension]
        public string? Optional { get; set; }

        [Measure]
        public int Value { get; set; }

        [Measure]
        public int? ValueB { get; set; }

        [Time]
        public DateTimeOffset Time { get; set; }
    }
}
