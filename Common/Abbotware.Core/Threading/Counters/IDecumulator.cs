// -----------------------------------------------------------------------
// <copyright file="IDecumulator.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Threading.Counters
{
    /// <summary>
    ///     interface for an object that can decrement its value
    /// </summary>
    public interface IDecumulator
    {
        /// <summary>
        ///     Gets the current value
        /// </summary>
        long Value { get; }

        /// <summary>
        ///     Decrements its value
        /// </summary>
        /// <returns>the new decremented value</returns>
        long Decrement();
    }
}