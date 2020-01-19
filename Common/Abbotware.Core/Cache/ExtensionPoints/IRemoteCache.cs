// -----------------------------------------------------------------------
// <copyright file="IRemoteCache.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache.ExtensionPoints
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// remote cache manager
    /// </summary>
    public interface IRemoteCache
    {
        /// <summary>
        /// Gets a collection of field-values for the specified key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>field-values</returns>
        Task<IReadOnlyDictionary<string, string>> GetFieldsAsync(string key, CancellationToken ct);

        /// <summary>
        /// Sets a collection of field-value pairs associated with a key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="fields">field-values to set</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>async handle</returns>
        Task SetFieldsAsync(string key, IEnumerable<KeyValuePair<string, string>> fields, CancellationToken ct);

        /// <summary>
        /// Gets a specific field-value for a key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="field">field</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>value if it exists</returns>
        Task<string> GetFieldAsync(string key, string field, CancellationToken ct);
    }
}