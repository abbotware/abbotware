// -----------------------------------------------------------------------
// <copyright file="Period.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Base Period Configuration
    /// </summary>
    /// <param name="Type">period type</param>
    /// <param name="Count">period count</param>
    public abstract record Period(PeriodType Type, ushort Count)
    {
    }

    /// <summary>
    /// Base Period Configuration
    /// </summary>
    /// <typeparam name="TCount">period count type</typeparam>
    /// <param name="Type">period type</param>
    /// <param name="Periods">period count</param>
    public abstract record Period<TCount>(PeriodType Type, TCount Periods) : Period(Type, (ushort)(object)Periods!)
    {
    }

    /// <summary>
    /// Daily Period
    /// </summary>
    /// <param name="Periods">period count</param>
    public record Day(PeriodsForDay Periods) : Period<PeriodsForDay>(PeriodType.Day, Periods)
    {
    }

    /// <summary>
    /// Monthly Period
    /// </summary>
    /// <param name="Periods">period count</param>
    public record Month(PeriodsForMonth Periods) : Period<PeriodsForMonth>(PeriodType.Month, Periods)
    {
    }

    /// <summary>
    /// Yearly Period
    /// </summary>
    /// <param name="Periods">period count</param>
    public record Year(PeriodsForYear Periods) : Period<PeriodsForYear>(PeriodType.Year, Periods)
    {
    }

    /// <summary>
    /// Yearly Period
    /// </summary>
    public record YearToDate() : Period<int>(PeriodType.YearToDate, 1)
    {
    }
}
