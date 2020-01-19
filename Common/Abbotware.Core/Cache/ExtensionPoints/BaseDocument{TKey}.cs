// -----------------------------------------------------------------------
// <copyright file="BaseDocument{TKey}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache.ExtensionPoints
{
    /// <summary>
    /// base class for a doucment key
    /// </summary>
    /// <typeparam name="TKey">key type</typeparam>
    public abstract class BaseDocument<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDocument{TKey}"/> class.
        /// </summary>
        /// <param name="key">key value</param>
        protected BaseDocument(TKey key)
        {
            this.Key = key;
        }

        /// <summary>
        /// Gets the key
        /// </summary>
        public TKey Key { get; }
    }
}