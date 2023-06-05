// -----------------------------------------------------------------------
// <copyright file="TypeDescriptorHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Helpers
{
    using System;
    using System.Collections.Concurrent;
    using System.ComponentModel;

    /// <summary>
    /// TypeDescripter helper methods
    /// </summary>
    public static class TypeDescriptorHelper
    {
        private static readonly ConcurrentDictionary<Type, TypeConverter> Cache = new();

        /// <summary>
        /// Gets the TypeConverter from the cache
        /// </summary>
        /// <param name="source">source type</param>
        /// <returns>converter</returns>
        public static TypeConverter GetConverter(Type source) => Cache.GetOrAdd(source, TypeDescriptor.GetConverter(source));
    }
}
