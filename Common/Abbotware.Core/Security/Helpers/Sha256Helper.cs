// -----------------------------------------------------------------------
// <copyright file="Sha256Helper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Security
{
    using System.Security.Cryptography;

    /// <summary>
    /// Helper function for Sha256
    /// </summary>
    public static class Sha256Helper
    {
        /// <summary>
        /// Encodes the given data using the SHA hash algorithm.
        /// </summary>
        /// <param name="data">data to hash</param>
        /// <param name="format">output format</param>
        /// <returns>sha 256 hash</returns>
        public static string GenerateHash(string data, HashStringFormat format = HashStringFormat.Hex)
        {
            data = Arguments.EnsureNotNull(data, nameof(data));

            using var algorithm = SHA256.Create();

            return Common.Hash(data, algorithm, format);
        }
    }
}
