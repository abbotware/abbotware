// -----------------------------------------------------------------------
// <copyright file="ITimeSeriesCollection{TY}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Chrono
{
    using System.Collections.Generic;

    /// <summary>
    /// interface for a time series of data
    /// </summary>
    /// <typeparam name="TY">type of the value</typeparam>
    public interface ITimeSeriesCollection<TY> : IEnumerable<ITimeSeriesValue<TY>>
        where TY : struct
    {
    }
}