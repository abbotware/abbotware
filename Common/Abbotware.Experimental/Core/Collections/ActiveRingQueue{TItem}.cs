// -----------------------------------------------------------------------
// <copyright file="ActiveRingQueue{TItem}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Sid Sacek</author>

namespace Abbotware.Core.Collections
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;
    using Abbotware.Core.Chrono;

    /// <summary>
    ///     Active Ring Queue collection
    /// </summary>
    /// <typeparam name="TItem">type of queue item</typeparam>
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "reviewed")]
    public abstract class ActiveRingQueue<TItem> : RingQueue<TItem>
    {
        /// <summary>
        /// array of worker threads
        /// </summary>
        private readonly Thread[] activeWorkers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveRingQueue{TItem}"/> class.
        ///     A thread-safe queue collection class with an active worker thread dedicated to dequeueing
        ///     and processing enqueued elements. The thread that's created is a first-class thread that
        ///     does not come from framework's thread-pool. Since this is a lock-free class, the active-
        ///     worker-thread never blocks or goes to sleep unless one of the OnEvent methods blocks.
        /// </summary>
        /// <param name="queueSize">
        ///     The size of the queue remains fixed for the entire life of the
        ///     collection object. The minimum and maximum queue sizes are described by the base class
        /// </param>
        /// <param name="workerCount">
        ///     The number of active worker threads to instantiate for the queue
        /// </param>
        protected ActiveRingQueue(int queueSize, int workerCount)
            : base(queueSize)
        {
            Arguments.Within(queueSize, 2, 1000, nameof(queueSize));
            Arguments.Within(workerCount, 1, 10, nameof(workerCount));

            this.activeWorkers = new Thread[workerCount];
            for (var i = 0; i < workerCount; ++i)
            {
                this.activeWorkers[i] = new Thread(this.ThreadStart);
            }
        }

        /// <summary>
        ///    Gets the exception that caused the active-worker-thread to terminate
        /// </summary>
        public Exception? Exception { get; private set; }

        /// <summary>
        ///     This method will activate all of the worker threads that were created in the constructor
        /// </summary>
        public void StartWorkers()
        {
            for (var i = 0; i < this.activeWorkers.Length; ++i)
            {
                this.activeWorkers[i].Start(i + 1);
            }
        }

        /// <inheritdoc/>
        public override bool Enqueue(ref TItem element, HighResolutionTimeSpan timer)
        {
            var result = base.Enqueue(ref element, timer);
            this.OnEnqueueCalled();
            return result;
        }

        /// <summary>
        ///     This method is reserved for the the active-worker-thread and will throw an
        ///     InvalidOperationException when called by an external source
        /// </summary>
        /// <returns>This method does not return since an InvalidOperationException will be thrown</returns>
        public override TItem Dequeue()
        {
            throw new InvalidOperationException("The Dequeue operation is reserved for the active worker thread");
        }

        /// <summary>
        ///     This method is reserved for the the active-worker-thread and will throw an
        ///     InvalidOperationException when called by an external source
        /// </summary>
        /// <returns>This method does not return since an InvalidOperationException will be thrown</returns>
        /// <param name="item">item for worker to process</param>
        public override bool Dequeue(out TItem item)
        {
            throw new InvalidOperationException("The Dequeue operation is reserved for the active worker thread");
        }

        /// <summary>
        ///     This event method is called when the active worker thread is first started up.
        ///     It is called by the very active-worker-thread itself. Use this event method to
        ///     further customize the thread upon class instantiation. When the callee returns
        ///     control of this thread, it will begin processing elements that get enqueued.
        /// </summary>
        /// <param name="workerId">id of worker thread</param>
        protected abstract void OnThreadStart(int workerId);

        /// <summary>
        ///     This event method is called to let the instance owner know when the object is
        ///     no longer being active, and may already have been marked as Disposed
        /// </summary>
        /// <param name="workerId">id of worker thread</param>
        protected abstract void OnThreadExit(int workerId);

        /// <summary>
        ///     This event method is called when an Exception is caught that was not handled by
        ///     any code in the path of the active-worker-thread's call stack. The owner of the
        ///     class instance gets the opportunity to handle the exception, and decides whether
        ///     the active-thread should terminate or simply ignore the exception and continue.
        ///     If the exception that is caught is a ThreadAbortException exception, this event
        ///     method will not be called, and the active-worker-thread will simply terminate.
        /// </summary>
        /// <param name="workerId">id of the worker thread</param>
        /// <param name="exception">The unhandled exception object</param>
        /// <returns>
        ///     The event handler returns 'true' to indicate that the exception was handled and
        ///     the active-worker-thread should resume processing, or returns 'false' to indicate
        ///     the active-worker-thread should terminate.
        /// </returns>
        protected abstract bool OnUnhandledException(int workerId, Exception exception);

        /// <summary>
        ///     Each time the Enqueue() method is called, the OnEnqueueCalled() event method is
        ///     also called after the base class's Enqueue() method is processed and returns
        /// </summary>
        protected abstract void OnEnqueueCalled();

        /// <summary>
        ///     Each time the active-worker-thread dequeues an item from the ring-queue, this
        ///     callback method is called to allow the user handler to process the element.
        /// </summary>
        /// <param name="workerId">id of worker thread</param>
        /// <param name="item">item for worker to process</param>
        protected abstract void OnProcessItem(int workerId, TItem item);

        /// <summary>
        ///     This event method is called each time the Dequeue() method is called by the
        ///     active-worker-thread and the ring-queue turns out to currently be empty.
        /// </summary>
        /// <param name="workerId">id of worker thread</param>
        protected abstract void OnIdle(int workerId);

        /// <summary>
        ///     This method will terminate the active-worker-threada and perform various cleanup
        /// </summary>
        /// <param name="disposing">Is 'true' if the Dispose() method was called, otherwise 'false'</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //-------------------------------------------------------------
                //  The active workers are not allowed to dispose themselves
                //-------------------------------------------------------------
                if (!this.activeWorkers.Any(worker => worker == Thread.CurrentThread))
                {
                    foreach (var worker in this.activeWorkers)
                    {
                        if (worker.IsAlive)
                        {
                            worker.Abort();
                        }
                    }
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        ///     This method is responsible for dequeueing and handling enqueued items
        /// </summary>
        /// <param name="argument">worker thread argument </param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "review, bottom of stack")]
        private void ThreadStart(object argument)
        {
            Arguments.NotNull(argument, nameof(argument));

            var worker_id = (int)argument;

            // alert the class owner that the active worker thread has started
            this.OnThreadStart(worker_id);

            try
            {
                for (var done = false; !done && !this.IsDisposed;)
                {
                    try
                    {
                        var dequeue_succeeded = base.Dequeue(out TItem? item);

                        if (dequeue_succeeded && item != null)
                        {
                            this.OnProcessItem(worker_id, item);
                        }
                        else
                        {
                            this.OnIdle(worker_id);
                        }
                    }
                    catch (Exception exception)
                    {
                        done = true;

                        if (!(exception is ThreadAbortException))
                        {
                            var exception_got_handled = this.OnUnhandledException(worker_id, exception);
                            done = !exception_got_handled;
                        }

                        this.Exception = done ? exception : null;
                    }
                }
            }
            finally
            {
                this.OnThreadExit(worker_id);
                this.Dispose();
            }
        }
    }
}