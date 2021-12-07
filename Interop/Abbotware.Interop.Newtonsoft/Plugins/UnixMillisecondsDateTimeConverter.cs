// -----------------------------------------------------------------------
// <copyright file="UnixMillisecondsDateTimeConverter.cs" company="Abbotware, LLC">
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
    using Abbotware.Core;
    using Abbotware.Core.Diagnostics;
    using global::Newtonsoft.Json;
    using global::Newtonsoft.Json.Converters;

    /// <summary>
    /// Converts a <see cref="DateTime"/> to and from Unix epoch time via Milliseconds
    /// </summary>
    public class UnixMillisecondsDateTimeConverter : DateTimeConverterBase
    {
        internal static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            writer = Arguments.EnsureNotNull(writer, nameof(writer));

            long milliseconds;

            if (value is DateTime dateTime)
            {
                milliseconds = (long)(dateTime.ToUniversalTime() - UnixEpoch).TotalMilliseconds;
            }
            else if (value is DateTimeOffset dateTimeOffset)
            {
                milliseconds = (long)(dateTimeOffset.ToUniversalTime() - UnixEpoch).TotalMilliseconds;
            }
            else
            {
                throw new JsonSerializationException("Expected date object value.");
            }

            if (milliseconds < 0)
            {
                throw new JsonSerializationException("Cannot convert date value that is before Unix epoch of 00:00:00 UTC on 1 January 1970.");
            }

            writer.WriteValue(milliseconds);
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
                    throw new JsonSerializationException($"Cannot convert null value to {objectType}");
                }

                return null;
            }

            long milliseconds;

            if (reader.TokenType == JsonToken.Integer)
            {
                milliseconds = (long)reader.Value!;
            }
            else if (reader.TokenType == JsonToken.String)
            {
                var value = (string)reader.Value!;

                if (!long.TryParse(value, out milliseconds))
                {
                    throw new JsonSerializationException($"Cannot convert invalid value:{value} to {objectType}.");
                }
            }
            else
            {
                throw new JsonSerializationException($"Unexpected token parsing date. Expected Integer or String, got {reader.TokenType}.");
            }

            if (milliseconds >= 0)
            {
                DateTime d = UnixEpoch.AddMilliseconds(milliseconds);

                Type t = nullable
                    ? Nullable.GetUnderlyingType(objectType)!
                    : objectType;

                if (t == typeof(DateTimeOffset))
                {
                    return new DateTimeOffset(d, TimeSpan.Zero);
                }

                return d;
            }
            else
            {
                throw new JsonSerializationException($"Cannot convert value that is before Unix epoch of 00:00:00 UTC on 1 January 1970 to {objectType}.");
            }
        }
    }
}