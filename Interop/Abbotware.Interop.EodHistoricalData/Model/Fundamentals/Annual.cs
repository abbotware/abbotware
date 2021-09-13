// -----------------------------------------------------------------------
// <copyright file="Annual.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.EodHistoricalData.Models
{
    using System;

    /// <summary>
    /// Annual POCO
    /// </summary>
    public record Annual(
        DateTimeOffset Date,
        decimal? EpsActual)
    {
    }
}