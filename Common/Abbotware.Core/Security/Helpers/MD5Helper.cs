// -----------------------------------------------------------------------
// <copyright file="MD5Helper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Security
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Helper function for MD5
    /// </summary>
    public static class MD5Helper
    {
        /// <summary>
        /// Convert bytes to an MD5 hash stored in a Guid
        /// </summary>
        /// <param name="bytes">bytes to has</param>
        /// <returns>MD5 encoded into a GUID</returns>
        public static Guid ToGuid(byte[] bytes)
        {
            bytes = Arguments.EnsureNotNull(bytes, nameof(bytes));

#if NET6_0_OR_GREATER
            var hash = MD5.HashData(bytes);
#else
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(bytes);
#endif

            return new Guid(hash);
        }

        /// <summary>
        /// Convert a string to an MD5 hash stored in a Guid
        /// </summary>
        /// <param name="message">string message</param>
        /// <returns>MD5 encoded into a GUID</returns>
        public static Guid ToGuid(string message)
        {
            message = Arguments.EnsureNotNull(message, nameof(message));

            return ToGuid(Encoding.UTF8.GetBytes(message));
        }

        /// <summary>
        /// Encodes the given data using the MD5 hash algorithm.
        /// </summary>
        /// <param name="data">data to hash</param>
        /// <param name="format">output format</param>
        /// <returns>md5 hash</returns>
        public static string GenerateHash(string data, HashStringFormat format = HashStringFormat.Hex)
        {
            data = Arguments.EnsureNotNull(data, nameof(data));

            using MD5 algorithm = MD5.Create();

            return Common.Hash(data, algorithm, format);
        }
    }
}