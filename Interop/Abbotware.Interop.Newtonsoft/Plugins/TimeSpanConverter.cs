// -----------------------------------------------------------------------
// <copyright file="TimeSpanConverter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Newtonsoft.Plugins
{
    using System;
    using System.Globalization;
    using Abbotware.Core;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Converter for TimeSpan properties on objects
    /// https://stackoverflow.com/questions/39876232/newtonsoft-json-serialize-timespan-format
    /// </summary>
    public class TimeSpanConverter : JsonConverter<TimeSpan>
    {
        /// <summary>
        /// Format: Days.Hours:Minutes:Seconds:Milliseconds
        /// </summary>
        public const string TimeSpanFormatString = @"d\.hh\:mm\:ss\:FFF";

        /// <summary>
        /// Format: Hours:Minutes:Seconds
        /// </summary>
        public const string TimeSpanShortFormatString = @"hh\:mm\:ss";

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
        {
            writer = Arguments.EnsureNotNull(writer, nameof(writer));

            var timespanFormatted = $"{value.ToString(TimeSpanFormatString, CultureInfo.InvariantCulture)}";

            writer.WriteValue(timespanFormatted);
        }

        /// <inheritdoc/>
        public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            reader = Arguments.EnsureNotNull(reader, nameof(reader));

            if (reader.Value == null)
            {
                return default;
            }

            var text = (string)reader.Value;

            if (!TimeSpan.TryParseExact(text, TimeSpanFormatString, null, out var parsedTimeSpan))
            {
                if (!TimeSpan.TryParseExact(text, TimeSpanShortFormatString, null, out parsedTimeSpan))
                {
                    throw new JsonSerializationException($"unable to parse:{text} into {objectType} at path:{reader.Path}");
                }
            }

            return parsedTimeSpan;
        }
    }
}
