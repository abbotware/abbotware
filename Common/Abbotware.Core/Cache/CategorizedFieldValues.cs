// -----------------------------------------------------------------------
// <copyright file="CategorizedFieldValues.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Cache.ExtensionPoints;
    using Abbotware.Core.Cache.Internal;
    using Abbotware.Core.Cache.LocalOperations;
    using Abbotware.Core.Cache.RemoteOperations;

    /// <summary>
    /// Remote Key with Categorized Field-Value sets
    /// </summary>
    public class CategorizedFieldValues : ICacheableCategorizedFieldValues, ICacheOperations, ICategorizedFieldValues
    {
        /// <summary>
        ///     out of process cache manager
        /// </summary>
        private readonly IRemoteCache cacheManager;

        /// <summary>
        ///     set of property categories
        /// </summary>
        // TODO: remove this and use VLookup<T1,T2,TValue> ?
        private readonly ConcurrentDictionary<string, string> categories = new();

        /// <summary>
        ///     collection of changes that need to be committed
        /// </summary>
        private readonly BagOfWork<Tuple<string?, string>> changed = new();

        /// <summary>
        ///     inprocess cache of values
        /// </summary>
        private readonly Dictionary<string, ConcurrentDictionary<string, string>> values = new();

        /// <summary>
        ///     Initializes a new instance of the <see cref="CategorizedFieldValues" /> class.
        /// </summary>
        /// <param name="type">type of item (part of compositie key)</param>
        /// <param name="id">id of item (part of compositie key)</param>
        /// <param name="manager">cache manager</param>
        public CategorizedFieldValues(string type, string id, IRemoteCache manager)
        {
            this.cacheManager = manager;
            this.Type = type;
            this.Id = id;
        }

        /// <inheritdoc />
        public ICategorizedFieldValues Local => this;

        /// <inheritdoc />
        public ICacheOperations Remote => this;

        /// <inheritdoc />
        public string Type { get; }

        /// <inheritdoc />
        public string Id { get; }

        /// <inheritdoc />
        public IEnumerable<string> Categories => this.categories.Keys;

        /// <inheritdoc />
        public IEnumerable<string> Fields => this.values.FirstOrDefault().Value?.Keys ?? new List<string>();

        /// <inheritdoc />
        // TODO: the key creation logic should be encapsulated
        public string RemoteKey => $"{this.Type}:{this.Id}";

        /// <inheritdoc />
        public int ValueCount
        {
            get
            {
                var sum = 0;

                foreach (var kvp in this.values)
                {
                    sum += kvp.Value.Count;
                }

                return sum;
            }
        }

        /// <inheritdoc />
        public async Task LoadAsync(CancellationToken ct)
        {
            this.values.Clear();

            var categories = await this.cacheManager.GetFieldsAsync(this.RemoteKey, ct)
                .ConfigureAwait(false);

            foreach (var c in categories)
            {
                this.categories.GetOrAdd(c.Key, c.Key);

                this.values.Add(c.Key, new ConcurrentDictionary<string, string>());

                var compositeKey = this.CompositeKey(c.Key);

                var kvs = await this.cacheManager.GetFieldsAsync(compositeKey, ct)
                    .ConfigureAwait(false);

                foreach (var kv in kvs)
                {
                    this.values[c.Key].GetOrAdd(kv.Key, kv.Value);
                }
            }
        }

        /// <inheritdoc />
        public async Task SaveAsync(CancellationToken ct)
        {
            var oldWork = this.changed.GetWork();

            var items = oldWork
                .GroupBy(x => x.Item1, x =>
                {
                    if (x.Item1 == null)
                    {
                        return new KeyValuePair<string, string>(x.Item2, x.Item2);
                    }

                    // get the latest value from memory
                    return new KeyValuePair<string, string>(x.Item2, this.values[x.Item1][x.Item2]);
                })
                .ToList();

            foreach (var i in items)
            {
                if (i.Key == null)
                {
                    await this.cacheManager.SetFieldsAsync(this.RemoteKey, i.ToList(), ct)
                        .ConfigureAwait(false);
                }
                else
                {
                    await this.cacheManager.SetFieldsAsync(this.CompositeKey(i.Key), i.ToList(), ct)
                        .ConfigureAwait(false);
                }
            }
        }

        /// <inheritdoc />
        public string? GetOrDefault(string category, string field)
        {
            if (!this.values.TryGetValue(category, out var cat))
            {
                return null;
            }

            if (!cat.TryGetValue(field, out var value))
            {
                return null;
            }

            return value;
        }

        /// <inheritdoc />
        public void AddOrUpdate(string category, string field, string value)
        {
            if (!this.categories.ContainsKey(category))
            {
                this.categories.GetOrAdd(category, category);
                this.values.Add(category, new ConcurrentDictionary<string, string>());

                // we are encoding null in the bag of work so we know something changed in this category
                this.changed.Add(new Tuple<string?, string>(null, category));
            }

            this.values[category].AddOrUpdate(field, value, (u, v) => value);

            this.changed.Add(new Tuple<string?, string>(category, field));
        }

        /// <summary>
        ///     Gets a field value
        /// </summary>
        /// <param name="category">key category</param>
        /// <param name="field">key name</param>
        /// <param name="reload">reload latest value</param>
        /// <returns>field value</returns>
        /// <param name="ct">cancellation token</param>
        public async Task<string?> GetOrDefaultAsync(string category, string field, bool reload, CancellationToken ct)
        {
            if (reload)
            {
                var newValue = await this.cacheManager.GetFieldAsync(this.CompositeKey(category), field, ct)
                    .ConfigureAwait(false);

                this.values[category].GetOrAdd(field, newValue);

                return newValue;
            }
            else
            {
                return this.GetOrDefault(category, field);
            }
        }

        /// <summary>
        ///     Gets a composite key based on the keypart
        /// </summary>
        /// <param name="keyPart">additional key part</param>
        /// <returns>composite key</returns>
        private string CompositeKey(string keyPart)
        {
            return $"{this.RemoteKey}:{keyPart}";
        }
    }
}