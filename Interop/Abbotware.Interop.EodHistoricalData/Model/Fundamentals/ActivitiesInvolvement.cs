// -----------------------------------------------------------------------
// <copyright file="ActivitiesInvolvement.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// ActivitiesInvolvement POCO
    /// </summary>
    public record ActivitiesInvolvement(
        ActivityType Activity,
        [property: MaxLength(3)] string Involvement)
    {
    }
}