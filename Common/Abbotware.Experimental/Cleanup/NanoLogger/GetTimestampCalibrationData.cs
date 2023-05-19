// -----------------------------------------------------------------------
// <copyright file="GetTimestampCalibrationData.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Diagnostics.Plugins
{
    using System.Collections.Generic;

    /// <summary>
    /// Calibration data for CPU timing information for GetTimestamp
    /// </summary>
    public class GetTimestampCalibrationData
    {
        /// <summary>
        /// Gets or sets the sample size
        /// </summary>
        public int SampleSize { get; set; }

        /// <summary>
        /// Gets or sets the delta between GetTimestamp calls
        /// </summary>
        public IReadOnlyDictionary<long, int> DeltaHistogram { get; set; } = new Dictionary<long, int>();

        /// <summary>
        /// Gets or sets the Min delta
        /// </summary>
        public long Min { get; set; }

        /// <summary>
        /// Gets or sets the Max delta
        /// </summary>
        public long Max { get; set; }
    }
}