// -----------------------------------------------------------------------
// <copyright file="KeyListConverter{TValue}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Newtonsoft.Plugins
{
    using System.Collections.Generic;

    /// <summary>
    /// class for flattening a dictionary
    /// </summary>
    /// <typeparam name="TValue">value type</typeparam>
    public abstract class KeyListConverter<TValue>
    {
        /// <summary>
        /// Convert a KeyValuePair to Value
        /// </summary>
        /// <param name="values">values</param>
        /// <returns>the converted value</returns>
        public abstract IReadOnlyCollection<TValue> Convert(Dictionary<string, Dictionary<string, TValue>> values);
    }
}