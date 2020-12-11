// -----------------------------------------------------------------------
// <copyright file="BaseDocumentProtocol{TModel,TKey}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache.ExtensionPoints
{
    using Abbotware.Core.Collections;

    /// <summary>
    /// Base class for a document encoder protocol
    /// </summary>
    /// <typeparam name="TModel">poco class type</typeparam>
    /// <typeparam name="TKey">document key type</typeparam>
    public abstract class BaseDocumentProtocol<TModel, TKey> : IEncodeKeyValueStore<TModel>, IDecodeKeyValueStore<TModel, TKey>
    {
        /// <summary>
        /// encodes a document into a kv collection
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="keyValues">kv collection</param>
        public abstract void Encode(TModel input, IEncodedKeyValueStore keyValues);

        /// <summary>
        /// decodes a kv collection into a document
        /// </summary>
        /// <param name="key">document key</param>
        /// <param name="keyValues">kv collection</param>
        /// <returns>decode document</returns>
        public abstract TModel Decode(TKey key, IEncodedKeyValueStore keyValues);
    }
}