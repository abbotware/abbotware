// -----------------------------------------------------------------------
// <copyright file="ProtoBufSerializer.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
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
    public class ProtoBufSerializer : BaseBinarySerialization
    {
        /// <inheritdoc />
        public override object Decode(byte[] storage, Type type)
        {
            using var stream = new MemoryStream(storage);

            var m = Serializer.Deserialize(type, stream);

            return m;
        }

        /// <inheritdoc />
        public override byte[] Encode<T>(T value)
        {
            using var stream = new MemoryStream();

            Serializer.Serialize(stream, value);

            return stream.ToArray();
        }

        /// <inheritdoc />
        public override T Decode<T>(byte[] storage)
        {
            using var stream = new MemoryStream(storage);

            var m = Serializer.Deserialize<T>(stream);

            return m;
        }
    }
}