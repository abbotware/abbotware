// -----------------------------------------------------------------------
// <copyright file="BetterDateTimeConverter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Newtonsoft.Plugins
{
    // Copyright (c) 2007 James Newton-King
    //
    // Permission is hereby granted, free of charge, to any person
    // obtaining a copy of this software and associated documentation
    // files (the "Software"), to deal in the Software without
    // restriction, including without limitation the rights to use,
    // copy, modify, merge, publish, distribute, sublicense, and/or sell
    // copies of the Software, and to permit persons to whom the
    // Software is furnished to do so, subject to the following
    // conditions:
    //
    // The above copyright notice and this permission notice shall be
    // included in all copies or substantial portions of the Software.
    //
    // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
    // EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
    // OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
    // NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
    // HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
    // WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
    // FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
    // OTHER DEALINGS IN THE SOFTWARE.
    using System;
    using System.Globalization;
    using Abbotware.Core;
    using Abbotware.Core.Diagnostics;
    using global::Newtonsoft.Json;
    using global::Newtonsoft.Json.Converters;

    /// <summary>
    /// Converts a <see cref="DateTime"/> to and from Unix epoch time via Milliseconds
    /// </summary>
    public class BetterDateTimeConverter : DateTimeConverterBase
    {
        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            reader = Arguments.EnsureNotNull(reader, nameof(reader));

            bool nullable = ReflectionHelper.IsNullableType(objectType);

            if (reader.TokenType == JsonToken.Null)
            {
                if (!nullable)
                {
                    throw new JsonSerializationException($"Cannot convert null value to {objectType}:{reader.Path}");
                }

                return null;
            }

            if (reader.TokenType == JsonToken.String)
            {
                var value = (string)reader.Value!;

                if (value == "0000-00-00")
                {
                    if (!nullable)
                    {
                        throw new JsonSerializationException($"Cannot convert null value to {objectType}:{reader.Path}");
                    }

                    return null;
                }

                if (!DateTimeOffset.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var parsed))
                {
                    throw new JsonSerializationException($"Cannot convert invalid value:{value} to {objectType}:{reader.Path}");
                }

                return parsed;
            }
            else
            {
                throw new JsonSerializationException($"Unexpected token parsing date. Expected String, got {reader.TokenType} for {reader.Path}");
            }
        }
    }
}