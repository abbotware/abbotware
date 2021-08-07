// -----------------------------------------------------------------------
// <copyright file="RsaExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cryptography
{
    using System;
    using System.Linq;
    using System.Numerics;
    using System.Security.Cryptography;

    /// <summary>
    /// Helper methods that deal with RSA encryption
    /// </summary>
    public static class RsaExtensions
    {
        /// <summary>
        /// Gets a Sign byte
        /// This is used to append a high-order 'zero' to a BigInteger to ensure it always has a positive sign
        /// </summary>
        private static byte[] Sign
        {
            get
            {
                return new byte[] { 0 };
            }
        }

        /// <summary>
        /// Gets a Marker byte
        /// This is used to insert a marker into a buffer to make the length of the buffer known during
        /// decryption. Buffers that initially start with leading zeros would otherwise produce incorrect
        /// results without the marker
        /// </summary>
        private static byte[] Marker
        {
            get
            {
                return new byte[] { 1 };
            }
        }

        /// <summary>
        /// This is the stantard way of encrypting data using the public RSA key
        /// </summary>
        /// <param name="cryptoServiceProvider">The RSACryptoServiceProvider instance containing the crypto keys</param>
        /// <param name="originalData">The data buffer to encrypt</param>
        /// <param name="paddingIsOaep">If true, uses OAEP padding, otherwise uses the older padding standard</param>
        /// <returns>cypher data</returns>
        public static byte[] EncryptUsingPublicKey(RSACryptoServiceProvider cryptoServiceProvider, byte[] originalData, bool paddingIsOaep)
        {
            Arguments.NotNull(cryptoServiceProvider, nameof(cryptoServiceProvider));
            Arguments.NotNull(originalData, nameof(originalData));

            return cryptoServiceProvider.Encrypt(originalData, paddingIsOaep);
        }

        /// <summary>
        /// This is the stantard way of decrypting data using the private RSA key
        /// </summary>
        /// <param name="cryptoServiceProvider">The RSACryptoServiceProvider instance containing the crypto keys</param>
        /// <param name="cypherData">The data buffer to decrypt</param>
        /// <param name="paddingIsOaep">If true, uses OAEP padding, otherwise uses the older padding standard</param>
        /// <returns>original data</returns>
        public static byte[] DecryptUsingPrivateKey(RSACryptoServiceProvider cryptoServiceProvider, byte[] cypherData, bool paddingIsOaep)
        {
            Arguments.NotNull(cryptoServiceProvider, nameof(cryptoServiceProvider));
            Arguments.NotNull(cypherData, nameof(cypherData));

            return cryptoServiceProvider.Decrypt(cypherData, paddingIsOaep);
        }

        // [ TODO ]
        // We need to use OAEP padding within the encrypted block if we need to be
        // compatible with other decryption libraries. There may also be security
        // reasons for using the proposed OAEP padding. See:
        // http://tools.ietf.org/html/rfc3447
        //

        /// <summary>
        /// This is the non-standard way of encrypting data using the private RSA key
        /// </summary>
        /// <param name="rsa">The RSA instance containing the crypto keys</param>
        /// <param name="originalData">The data buffer to encrypt</param>
        /// <returns>cypher data</returns>
        public static byte[] EncryptUsingPrivateKey(RSA rsa, byte[] originalData)
        {
            Arguments.NotNull(rsa, nameof(rsa));
            Arguments.NotNull(originalData, nameof(originalData));

            if (originalData.Length > ((rsa.KeySize / 8) - 1))
            {
                throw new ArgumentException("The input buffer is too large");
            }

            var parameters = rsa.ExportParameters(true);

            BigInteger original_data = originalData.ToBigInteger(true);
            BigInteger private_exponent = parameters.D.ToBigInteger();        // D is the private exponent ( i.e. the private key )
            BigInteger public_modulus = parameters.Modulus.ToBigInteger();  // Modulus == n == pq ( both public and private modulus )

            BigInteger cypher = BigInteger.ModPow(original_data, private_exponent, public_modulus);
            return cypher.ToByteArray().Reverse().ToArray();
        }

        /// <summary>
        /// This is the non-standard way of decrypting data using the pseudo-public RSA key
        /// </summary>
        /// <param name="rsa">The RSACryptoServiceProvider instance containing the crypto keys</param>
        /// <param name="cypherData">The data buffer to decrypt</param>
        /// <returns>original data</returns>
        public static byte[] DecryptUsingPublicKey(RSA rsa, byte[] cypherData)
        {
            Arguments.NotNull(rsa, nameof(rsa));
            Arguments.NotNull(cypherData, nameof(cypherData));

            if (cypherData.Length > ((rsa.KeySize / 8) + 1))
            {
                throw new ArgumentException("The input buffer is too large");
            }

            var parameters = rsa.ExportParameters(false);

            BigInteger cypher_data = cypherData.ToBigInteger();
            BigInteger public_exponent = parameters.Exponent.ToBigInteger();  // Exponent == e ( the public exponent )
            BigInteger public_modulus = parameters.Modulus.ToBigInteger();   // Modulus  == n ( both public and private modulus )

            BigInteger deciphered = BigInteger.ModPow(cypher_data, public_exponent, public_modulus);

            var bytes = deciphered.ToByteArray().Reverse().ToList();
            if (bytes[0] != Marker[0])
            {
                throw new CryptographicException("Was expecting a marker value within the decrypted data");
            }

            bytes.RemoveAt(0);     // remove the marker we inserted before during the encryption
            return bytes.ToArray();  // this should be the original buffer with the original length
        }

        /// <summary>
        ///  This returns the full-set of crypto keys, containing the private key
        ///  as well as the public key. This set of keys is kept secret
        /// </summary>
        /// <param name="containerName">RSA key container name</param>
        /// <returns>RSA container</returns>
        public static RSACryptoServiceProvider GetRsaServiceProvider(string containerName)
        {
            CspParameters cspParams = new()
            {
                KeyContainerName = containerName,
                Flags = CspProviderFlags.UseExistingKey | CspProviderFlags.UseMachineKeyStore,
            };

            RSACryptoServiceProvider rsa = new(cspParams);
            return rsa;
        }

        /// <summary>
        /// The extension method that converts a data buffer into a very large integer value
        /// </summary>
        /// <param name="original">The buffer of data that will be turned into an integer</param>
        /// <param name="addMarker">If true, a marker will get added as the high-order digit of the resulting value</param>
        /// <returns>A structure that represents a very large integer value</returns>
        private static BigInteger ToBigInteger(this byte[] original, bool addMarker = false)
        {
            // We reverse the byte array to make the data little-endian based due to
            // the requirements of the System.Numerics.BigInteger structure. Then we
            // insert a positive-value marker at the high-order position of the data
            // buffer so that buffers with leading zeroes retain their original size.
            // The buffer is going to be treated as one very large integer value.
            byte[] le_array = original
                .Reverse()
                .Concat(addMarker ? Marker : Sign)
                .ToArray();
            return new BigInteger(le_array);
        }
    }
}
