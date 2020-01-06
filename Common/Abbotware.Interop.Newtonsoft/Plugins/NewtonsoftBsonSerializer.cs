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
    public class NewtonsoftBsonSerializer : IBinarySerializaton
    {
        /// <inheritdoc/>
        public T Decode<T>(byte[] storage)
        {
            return (T)((IBinarySerializaton)this).Decode(storage, typeof(T));
        }

        /// <inheritdoc/>
        public object Decode(byte[] storage, Type type)
        {
            using MemoryStream ms = new MemoryStream(storage);
            using var reader = new BsonDataReader(ms);

            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize(reader, type);
        }

        /// <inheritdoc/>
        public byte[] Encode<T>(T @object)
        {
            using var stream = new MemoryStream();
            using (var writer = new BsonDataWriter(stream))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, @object);
            }

            return stream.ToArray();
        }
    }
}