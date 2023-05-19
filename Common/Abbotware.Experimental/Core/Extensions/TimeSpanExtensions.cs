// -----------------------------------------------------------------------
// <copyright file="TimeSpanExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Extensions
{
    using System;

    /// <summary>
    ///     Timespan extensions
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        ///     multiply a time span object by an integer multiplier
        /// </summary>
        /// <param name="extendedObject">the timespan object</param>
        /// <param name="multiplier">integer value to multiply</param>
        /// <returns>a Timespan object multiplied</returns>
        public static TimeSpan Multiply(this TimeSpan extendedObject, int multiplier)
        {
            return new TimeSpan(extendedObject.Ticks * multiplier);
        }
    }
}