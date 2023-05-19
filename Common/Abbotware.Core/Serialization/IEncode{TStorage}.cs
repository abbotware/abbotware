// -----------------------------------------------------------------------
// <copyright file="IEncode{TStorage}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Serialization
{
    /// <summary>
    /// Interface for encoding an object into storage
    /// </summary>
    /// <typeparam name="TStorage">storage type</typeparam>
    public interface IEncode<TStorage>
    {
        /// <summary>
        /// Encodes an object into a new storage type
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="value">object to encode</param>
        /// <returns>encoded message</returns>
        TStorage Encode<T>(T value);
    }

    /// <summary>
    /// Interface for encoding T into storage
    /// </summary>
    /// <typeparam name="T">type</typeparam>
    /// <typeparam name="TStorage">storage type</typeparam>
    public interface IEncode<T, TStorage>
    {
        /// <summary>
        /// Encodes an object into a new storage type
        /// </summary>
        /// <param name="value">object to encode</param>
        /// <returns>encoded message</returns>
        TStorage Encode(T value);
    }

    /// <summary>
    /// Interface for encoding an object into storage
    /// </summary>
    /// <typeparam name="TStorage">storage type</typeparam>
    public interface IEncodeInto<TStorage>
    {
        /// <summary>
        /// Encodes an object into an existing storage object
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="value">object to encode</param>
        /// <param name="storage">storage to encode into</param>
        void Encode<T>(T value, TStorage storage);
    }

    /// <summary>
    /// Interface for encoding T into storage
    /// </summary>
    /// <typeparam name="T">type</typeparam>
    /// <typeparam name="TStorage">storage type</typeparam>
    public interface IEncodeInto<T, TStorage>
    {
        /// <summary>
        /// Encodes an object into an existing storage object
        /// </summary>
        /// <param name="value">object to encode</param>
        /// <param name="storage">storage to encode into</param>
        void Encode(T value, TStorage storage);
    }
}