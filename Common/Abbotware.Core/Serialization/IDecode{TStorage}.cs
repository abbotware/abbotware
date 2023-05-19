// -----------------------------------------------------------------------
// <copyright file="IDecode{TStorage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Serialization
{
    /// <summary>
    /// Interface for decoding an object from stroage
    /// </summary>
    /// <typeparam name="TStorage">storage type</typeparam>
    public interface IDecode<TStorage>
    {
        /// <summary>
        ///  Decodes an object from the storage type
        /// </summary>
        /// <typeparam name="T">encoded object type</typeparam>
        /// <param name="storage">encoded object</param>
        /// <returns>decoded object</returns>
        T Decode<T>(TStorage storage);
    }

    /// <summary>
    /// Interface for decoding T from stroage
    /// </summary>
    /// <typeparam name="T">type</typeparam>
    /// <typeparam name="TStorage">storage type</typeparam>
    public interface IDecode<T, TStorage>
    {
        /// <summary>
        ///  Decodes an object from the storage type
        /// </summary>
        /// <param name="storage">encoded object</param>
        /// <returns>decoded object</returns>
        T Decode(TStorage storage);
    }
}