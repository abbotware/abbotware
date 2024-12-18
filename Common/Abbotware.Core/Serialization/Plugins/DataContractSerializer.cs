﻿// -----------------------------------------------------------------------
// <copyright file="DataContractSerializer.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Serialization.Plugins
{
    using System;
    using Abbotware.Core.Serialization;
    using Abbotware.Core.Serialization.Helpers;

    /// <summary>
    ///     Encoder that converts a Serializable object into a byte[] using the DataContractSerializer
    /// </summary>
    public class DataContractSerializer : BaseBinarySerialization
    {
        /// <inheritdoc />
        public override object? Decode(byte[] storage, Type type)
        {
            storage = Arguments.EnsureNotNull(storage, nameof(storage));
            type = Arguments.EnsureNotNull(type, nameof(type));

            return storage.DeserializeViaDataContract(type);
        }

        /// <inheritdoc />
        public override byte[] Encode<T>(T value)
        {
            Arguments.IsSerializable<T>();

            return value.ToXmlByteArrayViaDataContract();
        }

        /// <inheritdoc />
        public override T Decode<T>(byte[] storage)
        {
            storage = Arguments.EnsureNotNull(storage, nameof(storage));
            Arguments.IsSerializable<T>();

            return storage.DeserializeViaDataContract<T>()!;
        }
    }
}