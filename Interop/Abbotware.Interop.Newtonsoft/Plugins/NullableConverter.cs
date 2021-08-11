// -----------------------------------------------------------------------
// <copyright file="NullableConverter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Newtonsoft.Plugins
{
    using System;
    using Abbotware.Core;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Convertable for nullable structs
    /// </summary>
    public class NullableConverter : JsonConverter
    {
        /// <inheritdoc/>
        public override bool CanConvert(Type objectType) => Nullable.GetUnderlyingType(objectType) != null;

        /// <inheritdoc/>
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            reader = Arguments.EnsureNotNull(reader, nameof(reader));
            serializer = Arguments.EnsureNotNull(serializer, nameof(serializer));

            if (reader.TokenType == JsonToken.String)
            {
                if (reader.Value is string)
                {
                    var val = reader.Value as string;

                    if (string.IsNullOrWhiteSpace(val))
                    {
                        return null;
                    }
                }
            }

            var underlyingType = Nullable.GetUnderlyingType(objectType);

            return serializer.Deserialize(reader, underlyingType);
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            serializer = Arguments.EnsureNotNull(serializer, nameof(serializer));

            serializer.Serialize(writer, value);
        }
    }
}
