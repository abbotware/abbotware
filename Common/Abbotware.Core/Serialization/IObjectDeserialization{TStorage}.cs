// -----------------------------------------------------------------------
// <copyright file="IObjectDeserialization{TStorage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Serialization
{
    using System;

    /// <summary>
    /// interface for deserialization that can determine the object type automatically
    /// </summary>
    /// <typeparam name="TStorage">protocol storage type</typeparam>
    public interface IObjectDeserialization<TStorage>
    {
        /// <summary>
        ///  Decodes an object from storage automatically determining the type
        /// </summary>
        /// <param name="storage">encoded object</param>
        /// <returns>decoded object</returns>
        object? Decode(TStorage storage);
    }
}