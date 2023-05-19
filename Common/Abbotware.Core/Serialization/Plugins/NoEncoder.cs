// -----------------------------------------------------------------------
// <copyright file="NoEncoder.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Plugins.Serialization
{
    using System;
    using Abbotware.Core;

    /// <summary>
    ///     Encoder that converts a string into a byte[] using the specified character encoding
    /// </summary>
    public class NoEncoder : IBidirectionalConverter<byte[], byte[]>, IBidirectionalConverter<byte[], ReadOnlyMemory<byte>>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NoEncoder" /> class.
        /// </summary>
        public NoEncoder()
        {
        }

        /// <inheritdoc />
        public byte[] Convert(byte[] input)
        {
            return input;
        }

        /// <inheritdoc />
        public byte[] Convert(ReadOnlyMemory<byte> input)
        {
            return input.ToArray();
        }

        /// <inheritdoc />
        ReadOnlyMemory<byte> IBidirectionalConverter<byte[], ReadOnlyMemory<byte>>.Convert(byte[] input)
        {
            return new ReadOnlyMemory<byte>(input);
        }
    }
}