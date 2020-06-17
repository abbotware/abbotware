// -----------------------------------------------------------------------
// <copyright file="StringEncoder.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Plugins.Serialization
{
    using System;
    using System.Text;
    using Abbotware.Core;

    /// <summary>
    ///     Encoder that converts a string into a byte[] using the specified character encoding
    /// </summary>
    public class StringEncoder : IBidirectionalConverter<string, byte[]>, IBidirectionalConverter<string, ReadOnlyMemory<byte>>
    {
        /// <summary>
        ///     string encoding type to use
        /// </summary>
        private readonly Encoding textEncoding;

        /// <summary>
        ///     Initializes a new instance of the <see cref="StringEncoder" /> class.
        /// </summary>
        public StringEncoder()
            : this(new UTF8Encoding())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StringEncoder" /> class.
        /// </summary>
        /// <param name="encoding">string encoding type to use</param>
        public StringEncoder(Encoding encoding)
        {
            Arguments.NotNull(encoding, nameof(encoding));

            this.textEncoding = encoding;
        }

        /// <inheritdoc />
        public string Convert(byte[] message)
        {
            return this.textEncoding.GetString(message);
        }

        /// <inheritdoc />
        public byte[] Convert(string message)
        {
            return this.textEncoding.GetBytes(message);
        }

        /// <inheritdoc />
        public string Convert(ReadOnlyMemory<byte> input)
        {
            return this.Convert(input.ToArray());
        }

        /// <inheritdoc />
        ReadOnlyMemory<byte> IBidirectionalConverter<string, ReadOnlyMemory<byte>>.Convert(string input)
        {
            return new ReadOnlyMemory<byte>(this.Convert(input));
        }
    }
}