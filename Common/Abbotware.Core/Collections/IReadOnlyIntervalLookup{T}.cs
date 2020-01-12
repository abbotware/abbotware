// -----------------------------------------------------------------------
// <copyright file="IReadOnlyIntervalLookup{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Collections
{
    /// <summary>
    /// read only interface for the IntervalLookup collection
    /// </summary>
    /// <typeparam name="T">type of interval item</typeparam>
    public interface IReadOnlyIntervalLookup<T>
    {
        /// <summary>
        /// performs a lookup to get the associated item
        /// </summary>
        /// <param name="value">value to lookup</param>
        /// <returns>item assocated with the bound range</returns>
        T Lookup(int value);

        /// <summary>
        /// Checks to see if an interval contains the value
        /// </summary>
        /// <param name="value">value to check</param>
        /// <returns>true / false</returns>
        bool ContainedWithin(int value);
    }
}