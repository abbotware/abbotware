// -----------------------------------------------------------------------
// <copyright file="GraphvizContext.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.Graphviz.Api
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using Abbotware.Core;
    using Abbotware.Core.Logging;
    using Abbotware.Core.Objects;
    using Abbotware.Interop.Graphviz.Native.GvcDll;

    /// <summary>
    ///     Managed wrapper for an unmanaged Graphviz Context object from the gvc.dll
    /// </summary>
    public class GraphvizContext : BaseComponent
    {
        /// <summary>
        ///     Pointer to unmanaged graph object
        /// </summary>
        private readonly GraphvizContextSafeHandle graphvizContextPtr;

        /// <summary>
        ///     tracks the graph object used for the most recent AddLayout / FreeLayout calls
        /// </summary>
        private Graph? graph;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GraphvizContext" /> class.
        /// </summary>
        /// <param name="logger">injected logger for class</param>
        public GraphvizContext(ILogger logger)
            : base(logger)
        {
            Arguments.NotNull(logger, nameof(logger));

            try
            {
                this.graphvizContextPtr = NativeMethods.gvContext();

                if (this.graphvizContextPtr.IsInvalid)
                {
                    throw new GraphvizApiException("Failed on creating Graphviz Context");
                }

                var version = NativeMethods.gvcVersion(this.graphvizContextPtr);

                if (version == IntPtr.Zero)
                {
                    throw new GraphvizApiException("Failed on getting Graphviz Context version");
                }

                var versionString = Marshal.PtrToStringAnsi(version);

                var buildDate = NativeMethods.gvcBuildDate(this.graphvizContextPtr);

                if (buildDate == IntPtr.Zero)
                {
                    throw new GraphvizApiException("Failed on getting Graphviz Context build date");
                }

                var buildDateString = Marshal.PtrToStringAnsi(buildDate);

                this.Logger.Debug("Version:{0} BuildDate:{1} Ctx:{2}", versionString, buildDateString, this.graphvizContextPtr);
            }
            catch (Exception)
            {
                this.Dispose();
                throw;
            }
        }

        /// <summary>
        ///     adds a graph + layout to graphviz context
        /// </summary>
        /// <param name="graphObject">graph object to render</param>
        /// <param name="layoutEngine">layout engine to render with</param>
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "matching Graphviz Api definitions")]
        public void AddLayout(Graph graphObject, LayoutEngine layoutEngine)
        {
            Arguments.NotNull(graphObject, nameof(graphObject));

            this.ThrowIfDisposed();

            var funcRet = NativeMethods.gvLayout(this.graphvizContextPtr, graphObject.SafeHandle, layoutEngine.ToString().ToLowerInvariant().ToCharArray());

            var errorCode = Marshal.GetLastWin32Error();

            this.Logger.Debug("gvLayout({0},{1},{2}):{3}", this.graphvizContextPtr, graphObject.SafeHandle, layoutEngine.ToString().ToLowerInvariant(), funcRet);

            if (funcRet != NativeMethods.SUCCESS)
            {
                this.Logger.Error("gvLayout:{0}  Win32ErrorCode:{1}", funcRet, errorCode);
                throw new GraphvizApiException("error");
            }

            this.graph = graphObject;
        }

        /// <summary>
        ///     Renders the graph data in memory
        /// </summary>
        /// <param name="imageFormat">image format to use for rendering</param>
        /// <returns>binary data containing the image in memory</returns>
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "matching Graphviz Api definitions")]
        public byte[] RenderData(ImageFormat imageFormat)
        {
            this.ThrowIfDisposed();

            if (this.graph == null)
            {
                throw new GraphvizApiException("invalid use, must call add layout first");
            }

            ////CREATE COPY OF SAFEHANDLE WITH OWNS + FALSE

            var funcRet = NativeMethods.gvRenderData(this.graphvizContextPtr, this.graph.SafeHandle, imageFormat.ToString().ToLowerInvariant().ToCharArray(), out IntPtr buffer, out uint bufferLength);

            if (funcRet != NativeMethods.SUCCESS)
            {
                var errorCode = Marshal.GetLastWin32Error();
                this.Logger.Error("gvRenderData:{0}  Win32ErrorCode:{1}", funcRet, errorCode);
                throw new Win32Exception(errorCode, "Render Data Failed");
            }

            this.Logger.Debug("render data size:{0}", bufferLength);

            // Create an array to hold the rendered graph
            var bytes = new byte[bufferLength];

            // Copy the image from the IntPtr
            Marshal.Copy(buffer, bytes, 0, (int)bufferLength);

            return bytes;
        }

        /// <summary>
        ///     Renders the graph data to the specified filename
        /// </summary>
        /// <param name="imageFormat">image format to use for rendering</param>
        /// <param name="outputFilePath">file name to safe image to</param>
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "matching Graphviz Api definitions")]
        public void Save(ImageFormat imageFormat, string outputFilePath)
        {
            outputFilePath = Arguments.EnsureNotNullOrWhitespace(outputFilePath, nameof(outputFilePath));

            this.ThrowIfDisposed();

            if (this.graph == null)
            {
                throw new GraphvizApiException("invalid use, must call add layout first");
            }

            var funcRet = NativeMethods.gvRenderFilename(this.graphvizContextPtr, this.graph.SafeHandle, imageFormat.ToString().ToLowerInvariant().ToCharArray(), outputFilePath.ToCharArray());

            if (funcRet != NativeMethods.SUCCESS)
            {
                var errorCode = Marshal.GetLastWin32Error();
                this.Logger.Error("gvRenderFilename:{0}  Win32ErrorCode:{1}", funcRet, errorCode);
                throw new Win32Exception(errorCode, "Render Filename");
            }
        }

        /// <summary>
        ///     Removes / free layout from Graphviz Context
        /// </summary>
        public void FreeLayout()
        {
            this.ThrowIfDisposed();

            this.InternalFreeLayout();
        }

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            if (!this.graphvizContextPtr.IsInvalid)
            {
                this.InternalFreeLayout();

                this.Logger.Debug("gvFreeContext({0})", this.graphvizContextPtr);

                this.graphvizContextPtr?.Dispose();
            }

            base.OnDisposeManagedResources();
        }

        /// <summary>
        ///     internal free layout
        /// </summary>
        private void InternalFreeLayout()
        {
            if (this.graph != null)
            {
                var funcRet = NativeMethods.gvFreeLayout(this.graphvizContextPtr, this.graph.SafeHandle);

                if (funcRet != NativeMethods.SUCCESS)
                {
                    var errorCode = Marshal.GetLastWin32Error();
                    this.Logger.Error("gvFreeLayout:{0}  Win32ErrorCode:{1}", funcRet, errorCode);
                }
            }
        }
    }
}