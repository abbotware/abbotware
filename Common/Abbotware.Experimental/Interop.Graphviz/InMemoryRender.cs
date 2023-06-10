// -----------------------------------------------------------------------
// <copyright file="InMemoryRender.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Graphviz
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.Graphviz.Api;

    /// <summary>
    ///     Class that encapsulates the rendering of Graphviz images in memory
    /// </summary>
    public class InMemoryRender : BaseComponent
    {
        /// <summary>
        ///     wrapped GraphvizContext object for rendering
        /// </summary>
        private readonly GraphvizContext ctx;

        /// <summary>
        ///     wrapped Graph object for storing graph data
        /// </summary>
        private readonly Graph graph;

        /// <summary>
        ///     Image type for rendering
        /// </summary>
        private readonly ImageFormat imageFormat;

        /// <summary>
        ///     Layout engine for rendering
        /// </summary>
        private readonly LayoutEngine layoutEngine;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryRender"/> class.
        /// </summary>
        /// <param name="graphData">graph data</param>
        /// <param name="layoutEngine">layout engine type</param>
        /// <param name="imageFormat">image format type</param>
        /// <param name="logger">injected logger</param>
        public InMemoryRender(string graphData, LayoutEngine layoutEngine, ImageFormat imageFormat, ILogger logger)
            : base(logger)
        {
            Arguments.NotNullOrWhitespace(graphData, nameof(graphData));
            Arguments.NotNull(logger, nameof(logger));

            this.layoutEngine = layoutEngine;
            this.imageFormat = imageFormat;

            try
            {
                this.ctx = new GraphvizContext(logger);

                this.graph = new Graph(graphData, logger);
            }
            catch (Exception)
            {
                this.Dispose();
                throw;
            }
        }

        /// <summary>
        ///     Renders an image in a byte array
        /// </summary>
        /// <returns>the byte[] containing the image</returns>
        public byte[] Render()
        {
            this.ThrowIfDisposed();

            this.ctx.AddLayout(this.graph, this.layoutEngine);

            return this.ctx.RenderData(this.imageFormat);
        }

        /// <inheritdoc />
        protected override void OnDisposeUnmanagedResources()
        {
            this.ctx?.Dispose();

            this.graph?.Dispose();

            base.OnDisposeUnmanagedResources();
        }
    }
}