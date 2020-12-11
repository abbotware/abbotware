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
    public class BinaryFormatterSerializer : BaseBinarySerialization
    {
        /// <inheritdoc />
        public override object Decode(byte[] storage, Type type)
        {
            return storage.DeserializeViaBinaryFormatter(type);
        }

        /// <inheritdoc />
        public override byte[] Encode<T>(T value)
        {
            return value.ToXmlByteArrayViaBinaryFormatter();
        }

        /// <inheritdoc />
        public override T Decode<T>(byte[] storage)
        {
            return storage.DeserializeViaBinaryFormatter<T>();
        }
    }
}