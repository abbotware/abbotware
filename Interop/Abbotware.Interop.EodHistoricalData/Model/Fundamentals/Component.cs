// -----------------------------------------------------------------------
// <copyright file="Component.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Fundamental\Component POCO
    /// </summary>
    public record Component(
        [property: MaxLength(50)] string Code,
        [property: MaxLength(25)] string Exchange,
        [property: MaxLength(200)] string Name,
        [property: MaxLength(100)] string Sector,
        [property: MaxLength(100)] string Industry)
    {
    }
}
