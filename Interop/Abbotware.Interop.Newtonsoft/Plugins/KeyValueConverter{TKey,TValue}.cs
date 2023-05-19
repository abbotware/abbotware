// -----------------------------------------------------------------------
// <copyright file="KeyValueConverter{TKey,TValue}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Newtonsoft.Plugins
{
    using System.Collections.Generic;

    /// <summary>
    /// class for flattening a dictionary
    /// </summary>
    /// <typeparam name="TKey">key type</typeparam>
    /// <typeparam name="TValue">value type</typeparam>
    public abstract class KeyValueConverter<TKey, TValue>
    {
        /// <summary>
        /// Convert a KeyValuePair to Value
        /// </summary>
        /// <param name="kvp">key value pair</param>
        /// <returns>the converted value</returns>
        public abstract TValue Convert(KeyValuePair<TKey, TValue> kvp);
    }
}