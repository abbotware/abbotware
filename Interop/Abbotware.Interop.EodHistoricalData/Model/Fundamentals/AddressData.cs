// -----------------------------------------------------------------------
// <copyright file="AddressData.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System.ComponentModel.DataAnnotations;
    using Abbotware.Core.Metadata;

    /// <summary>
    /// Address Data POCO
    /// </summary>
    public record AddressData(
        [property: MaxLength(Length.Street)] string? Street,
        [property: MaxLength(Length.City)] string? City,
        [property: MaxLength(Length.Region)] string? State,
        [property: MaxLength(Length.Country)] string? Country,
        [property: MaxLength(Length.PostalCode)] string? Zip)
    {
    }
}