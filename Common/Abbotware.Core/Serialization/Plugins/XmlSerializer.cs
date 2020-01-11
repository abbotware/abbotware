// -----------------------------------------------------------------------
// <copyright file="XmlSerializer.cs" company="Abbotware, LLC">
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
    ///     Encoder that converts a Serializable object into a byte[] using the XmlSerializer
    /// </summary>
    public class XmlSerializer : IBinarySerializaton
    {
        /// <inheritdoc />
        public object Decode(byte[] storage, Type type)
        {
            return storage.DeserializeViaXmlSerializer(type);
        }

        /// <inheritdoc />
        public byte[] Encode<T>(T value)
        {
            Arguments.IsSerializable<T>();

            return value.ToXmlByteArrayViaXmlSerializer();
        }

        /// <inheritdoc />
        public T Decode<T>(byte[] storage)
        {
            Arguments.IsSerializable<T>();

            return storage.DeserializeViaXmlSerializer<T>();
        }
    }
}