namespace Abbotware.UnitTests.Core
{
    using System;
    using Abbotware.Core.Diagnostics.Plugins;
    using Abbotware.Core.Diagnostics.Plugins.Configuration;
    using Abbotware.Interop.NUnit;
    using NUnit.Framework;

    [TestFixture]
    [Category("Experimental")]
    public class MemoryLoggerTests
    {
        [Test]
        public void Create()
        {
            var cfg = new NanoLoggerConfiguration();
            cfg.Add(0, 10);

            var ml = new NanoLogger(cfg);

            ml.Begin(0, 0, 0);
            ml.Begin(0, 0, 0, 1);
            ml.End(0, 0, 0);
            ml.Begin(0, 0, 0);

            ml.Begin(0, 0, 0);
            ml.Begin(0, 0, 0, 1);
            ml.End(0, 0, 0);
            ml.Begin(0, 0, 0);

            ml.Begin(0, 0, 0);
            ml.Begin(0, 0, 0, 1);
        }

        [Test]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Error_CorrelationId_DoesNotExist()
        {
            var cfg = new NanoLoggerConfiguration();
            cfg.Add(0, 10);

            var ml = new NanoLogger(cfg);

            ml.Begin(1, 0, 0);
        }

        [Test]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Error_TooManyRecords()
        {
            var cfg = new NanoLoggerConfiguration();
            cfg.Add(0, 10);

            var ml = new NanoLogger(cfg);

            ml.Begin(0, 0, 0);
            ml.Begin(0, 0, 0, 1);
            ml.End(0, 0, 0);
            ml.Begin(0, 0, 0);

            ml.Begin(0, 0, 0);
            ml.Begin(0, 0, 0, 1);
            ml.End(0, 0, 0);
            ml.Begin(0, 0, 0);

            ml.Begin(0, 0, 0);
            ml.Begin(0, 0, 0, 1);
            ml.End(0, 0, 0);
        }

        [Test]
        public void Create_DifferentIndexes()
        {
            var cfg = new NanoLoggerConfiguration();
            cfg.Add(0, 10);
            cfg.Add(1, 5);

            var ml = new NanoLogger(cfg);

            ml.Begin(0, 0, 0);
            ml.Begin(0, 0, 0, 1);
            ml.End(0, 0, 0);
            ml.Begin(0, 0, 0);
            ml.Begin(0, 0, 0);
            ml.Begin(0, 0, 0, 1);
            ml.End(0, 0, 0);
            ml.Begin(0, 0, 0);
            ml.Begin(0, 0, 0);
            ml.Begin(0, 0, 0, 1);

            ml.Begin(1, 0, 0);
            ml.Begin(1, 0, 0, 1);
            ml.End(1, 0, 0);
            ml.Begin(1, 0, 0);
            ml.Begin(1, 0, 0);
        }

        [Test]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Error_TooManyRecords_DifferentIndex()
        {
            var cfg = new NanoLoggerConfiguration();
            cfg.Add(0, 10);
            cfg.Add(1, 5);

            var ml = new NanoLogger(cfg);

            ml.Begin(0, 0, 0);
            ml.Begin(0, 0, 0, 1);
            ml.End(0, 0, 0);
            ml.Begin(0, 0, 0);

            ml.Begin(0, 0, 0);
            ml.Begin(0, 0, 0, 1);
            ml.End(0, 0, 0);
            ml.Begin(0, 0, 0);

            ml.Begin(0, 0, 0);
            ml.Begin(0, 0, 0, 1);

            ml.Begin(1, 0, 0);
            ml.Begin(1, 0, 0, 1);
            ml.End(1, 0, 0);
            ml.Begin(1, 0, 0);
            ml.Begin(1, 0, 0);
            ml.Begin(1, 0, 0, 1);
        }
    }
}