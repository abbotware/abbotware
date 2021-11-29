// -----------------------------------------------------------------------
// <copyright file="GraphManagedApi.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Graphviz.Api
{
    using System.Globalization;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.Graphviz.GraphDll;

    /// <summary>
    ///     Managed wrapper with error checking for the native methods in the graph.dll
    /// </summary>
    internal class GraphManagedApi : BaseComponent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GraphManagedApi" /> class.
        /// </summary>
        /// <param name="logger">injected logger</param>
        public GraphManagedApi(ILogger logger)
            : base(logger)
        {
            Arguments.NotNull(logger, nameof(logger));
        }

        /// <summary>
        ///     Reads a graph from a string.
        /// </summary>
        /// <param name="graphData">graph data</param>
        /// <returns>pointer to the unmanaged graph object</returns>
        public GraphSafeHandle MemRead(string graphData)
        {
            Arguments.NotNullOrWhitespace(graphData, nameof(graphData));

            this.InitializeIfRequired();

            var graphHandle = NativeMethods.agmemread(graphData.ToCharArray());

            this.Logger.Debug("agmemread('{0:20}'...):{1}", graphData, graphHandle);

            if (graphHandle.IsInvalid)
            {
                throw new GraphvizApiException("graph handle is invalid.. could not parse / read input data");
            }

            return graphHandle;
        }

        /// <inheritdoc />
        protected override void OnInitialize()
        {
            this.Logger.Debug("aginitlib({0},{1},{2})", NativeMethods.SIZEOF_AGRAPH, NativeMethods.SIZEOF_ANODE, NativeMethods.SIZEOF_AEDGE);
            NativeMethods.aginitlib(NativeMethods.SIZEOF_AGRAPH, NativeMethods.SIZEOF_ANODE, NativeMethods.SIZEOF_AEDGE);
        }
    }
}