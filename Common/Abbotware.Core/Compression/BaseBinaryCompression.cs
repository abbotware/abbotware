// -----------------------------------------------------------------------
// <copyright file="BaseBinaryCompression.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Compression
{
    using System.IO;

    /// <summary>
    /// Base class for implementing stream based compression plugins that compress byte[] to byte[]
    /// </summary>
    public abstract class BaseBinaryCompression : IBinaryCompression
    {
        /// <inheritdoc/>
        public byte[] Compress(byte[] uncompressed)
        {
            using var input = new MemoryStream(uncompressed);
            using var output = new MemoryStream();

            this.OnCompress(input, output);

            return output.ToArray();
        }

        /// <inheritdoc/>
        public byte[] Decompress(byte[] compressed)
        {
            using var input = new MemoryStream(compressed);
            using var output = new MemoryStream();

            this.OnDecompress(input, output);

            return output.ToArray();
        }

        /// <summary>
        /// callback to compress data from input to output stream
        /// </summary>
        /// <param name="input">input stream</param>
        /// <param name="output">output stream</param>
        protected abstract void OnCompress(Stream input, Stream output);

        /// <summary>
        /// callback to decompress data from input to output stream
        /// </summary>
        /// <param name="input">input stream</param>
        /// <param name="output">output stream</param>
        protected abstract void OnDecompress(Stream input, Stream output);
    }
}
