// -----------------------------------------------------------------------
// <copyright file="TimeUnitType.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream
{
    /// <summary>
    /// AWS Timestream Time Unit type
    /// </summary>
    public enum TimeUnitType
    {
        /// <summary>
        /// Unknown time unit
        /// </summary>
        Unknown,

        /// <summary>
        /// Nanoseconds time unit granularity
        /// </summary>
        Nanoseconds,

        /// <summary>
        /// Microseconds time unit granularity
        /// </summary>
        Microseconds,

        /// <summary>
        /// Milliseconds time unit granularity
        /// </summary>
        Milliseconds,

        /// <summary>
        /// Seconds time unit granularity
        /// </summary>
        Seconds,
    }
}
