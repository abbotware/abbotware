// -----------------------------------------------------------------------
// <copyright file="IAccumulator.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Threading.Counters
{
    /// <summary>
    ///     interface for an object that can increment its value
    /// </summary>
    public interface IAccumulator
    {
        /// <summary>
        ///     Gets the current value
        /// </summary>
        long Value { get; }

        /// <summary>
        ///     Increments its value
        /// </summary>
        /// <returns>the new incremented value</returns>
        long Increment();
    }
}