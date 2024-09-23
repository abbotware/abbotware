namespace Abbotware.UnitTests.Core
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Abbotware.Core.Helpers;
    using NUnit.Framework;
    using Assert = Abbotware.Interop.NUnit.LegacyAssert;

    [TestFixture]
    [Category("Core")]
    [Category("Core.Helpers")]
    public class DirectoryHelperTests
    {
        [Test]

        public void DirectoryHelper_Usage_1()
        {
            var path = DirectoryHelper.GenerateSearchPaths("some.file").ToArray();
            var current = Environment.CurrentDirectory;

            var slash = Path.DirectorySeparatorChar;

            Assert.AreEqual(2, path.Length);

            Assert.AreEqual(current + $"{slash}some.file", path[0]);
            Assert.AreEqual(current + $"{slash}..{slash}some.file", path[1]);
        }

        [Test]
        public void DirectoryHelper_Usage_2()
        {
            var path = DirectoryHelper.GenerateSearchPaths("some.file", "folder").ToArray();
            var current = Environment.CurrentDirectory;
            var assembly = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);

            var slash = Path.DirectorySeparatorChar;

            Assert.AreEqual(6, path.Length);

            Assert.AreEqual(current + $"{slash}folder{slash}some.file", path[0]);
            Assert.AreEqual(current + $"{slash}..{slash}folder{slash}some.file", path[1]);
            Assert.AreEqual(assembly + $"{slash}folder{slash}some.file", path[2]);
            Assert.AreEqual(assembly + $"{slash}..{slash}folder{slash}some.file", path[3]);
            Assert.AreEqual(current + $"{slash}some.file", path[4]);
            Assert.AreEqual(current + $"{slash}..{slash}some.file", path[5]);
        }
    }
}
