// -----------------------------------------------------------------------
// <copyright file="Candle.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System;
    using Abbotware.Interop.Newtonsoft.Plugins;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Candle Poco
    /// </summary>
    /// <param name="DateTime">cusip</param>
    /// <param name="Open">open price</param>
    /// <param name="Close">close price</param>
    /// <param name="High">period high</param>
    /// <param name="Low">period low</param>
    /// <param name="Volume">volume</param>
    public record Candle(
        [property:JsonConverter(typeof(UnixMillisecondsDateTimeConverter))]
        DateTime? DateTime,
        decimal? Open,
        decimal? Close,
        decimal? High,
        decimal? Low,
        double? Volume)
    {
    }
}