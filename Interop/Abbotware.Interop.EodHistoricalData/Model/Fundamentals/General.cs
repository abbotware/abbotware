// -----------------------------------------------------------------------
// <copyright file="General.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Abbotware.Core.Metadata;
    using Abbotware.Interop.Newtonsoft.Plugins;
    using global::Newtonsoft.Json;

    /// <summary>
    /// Fundamental\General POCO
    /// </summary>
    public record General(
        [property: MaxLength(10)] string Code,
        [property: MaxLength(20)] string Type,
        [property: MaxLength(200)] string Name,
        [property: MaxLength(25)] string Exchange,
        [property: MaxLength(5)] string CurrencyCode,
        [property: MaxLength(10)] string CurrencyName,
        [property: MaxLength(2)] string CurrencySymbol,
        [property: MaxLength(Length.Country)] string CountryName,
        [property: MaxLength(5)] string CountryISO,
        [property: MaxLength(Length.Isin)] string? Isin,
        [property: MaxLength(Length.Cusip)] string? Cusip,
        [property: MaxLength(10)] string? Cik,
        [property: MaxLength(10)] string? EmployerIdNumber,
        [property: MaxLength(10)] string? FiscalYearEnd,
        [property: JsonConverter(typeof(BetterDateTimeConverter))] DateTimeOffset? IPODate,
        [property: MaxLength(25)] string? InternationalDomestic,
        [property: MaxLength(50)] string? Sector,
        [property: MaxLength(50)] string? Industry,
        [property: MaxLength(100)] string? GicSector,
        [property: MaxLength(100)] string? GicGroup,
        [property: MaxLength(100)] string? GicIndustry,
        [property: MaxLength(100)] string? GicSubIndustry,
        [property: MaxLength(50)] string? HomeCategory,
        [property: MaxLength(50)] string? Category,
        bool? IsDelisted,
        string? Description,
        [property: MaxLength(Length.CombinedAddress)] string? Address,
        [property: MaxLength(Length.Phone)] string? Phone,
        [property: MaxLength(Length.Url)] string? WebUrl,
        [property: MaxLength(Length.Url)] string? LogoUrl,
        int? FullTimeEmployees,
        [property: JsonConverter(typeof(BetterDateTimeConverter))] DateTimeOffset? UpdatedAt)
    {
        /// <summary>
        /// Gets the Address data
        /// </summary>
        public AddressData? AddressData { get; init; }

        /// <summary>
        /// Gets the Listings
        /// </summary>
        [JsonConverter(typeof(DictionaryFlattener<string, Listing>))]
        public IReadOnlyCollection<Listing>? Listings { get; init; }

        /// <summary>
        /// Gets the Officers
        /// </summary>
        [JsonConverter(typeof(DictionaryFlattener<string, Officer>))]
        public IReadOnlyCollection<Officer>? Officers { get; init; }
    }
}