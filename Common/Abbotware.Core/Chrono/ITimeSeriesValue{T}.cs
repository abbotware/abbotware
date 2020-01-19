// -----------------------------------------------------------------------
// <copyright file="ITimeSeriesValue{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Chrono
{
    using System;
    using Abbotware.Core.Math;

    /// <summary>
    /// a data point representing a time series value
    /// </summary>
    /// <typeparam name="T">type of the value</typeparam>
    public interface ITimeSeriesValue<T> : IPoint<DateTimeOffset, T>
    {
    }
}