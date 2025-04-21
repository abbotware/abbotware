namespace Abbotware.UnitTests.Interop.Amazon
{
    using System;
    using Abbotware.IntegrationTests.Interop.Amazon.TestClasses.Timestream;
    using Abbotware.Interop.Aws.Timestream.Protocol;
    using Abbotware.Interop.Aws.Timestream.Protocol.Plugins;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Interop.Amazon")]
    [Category("Interop.Amazon.Timestream")]
    internal class Builder_Tests : BaseNUnitTest
    {
        [Test]
        public void AddMesureDimension_SameName()
        {
            var pb = new ProtocolBuilder<SingleMeasureWithTime>();
            pb.AddMeasure(x => x.Name);

            Assert.Throws<ArgumentException>(() => pb.AddDimension(x => x.Name));
        }
    }
}
