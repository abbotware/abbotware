// -----------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Graphviz.GraphDll
{
    using System;
    using System.Runtime.InteropServices;
    using Abbotware.Interop.Graphviz.Api;

    /// <summary>
    ///     Managed wrapper for the native methods in the graph.dll
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        ///     Name of the dll
        /// </summary>
        public const string LIB_GRAPH = "graph.dll";

        /// <summary>
        ///     size of the unmanaged graph structure
        /// </summary>
        public const int SIZEOF_AGRAPH = 296;

        /// <summary>
        ///     size of the unmanaged node structure
        /// </summary>
        public const int SIZEOF_ANODE = 312;

        /// <summary>
        ///     size of the unmanaged edge structure
        /// </summary>
        public const int SIZEOF_AEDGE = 192;

        /// <summary>
        ///     Initializes the graph library
        /// </summary>
        /// <param name="graphSize">size of the unmanaged graph structure</param>
        /// <param name="nodeSize">size of the unmanaged node structure</param>
        /// <param name="edgeSize">size of the unmanaged edge structure</param>
        [DllImport(LIB_GRAPH, CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        public static extern void aginitlib(int graphSize, int nodeSize, int edgeSize);

        /// <summary>
        ///     Reads a graph from a string.
        /// </summary>
        /// <param name="graphData">graph data</param>
        /// <returns>pointer to the unmanaged graph object</returns>
        [DllImport(LIB_GRAPH, CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        public static extern GraphSafeHandle agmemread([MarshalAs(UnmanagedType.LPStr)] string graphData);

        ////TODO: change IntPtr to a GraphvizContextHandle : SafeHandle

        /// <summary>
        ///     Releases the resources used by a graph.
        /// </summary>
        /// <param name="graphPtr">pointer to the unmanaged graph object</param>
        [DllImport(LIB_GRAPH, CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        public static extern void agclose(IntPtr graphPtr);
    }
}