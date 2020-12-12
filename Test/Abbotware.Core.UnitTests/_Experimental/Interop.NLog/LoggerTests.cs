namespace Abbotware.UnitTests.Core
{
    using System;
    using System.Linq;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Logging;
    using Abbotware.Interop.NLog.Plugins;
    using Abbotware.Utility.UnitTest.Using.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Experimental")]
    public class LoggerTests : BaseNUnitTest
    {
        public LoggerTests()
        {
            this.NewLogger = this.LoggerFactory.Create(this.GetType());
        }

        /// <summary>
        /// Gets the logger (in memory) for unit test
        /// </summary>
        protected ILogger NewLogger { get; }

        /// <summary>
        /// Gets the loggerFactory (in memory) for unit test
        /// </summary>
        protected IMemoryLoggerFactory LoggerFactory { get; } = new MemoryLoggerFactory();

        [Test]
        public void Create()
        {
            var l = new Logger();

            Assert.IsNotNull(l);
        }

        [Test]
        public void Factory_Create_With_Name()
        {
            var l = this.LoggerFactory.Create("TestLogger");

            Assert.That("TestLogger", Is.EqualTo(l.Name));
        }

        [Test]
        public void Factory_Create_With_GenericType()
        {
            var l = this.LoggerFactory.Create<LoggerTests>();

            Assert.That(nameof(LoggerTests), Is.EqualTo(l.Name));
        }

        [Test]
        public void Factory_Create_With_Type()
        {
            var l = this.LoggerFactory.Create(typeof(LoggerTests));

            Assert.That(nameof(LoggerTests), Is.EqualTo(l.Name));
        }

        [Test]
        public void ScopedLogger_Using()
        {
            var l = this.LoggerFactory.Create(typeof(LoggerTests));

            using (var s = l.TimingScope("TestScope"))
            {
                Assert.That(s.ScopeName, Is.EqualTo("TestScope"));
                Assert.That(s.MemberName, Is.EqualTo("ScopedLogger_Using"));

                System.Threading.Thread.Sleep(30);

                Assert.That(s.Elapsed, Is.AtLeast(TimeSpan.FromMilliseconds(30)));
            }

            Assert.That(this.LoggerFactory.Messages, Is.Not.Empty);
            Assert.That(this.LoggerFactory.Messages.Count(), Is.EqualTo(2));
        }
    }
}