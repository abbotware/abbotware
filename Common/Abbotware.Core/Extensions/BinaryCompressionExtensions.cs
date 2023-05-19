// -----------------------------------------------------------------------
// <copyright file="BinaryCompressionExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Extensions
{
    using System.Text;
    using Abbotware.Core.Compression;
    using Abbotware.Core.Compression.Plugins;

    /// <summary>
    /// Extension methods for IBinaryCompression
    /// </summary>
    public static class BinaryCompressionExtensions
    {
        /// <summary>
        /// compresses a string into binary via the extended binary compression plugin
        /// </summary>
        /// <param name="compression">binary compression plugin</param>
        /// <param name="s">string to encode</param>
        /// <returns>binary</returns>
        public static byte[] CompressString(this IBinaryCompression compression, string s)
        {
            return CompressString(compression, s, Encoding.UTF8);
        }

        /// <summary>
        /// compresses a string into binary via the extended binary compression plugin
        /// </summary>
        /// <param name="compression">binary compression plugin</param>
        /// <param name="s">string to encode</param>
        /// <param name="encoding">string encoding type</param>
        /// <returns>binary</returns>
        public static byte[] CompressString(this IBinaryCompression compression, string s, Encoding encoding)
        {
            var decorator = new BinaryToString(compression, encoding);

            return decorator.Compress(s);
        }

        /// <summary>
        /// compresses bytess into string via the extended binary compression plugin
        /// </summary>
        /// <param name="compression">binary compression plugin</param>
        /// <param name="bytes">bytes to decode</param>
        /// <returns>string</returns>
        public static string DecompressString(this IBinaryCompression compression, byte[] bytes)
        {
            return DecompressString(compression, bytes, Encoding.UTF8);
        }

        /// <summary>
        /// compresses bytess into string via the extended binary compression plugin
        /// </summary>
        /// <param name="compression">binary compression plugin</param>
        /// <param name="bytes">bytes to decode</param>
        /// <param name="encoding">string encoding type</param>
        /// <returns>string</returns>
        public static string DecompressString(this IBinaryCompression compression, byte[] bytes, Encoding encoding)
        {
            var decorator = new BinaryToString(compression, encoding);

            return decorator.Decompress(bytes);
        }
    }
}
