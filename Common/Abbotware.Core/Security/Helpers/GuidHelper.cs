// -----------------------------------------------------------------------
// <copyright file="GuidHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Security
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// Security realted Guid Helper methods
    /// </summary>
    public static class GuidHelper
    {
        /// <summary>
        /// Generates a cryptographically random GUID via RNGCryptoServiceProvider
        /// </summary>
        /// <returns>guid created by via RNGCryptoServiceProvider</returns>
        public static Guid NewRandomGuid()
        {
            using var rng = RandomNumberGenerator.Create();

            var bytes = new byte[16];
            rng.GetBytes(bytes);

            // Alter bytes to inciate a version 4 GUID based on random generation
            // https://www.ietf.org/rfc/rfc4122.txt
            bytes[7] = (byte)((bytes[7] | 0x40) & 0x4F);
            bytes[8] = (byte)((bytes[8] | 0x80) & 0xBF);

            return new Guid(bytes);
        }
    }
}
