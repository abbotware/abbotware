// -----------------------------------------------------------------------
// <copyright file="GZipCompression.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Compression.Plugins
{
    using System.IO;
    using System.IO.Compression;
    using System.Text;

    /// <summary>
    /// Wrapper for System.IO.Compression.GZipStream
    /// </summary>
    public class GZipCompression : IStringCompression, IByteCompression
    {
        /// <inheritdoc/>
        public byte[] Compress(byte[] bytes)
        {
            using var input = new MemoryStream(bytes);
            using var output = new MemoryStream();

            this.CompressTo(input, output);

            return output.ToArray();
        }

        /// <inheritdoc/>
        public byte[] Decompress(byte[] bytes)
        {
            using var input = new MemoryStream(bytes);
            using var output = new MemoryStream();

            this.DecompressTo(input, output);

            return output.ToArray();
        }

        /// <inheritdoc/>
        byte[] ICompression<string>.Compress(string uncompressed)
        {
            var bytes = Encoding.UTF8.GetBytes(uncompressed);

            return this.Compress(bytes);
        }

        /// <inheritdoc/>
        string ICompression<string>.Decompress(byte[] bytes)
        {
            return Encoding.UTF8.GetString(this.Decompress(bytes));
        }

        private void CompressTo(Stream input, Stream output)
        {
            using var gz = new GZipStream(output, CompressionLevel.Optimal, false);

            input.CopyTo(gz);
        }

        private void DecompressTo(Stream input, Stream output)
        {
            using var gz = new GZipStream(input, CompressionMode.Decompress, false);

            gz.CopyTo(output);
        }
    }
}
