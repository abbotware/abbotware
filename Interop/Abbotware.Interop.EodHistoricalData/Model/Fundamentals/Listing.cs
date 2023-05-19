// -----------------------------------------------------------------------
// <copyright file="Listing.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System.ComponentModel.DataAnnotations;
    using Abbotware.Core.Metadata;

    /// <summary>
    /// Listing POCO
    /// </summary>
    public record Listing(
        [property: MaxLength(50)] string Code,
        [property: MaxLength(10)] string Exchange,
        [property: MaxLength(200)] string Name)
    {
    }
}