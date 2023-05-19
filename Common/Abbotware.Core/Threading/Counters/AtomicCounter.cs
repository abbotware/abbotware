// -----------------------------------------------------------------------
// <copyright file="AtomicCounter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Threading.Counters
{
    using System.Threading;

    /// <summary>
    ///     thread-safe counter
    /// </summary>
    /// <remarks>
    ///     Use this class in composition with another class.
    /// </remarks>
    public class AtomicCounter : IAtomicCounter
    {
        /// <summary>
        ///     counter
        /// </summary>
        private long sequenceId;

        /// <summary>
        ///     Gets the current value for this counter
        /// </summary>
        public long Value
        {
            get
            {
                return Volatile.Read(ref this.sequenceId);
            }
        }

        /// <summary>
        ///     Increments the counter and returns the value
        /// </summary>
        /// <returns>the incremented value</returns>
        public virtual long Increment()
        {
            return Interlocked.Increment(ref this.sequenceId);
        }

        /// <summary>
        ///     Decrements the counter and returns the value
        /// </summary>
        /// <returns>the decremented value</returns>
        public virtual long Decrement()
        {
            return Interlocked.Decrement(ref this.sequenceId);
        }
    }
}