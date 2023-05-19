// -----------------------------------------------------------------------
// <copyright file="IUniquelyIdentifiable.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core
{
    using System;

    /// <summary>
    ///     Interface that represents an object that can be uniquely identified
    /// </summary>
    /// <typeparam name="TKey">Key Type</typeparam>
    public interface IUniquelyIdentifiable<out TKey>
        where TKey : IComparable, IEquatable<TKey>, IComparable<TKey>
    {
        /// <summary>
        ///     Gets the Id for the object
        /// </summary>
        TKey Id { get; }
    }
}