// -----------------------------------------------------------------------
// <copyright file="Aes.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Security.Cryptography
{
    using System;
    using System.IO;
    using System.Security;
    using System.Security.Cryptography;
    using System.Text;
    using Abbotware.Core.Objects;

    /// <summary>
    ///     Class that can Aes encrype/decrypt data
    /// </summary>
    public class Aes : BaseComponent
    {
        /// <summary>
        ///     default encryption key
        /// </summary>
        private static readonly byte[] DefaultKey =
            {
                123,
                217,
                19,
                11,
                24,
                26,
                85,
                45,
                114,
                184,
                27,
                162,
                37,
                112,
                222,
                209,
                241,
                24,
                175,
                144,
                173,
                53,
                196,
                29,
                24,
                26,
                17,
                218,
                131,
                236,
                53,
                209,
            };

        /// <summary>
        ///     default initialization vector
        /// </summary>
        private static readonly byte[] DefaultInitializationVector =
            {
                146,
                64,
                191,
                111,
                23,
                3,
                113,
                119,
                231,
                121,
                221,
                112,
                79,
                32,
                114,
                156,
            };

        /// <summary>
        ///     decrypter object
        /// </summary>
        private readonly ICryptoTransform decryptor;

        /// <summary>
        ///     encoder for binary data
        /// </summary>
        private readonly Encoding encoder;

        /// <summary>
        ///     encryprter  object
        /// </summary>
        private readonly ICryptoTransform encryptor;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Aes" /> class.
        /// </summary>
        public Aes()
            : this(Aes.DefaultKey, Aes.DefaultInitializationVector)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Aes" /> class.
        /// </summary>
        /// <param name="key">encryption key</param>
        /// <param name="iv">initialization vector</param>
        public Aes(byte[] key, byte[] iv)
        {
            using (var rm = new RijndaelManaged())
            {
                this.encryptor = rm.CreateEncryptor(key, iv);
                this.decryptor = rm.CreateDecryptor(key, iv);
            }

            this.encoder = new UTF8Encoding();
        }

        /// <summary>
        ///     Encrypt string to Base64 encoded string
        /// </summary>
        /// <param name="clearText">plain text</param>
        /// <returns>encrypted string</returns>
        public string EncryptToBase64(string clearText)
        {
            Arguments.NotNull(clearText, nameof(clearText));

            return Convert.ToBase64String(this.Encrypt(this.encoder.GetBytes(clearText)));
        }

        /// <summary>
        ///     decrypt Base64 encoded string into a SecureString
        /// </summary>
        /// <param name="cipherText">encrypted text</param>
        /// <returns>encrypted string</returns>
        public SecureString DecryptFromBase64ToSecureString(string cipherText)
        {
            Arguments.NotNull(cipherText, nameof(cipherText));

            var binary = this.Decrypt(Convert.FromBase64String(cipherText));

            // TODO: this is a hack for now
            var hack = this.encoder.GetString(binary);

            var secure = new SecureString();

            Array.ForEach(hack.ToCharArray(), secure.AppendChar);

            secure.MakeReadOnly();

            return secure;
        }

        /// <summary>
        ///     Decrypts a base64 encoded string into plain text
        /// </summary>
        /// <param name="cipherText">encrypted text</param>
        /// <returns>plain text</returns>
        public string DecryptFromBase64ToString(string cipherText)
        {
            Arguments.NotNull(cipherText, nameof(cipherText));

            return this.encoder.GetString(this.Decrypt(Convert.FromBase64String(cipherText)));
        }

        /// <summary>
        ///     Encrypt binary data
        /// </summary>
        /// <param name="buffer">buffer to encrypt</param>
        /// <returns>encrypted buffer</returns>
        public byte[] Encrypt(byte[] buffer)
        {
            Arguments.NotNull(buffer, nameof(buffer));

            return Aes.Transform(buffer, this.encryptor);
        }

        /// <summary>
        ///     Decrypt binary data
        /// </summary>
        /// <param name="buffer">buffer to decrypt</param>
        /// <returns>decrypted buffer</returns>
        public byte[] Decrypt(byte[] buffer)
        {
            Arguments.NotNull(buffer, nameof(buffer));

            return Aes.Transform(buffer, this.decryptor);
        }

        /// <summary>
        ///     Performs a cryptographic transformation
        /// </summary>
        /// <param name="buffer">buffer to apply changes</param>
        /// <param name="cryptoTransform">operation</param>
        /// <returns>transforemd data</returns>
        protected static byte[] Transform(byte[] buffer, ICryptoTransform cryptoTransform)
        {
            buffer = Arguments.EnsureNotNull(buffer, nameof(buffer));
            Arguments.NotNull(cryptoTransform, nameof(cryptoTransform));

            using var memStream = new MemoryStream();
            using var cs = new CryptoStream(memStream, cryptoTransform, CryptoStreamMode.Write);

            cs.Write(buffer, 0, buffer.Length);

            return memStream.ToArray();
        }

        /// <inheritdoc />
        protected override void OnDisposeManagedResources()
        {
            base.OnDisposeManagedResources();

            this.decryptor?.Dispose();
            this.encryptor?.Dispose();
        }
    }
}