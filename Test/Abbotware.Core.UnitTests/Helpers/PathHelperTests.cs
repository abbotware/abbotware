namespace Abbotware.UnitTests.Core
{
    using Abbotware.Core.Helpers;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;
    using Assert = Abbotware.Interop.NUnit.LegacyAssert;

    [TestFixture]
    [Category("Core")]
    [Category("Core.Helpers")]
    public class PathHelperTests : BaseNUnitTest
    {
        [Test]
        [Category("windows")]

        public void LogFileModuleNameRelative_Windows()
        {
            this.SkipTestOnLinux();

            var windows = @"../../L:ogs/[NOT AVAILABLE][UnitTest].l:og";

            var cleaned = PathHelper.CleanPath(windows);

            Assert.AreEqual("..\\..\\Logs\\[NOT AVAILABLE][UnitTest].log", cleaned);
        }

        [Test]
        [Category("windows")]

        public void LogFileModuleNameRooted_Windows()
        {
            this.SkipTestOnLinux();

            var windows = @"C:/temp/L:ogs/[NOT AVAILABLE][UnitTest].l:og";

            var cleaned = PathHelper.CleanPath(windows);

            Assert.AreEqual("C:\\temp\\Logs\\[NOT AVAILABLE][UnitTest].log", cleaned);
        }

        [Test]
        [Category("linux")]

        public void LogFileModuleNameRelative_Linux()
        {
            this.SkipTestOnWindows();

            var windows = @"../../L:ogs/[NOT AVAILABLE][UnitTest].l:og";

            var cleaned = PathHelper.CleanPath(windows);

            Assert.AreEqual("../../Logs/[NOT AVAILABLE][UnitTest].log", cleaned);
        }

        [Test]
        [Category("linux")]

        public void LogFileModuleNameRooted_Linux()
        {
            this.SkipTestOnWindows();

            var windows = @"C:/temp/L:ogs/[NOT AVAILABLE][UnitTest].l:og";

            var cleaned = PathHelper.CleanPath(windows);

            Assert.AreEqual("C/temp/Logs/[NOT AVAILABLE][UnitTest].log", cleaned);
        }
    }
}
