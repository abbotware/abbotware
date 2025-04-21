// -----------------------------------------------------------------------
// <copyright file="PropertySet.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Abbotware.Core.Cache.ExtensionPoints;

    /// <summary>
    /// Key/Value property cache with timestamp
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="PropertySet"/> class.
    /// </remarks>
    /// <param name="type">type of item (part of compositie key)</param>
    /// <param name="id">id of item (part of compositie key)</param>
    /// <param name="manager">cache manager</param>
    public class PropertySet(string type, string id, IRemoteCache manager)
    {
        /// <summary>
        /// categorized field-value set store
        /// </summary>
        private readonly CategorizedFieldValues categorizedFieldSet = new(type, id, manager);

        /// <summary>
        /// Gets the categories
        /// </summary>
        public IEnumerable<string> Categories => this.categorizedFieldSet.Local.Categories;

        /// <summary>
        /// Gets the property names
        /// </summary>
        public IEnumerable<string> Names => this.categorizedFieldSet.Local.Fields;

        /// <summary>
        /// Gets all properties
        /// </summary>
        public IEnumerable<Property> Properties
        {
            get
            {
                var l = new List<Property>(this.Names.Count());

                foreach (var k in this.Names)
                {
                    var p = this.GetOrDefault(k);

                    if (p == null)
                    {
                        continue;
                    }

                    l.Add(p);
                }

                return l;
            }
        }

        /// <summary>
        /// adds or updates a field-value
        /// </summary>
        /// <param name="field">key</param>
        /// <param name="value">value</param>
        public void AddOrUpdate(string field, string value)
        {
            this.AddOrUpdate(field, value, DateTimeOffset.Now);
        }

        /// <summary>
        /// adds or updates a field-value
        /// </summary>
        /// <param name="field">field</param>
        /// <param name="value">value</param>
        /// <param name="date">date</param>
        public void AddOrUpdate(string field, string value, DateTimeOffset date)
        {
            this.categorizedFieldSet.Local.AddOrUpdate("value", field, value);
            this.categorizedFieldSet.Local.AddOrUpdate("timestamp", field, date.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Gets a property by field name
        /// </summary>
        /// <param name="field">field</param>
        /// <returns>a property with optional timestamp</returns>
        public Property? GetOrDefault(string field)
        {
            field = Arguments.EnsureNotNull(field, nameof(field));

            var fieldValue = this.categorizedFieldSet.Local.GetOrDefault("value", field);

            if (string.IsNullOrWhiteSpace(fieldValue))
            {
                return null;
            }

            var p = new Property
            {
                Field = field,
                Value = fieldValue,
            };

            var dateValue = this.categorizedFieldSet.Local.GetOrDefault("timestamp", field);

            if (DateTimeOffset.TryParse(dateValue, out var date))
            {
                p.Timestamp = date;
            }

            return p;
        }

        /// <summary>
        /// save changes to the cache
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        public async Task SaveAsync(CancellationToken ct)
        {
            await this.categorizedFieldSet.Remote.SaveAsync(ct)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// load changes to the cache
        /// </summary>
        /// <param name="ct">cancellation token</param>
        /// <returns>async task</returns>
        public async Task LoadAsync(CancellationToken ct)
        {
            await this.categorizedFieldSet.Remote.LoadAsync(ct)
                .ConfigureAwait(false);
        }
    }
}