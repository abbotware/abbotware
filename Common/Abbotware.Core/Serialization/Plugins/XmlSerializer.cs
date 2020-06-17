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
    public class XmlSerializer : BaseBinarySerialization
    {
        /// <inheritdoc />
        public override object Decode(byte[] storage, Type type)
        {
            return storage.DeserializeViaXmlSerializer(type);
        }

        /// <inheritdoc />
        public override byte[] Encode<T>(T value)
        {
            Arguments.IsSerializable<T>();

            return value.ToXmlByteArrayViaXmlSerializer();
        }

        /// <inheritdoc />
        public override T Decode<T>(byte[] storage)
        {
            Arguments.IsSerializable<T>();

            return storage.DeserializeViaXmlSerializer<T>();
        }
    }
}