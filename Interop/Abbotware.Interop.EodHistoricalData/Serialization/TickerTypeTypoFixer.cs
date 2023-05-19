// -----------------------------------------------------------------------
// <copyright file="TickerTypeTypoFixer.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Serialization
{
    using System;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.Interop.EodHistoricalData.Models;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Fix for Typeo in TickerType
    /// </summary>
    public class TickerTypeTypoFixer : JsonConverter
    {
        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TickerType);
        }

        /// <inheritdoc/>
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            reader = Arguments.EnsureNotNull(reader, nameof(reader));

            if (reader.TokenType == JsonToken.None || reader.TokenType == JsonToken.Null)
            {
                return TickerType.Unknown;
            }

            if (reader.TokenType == JsonToken.String)
            {
                if (reader.Value is string)
                {
                    var v = reader.Value as string;

                    if (v.IsBlank())
                    {
                        return TickerType.Unknown;
                    }

                    switch (v)
                    {
                        case "Commmon Stock":
                        case "Common Stock":
                            return TickerType.CommonShare;
                        case "ETF":
                            return TickerType.ExchangeTradedFund;
                        case "ETN":
                            return TickerType.ExchangeTradedNote;
                        case "Preferred Share":
                        case "Preferred Stock":
                            return TickerType.PreferredShare;
                        case "Mutual Fund":
                        case "FUND":
                        case "Fund":
                            return TickerType.MutualFund;
                    }

                    if (!Enum.TryParse<TickerType>(v, true, out var r))
                    {
                        throw new NotSupportedException($"ticker type:{v} unexpected");
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