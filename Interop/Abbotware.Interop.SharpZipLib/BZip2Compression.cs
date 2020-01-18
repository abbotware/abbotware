// -----------------------------------------------------------------------
// <copyright file="BZip2Compression.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.SharpZipLib
{
    using System.IO;
    using Abbotware.Core.Compression;
    using ICSharpCode.SharpZipLib.BZip2;

    /// <summary>
    /// Compression Plugin / Wrapper for ICSharpCode.SharpZipLib.BZip2
    /// </summary>
    public class BZip2Compression : BaseBinaryCompression
    {
        /// <inheritdoc/>
        protected override void OnCompress(Stream input, Stream output)
        {
            BZip2.Compress(input, output, false, 9);
        }

        /// <inheritdoc/>
        protected override void OnDecompress(Stream input, Stream output)
        {
            BZip2.Decompress(input, output, false);
        }
    }
}