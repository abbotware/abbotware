// -----------------------------------------------------------------------
// <copyright file="NullableTimeSpanConverter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.SystemTextJson
{
    using System;
    using System.Globalization;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;

    /// <summary>
    /// Custom Json Converter for TimeSpan?
    /// </summary>
    public class NullableTimeSpanConverter : JsonConverter<TimeSpan?>
    {
        /// <inheritdoc/>
        public override TimeSpan? Read(
                   ref Utf8JsonReader reader,
                   Type typeToConvert,
                   JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            var s = reader.GetString();

            if (s.IsBlank())
            {
                return null;
            }

            return TimeSpan.Parse(s, CultureInfo.InvariantCulture);
        }

        /// <inheritdoc/>
        public override void Write(
            Utf8JsonWriter writer,
            TimeSpan? value,
            JsonSerializerOptions options)
        {
            writer = Arguments.EnsureNotNull(writer, nameof(writer));

            writer.WriteStringValue(value.ToString());
        }
    }
}
