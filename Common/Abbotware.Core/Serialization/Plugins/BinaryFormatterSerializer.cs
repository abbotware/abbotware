// -----------------------------------------------------------------------
// <copyright file="BinaryFormatterSerializer.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Serialization.Plugins
{
    using System;
    using Abbotware.Core.Serialization;
    using Abbotware.Core.Serialization.Helpers;

    /// <summary>
    ///     Encoder that converts a serializable object into a byte[] using the BinaryFormatter serializer
    /// </summary>
    public class BinaryFormatterSerializer : IBinarySerializaton, IObjectDeserialization<byte[]>
    {
        /// <inheritdoc />
        public object Decode(byte[] storage, Type type)
        {
            return this.Decode(storage);
        }

        /// <inheritdoc />
        public byte[] Encode<T>(T value)
        {
            return value.ToXmlByteArrayViaBinaryFormatter();
        }

        /// <inheritdoc />
        public T Decode<T>(byte[] storage)
        {
            return storage.DeserializeViaBinaryFormatter<T>();
        }

        /// <inheritdoc />
        public object Decode(byte[] storage)
        {
            return storage.DeserializeViaBinaryFormatter();
        }
    }
}