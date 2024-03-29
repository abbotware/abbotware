﻿// -----------------------------------------------------------------------
// <copyright file="WriteListExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using Abbotware.Core.Cache.LocalOperations;
    using Abbotware.Core.Chrono;

    /// <summary>
    ///     Extension methods for IWriteList
    /// </summary>
    public static class WriteListExtensions
    {
        /// <summary>
        ///     Adds a TimeSeriesValue to the list
        /// </summary>
        /// <typeparam name="TY">value type</typeparam>
        /// <param name="that">extended object</param>
        /// <param name="date">date</param>
        /// <param name="value">value</param>
        public static void AddTimeSeriesValue<TY>(this IWriteList<TimeSeriesValue<TY>> that, DateTimeOffset date, TY value)
            where TY : struct
        {
            that = Arguments.EnsureNotNull(that, nameof(that));

            that.Add(new TimeSeriesValue<TY>(date, value));
        }
    }
}