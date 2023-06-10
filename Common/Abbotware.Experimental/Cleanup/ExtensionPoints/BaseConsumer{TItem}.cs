// -----------------------------------------------------------------------
// <copyright file="BaseConsumer{TItem}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.ExtensionPoints
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Abbotware.Core.Collections;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Objects;

    /// <summary>
    ///     base class for producer/consumer queue
    /// </summary>
    /// <typeparam name="TItem">item type to queue</typeparam>
    public abstract class BaseConsumer<TItem> : BaseComponent, IConsumer<TItem>
        where TItem : class
    {
        /// <summary>
        ///     base priority of worker threads
        /// </summary>
        private readonly ThreadPriority basePriority;

        /// <summary>
        ///     name to create threads with
        /// </summary>
        private readonly string threadName;

        /// <summary>
        ///     internal work queue
        /// </summary>
        private readonly WorkQueue workQueue;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseConsumer{TItem}" /> class.
        /// </summary>
        /// <param name="workerThreadCount">worker thread count</param>
        /// <param name="threadName">name to use for threads</param>
        /// <param name="basePriority">base thread priority for workers</param>
        /// <param name="logger">injected logger</param>
        protected BaseConsumer(uint workerThreadCount, string threadName, ThreadPriority basePriority, ILogger logger)
            : base(logger)
        {
            Arguments.Within(workerThreadCount, 1, 10, nameof(workerThreadCount));
            Arguments.NotNull(threadName, nameof(threadName));
            Arguments.NotNull(logger, nameof(logger));

            this.threadName = threadName;
            this.basePriority = basePriority;
            this.workQueue = new WorkQueue(this, (int)workerThreadCount);
        }

        /// <inheritdoc />
        public void Shutdown(bool waitForWorkers)
        {
            this.workQueue.Shutdown(waitForWorkers);
        }

        /// <inheritdoc />
        public void Enqueue(TItem item)
        {
            this.InitializeIfRequired();

            if (item != null)
            {
                this.OnPreEnqueue(item);
                this.workQueue.Enqueue(item);
            }
        }

        /// <summary>
        ///     Enqueues all items
        /// </summary>
        /// <param name="items">items to enqueue</param>
        public void EnqueueAll(TItem[] items)
        {
            items = Arguments.EnsureNotNull(items, nameof(items));

            this.EnqueueAll((IEnumerable<TItem>)items);
        }

        /// <summary>
        ///     Enqueues all items
        /// </summary>
        /// <param name="items">items to enqueue</param>
        public void EnqueueAll(IEnumerable<TItem> items)
        {
            items = Arguments.EnsureNotNull(items, nameof(items));

            this.InitializeIfRequired();

            foreach (var item in items)
            {
                if (item == null)
                {
                    continue;
                }

                this.OnPreEnqueue(item);
                this.workQueue.Enqueue(item);
            }
        }

        /// <inheritdoc />
        protected sealed override void OnInitialize()
        {
            this.workQueue.StartWorkers();

            this.OnProducerConsumerInitialize();
        }

        /// <summary>
        ///     Hook to implement custom initalization logic
        /// </summary>
        protected virtual void OnProducerConsumerInitialize()
        {
        }

        /// <summary>
        ///     Hook to implement custom pre enqueue logic
        /// </summary>
        /// <param name="item">item to enqueue</param>
        protected virtual void OnPreEnqueue(TItem item)
        {
            Arguments.NotNull(item, nameof(item));
        }

        /// <summary>
        ///     Hook to implement custom process item logic
        /// </summary>
        /// <param name="item">item to process</param>
        protected abstract void OnProcessItem(TItem item);

        /// <summary>
        ///     Hook to implement custom post dequeue logic
        /// </summary>
        /// <param name="item">item to dequeue</param>
        protected virtual void OnPostDequeue(TItem item)
        {
            Arguments.NotNull(item, nameof(item));
        }

        /// <summary>
        ///     queue implementation for producer/consumer
        /// </summary>
        private sealed class WorkQueue : ActiveRingQueue<TItem>
        {
            /// <summary>
            ///     owner object
            /// </summary>
            private readonly BaseConsumer<TItem> owner;

            /// <summary>
            ///     worker threads
            /// </summary>
            private readonly List<Thread> workers;

            /// <summary>
            ///     flag to indicate shutdown
            /// </summary>
            private bool shuttingDown;

            /// <summary>
            ///     Initializes a new instance of the <see cref="WorkQueue" /> class.
            /// </summary>
            /// <param name="ownerObject">owner</param>
            /// <param name="workerCount">number of worker threads</param>
            public WorkQueue(BaseConsumer<TItem> ownerObject, int workerCount)
                : base(500, workerCount)
            {
                Arguments.NotNull(ownerObject, nameof(ownerObject));
                Arguments.Within(workerCount, 1, 10, nameof(workerCount));

                this.owner = ownerObject;
                this.workers = new List<Thread>(workerCount);
            }

            /// <summary>
            ///     Shutsdown the consumer
            /// </summary>
            /// <param name="waitForWorkers">flag to indicate block for worker shutdown</param>
            public void Shutdown(bool waitForWorkers)
            {
                this.shuttingDown = true;

                if (waitForWorkers)
                {
                    Thread[] worker_threads;
                    lock (this.workers)
                    {
                        worker_threads = this.workers.ToArray();
                    }

                    foreach (var thread in worker_threads)
                    {
                        thread.Join();
                    }
                }
            }

            /// <inheritdoc />
            protected override void OnThreadStart(int workerId)
            {
                var current_thread = Thread.CurrentThread;

                var name = FormattableString.Invariant($"{this.owner.threadName} Thread {workerId}");

                current_thread.Name = name;
                current_thread.Priority = this.owner.basePriority;

                lock (this.workers)
                {
                    this.workers.Add(current_thread);
                }

                this.owner.Logger.Info(FormattableString.Invariant($"Thread started up: {current_thread.Name}"));
            }

            /// <inheritdoc />
            protected override void OnThreadExit(int workerId)
            {
                this.owner.Logger.Info(FormattableString.Invariant($"Thread shut down: {Thread.CurrentThread.Name}"));
            }

            /// <inheritdoc />
            protected override void OnEnqueueCalled()
            {
            }

            /// <inheritdoc />
            protected override void OnIdle(int workerId)
            {
                if (Volatile.Read(ref this.shuttingDown))
                {
#if NETSTANDARD2_1
                    Thread.CurrentThread.Abort();
#endif
                }
                else
                {
                    Thread.SpinWait(100);
                }
            }

            /// <inheritdoc />
            protected override void OnProcessItem(int workerId, TItem item)
            {
                this.owner.OnProcessItem(item);

                this.owner.OnPostDequeue(item);
            }

            /// <inheritdoc />
            protected override bool OnUnhandledException(int workerId, Exception exception)
            {
                return true;
            }
        }
    }
}