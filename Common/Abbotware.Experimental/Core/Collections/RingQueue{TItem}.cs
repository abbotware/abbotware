// -----------------------------------------------------------------------
// <copyright file="RingQueue{TItem}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Sid Sacek</author>

namespace Abbotware.Core.Collections
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Abbotware.Core.Chrono;

    /// <summary>
    ///     This class specializes in creating a non-locking fixed-size queue object. Since this
    ///     class is thread-safe, the user should not take any locks before calling its methods.
    ///     The capacity of queue will always be fixed in size, and cannot grow larger when the
    ///     queue becomes full.
    /// </summary>
    /// <typeparam name="TItem">The generic type can be any class or value type</typeparam>
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "reviewed")]
    public class RingQueue<TItem> : IDisposable
    {
        /// <summary>
        /// error message for diposed object
        /// </summary>
        private const string IsDisposedMessage = "Cannot access disposed RingQueue object";

        /// <summary>
        /// error message for overflow
        /// </summary>
        private const string OverflowedMessage = "The 'Enqueue Counter' has overflowed";

        /// <summary>
        ///     the queue size
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly bool containsValueTypes;

        /// <summary>
        ///     the queue size
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly int queueSize;

        /// <summary>
        ///     total # of timeouts while trying to dequeue
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int dequeueTimeouts;

        /// <summary>
        ///     total # of timeouts while trying to enqueue
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int enqueueTimeouts;

        /// <summary>
        ///     index of where items get dequeued from
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int queueHead;

        /// <summary>
        ///     index of where items get enqueued to
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int queueTail;

        /// <summary>
        ///     item queue
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ItemWrapper[] queue;

        /// <summary>
        /// Initializes a new instance of the <see cref="RingQueue{TItem}"/> class.
        /// A thread-safe queue collection class.
        /// </summary>
        /// <param name="queueSize">
        ///     The size of the queue remains fixed for the entire life of the
        ///     collection object. The minimum queue size can be 2 entries and the maximum can be 1,000
        ///     entries
        /// </param>
        public RingQueue(int queueSize)
        {
            Arguments.Within(queueSize, 1, 10, nameof(queueSize));

            this.containsValueTypes = typeof(TItem).IsValueType;

            this.queue = new ItemWrapper[queueSize];
            this.queueSize = queueSize;

            this.queueHead = 0;
            this.queueTail = 0;

            this.dequeueTimeouts = 0;
            this.enqueueTimeouts = 0;
        }

        /// <summary>
        ///     Gets a value indicating whether or not the queue contains value types
        /// </summary>
        public bool ContainsValueTypes
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return this.containsValueTypes; }
        }

        /// <summary>
        ///     Gets the fixed-size capacity of the RingQueue
        /// </summary>
        public int Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return this.queueSize; }
        }

        /// <summary>
        ///     Gets the number of entries currently available in the queue
        /// </summary>
        public int Count
        {
            get
            {
                this.ThrowIfDisposed();

                var head = this.QueueHead; // fetch these values in this order
                var tail = this.QueueTail;

                head = ToIndexValue(head); // always use the positive value versions
                tail = ToIndexValue(tail);

                return tail - head;
            }
        }

        /// <summary>
        ///     Gets the total number of entries dequeued from the RingQueue
        /// </summary>
        public int TotalDequeues
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var head = this.QueueHead;
                return ToIndexValue(head); // always use the positive version
            }
        }

        /// <summary>
        ///     Gets the total number of entries enqueued into the RingQueue
        /// </summary>
        public int TotalEnqueues
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var tail = this.QueueTail;
                return ToIndexValue(tail); // always use the positive version
            }
        }

        /// <summary>
        ///     Gets the total number of entries that failed to dequeue due to timeouts
        /// </summary>
        public int TotalDequeueTimeouts
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return Interlocked.CompareExchange(ref this.dequeueTimeouts, 0, 0); }
        }

        /// <summary>
        ///     Gets the total number of entries that failed to be enqueued due to timeouts
        /// </summary>
        public int TotalEnqueueTimeouts
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return Interlocked.CompareExchange(ref this.enqueueTimeouts, 0, 0); }
        }

        /// <summary>
        ///    Gets a value indicating whether the RingQueue object has already been disposed
        /// </summary>
        public bool IsDisposed
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return this.Queue == null; }
        }

        /// <summary>
        ///     Gets the index of where the next available item holder will be fetched from
        /// </summary>
        protected int QueueHead
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return Interlocked.CompareExchange(ref this.queueHead, 0, 0); }
        }

        /// <summary>
        ///     Gets the index of where the next available item holder will be returned to
        /// </summary>
        protected int QueueTail
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return Interlocked.CompareExchange(ref this.queueTail, 0, 0); }
        }

        /// <summary>
        ///     Gets the available item holders collection in a thread-safe manner
        /// </summary>
        private ItemWrapper[] Queue
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return Interlocked.CompareExchange(ref this.queue, null, null); }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Adds the caller's element to the queue if and when the queue has available space
        /// </summary>
        /// <param name="element">The caller's element being added to the queue</param>
        /// <returns>If enqueueing succeeds, the call returns true, otherwise false</returns>
        public bool Enqueue(TItem element)
        {
            Arguments.NotNull(element, nameof(element));

            return this.Enqueue(ref element);
        }

        /// <summary>
        ///     Adds the caller's element to the queue if and when the queue has available space
        /// </summary>
        /// <param name="element">The caller's element being added to the queue</param>
        /// <param name="timeout">
        ///     An optional TimeSpan representing the amount of time
        ///     to spend trying to enqueue the element while space remains unavailable.
        ///     If no timeout value is supplied, a default of 10-seconds will be used
        /// </param>
        /// <returns>If enqueueing succeeds, the call returns true, otherwise false</returns>
        public bool Enqueue(TItem element, TimeSpan timeout)
        {
            Arguments.NotNull(element, nameof(element));
            Arguments.IsPositiveAndNotZero(timeout, nameof(timeout));

            return this.Enqueue(ref element, timeout);
        }

        /// <summary>
        ///     Adds the caller's value-type element to the queue if and when the queue has available space
        /// </summary>
        /// <param name="element">A large-sized value-type passed by reference that's expensive to pass directly by value</param>
        /// <returns>If enqueueing succeeds, the call returns true, otherwise false</returns>
        public bool Enqueue(ref TItem element)
        {
            const long TEN_SECOND_TIMEOUT = 10 * TimeSpan.TicksPerSecond; // 10-second default expiration timer

            return this.Enqueue(ref element, new TimeSpan(TEN_SECOND_TIMEOUT));
        }

        /// <summary>
        ///     Adds the caller's value-type element to the queue if and when the queue has available space
        /// </summary>
        /// <param name="element">A large-sized value-type passed by reference that's expensive to pass directly by value</param>
        /// <param name="timeout">
        ///     An optional TimeSpan representing the amount of time
        ///     to spend trying to enqueue the element while space remains unavailable.
        ///     If no timeout value is supplied, a default of 10-seconds will be used
        /// </param>
        /// <returns>If enqueueing succeeds, the call returns true, otherwise false</returns>
        public bool Enqueue(ref TItem element, TimeSpan timeout)
        {
            Arguments.NotNull(element, nameof(element));
            Arguments.IsPositiveAndNotZero(timeout, nameof(timeout));

            //-----------------------------------------------------------------
            //  Set a termination time point to prevent from looping forever
            //-----------------------------------------------------------------
            var timer = new HighResolutionTimeSpan(timeout);

            return this.Enqueue(ref element, timer);
        }

        /// <summary>
        ///     Adds the caller's value-type element to the queue if and when the queue has available space
        /// </summary>
        /// <param name="element">A large-sized value-type passed by reference that's expensive to pass directly by value</param>
        /// <param name="timer">
        ///     An otimer representing the amount of time
        ///     to spend trying to enqueue the element while space remains unavailable.
        /// </param>
        /// <returns>If enqueueing succeeds, the call returns true, otherwise false</returns>
        [SuppressMessage("Microsoft.Usage", "CA2219:DoNotRaiseExceptionsInExceptionClauses", Justification = "reviewed")]
        public virtual bool Enqueue(ref TItem element, HighResolutionTimeSpan timer)
        {
            Arguments.NotNull(element, nameof(element));

            // cache this reference so that it can't change on us
            for (var queueCopy = this.Queue; ;)
            {
                this.ThrowIfDisposed();

                //-------------------------------------------------------------
                //  If there's available space in the queue, try to enqueue
                //  the user element by taking ownership of an queue slot
                //-------------------------------------------------------------
                var head_index = this.QueueHead; // fetch these values in this order
                var tail_index = this.QueueTail;

                //-------------------------------------------------------------
                //  For Enqueues, the tail index must be positive, but the
                //  head index can be negative, which we will work around
                //-------------------------------------------------------------
                head_index = ToIndexValue(head_index);

                if (tail_index >= 0)
                {
                    if (tail_index >= int.MaxValue)
                    {
                        throw new OverflowException(OverflowedMessage);
                    }

                    var used_count = tail_index - head_index;
                    if (used_count < this.Size)
                    {
                        var queue_index = tail_index % this.Size;

                        var lock_value = ToLockValue(tail_index);

                        // By complimenting the tail index value, we effectively take a lock
                        var expected_index = Interlocked.CompareExchange(ref this.queueTail, lock_value, tail_index);
                        if (expected_index == tail_index)
                        {
                            try
                            {
                                queueCopy[queue_index].Index = tail_index; // set the index for sanity checks
                                queueCopy[queue_index].Value = element;
                                return true;
                            }
                            finally
                            {
                                Interlocked.MemoryBarrier();

                                // release the lock now and set the latest index for the next usage
                                var expected_compliment = Interlocked.Exchange(ref this.queueTail, tail_index + 1);
                                Interlocked.MemoryBarrier();

                                if (expected_compliment != lock_value)
                                {
                                    throw new InvalidOperationException("expected compliment != lock value");
                                }
                            }
                        }
                    }
                }

                //-------------------------------------------------------------
                //  Since our thread failed to take ownership of the queue's
                //  slot, check to see if we should terminate or keep trying
                //-------------------------------------------------------------
                if (timer.IsExpired)
                {
                    Interlocked.Increment(ref this.enqueueTimeouts);
                    return false;
                }
            }
        }

        /// <summary>
        ///     Dequeues and returns the oldest entry from the queue if one is available,
        ///     otherwise the method returns a null reference. If the generic type is
        ///     a Value type, then use the alternative 'bool Dequeue( out TItem value )'
        ///     method instead, otherwise an InvalidOperationException will be thrown.
        /// </summary>
        /// <returns>Retrieves the oldest entry in the queue if one exists, otherwise the method returns a null</returns>
        public virtual TItem Dequeue()
        {
            if (this.containsValueTypes)
            {
                throw new InvalidOperationException("This method does not support dequeue for Value types");
            }

            // at this point, it's always a reference type
            return this.Dequeue(out TItem reference_type) ? reference_type : default;
        }

        /// <summary>
        ///     Dequeues the oldest entry from the queue, places it in the out parameter, and
        ///     returns a boolean true. If no entries are available, returns a boolean false.
        /// </summary>
        /// <param name="item">The destination for the dequeued item</param>
        /// <returns>
        ///     Returns a boolean true if the method succeeds in retrieving an queue
        ///     item, otherwise the method returns a boolean false
        /// </returns>
        [SuppressMessage("Microsoft.Usage", "CA2219:DoNotRaiseExceptionsInExceptionClauses", Justification = "reviewed")]
        public virtual bool Dequeue(out TItem item)
        {
            item = default;
            var queueCopy = this.Queue; // cache this reference so that it can't change on us

            for (var try_count = 0; try_count < 100; ++try_count)
            {
                this.ThrowIfDisposed();

                var head_index = this.QueueHead; // fetch these values in this order
                var tail_index = this.QueueTail;

                //-------------------------------------------------------------
                //  For Dequeues, the head index must be positive, but the
                //  tail index can be negative, which we will work around
                //-------------------------------------------------------------
                tail_index = ToIndexValue(tail_index);

                // Check whether we can perform this operation right now
                if (head_index >= 0)
                {
                    if (head_index == tail_index)
                    {
                        return false; // sorry, but the queue is currently empty
                    }

                    var queue_index = head_index % this.Size;

                    var lock_value = ToLockValue(head_index);

                    // By complimenting the head index value, we effectively take a lock
                    var expected_index = Interlocked.CompareExchange(ref this.queueHead, lock_value, head_index);
                    if (expected_index == head_index)
                    {
                        try
                        {
                            Interlocked.MemoryBarrier();
                            var wrapped_item = queueCopy[queue_index];

                            item = wrapped_item.Value;
                            return true;
                        }
                        finally
                        {
                            // release the lock now and set the latest index for the next usage
                            var expected_compliment = Interlocked.Exchange(ref this.queueHead, head_index + 1);
                            Interlocked.MemoryBarrier();

                            if (expected_compliment != lock_value)
                            {
                                throw new InvalidOperationException("expected compliment != lock value");
                            }
                        }
                    }
                }
            }

            Interlocked.Increment(ref this.dequeueTimeouts);
            return false;
        }

        /// <summary>
        ///     Indicates whether the value contains a locked pattern of bits
        /// </summary>
        /// <param name="value">The value the be checked for a locked pattern</param>
        /// <returns>Returns boolean true if the value has a locked pattern, otherwise false</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static bool IsLocked(int value)
        {
            return value < 0; // all negative numbers are considered lock patterns
        }

        /// <summary>
        ///     Returns a complimented (lock patterned) value, if the value is not considered locked
        /// </summary>
        /// <param name="value">The value that needs to be converted</param>
        /// <returns>Returns a lock-pattern value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static int ToLockValue(int value)
        {
            return IsLocked(value) ? value : ~value;
        }

        /// <summary>
        ///     Returns a non-lock patterned value, if the value is considered locked
        /// </summary>
        /// <param name="value">The value that needs to be converted</param>
        /// <returns>Returns a value that can be used to index an array</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static int ToIndexValue(int value)
        {
            return IsLocked(value) ? ~value : value;
        }

        /// <summary>
        ///     Dispose
        /// </summary>
        /// <param name="disposing">finizlaer/explicitly called</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.IsDisposed)
            {
                if (disposing)
                {
                    Interlocked.Exchange(ref this.queue, null);
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
            }
        }

        /// <summary>
        ///     Throws an ObjectDisposedException if the object is currently disposed
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowIfDisposed()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(IsDisposedMessage);
            }
        }

        /// <summary>
        ///     class ItemWrapper
        ///     This object is a container for the objects being stored in the queue so that no two
        ///     queue entries are ever confused for being one and the same entry
        /// </summary>
        private struct ItemWrapper
        {
            /// <summary>
            /// item index
            /// </summary>
            public int Index;

            /// <summary>
            /// item value
            /// </summary>
            public TItem Value;
        }
    }
}