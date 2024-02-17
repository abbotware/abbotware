// -----------------------------------------------------------------------
// <copyright file="IndexedObserveable.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Abbotware.Core.Collections
{
    /// <summary>
    /// Upsert action behavior
    /// </summary>
    public enum UpsertAction
    {
        /// <summary>
        /// Moves the updated item to the front of the list
        /// </summary>
        RelocateToFront,

        /// <summary>
        /// Moves the updated item to the back of the list
        /// </summary>
        RelocateToBack,

        /// <summary>
        /// Updates the item in place
        /// </summary>
        UpdateInPlace,
    }

    /// <summary>
    /// Observable Collection that
    /// </summary>
    /// <typeparam name="TKey">key type</typeparam>
    /// <typeparam name="TItem">item type</typeparam>
    public class IndexedObserveable<TKey, TItem> : INotifyCollectionChanged, IEnumerable<TItem>
        where TKey : notnull
    {
        private readonly ObservableCollection<TItem> collection = new();
        private readonly Dictionary<TKey, TItem> index = new();

        /// <inheritdoc/>
        public event NotifyCollectionChangedEventHandler? CollectionChanged
        {
            add
            {
                this.collection.CollectionChanged += value;
            }

            remove
            {
                this.collection.CollectionChanged -= value;
            }
        }

        /// <summary>
        /// Insert or Update
        /// </summary>
        /// <param name="key">key of item</param>
        /// <param name="item">item</param>
        /// <param name="action">upsert action</param>
        public void Upsert(TKey key, TItem item, UpsertAction action)
        {
            lock (this.index)
            {
                switch (action)
                {
                    case UpsertAction.RelocateToBack:
                        _ = this.Remove(key);
                        this.index[key] = item;
                        this.collection.Insert(0, item);
                        break;
                    case UpsertAction.RelocateToFront:
                        _ = this.Remove(key);
                        this.index[key] = item;
                        this.collection.Add(item);
                        break;
                    case UpsertAction.UpdateInPlace:
                        if (this.index.TryGetValue(key, out var previous))
                        {
                            var idx = this.collection.IndexOf(previous);
                            this.collection[idx] = item;
                        }
                        else
                        {
                            this.collection.Add(item);
                        }

                        this.index[key] = item;

                        break;
                }
            }
        }

        /// <summary>
        /// Insert or Update
        /// </summary>
        /// <param name="key">key of item to remove</param>
        /// <returns>the removed item (if any)</returns>
        public TItem? Remove(TKey key)
        {
            lock (this.index)
            {
                if (!this.index.TryGetValue(key, out var previous))
                {
                    return default;
                }

                this.collection.Remove(previous);
                return previous;
            }
        }

        /// <inheritdoc/>
        public IEnumerator<TItem> GetEnumerator() => this.collection.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => this.collection.GetEnumerator();
    }
}
