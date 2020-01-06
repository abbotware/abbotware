// -----------------------------------------------------------------------
// <copyright file="IProtocol{TStorage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Serialization
{
    /// <summary>
    ///     Generic interface for encoding / decoding objects into storage
    /// </summary>
    /// <typeparam name="TStorage">protocol storage type</typeparam>
    public interface IProtocol<TStorage>
    {
        /// <summary>
        /// Encodes an object into the storage type
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="value">object to encode</param>
        /// <returns>encoded message</returns>
        TStorage Encode<T>(T value);

        /// <summary>
        ///  Decodes an object from the storage type
        /// </summary>
        /// <typeparam name="T">encoded object type</typeparam>
        /// <param name="storage">encoded object</param>
        /// <returns>decoded object</returns>
        T Decode<T>(TStorage storage);
    }
}