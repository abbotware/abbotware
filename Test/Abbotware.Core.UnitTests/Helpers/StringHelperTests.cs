namespace Abbotware.UnitTests.Core
{
    using Abbotware.Core.Helpers;
    using NUnit.Framework;
    using Assert = Abbotware.Interop.NUnit.LegacyAssert;

    [TestFixture]
    [Category("Core")]
    [Category("Core.Helpers")]
    public class StringHelperTests
    {
        [Test]
        public void JoinWithoutEmpty_Usage()
        {
            var t1 = StringHelper.JoinWithoutEmpty(string.Empty, null);
            Assert.AreEqual(string.Empty, t1);

            var t2 = StringHelper.JoinWithoutEmpty("test", null);
            Assert.AreEqual("test", t2);

            var t3 = StringHelper.JoinWithoutEmpty("test", null, "test1", null);
            Assert.AreEqual("test,test1", t3);
        }
    }
}