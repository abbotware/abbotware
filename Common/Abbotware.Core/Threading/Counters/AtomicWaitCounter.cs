// -----------------------------------------------------------------------
// <copyright file="AtomicWaitCounter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Threading.Counters
{
    using System.Threading.Tasks;

    /// <summary>
    ///     thread-safe counter
    /// </summary>
    /// <remarks>
    ///     Use this class in composition with another class.
    /// </remarks>
    public sealed class AtomicWaitCounter : IAccumulator
    {
        /// <summary>
        ///     Internal atomic counter
        /// </summary>
        private readonly AtomicCounter counter = new AtomicCounter();

        /// <summary>
        ///     max value for counter before wait handle is released
        /// </summary>
        private readonly long max;

        /// <summary>
        ///     wait handle for signaling threads that are waiting for this counter
        /// </summary>
        private readonly TaskCompletionSource<bool> signal = new();

        /// <summary>
        ///     Initializes a new instance of the <see cref="AtomicWaitCounter" /> class.
        /// </summary>
        /// <param name="max">max value for the wait handle</param>
        public AtomicWaitCounter(long max)
        {
            Arguments.IsPositiveAndNotZero(max, nameof(max));

            this.max = max;
        }

        /// <summary>
        ///     Gets the Completion task used for synchronization / waiting
        /// </summary>
        public Task Completion => this.signal.Task;

        /// <inheritdoc />
        public long Value
        {
            get { return this.counter.Value; }
        }

        /// <inheritdoc />
        public long Increment()
        {
            var temp = this.counter.Increment();

            if (temp >= this.max)
            {
                this.signal.SetResult(true);
            }

            return temp;
        }
    }
}