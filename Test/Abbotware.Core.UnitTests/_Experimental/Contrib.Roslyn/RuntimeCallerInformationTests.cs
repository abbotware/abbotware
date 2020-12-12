namespace Abbotware.UnitTests.Core
{
    using Abbotware.Contrib.Roslyn;
    using Abbotware.Core.Diagnostics;
    using NUnit.Framework;

    [TestFixture]
    [Category("Experimental")]
    public class RuntimeCallerInformationTests
    {
        [Test]
        public void UsageForRuntimeCallerInformation()
        {
            var u = new UsageForRuntimeCallerInformation();

            u.UserMethod("test", new RuntimeCallerInformation());
        }

        [Test]
        public void UsageForRuntimeParameterInformation()
        {
            var u = new UsageForRuntimeParameterInformation();

            u.UserTypes();
        }
    }
}