// -----------------------------------------------------------------------
// <copyright file="Hash.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Security
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Security.Cryptography;
    using Abbotware.Core.Exceptions;

    /// <summary>
    ///     Hash algorithm without a required key
    /// </summary>
    public enum HashAlgorithm
    {
        /// <summary>
        ///     MD5 Algorithm
        /// </summary>
        MD5,

        /// <summary>
        ///     SHA1 Algorithm
        /// </summary>
        Sha1,

        /// <summary>
        ///     One's Compliment Algorithm
        /// </summary>
        OnesCompliment,
    }

    /// <summary>
    ///     Hash algorithm requiring a key
    /// </summary>
    public enum HashAlgorithmWithKey
    {
        /// <summary>
        ///     HMAC SHA1 Algorithm
        /// </summary>
        HmacSha256,

        /// <summary>
        ///     HMAC One's Compliment Algorithm
        /// </summary>
        HmacOnesCompliment,
    }

    /// <summary>
    ///     Wrapper type for computing data hashes
    /// </summary>
    public class Hash
    {
        /// <summary>
        ///     backing store for binary hash data
        /// </summary>
        private readonly byte[] data;

        /// <summary>
        /// Initializes a new instance of the <see cref="Hash"/> class.
        /// </summary>
        /// <param name="hashType">hash algorithm to use</param>
        /// <param name="data">binary data to hash</param>
        public Hash(HashAlgorithm hashType, byte[] data)
        {
            Arguments.NotNull(data, nameof(data));

            switch (hashType)
            {
                case HashAlgorithm.Sha1:
                    {
#pragma warning disable CA5350 // Do Not Use Weak Cryptographic Algorithms
                        using var sha1 = SHA1.Create();
#pragma warning restore CA5350 // Do Not Use Weak Cryptographic Algorithms

                        this.data = sha1.ComputeHash(data);
                        break;
                    }

                case HashAlgorithm.MD5:
                    {
#pragma warning disable CA5351 // Do Not Use Broken Cryptographic Algorithms
                        using var md5 = MD5.Create();
#pragma warning restore CA5351 // Do Not Use Broken Cryptographic Algorithms

                        this.data = md5.ComputeHash(data);
                        break;
                    }

                case HashAlgorithm.OnesCompliment:
                    {
                        byte val = 0xff;

                        foreach (var b in data)
                        {
                            val = (byte)(val ^ b);
                        }

                        this.data = new byte[1]
                        {
                        val,
                        };

                        break;
                    }

                default:
                    {
                        throw AbbotwareException.Create("Unexpected Switch Case:{0} Should not reach this code", hashType);
                    }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hash"/> class.
        /// </summary>
        /// <param name="hashType">hash algorithm to use</param>
        /// <param name="data">binary data to hash</param>
        /// <param name="key">binary key to use in hash algorithm</param>
        public Hash(HashAlgorithmWithKey hashType, byte[] data, byte[] key)
        {
            Arguments.NotNull(data, nameof(data));
            Arguments.NotNull(key, nameof(key));

            switch (hashType)
            {
                case HashAlgorithmWithKey.HmacSha256:
                    {
                        using var h = HMAC.Create("System.Security.Cryptography.HMACSHA256")!;

                        h.Key = key;

                        this.data = h.ComputeHash(data);

                        break;
                    }

                case HashAlgorithmWithKey.HmacOnesCompliment:
                    {
                        byte val = 0;

                        foreach (var b in key)
                        {
                            val = (byte)(val ^ b);
                        }

                        foreach (var b in data)
                        {
                            val = (byte)(val ^ b);
                        }

                        this.data = new byte[1] { val };

                        break;
                    }

                default:
                    {
                        throw AbbotwareException.Create("Unexpected Switch Case:{0} Should not reach this code", hashType);
                    }
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var temp = string.Format(CultureInfo.InvariantCulture, "Hash: {0}", BitConverter.ToString(this.data));

            return temp;
        }
    }
}