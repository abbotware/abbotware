// -----------------------------------------------------------------------
// <copyright file="EventSpan.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Chrono
{
    using System;

    /// <summary>
    ///     this class is used to check cool down time between discrete events.  There needs to be a minimum cool down time
    ///     between for a time to be considered a new event
    /// </summary>
    public class EventSpan
    {
        /// <summary>
        ///     internal lock object
        /// </summary>
        private readonly object syncObject = new object();

        /// <summary>
        ///     Initializes a new instance of the <see cref="EventSpan" /> class.
        /// </summary>
        /// <param name="coolDownInterval">time span to use as an interval</param>
        public EventSpan(TimeSpan coolDownInterval)
        {
            this.CoolDownInterval = coolDownInterval;
        }

        /// <summary>
        ///     Gets or sets the time of the first event
        /// </summary>
        public DateTimeOffset? First { get; protected set; }

        /// <summary>
        ///     Gets or sets the time of the last event
        /// </summary>
        public DateTimeOffset? Last { get; protected set; }

        /// <summary>
        ///     Gets the minimum cool down timespan between events
        /// </summary>
        public TimeSpan CoolDownInterval { get; }

        /// <summary>
        ///     resets the EventSpan object as if new event previously occurred
        /// </summary>
        public void Clear()
        {
            lock (this.syncObject)
            {
                this.First = null;
                this.Last = null;
            }
        }

        /// <summary>
        ///     checks if now is considered a new event
        /// </summary>
        /// <returns>true if the now is considered a new event</returns>
        public bool IsNowAnEvent()
        {
            return this.IsNewEvent(DateTimeOffset.Now);
        }

        /// <summary>
        ///     checks if the specified time is considered a new event
        /// </summary>
        /// <param name="timeToCheck">time to check against previous time</param>
        /// <returns>true if the time supplied is considered a new event</returns>
        public bool IsNewEvent(DateTimeOffset timeToCheck)
        {
            lock (this.syncObject)
            {
                if (!this.Last.HasValue)
                {
                    this.Last = timeToCheck;
                    this.First = timeToCheck;
                    return true;
                }

                if (this.Last.Value + this.CoolDownInterval > timeToCheck)
                {
                    return false;
                }

                this.Last = timeToCheck;
                return true;
            }
        }
    }
}