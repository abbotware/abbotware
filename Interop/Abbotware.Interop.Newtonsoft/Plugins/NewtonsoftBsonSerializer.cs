// -----------------------------------------------------------------------
// <copyright file="NewtonsoftBsonSerializer.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Newtonsoft.Plugins
{
    using System;
    using System.IO;
    using Abbotware.Core.Serialization;
    using global::Newtonsoft.Json;
    using global::Newtonsoft.Json.Bson;

    /// <summary>
    /// BSON serializer using Newtonsoft JSON
    /// </summary>
    public class NewtonsoftBsonSerializer : BaseBinarySerialization
    {
        /// <inheritdoc/>
        public override T Decode<T>(byte[] storage)
        {
            return (T)((IBinarySerializaton)this).Decode(storage, typeof(T));
        }

        /// <inheritdoc/>
        public override object Decode(byte[] storage, Type type)
        {
            using MemoryStream ms = new(storage);
            using var reader = new BsonDataReader(ms);

            var serializer = new JsonSerializer();
            return serializer.Deserialize(reader, type);
        }

        /// <inheritdoc/>
        public override byte[] Encode<T>(T value)
        {
            using var stream = new MemoryStream();
            using var writer = new BsonDataWriter(stream);

            var serializer = new JsonSerializer();
            serializer.Serialize(writer, value);

            return stream.ToArray();
        }
    }
}