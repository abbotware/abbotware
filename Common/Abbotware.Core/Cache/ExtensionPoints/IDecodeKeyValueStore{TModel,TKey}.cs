// -----------------------------------------------------------------------
// <copyright file="IDecodeKeyValueStore{TModel,TKey}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache.ExtensionPoints
{
    using Abbotware.Core.Collections;

    /// <summary>
    /// interface for decoding a model from a kv collection
    /// </summary>
    /// <typeparam name="TModel">model type</typeparam>
    /// <typeparam name="TKey">key type</typeparam>
    public interface IDecodeKeyValueStore<TModel, TKey>
    {
        /// <summary>
        /// decodes the model in the kv collection
        /// </summary>
        /// <param name="key">document key for model</param>
        /// <param name="keyValues">kv collection</param>
        /// <returns>model</returns>
        TModel Decode(TKey key, IEncodedKeyValueStore keyValues);
    }
}