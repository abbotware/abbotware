// -----------------------------------------------------------------------
// <copyright file="TimestreamConstants.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Aws.Timestream
{
    /// <summary>
    /// AWS Timestream related constants
    /// </summary>
    public static class TimestreamConstants
    {
        /// <summary>
        /// Max Number of Records for a batch
        /// </summary>
        public const int MaxRecordBatch = 100;

        /// <summary>
        /// Max length for a value (Dimension or Metric)
        /// </summary>
        public const int MaxValueLength = 2048;
    }
}
