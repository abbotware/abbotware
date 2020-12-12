// <copyright file="GraphvizUnitTests.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>

namespace Abbotware.Tests.Interop.Graphviz
{
    using System;
    using System.Threading;
    using Abbotware.Core.ExtensionPoints;
    using Abbotware.Core.IO;
    using Abbotware.Core.Plugins;
    using Abbotware.Core.Threading.Counters;
    using Abbotware.Interop.Graphviz;
    using Abbotware.Interop.Graphviz.Api;
    using Abbotware.Interop.Windows;
    using Abbotware.Interop.Windows.Kernel32;
    using Abbotware.UnitTest;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SystemDrawingImageFormat = System.Drawing.Imaging.ImageFormat;

    [TestClass]
    [DeploymentItem("graphviz", "graphviz")]
    public class GraphvizUnitTests : ContainerUnitTestBase
    {
        public const string DIGRPAH = @"digraph {
  Entity1 -> Entity2, Entity1 -> Entity3

 Entity1 [shape=""circle""] , Entity2 [shape=""rectangle""]
}";

        private readonly object mutex = new object();

        [TestMethod]
        [TestCategory("Interop.Graphviz")]
        public void T01_GraphVizContext_Create_Delete()
        {
            using (var context = new GraphvizContext(this.Container.Resolve<ILogger>()))
            {
                Assert.AreEqual(1, ActiveInstanceCounter<GraphvizContext>.GlobalActiveCount);
            }

            Assert.AreEqual(0, ActiveInstanceCounter<GraphvizContext>.GlobalActiveCount);
        }

        [TestMethod]
        [TestCategory("Interop.Graphviz")]
        public void T02_Graph_Create_Delete()
        {
            using (var context = new Graph(GraphvizUnitTests.DIGRPAH, this.Container.Resolve<ILogger>()))
            {
                Assert.AreEqual(1, ActiveInstanceCounter<Graph>.GlobalActiveCount);
            }

            Assert.AreEqual(0, ActiveInstanceCounter<Graph>.GlobalActiveCount);
        }

        [TestMethod]
        [TestCategory("Interop.Graphviz")]
        public void T03_InMemmoryRender_Create_Delete()
        {
            using (var context = new InMemoryRender(GraphvizUnitTests.DIGRPAH, LayoutEngine.Dot, ImageFormat.Png, this.Container.Resolve<ILogger>()))
            {
                Assert.AreEqual(1, ActiveInstanceCounter<InMemoryRender>.GlobalActiveCount);
            }

            Assert.AreEqual(0, ActiveInstanceCounter<InMemoryRender>.GlobalActiveCount);
        }

        [TestMethod]
        [TestCategory("Interop.Graphviz")]
        [ExpectedException(typeof(GraphvizApiException))]
        public void T06_InMemmoryRender_Render_Bad()
        {
            Assert.AreEqual(0, ActiveInstanceCounter<Graph>.GlobalActiveCount);
            Assert.AreEqual(0, ActiveInstanceCounter<GraphvizContext>.GlobalActiveCount);
            Assert.AreEqual(0, ActiveInstanceCounter<InMemoryRender>.GlobalActiveCount);

            try
            {
                using (var context = new InMemoryRender("test", LayoutEngine.Dot, ImageFormat.Png, NullLogger.Instance))
                {
                }
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();

                Assert.AreEqual(0, ActiveInstanceCounter<Graph>.GlobalActiveCount);
                Assert.AreEqual(0, ActiveInstanceCounter<GraphvizContext>.GlobalActiveCount);
                Assert.AreEqual(0, ActiveInstanceCounter<InMemoryRender>.GlobalActiveCount);
            }
        }

        [TestMethod]
        [TestCategory("Interop.Graphviz")]
        public void T04_InMemmoryRender_Render_Png()
        {
            using (var context = new InMemoryRender(GraphvizUnitTests.DIGRPAH, LayoutEngine.Dot, ImageFormat.Png, this.Container.Resolve<ILogger>()))
            {
                var data = context.Render();
                Assert.AreEqual(8743, data.Length);

                using (var image = data.ToImage())
                using (var temp = new TemporaryFileStream())
                {
                    image.Save(temp, SystemDrawingImageFormat.Png);
                }
            }
        }

        [TestMethod]
        [TestCategory("Interop.Graphviz")]
        public void T05_GraphVizContext_Create_Delete()
        {
            Assert.AreEqual(0, ActiveInstanceCounter<Graph>.GlobalActiveCount);
            Assert.AreEqual(0, ActiveInstanceCounter<GraphvizContext>.GlobalActiveCount);
            Assert.AreEqual(0, ActiveInstanceCounter<InMemoryRender>.GlobalActiveCount);
        }

        protected override void OnTestInitialize()
        {
            Monitor.Enter(this.mutex);
            Kernel32Methods.SetDllDirectory("graphviz");

            GC.Collect(2, GCCollectionMode.Forced, true);
            GC.WaitForPendingFinalizers();
        }

        protected override void OnTestCleanup()
        {
            Kernel32Methods.ResetDllDirectory();
            Monitor.Exit(this.mutex);

            GC.Collect(2, GCCollectionMode.Forced, true);
            GC.WaitForPendingFinalizers();
        }
    }
}