// -----------------------------------------------------------------------
// <copyright file="Officer.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System.ComponentModel.DataAnnotations;
    using Abbotware.Core.Metadata;

    /// <summary>
    /// Officer POCO
    /// </summary>
    public record Officer(
        [property: MaxLength(Length.CombinedName)] string Name,
        [property: MaxLength(Length.JobTitle)] string Title,
        [property: MaxLength(Length.Year)] string? YearBorn)
    {
    }
}