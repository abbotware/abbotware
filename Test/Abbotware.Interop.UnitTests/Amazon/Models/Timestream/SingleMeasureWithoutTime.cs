namespace Abbotware.IntegrationTests.Interop.Amazon.TestClasses.Timestream
{
    using Abbotware.Interop.Aws.Timestream.Attributes;

    public class SingleMeasureWithoutTime
    {
        [Dimension]
        public string Name { get; set; }

        [Measure]
        public int Value { get; set; }
    }
}
