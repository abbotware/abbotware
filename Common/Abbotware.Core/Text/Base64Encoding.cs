// -----------------------------------------------------------------------
// <copyright file="Base64Encoding.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Text
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    /// <summary>
    /// Minimal Base64 Encoding class
    /// </summary>
    public class Base64Encoding : Encoding
    {
        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override int GetByteCount(char[] chars, int index, int count)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override byte[] GetBytes(string s)
        {
            return System.Convert.FromBase64String(s);
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override int GetCharCount(byte[] bytes, int index, int count)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override int GetMaxByteCount(int charCount)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override int GetMaxCharCount(int byteCount)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override string GetString(byte[] bytes)
        {
            return System.Convert.ToBase64String(bytes);
        }
    }
}