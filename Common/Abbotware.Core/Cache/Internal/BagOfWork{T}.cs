// -----------------------------------------------------------------------
// <copyright file="BagOfWork{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache.Internal
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Bag of work items
    /// </summary>
    /// <typeparam name="T">work item type</typeparam>
    public class BagOfWork<T>
    {
        /// <summary>
        /// list of fields that have changed that will be saved
        /// </summary>
        private ConcurrentQueue<T> changed = new ConcurrentQueue<T>();

        /// <summary>
        /// Gets the current work items in the bag
        /// </summary>
        /// <returns>work items</returns>
        public IEnumerable<T> GetWork()
        {
            var currentWork = this.changed;

            var newWork = new ConcurrentQueue<T>();

            Interlocked.Exchange(ref this.changed, newWork);

            return currentWork;
        }

        /// <summary>
        /// Adds a work item
        /// </summary>
        /// <param name="work">work item</param>
        public void Add(T work)
        {
            this.changed.Enqueue(work);
        }
    }
}