// -----------------------------------------------------------------------
// <copyright file="BinaryToString.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Compression.Plugins
{
    using System.Text;

    /// <summary>
    /// Decorator class that wraps any IBinaryCompression into encoded strings
    /// </summary>
    public class BinaryToString : IStringCompression
    {
        private readonly IBinaryCompression binary;

        private readonly Encoding encoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryToString"/> class.
        /// </summary>
        /// <param name="binary">binary compression plugin</param>
        /// <param name="encoding">text conding</param>
        public BinaryToString(IBinaryCompression binary, Encoding encoding)
        {
            this.binary = binary;
            this.encoding = encoding;
        }

        /// <inheritdoc/>
        public byte[] Compress(string uncompressed)
        {
            var bytes = this.encoding.GetBytes(uncompressed);

            return this.binary.Compress(bytes);
        }

        /// <inheritdoc/>
        public string Decompress(byte[] compressed)
        {
            return this.encoding.GetString(this.binary.Decompress(compressed));
        }
    }
}
