using Abbotware.Interop.Aws.Timestream.Attributes;

namespace Abbotware.IntegrationTests.Interop.Amazon.TestClasses.Timestream
{
    public class SingleMeasureTest
    {
        [Dimension]
        public string Name { get; set; }

        [MeasureValue]
        public int Value { get; set; }
    }
}
