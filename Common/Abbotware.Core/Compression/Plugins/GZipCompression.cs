// -----------------------------------------------------------------------
// <copyright file="GZipCompression.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Compression.Plugins
{
    using System.IO;
    using System.IO.Compression;

    /// <summary>
    /// Wrapper for System.IO.Compression.GZipStream
    /// </summary>
    public class GZipCompression : BaseBinaryCompression
    {
        /// <inheritdoc/>
        protected override void OnCompress(Stream input, Stream output)
        {
            input = Arguments.EnsureNotNull(input, nameof(input));

            using var gz = new GZipStream(output, CompressionLevel.Optimal, false);

            input.CopyTo(gz);
        }

        /// <inheritdoc/>
        protected override void OnDecompress(Stream input, Stream output)
        {
            using var gz = new GZipStream(input, CompressionMode.Decompress, false);

            gz.CopyTo(output);
        }
    }
}
