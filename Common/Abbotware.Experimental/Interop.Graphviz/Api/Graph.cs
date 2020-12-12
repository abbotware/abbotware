// -----------------------------------------------------------------------
// <copyright file="Graph.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Graphviz.Api
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
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
            : base(logger)
        {
            Arguments.NotNullOrWhitespace(graphData, nameof(graphData));
            Arguments.NotNull(logger, nameof(logger));

            var sublogger = this.Logger.Create("GraphManagedApi");

            try
            {
                this.graphManagedApi = new GraphManagedApi(sublogger);

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