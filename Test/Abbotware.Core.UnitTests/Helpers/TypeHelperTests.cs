namespace Abbotware.UnitTests.Core
{
    using Abbotware.Core.Helpers;
    using NUnit.Framework;

    [TestFixture]
    [Category("Core")]
    [Category("Core.Helpers")]
    public class TypeHelperTests
    {
        [Test]
        public void TypeHelper_TestUsage()
        {
            var t1 = TypeHelper.NamespaceAndClass("System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e");
            Assert.AreEqual("System.String, System.Private.CoreLib", t1);

            var t2 = TypeHelper.NamespaceAndClass("System.String, System.Private.CoreLib, Version=4.0.0.0");
            Assert.AreEqual("System.String, System.Private.CoreLib", t2);

            var t3 = TypeHelper.NamespaceAndClass("System.String, System.Private.CoreLib");
            Assert.AreEqual("System.String, System.Private.CoreLib", t3);

            var t4 = TypeHelper.NamespaceAndClass("System.String");
            Assert.AreEqual("System.String", t4);
        }
    }
}