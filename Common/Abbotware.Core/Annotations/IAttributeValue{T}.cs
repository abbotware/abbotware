// -----------------------------------------------------------------------
// <copyright file="IAttributeValue{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Annotations
{
    /// <summary>
    /// Base interface for an attribute with a value
    /// </summary>
    /// <typeparam name="T">value type</typeparam>
    public interface IAttributeValue<T>
    {
        /// <summary>
        /// Gets the attribute value.
        /// </summary>
        T Value { get; }
    }
}