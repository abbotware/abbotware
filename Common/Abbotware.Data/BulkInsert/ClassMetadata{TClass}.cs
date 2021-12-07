// -----------------------------------------------------------------------
// <copyright file="ClassMetadata{TClass}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Data.BulkInsert
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Abbotware.Core;
    using Abbotware.Core.Diagnostics;

    /// <summary>
    ///     class that contains meta data / reflection info for a class
    /// </summary>
    /// <typeparam name="TClass">Class Type</typeparam>
    public class ClassMetadata<TClass>
        where TClass : class
    {
        /// <summary>
        ///     provides property name to ordinal lookups
        /// </summary>
        private readonly Dictionary<string, int> ordinal = new();

        /// <summary>
        ///     list of functions to retrieve property value in ordinal order
        /// </summary>
        private readonly List<Func<TClass, object>> propertyFunctors = new();

        /// <summary>
        ///     list of all PropertyInfo in ordinal order
        /// </summary>
        private readonly List<PropertyInfo> propertyInfo = new();

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClassMetadata{TClass}" /> class.
        /// </summary>
        public ClassMetadata()
        {
            var properties = ReflectionHelper.Properties<TClass>();

            var ordinal = 0;

            foreach (var propertyInfo in properties)
            {
                this.propertyFunctors.Add(x => propertyInfo.GetValue(x)!);

                this.propertyInfo.Add(propertyInfo);

                this.ordinal.Add(propertyInfo.Name, ordinal++);
            }
        }

        /// <summary>
        ///     Gets the number of properties for the class
        /// </summary>
        public int PropertyCount
        {
            get
            {
                return this.propertyFunctors.Count;
            }
        }

        /// <summary>
        ///     Gets the ordinal id for a property name
        /// </summary>
        /// <param name="name">name of property</param>
        /// <returns>ordinal id used in other functions</returns>
        public int GetOrdinal(string name)
        {
            name = Arguments.EnsureNotNullOrWhitespace(name, nameof(name));

            return this.ordinal[name];
        }

        /// <summary>
        ///     gets the property value based on property name
        /// </summary>
        /// <param name="propertyName">name of property</param>
        /// <param name="instance">instance of class</param>
        /// <returns>property value</returns>
        public object? GetPropertyValue(string propertyName, TClass instance)
        {
            propertyName = Arguments.EnsureNotNullOrWhitespace(propertyName, nameof(propertyName));
            instance = Arguments.EnsureNotNull(instance, nameof(instance));

            return this.GetPropertyValue(this.GetOrdinal(propertyName), instance);
        }

        /// <summary>
        ///     gets the property value based on property ordinal id
        /// </summary>
        /// <param name="ordinalId">ordinal id of property</param>
        /// <param name="instance">instance of class</param>
        /// <returns>property value</returns>
        public object GetPropertyValue(int ordinalId, TClass instance)
        {
            instance = Arguments.EnsureNotNull(instance, nameof(instance));

            if (this.propertyFunctors.Count < ordinalId)
            {
                throw new KeyNotFoundException(FormattableString.Invariant($"Ordinal:{ordinalId} not found"));
            }

            var temp = this.propertyFunctors[ordinalId]?.Invoke(instance);

            if (temp is DateTimeOffset offset)
            {
                if (offset == DateTimeOffset.MinValue)
                {
                    throw new NotSupportedException("Check if DBNull before getting value");
                }
            }

            if (temp is DateTime time)
            {
                if (time == DateTime.MinValue)
                {
                    throw new NotSupportedException("Check if DBNull before getting value");
                }
            }

            return temp!;
        }
    }
}