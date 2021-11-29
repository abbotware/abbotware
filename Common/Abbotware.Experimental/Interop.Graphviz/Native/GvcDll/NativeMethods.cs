// -----------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Graphviz.Native.GvcDll
{
    using System;
    using System.Runtime.InteropServices;
    using Abbotware.Interop.Graphviz.Api;

    /// <summary>
    ///     Managed wrapper for the native methods in the gvc.dll
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        ///     Name of the Dll
        /// </summary>
        public const string LIB_GVC = "gvc.dll";

        /// <summary>
        ///     return value for success
        /// </summary>
        public const int SUCCESS = 0;

        /// <summary>
        ///     Creates a new Graphviz context.
        /// </summary>
        /// <returns>pointer to unmanaged graphviz context</returns>
        [DllImport(NativeMethods.LIB_GVC, CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        public static extern GraphvizContextSafeHandle gvContext();

        ////TODO: change IntPtr to a GraphvizContextHandle : SafeHandle

        /// <summary>
        ///     Releases an unmanaged Graphviz context's resources.
        /// </summary>
        /// <param name="graphVizContextPtr">pointer to unmanaged graphviz context</param>
        /// <returns>value indicating success or failure</returns>
        [DllImport(NativeMethods.LIB_GVC, CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        public static extern int gvFreeContext(IntPtr graphVizContextPtr);

        /// <summary>
        ///     Gets the Graphviz context's version
        /// </summary>
        /// <param name="graphVizContextPtr">pointer to unmanaged graphviz context</param>
        /// <returns>pointer to char* string containing version</returns>
        [DllImport(NativeMethods.LIB_GVC, CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        public static extern IntPtr gvcVersion(GraphvizContextSafeHandle graphVizContextPtr);

        /// <summary>
        ///     Gets the Graphviz context's build date
        /// </summary>
        /// <param name="graphVizContextPtr">pointer to unmanaged graphviz context</param>
        /// <returns>pointer to char* string containing build date</returns>
        [DllImport(NativeMethods.LIB_GVC, CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        public static extern IntPtr gvcBuildDate(GraphvizContextSafeHandle graphVizContextPtr);

        /// <summary>
        ///     Applies a layout to a graph using the given engine.
        /// </summary>
        /// <param name="graphVizContextPtr">pointer to unmanaged graphviz context</param>
        /// <param name="graphPtr">pointer to unmanaged graph object</param>
        /// <param name="layoutEngine">layout engine to use on graph</param>
        /// <returns>value indicating success or failure</returns>
        [DllImport(NativeMethods.LIB_GVC, CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        public static extern int gvLayout(GraphvizContextSafeHandle graphVizContextPtr, GraphSafeHandle graphPtr, [MarshalAs(UnmanagedType.LPStr)] char[] layoutEngine);

        /// <summary>
        ///     Releases the resources used by a layout.
        /// </summary>
        /// <param name="graphVizContextPtr">pointer to unmanaged graphviz context</param>
        /// <param name="graphPtr">pointer to unmanaged graph object</param>
        /// <returns>value indicating success or failure</returns>
        [DllImport(NativeMethods.LIB_GVC, CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        public static extern int gvFreeLayout(GraphvizContextSafeHandle graphVizContextPtr, GraphSafeHandle graphPtr);

        /// <summary>
        ///     Renders a graph to a file.
        /// </summary>
        /// <param name="graphVizContextPtr">pointer to unmanaged graphviz context</param>
        /// <param name="graphPtr">pointer to unmanaged graph object</param>
        /// <param name="imageFormat">format of output image</param>
        /// <param name="outputFilePath">name of the file to be used for saving the image</param>
        /// <returns>value indicating success or failure</returns>
        [DllImport(NativeMethods.LIB_GVC, CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        public static extern int gvRenderFilename(GraphvizContextSafeHandle graphVizContextPtr, GraphSafeHandle graphPtr, [MarshalAs(UnmanagedType.LPStr)] char[] imageFormat, [MarshalAs(UnmanagedType.LPStr)] char[] outputFilePath);

        /// <summary>
        ///     Renders a graph in memory.
        /// </summary>
        /// <param name="graphVizContextPtr">pointer to unmanaged graphviz context</param>
        /// <param name="graphPtr">pointer to unmanaged graph object</param>
        /// <param name="imageFormat">format of output image</param>
        /// <param name="renderDataResultPtr">pointer to binary image data</param>
        /// <param name="renderDataLength">length of the binary image data</param>
        /// <returns>value indicating success or failure</returns>
        [DllImport(NativeMethods.LIB_GVC, CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
        public static extern int gvRenderData(GraphvizContextSafeHandle graphVizContextPtr, GraphSafeHandle graphPtr, [MarshalAs(UnmanagedType.LPStr)] char[] imageFormat, out IntPtr renderDataResultPtr, out uint renderDataLength);
    }
}