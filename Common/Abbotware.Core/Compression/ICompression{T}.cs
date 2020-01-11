// -----------------------------------------------------------------------
// <copyright file="ICompression{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Compression
{
    /// <summary>
    /// generic interface for compression
    /// </summary>
    /// <typeparam name="T">Data Type</typeparam>
    public interface ICompression<T>
    {
        /// <summary>
        /// Compression data to bytes
        /// </summary>
        /// <param name="uncompressed">data to compress</param>
        /// <returns>compress bytes</returns>
        byte[] Compress(T uncompressed);

        /// <summary>
        /// Decompress bytes to data
        /// </summary>
        /// <param name="compressed">compressed bytes</param>
        /// <returns>decompressed data</returns>
        T Decompress(byte[] compressed);
    }
}