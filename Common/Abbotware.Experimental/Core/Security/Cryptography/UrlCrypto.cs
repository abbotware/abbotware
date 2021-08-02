// -----------------------------------------------------------------------
// <copyright file="UrlCrypto.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cryptography
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;

    /// <summary>
    /// Url Encryption Utilities
    /// </summary>
    public static class UrlCrypto
    {
        /// <summary>
        /// size of noise data
        /// </summary>
        private const int NoiseLength = 4;

        /// <summary>
        /// buffer of noise data
        /// </summary>
        private static readonly List<byte> NoiseBuffer = new List<byte>();

        /// <summary>
        /// Gets a block of 'noise' data
        /// </summary>
        private static byte[] Noise
        {
            get
            {
                lock (NoiseBuffer)
                {
                    if (NoiseBuffer.Count < NoiseLength)
                    {
                        NoiseBuffer.AddRange(Guid.NewGuid().ToByteArray());
                    }

                    var noise = NoiseBuffer.Take(NoiseLength).ToArray();
                    NoiseBuffer.RemoveRange(0, NoiseLength);
                    return noise;
                }
            }
        }

        /// <summary>
        /// This will produce a data block that is RSA encrypted using proprietary algorithms
        /// </summary>
        /// <param name="rsa">The rsa instance containing the crypto keys</param>
        /// <param name="text">The original text block to encrypt</param>
        /// <returns>An array of bytes representing the encrypted text</returns>
        public static byte[] EncryptText(RSA rsa, string text)
        {
            Arguments.NotNull(rsa, nameof(rsa));
            Arguments.NotNull(text, nameof(text));

            var input = Encoding.UTF8.GetBytes(text);

            var output = new List<byte>(text.Length * 3);

            while (input.Length > 0)
            {
                int length = Math.Min(input.Length, (rsa.KeySize / 8) - 1 - NoiseLength);

                byte[] head = input.Take(length).ToArray();
                byte[] cipher = RsaExtensions.EncryptUsingPrivateKey(rsa, Noise.Concat(head).ToArray());

                output.Add((byte)(cipher.Length >> 0));
                output.Add((byte)(cipher.Length >> 8));
                output.AddRange(cipher);

                input = input.Skip(length).ToArray();
            }

            return output.ToArray();
        }

        /// <summary>
        /// This will decrypt a cipher block previously encrypted using the EncryptText method
        /// </summary>
        /// <param name="rsa">The rsa instance containing the crypto keys</param>
        /// <param name="cipher">The cipher block that needs to be decypted</param>
        /// <returns>A decrypted text string</returns>
        public static string DecryptText(RSA rsa, byte[] cipher)
        {
            Arguments.NotNull(rsa, nameof(rsa));
            Arguments.NotNull(cipher, nameof(cipher));

            var output = new List<byte>(cipher.Length * 2);

            while (cipher.Length > 0)
            {
                int length = ((int)cipher[0] << 0)
                           + ((int)cipher[1] << 8);

                byte[] block = cipher.Skip(2).Take(length).ToArray();
                byte[] deciphered = RsaExtensions.DecryptUsingPublicKey(rsa, block);
                output.AddRange(deciphered.Skip(NoiseLength));

                cipher = cipher.Skip(2 + length).ToArray();
            }

            string deciphered_text = Encoding.UTF8.GetString(output.ToArray());
            return deciphered_text;
        }

        /// <summary>
        /// This will produce a URL-encoded string usable in a URL link
        /// </summary>
        /// <param name="original">The data to URL-encode</param>
        /// <returns>A string that can be place in a URL link</returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "false positive")]
        public static string ToUrlEncodedString(string original)
        {
            var bytes = Encoding.UTF8.GetBytes(original);

            string url_encoded = ToUrlEncodedString(bytes);
            return url_encoded;
        }

        /// <summary>
        /// This will produce a URL-encoded string usable in a URL link
        /// </summary>
        /// <param name="data">The data to URL-encode</param>
        /// <returns>A string that can be place in a URL link</returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "false positive")]
        public static string ToUrlEncodedString(byte[] data)
        {
            string base64 = Convert.ToBase64String(data);
            string url_encoded = HttpUtility.UrlEncode(base64);
            return url_encoded;
        }

        /// <summary>
        /// This will reverse the EncodeUrlData operation
        /// </summary>
        /// <param name="urlPart">A part of the URL that needs to be decoded</param>
        /// <returns>unencoded data</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#", Justification = "false positive")]
        public static byte[] FromUrlEncodedString(string urlPart)
        {
            string base64 = HttpUtility.UrlDecode(urlPart);
            byte[] buffer = Convert.FromBase64String(base64);
            return buffer;
        }
    }
}
