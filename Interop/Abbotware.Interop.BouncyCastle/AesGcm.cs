﻿// -----------------------------------------------------------------------
// <copyright file="AesGcm.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.BouncyCastle
{
    using System;
    using System.IO;
    using System.Text;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using global::Org.BouncyCastle.Crypto;
    using global::Org.BouncyCastle.Crypto.Engines;
    using global::Org.BouncyCastle.Crypto.Generators;
    using global::Org.BouncyCastle.Crypto.Modes;
    using global::Org.BouncyCastle.Crypto.Parameters;
    using global::Org.BouncyCastle.Security;

    /// <summary>
    /// Wrapper class for AES with GCM
    /// </summary>
    /// <remarks>
    /// This work (Modern Encryption of a String C#, by James Tuley),
    /// identified by James Tuley, is free of known copyright restrictions.
    /// https://gist.github.com/4336842
    /// http://creativecommons.org/publicdomain/mark/1.0/
    /// </remarks>
    public static class AesGcm
    {
        /// <summary>
        /// Nonce bit size (Preconfigured Encryption Parameters)
        /// </summary>
        private const int NonceBitSize = 128;

        /// <summary>
        /// MAC bit size (Preconfigured Encryption Parameters)
        /// </summary>
        private const int MacBitSize = 128;

        /// <summary>
        /// key bit size (Preconfigured Encryption Parameters)
        /// </summary>
        private const int KeyBitSize = 256;

        /// <summary>
        /// salt bit size (Preconfigured Password Key Derivation Parameters)
        /// </summary>
        private const int SaltBitSize = 128;

        /// <summary>
        /// iterations (Preconfigured Password Key Derivation Parameters)
        /// </summary>
        private const int Iterations = 10000;

        /// <summary>
        /// minimum password length (Preconfigured Password Key Derivation Parameters)
        /// </summary>
        private const int MinPasswordLength = 12;

        /// <summary>
        /// Random number generator
        /// </summary>
        private static readonly SecureRandom Random = new();

        /// <summary>
        /// Helper that generates a random new key on each call.
        /// </summary>
        /// <returns>new key</returns>
        public static byte[] NewKey()
        {
            var key = new byte[KeyBitSize / 8];
            Random.NextBytes(key);
            return key;
        }

        /// <summary>
        /// Simple Encryption And Authentication (AES-GCM) of a UTF8 string.
        /// </summary>
        /// <param name="secretMessage">The secret message.</param>
        /// <param name="key">The key.</param>
        /// <param name="nonSecretPayload">Optional non-secret payload.</param>
        /// <returns>
        /// Encrypted Message
        /// </returns>
        /// <exception cref="System.ArgumentException">Secret Message Required!;secretMessage</exception>
        /// <remarks>
        /// Adds overhead of (Optional-Payload + BlockSize(16) + Message +  HMac-Tag(16)) * 1.33 Base64
        /// </remarks>
        public static string SimpleEncrypt(string secretMessage, byte[] key, byte[]? nonSecretPayload = null)
        {
            if (string.IsNullOrEmpty(secretMessage))
            {
                throw new ArgumentException("Secret Message Required!", nameof(secretMessage));
            }

            var plainText = Encoding.UTF8.GetBytes(secretMessage);
            var cipherText = SimpleEncrypt(plainText, key, nonSecretPayload);
            return Convert.ToBase64String(cipherText);
        }

        /// <summary>
        /// Simple Decryption and Authentication (AES-GCM) of a UTF8 Message
        /// </summary>
        /// <param name="encryptedMessage">The encrypted message.</param>
        /// <param name="key">The key.</param>
        /// <param name="nonSecretPayloadLength">Length of the optional non-secret payload.</param>
        /// <returns>Decrypted Message</returns>
        public static string? SimpleDecrypt(string encryptedMessage, byte[] key, int nonSecretPayloadLength = 0)
        {
            if (string.IsNullOrEmpty(encryptedMessage))
            {
                throw new ArgumentException("Encrypted Message Required!", nameof(encryptedMessage));
            }

            var cipherText = Convert.FromBase64String(encryptedMessage);
            var plainText = SimpleDecrypt(cipherText, key, nonSecretPayloadLength);
            return plainText == null ? null : Encoding.UTF8.GetString(plainText);
        }

        /// <summary>
        /// Simple Encryption And Authentication (AES-GCM) of a UTF8 String
        /// using key derived from a password (PBKDF2).
        /// </summary>
        /// <param name="secretMessage">The secret message.</param>
        /// <param name="password">The password.</param>
        /// <param name="nonSecretPayload">The non secret payload.</param>
        /// <returns>
        /// Encrypted Message
        /// </returns>
        /// <remarks>
        /// Significantly less secure than using random binary keys.
        /// Adds additional non secret payload for key generation parameters.
        /// </remarks>
        public static string SimpleEncryptWithPassword(string secretMessage, string password, byte[]? nonSecretPayload = null)
        {
            if (string.IsNullOrEmpty(secretMessage))
            {
                throw new ArgumentException("Secret Message Required!", nameof(secretMessage));
            }

            var plainText = Encoding.UTF8.GetBytes(secretMessage);
            var cipherText = SimpleEncryptWithPassword(plainText, password, nonSecretPayload);
            return Convert.ToBase64String(cipherText);
        }

        /// <summary>
        /// Simple Decryption and Authentication (AES-GCM) of a UTF8 message
        /// using a key derived from a password (PBKDF2)
        /// </summary>
        /// <param name="encryptedMessage">The encrypted message.</param>
        /// <param name="password">The password.</param>
        /// <param name="nonSecretPayloadLength">Length of the non secret payload.</param>
        /// <returns>
        /// Decrypted Message
        /// </returns>
        /// <exception cref="System.ArgumentException">Encrypted Message Required!;encryptedMessage</exception>
        /// <remarks>
        /// Significantly less secure than using random binary keys.
        /// </remarks>
        public static string? SimpleDecryptWithPassword(string encryptedMessage, string password, int nonSecretPayloadLength = 0)
        {
            if (encryptedMessage.IsBlank())
            {
                throw new ArgumentException("Encrypted Message Required!", nameof(encryptedMessage));
            }

            var cipherText = Convert.FromBase64String(encryptedMessage);
            var plainText = SimpleDecryptWithPassword(cipherText, password, nonSecretPayloadLength);
            return plainText == null ? null : Encoding.UTF8.GetString(plainText);
        }

        /// <summary>
        /// Simple Encryption And Authentication (AES-GCM) of a UTF8 string.
        /// </summary>
        /// <param name="secretMessage">The secret message.</param>
        /// <param name="key">The key.</param>
        /// <param name="nonSecretPayload">Optional non-secret payload.</param>
        /// <returns>Encrypted Message</returns>
        /// <remarks>
        /// Adds overhead of (Optional-Payload + BlockSize(16) + Message +  HMac-Tag(16)) * 1.33 Base64
        /// </remarks>
        public static byte[] SimpleEncrypt(byte[] secretMessage, byte[] key, byte[]? nonSecretPayload = null)
        {
            // User Error Checks
            if (key == null || key.Length != KeyBitSize / 8)
            {
                throw new ArgumentException("Key needs to be {KeyBitSize} bits!", nameof(key));
            }

            if (secretMessage == null || secretMessage.Length == 0)
            {
                throw new ArgumentException("Secret Message Required!", nameof(secretMessage));
            }

            // Non-secret Payload Optional
            nonSecretPayload ??= Array.Empty<byte>();

            // Using random nonce large enough not to repeat
            var nonce = new byte[NonceBitSize / 8];
            Random.NextBytes(nonce, 0, nonce.Length);

            var cipher = new GcmBlockCipher(new AesEngine());
            var parameters = new AeadParameters(new KeyParameter(key), MacBitSize, nonce, nonSecretPayload);
            cipher.Init(true, parameters);

            // Generate Cipher Text With Auth Tag
            var cipherText = new byte[cipher.GetOutputSize(secretMessage.Length)];
            var len = cipher.ProcessBytes(secretMessage, 0, secretMessage.Length, cipherText, 0);
            cipher.DoFinal(cipherText, len);

            // Assemble Message
            using var combinedStream = new MemoryStream();
            using (var binaryWriter = new BinaryWriter(combinedStream))
            {
                // Prepend Authenticated Payload
                binaryWriter.Write(nonSecretPayload);

                // Prepend Nonce
                binaryWriter.Write(nonce);

                // Write Cipher Text
                binaryWriter.Write(cipherText);
            }

            return combinedStream.ToArray();
        }

        /// <summary>
        /// Simple Decryption and Authentication (AES-GCM) of a UTF8 Message
        /// </summary>
        /// <param name="encryptedMessage">The encrypted message.</param>
        /// <param name="key">The key.</param>
        /// <param name="nonSecretPayloadLength">Length of the optional non-secret payload.</param>
        /// <returns>Decrypted Message</returns>
        public static byte[]? SimpleDecrypt(byte[] encryptedMessage, byte[] key, int nonSecretPayloadLength = 0)
        {
            // User Error Checks
            if (key == null || key.Length != KeyBitSize / 8)
            {
                throw new ArgumentException($"Key needs to be {KeyBitSize} bits!", nameof(key));
            }

            if (encryptedMessage == null || encryptedMessage.Length == 0)
            {
                throw new ArgumentException("Encrypted Message Required!", nameof(encryptedMessage));
            }

            using var cipherStream = new MemoryStream(encryptedMessage);
            using var cipherReader = new BinaryReader(cipherStream);

            // Grab Payload
            var nonSecretPayload = cipherReader.ReadBytes(nonSecretPayloadLength);

            // Grab Nonce
            var nonce = cipherReader.ReadBytes(NonceBitSize / 8);

            var cipher = new GcmBlockCipher(new AesEngine());
            var parameters = new AeadParameters(new KeyParameter(key), MacBitSize, nonce, nonSecretPayload);
            cipher.Init(false, parameters);

            // Decrypt Cipher Text
            var cipherText = cipherReader.ReadBytes(encryptedMessage.Length - nonSecretPayloadLength - nonce.Length);
            var plainText = new byte[cipher.GetOutputSize(cipherText.Length)];

            try
            {
                var len = cipher.ProcessBytes(cipherText, 0, cipherText.Length, plainText, 0);
                cipher.DoFinal(plainText, len);
            }
            catch (InvalidCipherTextException)
            {
                // Return null if it doesn't authenticate
                return null;
            }

            return plainText;
        }

        /// <summary>
        /// Simple Encryption And Authentication (AES-GCM) of a UTF8 String
        /// using key derived from a password.
        /// </summary>
        /// <param name="secretMessage">The secret message.</param>
        /// <param name="password">The password.</param>
        /// <param name="nonSecretPayload">The non secret payload.</param>
        /// <returns>
        /// Encrypted Message
        /// </returns>
        /// <exception cref="System.ArgumentException">Must have a password of minimum length;password</exception>
        /// <remarks>
        /// Significantly less secure than using random binary keys.
        /// Adds additional non secret payload for key generation parameters.
        /// </remarks>
        public static byte[] SimpleEncryptWithPassword(byte[] secretMessage, string password, byte[]? nonSecretPayload = null)
        {
            nonSecretPayload ??= Array.Empty<byte>();

            password = Arguments.EnsureNotNull(password, nameof(password));

            // User Error Checks
            if (password.Length < MinPasswordLength)
            {
                throw new ArgumentException($"Must have a password of at least {MinPasswordLength} characters!", nameof(password));
            }

            if (secretMessage == null || secretMessage.Length == 0)
            {
                throw new ArgumentException("Secret Message Required!", nameof(secretMessage));
            }

            var generator = new Pkcs5S2ParametersGenerator();

            // Use Random Salt to minimize pre-generated weak password attacks.
            var salt = new byte[SaltBitSize / 8];
            Random.NextBytes(salt);

            generator.Init(
                PbeParametersGenerator.Pkcs5PasswordToBytes(password.ToCharArray()),
                salt,
                Iterations);

            // Generate Key
            var key = (KeyParameter)generator.GenerateDerivedMacParameters(KeyBitSize);

            // Create Full Non Secret Payload
            var payload = new byte[salt.Length + nonSecretPayload.Length];
            Array.Copy(nonSecretPayload, payload, nonSecretPayload.Length);
            Array.Copy(salt, 0, payload, nonSecretPayload.Length, salt.Length);

            return SimpleEncrypt(secretMessage, key.GetKey(), payload);
        }

        /// <summary>
        /// Simple Decryption and Authentication of a UTF8 message
        /// using a key derived from a password
        /// </summary>
        /// <param name="encryptedMessage">The encrypted message.</param>
        /// <param name="password">The password.</param>
        /// <param name="nonSecretPayloadLength">Length of the non secret payload.</param>
        /// <returns>
        /// Decrypted Message
        /// </returns>
        /// <exception cref="System.ArgumentException">Must have a password of minimum length;password</exception>
        /// <remarks>
        /// Significantly less secure than using random binary keys.
        /// </remarks>
        public static byte[]? SimpleDecryptWithPassword(byte[] encryptedMessage, string password, int nonSecretPayloadLength = 0)
        {
            password = Arguments.EnsureNotNullOrWhitespace(password, nameof(password));

            // User Error Checks
            if (password.Length < MinPasswordLength)
            {
                throw new ArgumentException($"Must have a password of at least {MinPasswordLength} characters!", nameof(password));
            }

            if (encryptedMessage == null || encryptedMessage.Length == 0)
            {
                throw new ArgumentException("Encrypted Message Required!", nameof(encryptedMessage));
            }

            var generator = new Pkcs5S2ParametersGenerator();

            // Grab Salt from Payload
            var salt = new byte[SaltBitSize / 8];
            Array.Copy(encryptedMessage, nonSecretPayloadLength, salt, 0, salt.Length);

            generator.Init(
                PbeParametersGenerator.Pkcs5PasswordToBytes(password.ToCharArray()),
                salt,
                Iterations);

            // Generate Key
            var key = (KeyParameter)generator.GenerateDerivedMacParameters(KeyBitSize);

            return SimpleDecrypt(encryptedMessage, key.GetKey(), salt.Length + nonSecretPayloadLength);
        }
    }
}