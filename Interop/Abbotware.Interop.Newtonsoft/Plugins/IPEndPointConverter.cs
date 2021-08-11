// -----------------------------------------------------------------------
// <copyright file="IPEndPointConverter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Newtonsoft.Plugins
{
    using System;
    using System.Net;
    using Abbotware.Core;
    using global::Newtonsoft.Json;
    using global::Newtonsoft.Json.Linq;

    /// <inheritdoc />
    /// <summary>
    ///     Json converter for ip endpoint
    /// </summary>
    public class IPEndPointConverter : JsonConverter<IPEndPoint>
    {
        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, IPEndPoint? value, JsonSerializer serializer)
        {
            writer = Arguments.EnsureNotNull(writer, nameof(writer));
            serializer = Arguments.EnsureNotNull(serializer, nameof(serializer));

            if (value == null)
            {
                return;
            }

            writer.WriteStartObject();
            writer.WritePropertyName("Address");

            serializer.Serialize(writer, value.Address);

            writer.WritePropertyName("Port");
            writer.WriteValue(value.Port);
            writer.WriteEndObject();
        }

        /// <inheritdoc />
        public override IPEndPoint? ReadJson(JsonReader reader, Type objectType, IPEndPoint? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);

            if (jo == null)
            {
                return null;
            }

            var address = jo["Address"]!.ToObject<IPAddress>(serializer);
            var port = jo["Port"]!.Value<int>();
            return new IPEndPoint(address, port);
        }
    }
}