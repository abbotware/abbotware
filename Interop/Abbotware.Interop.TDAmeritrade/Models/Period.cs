// -----------------------------------------------------------------------
// <copyright file="Period.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.TDAmeritrade.Models
{
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
    public abstract record Period<TCount>(PeriodType Type, TCount Periods)
        : Period(Type, (ushort)(object)Periods!)
    {
    }

    /// <summary>
    /// Daily Period
    /// </summary>
    /// <param name="Periods">period count</param>
    public record Day(HowManyDays Periods)
        : Period<HowManyDays>(PeriodType.Day, Periods)
    {
    }

    /// <summary>
    /// Monthly Period
    /// </summary>
    /// <param name="Periods">period count</param>
    public record Month(HowManyMonths Periods)
        : Period<HowManyMonths>(PeriodType.Month, Periods)
    {
    }

    /// <summary>
    /// Yearly Period
    /// </summary>
    /// <param name="Periods">period count</param>
    public record Year(HowManyYears Periods)
        : Period<HowManyYears>(PeriodType.Year, Periods)
    {
    }

    /// <summary>
    /// Yearly Period
    /// </summary>
    public record YearToDate()
        : Period<int>(PeriodType.YearToDate, 1)
    {
    }
}
