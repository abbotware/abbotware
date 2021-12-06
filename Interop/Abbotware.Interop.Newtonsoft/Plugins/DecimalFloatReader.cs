// <copyright file="DecimalFloatReader.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>

namespace Abbotware.Interop.Newtonsoft.Plugins
{
    using System;
    using Abbotware.Core;
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
            var isDecimal = objectType == typeof(decimal);

            reader = Arguments.EnsureNotNull(reader, nameof(reader));

            if (reader.TokenType == JsonToken.Float)
            {
                if (isDecimal)
                {
                    return (decimal)(double)reader.Value!;
                }
                else
                {
                    return (double)reader.Value!;
                }
            }
            else if (reader.TokenType == JsonToken.Integer)
            {
                if (isDecimal)
                {
                    return (decimal)(long)reader.Value!;
                }
                else
                {
                    return (double)(long)reader.Value!;
                }
            }
            else if (reader.TokenType == JsonToken.String)
            {
                var strValue = (string)reader.Value!;

                if (isDecimal)
                {
                    if (decimal.TryParse(strValue, out var parsedDecimal))
                    {
                        return parsedDecimal;
                    }
                }

#if NETSTANDARD2_0
                strValue = strValue.Replace("%", string.Empty);
#else
                strValue = strValue.Replace("%", string.Empty, StringComparison.InvariantCultureIgnoreCase);
#endif
                if (double.TryParse(strValue, out var parsedDouble))
                {
                    if (double.IsNaN(parsedDouble) || double.IsInfinity(parsedDouble))
                    {
                        return null;
                    }

                    if (isDecimal)
                    {
                        return (decimal)parsedDouble;
                    }
                    else
                    {
                        return parsedDouble;
                    }
                }

                switch (strValue)
                {
                    case "No Data":
                        return null;
                    case "-":
                        return null;
                    case "&mdash;":
                    case "R":
                        Console.WriteLine($"Bad {objectType} Data: {strValue} at path:{reader.Path}");
                        return null;
                    default:
                        throw new JsonSerializationException($"Cannot convert invalid value:{strValue} to {objectType} at path:{reader.Path}.");
                }
            }

            throw new JsonSerializationException($"Unexpected token parsing decimal. Expected Float or String, got {reader.TokenType}.");
        }

        /// <inheritdoc/>>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal) || objectType == typeof(decimal?) || objectType == typeof(double) || objectType == typeof(double?);
        }
    }
}
