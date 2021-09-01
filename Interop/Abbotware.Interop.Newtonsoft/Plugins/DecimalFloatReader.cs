// <copyright file="DecimalFloatReader.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>

namespace Abbotware.Interop.Newtonsoft.Plugins
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Diagnostics;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Supports NAN -> Decimal? conversions
    /// </summary>
    public class DecimalFloatReader : JsonConverter
    {
        /// <inheritdoc/>>
        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc/>>
        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        /// <inheritdoc/>>
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>>
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            reader = Arguments.EnsureNotNull(reader, nameof(reader));

            if (reader.TokenType == JsonToken.Float)
            {
                return (decimal)(double)reader.Value!;
            }
            else if (reader.TokenType == JsonToken.Integer)
            {
                return (decimal)(long)reader.Value!;
            }
            else if (reader.TokenType == JsonToken.String)
            {
                if (decimal.TryParse((string)reader.Value!, out var parsedDecimal))
                {
                    return parsedDecimal;
                }

                if (double.TryParse((string)reader.Value!, out var parsedDouble))
                {
                    if (double.IsNaN(parsedDouble) || double.IsInfinity(parsedDouble))
                    {
                        return null;
                    }
                }

                throw new JsonSerializationException($"Cannot convert invalid value to {objectType}.");
            }

            throw new JsonSerializationException($"Unexpected token parsing date. Expected Float or String, got {reader.TokenType}.");
        }

        /// <inheritdoc/>>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal) || objectType == typeof(decimal?);
        }
    }
}
