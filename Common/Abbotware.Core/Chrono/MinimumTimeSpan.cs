// -----------------------------------------------------------------------
// <copyright file="MinimumTimeSpan.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Chrono
{
    using System;

    /// <summary>
    /// edge based time check to ensure events occur between a minumum time span
    /// </summary>
    public class MinimumTimeSpan
    {
        /// <summary>
        /// minimum time between time checks
        /// </summary>
        private readonly TimeSpan minimumWait;

        /// <summary>
        /// time current timespan expires
        /// </summary>
        private DateTime expirationTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinimumTimeSpan"/> class.
        /// </summary>
        /// <param name="minimumWait">minimum time between time checks</param>
        public MinimumTimeSpan(TimeSpan minimumWait)
        {
            this.minimumWait = minimumWait;
            this.expirationTime = DateTime.Now + this.minimumWait;
        }

        /// <summary>
        /// Gets a value indicating whether the minimum timespan passed since the last check (if true, the timespan resets)
        /// </summary>
        public bool IsExpired
        {
            get
            {
                var nextTime = DateTime.Now;

                if (nextTime < this.expirationTime)
                {
                    return false;
                }

                this.expirationTime = nextTime + this.minimumWait;

                return true;
            }
        }
    }
}