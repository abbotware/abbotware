// -----------------------------------------------------------------------
// <copyright file="ITimeSeriesCollection{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Chrono
{
    using System.Collections.Generic;

    /// <summary>
    /// interface for a time series of data
    /// </summary>
    /// <typeparam name="T">type of the value</typeparam>
    public interface ITimeSeriesCollection<T> : IEnumerable<ITimeSeriesValue<T>>
    {
    }
}