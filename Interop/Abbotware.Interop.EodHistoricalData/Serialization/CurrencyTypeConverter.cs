// -----------------------------------------------------------------------
// <copyright file="CurrencyTypeConverter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Serialization
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.Interop.Iso;
    using global::Newtonsoft.Json;

    /// <summary>
    /// CurrencyType Json Converter
    /// </summary>
    public class CurrencyTypeConverter : JsonConverter
    {
        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Currency);
        }

        /// <inheritdoc/>
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            reader = Arguments.EnsureNotNull(reader, nameof(reader));

            if (reader.TokenType == JsonToken.Null)
            {
                return Currency.None;
            }

            if (reader.TokenType == JsonToken.String)
            {
                if (reader.Value is string)
                {
                    var v = reader.Value as string;

                    if (v.IsBlank())
                    {
                        return Currency.None;
                    }

                    switch (v)
                    {
                        case "NA":
                        case "Unknown":
                        case "ZAC":
                        case "ILA":
                        case "GBX":
                            return Currency.None;
                    }

                    if (!Enum.TryParse<Currency>(v, true, out var r))
                    {
                        throw new NotSupportedException($"currency type:{v} unexpected");
                    }

                    return r;
                }
            }

            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}