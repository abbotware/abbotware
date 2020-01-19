// -----------------------------------------------------------------------
// <copyright file="IEncodeKeyValueCollection{TModel}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Cache.ExtensionPoints
{
    using Abbotware.Core.Collections;

    /// <summary>
    /// Interface for encoding a model into a kv collection
    /// </summary>
    /// <typeparam name="TModel">model type</typeparam>
    public interface IEncodeKeyValueCollection<TModel>
    {
        /// <summary>
        /// Encodes the model in the kv collection
        /// </summary>
        /// <param name="input">model</param>
        /// <param name="keyValues">kv collection</param>
        void Encode(TModel input, IEncodedKeyValueCollection keyValues);
    }
}