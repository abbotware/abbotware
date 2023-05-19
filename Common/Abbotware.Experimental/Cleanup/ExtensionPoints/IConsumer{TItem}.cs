// -----------------------------------------------------------------------
// <copyright file="IConsumer{TItem}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.ExtensionPoints
{
    using Abbotware.Core.Objects;

    /// <summary>
    ///     interface for producer /consumer
    /// </summary>
    /// <typeparam name="TItem">item type to queue</typeparam>
    public interface IConsumer<TItem> : IInitializable
        where TItem : class
    {
        /// <summary>
        ///     Shutsdown the consumer
        /// </summary>
        /// <param name="waitForWorkers">flag to indicate block for worker shutdown</param>
        void Shutdown(bool waitForWorkers);

        /// <summary>
        ///     Enqueues an item
        /// </summary>
        /// <param name="item">item to enqueue</param>
        void Enqueue(TItem item);
    }
}