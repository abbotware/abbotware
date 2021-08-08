// -----------------------------------------------------------------------
// <copyright file="ISerialization{TStorage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Serialization
{
    using System;

    /// <summary>
    /// interface for serialization into a storage type
    /// </summary>
    /// <typeparam name="TStorage">protocol storage type</typeparam>
    public interface ISerialization<TStorage> : IProtocol<TStorage>
    {
        /// <summary>
        ///  Decodes an object from the storage type
        /// </summary>
        /// <param name="storage">encoded object</param>
        /// <param name="type">encoded object type</param>
        /// <returns>decoded object</returns>
        object? Decode(TStorage storage, Type type);
    }
}