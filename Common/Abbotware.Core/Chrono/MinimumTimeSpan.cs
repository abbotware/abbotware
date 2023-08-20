// -----------------------------------------------------------------------
// <copyright file="MinimumTimeSpan.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Chrono
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// edge based time check to ensure events occur between a minumum time span
    /// </summary>
    public class MinimumTimeSpan
    {
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
            this.MinimumWaitTime = minimumWait;
            this.SetExpiration();
        }

        /// <summary>
        /// Gets a value indicating whether the minimum timespan passed since the last check (if true, the timespan resets)
        /// </summary>
        public bool IsExpired
        {
            get
            {
                var currentTime = DateTime.Now;

                if (currentTime < this.expirationTime)
                {
                    return false;
                }

                this.SetExpired(currentTime);

                return true;
            }
        }

        /// <summary>
        /// Gets the minimum time between time checks
        /// </summary>
        public TimeSpan MinimumWaitTime { get; }

        /// <summary>
        /// Waits for the timespan to expire - this can act as a throttle
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>async handle</returns>
        public async Task<TimeSpan> ThrottleAsync(CancellationToken ct)
        {
            var delay = this.expirationTime - DateTime.Now;

            if (delay > TimeSpan.Zero)
            {
                await Task.Delay(delay, ct)
                    .ConfigureAwait(false);
            }
            else
            {
                delay = TimeSpan.Zero;
            }

            this.SetExpiration();

            return delay;
        }

        private void SetExpiration()
        {
            this.SetExpired(DateTime.Now);
        }

        private void SetExpired(DateTime time)
        {
            this.expirationTime = time + this.MinimumWaitTime;
        }
    }
}