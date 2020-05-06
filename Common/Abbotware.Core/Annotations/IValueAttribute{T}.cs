// -----------------------------------------------------------------------
// <copyright file="IValueAttribute{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Annotations
{
    /// <summary>
    /// Base interface for an attribute with a value
    /// </summary>
    /// <typeparam name="T">value type</typeparam>
    public interface IValueAttribute<T>
    {
        /// <summary>
        /// Gets the attribute value.
        /// </summary>
        T Value { get; }
    }
}
