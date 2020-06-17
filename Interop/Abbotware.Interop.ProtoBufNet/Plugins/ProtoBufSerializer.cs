// -----------------------------------------------------------------------
// <copyright file="ProtoBufSerializer.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.ProtoBufNet.Plugins
{
    using System;
    using System.IO;
    using Abbotware.Core.Serialization;
    using global::ProtoBuf;

    /// <summary>
    ///     Encoder that converts a Serializable object into a byte[] using the XmlSerializer
    /// </summary>
    public class ProtoBufSerializer : IBinarySerializaton
    {
        /// <inheritdoc />
        public object Decode(byte[] storage, Type type)
        {
            using var stream = new MemoryStream(storage);

            var m = Serializer.Deserialize(type, stream);

            return m;
        }

        /// <inheritdoc />
        public byte[] Encode<T>(T value)
        {
            using var stream = new MemoryStream();

            Serializer.Serialize(stream, value);

            return stream.ToArray();
        }

        /// <inheritdoc />
        public T Decode<T>(byte[] storage)
        {
            using var stream = new MemoryStream(storage);

            var m = Serializer.Deserialize<T>(stream);

            return m;
        }

        /// <inheritdoc />
        public object Decode(ReadOnlyMemory<byte> storage, Type type)
        {
            return this.Decode(storage.ToArray(), type);
        }

        /// <inheritdoc />
        ReadOnlyMemory<byte> IEncode<ReadOnlyMemory<byte>>.Encode<T>(T value)
        {
            return new ReadOnlyMemory<byte>(this.Encode(value));
        }

        /// <inheritdoc />
        public T Decode<T>(ReadOnlyMemory<byte> storage)
        {
            return this.Decode<T>(storage.ToArray());
        }
    }
}