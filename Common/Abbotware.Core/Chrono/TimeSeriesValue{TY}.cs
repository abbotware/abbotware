// -----------------------------------------------------------------------
// <copyright file="TimeSeriesValue{TY}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Chrono
{
    using System;
    using Abbotware.Core.Math;

    /// <summary>
    /// Time series value
    /// </summary>
    /// <typeparam name="TY">type of the value</typeparam>
    public record class TimeSeriesValue<TY>(DateTimeOffset X, TY Y) : Point<DateTimeOffset, TY>(X, Y), ITimeSeriesValue<TY>
        where TY : notnull
    {
    }
}