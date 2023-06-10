// -----------------------------------------------------------------------
// <copyright file="Graph.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Graphviz.Api
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Objects;

    /// <summary>
    ///     Managed wrapper for an unmanaged graph object from the graph.dll
    /// </summary>
    public class Graph : BaseComponent
    {
        /// <summary>
        ///     internal object that wraps all native api calls
        /// </summary>
        private readonly GraphManagedApi graphManagedApi;

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph"/> class.
        /// </summary>
        /// <param name="graphData">graph data</param>
        /// <param name="logger">injected logger for class</param>
        public Graph(string graphData, ILogger logger)
        {
            graphData = Arguments.EnsureNotNullOrWhitespace(graphData, nameof(graphData));

            try
            {
                this.graphManagedApi = new GraphManagedApi(logger);

                this.SafeHandle = this.graphManagedApi.MemRead(graphData);
            }
            catch (Exception)
            {
                this.Dispose();
                throw;
            }
        }

        /// <summary>
        ///     Gets the pointer to the unmanaged Graph object
        /// </summary>
        public GraphSafeHandle SafeHandle { get; }

        /// <inheritdoc />
        protected override void OnDisposeUnmanagedResources()
        {
            this.SafeHandle?.Dispose();

            base.OnDisposeUnmanagedResources();
        }
    }
}