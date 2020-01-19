// -----------------------------------------------------------------------
// <copyright file="ICategorizedFieldValues.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache.LocalOperations
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for a cache backed Categorized Field-Values for a RemoteKey
    /// </summary>
    public interface ICategorizedFieldValues
    {
        /// <summary>
        ///     Gets the Id component of the remote key
        /// </summary>
        string Id { get; }

        /// <summary>
        ///     Gets the Type portion of the remote key
        /// </summary>
        string Type { get; }

        /// <summary>
        ///     Gets the keys
        /// </summary>
        IEnumerable<string> Fields { get; }

        /// <summary>
        ///     Gets the categories
        /// </summary>
        IEnumerable<string> Categories { get; }

        /// <summary>
        ///     Gets the count of the fields in the categories
        /// </summary>
        int ValueCount { get; }

        /// <summary>
        ///     Adds categorized field-value
        /// </summary>
        /// <param name="category">category</param>
        /// <param name="field">field name</param>
        /// <param name="value">value</param>
        void AddOrUpdate(string category, string field, string value);

        /// <summary>
        ///     Gets a field-value
        /// </summary>
        /// <param name="category">category</param>
        /// <param name="field">field name</param>
        /// <returns>field value</returns>
        string? GetOrDefault(string category, string field);
    }
}