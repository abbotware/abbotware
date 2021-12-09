// -----------------------------------------------------------------------
// <copyright file="TypeCreatedCounter.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Threading.Counters
{
    /// <summary>
    ///     special class that can be used for counting instances of a specific type of an object.  This only increments and
    ///     never decrements.  Useful for diagnostics
    /// </summary>
    /// <remarks>
    ///     Use this class in composition with another class.
    /// </remarks>
    /// <typeparam name="T">type to use for counting</typeparam>
    public sealed class TypeCreatedCounter<T>
    {
        /// <summary>
        ///     global static counter
        /// </summary>
        private static readonly AtomicCounter Sequence = new();

        /// <summary>
        ///     Initializes a new instance of the <see cref="TypeCreatedCounter{T}" /> class.
        /// </summary>
        public TypeCreatedCounter() => this.InstanceId = Sequence.Increment() - 1;

        /// <summary>
        ///     Gets the global count for this instance type
        /// </summary>
        public static long Count => Sequence.Value;

        /// <summary>
        ///     Gets the Instance Id
        /// </summary>
        public long InstanceId { get; }

        /// <summary>
        ///     Gets the global count for this instance type
        /// </summary>
        public long CreatedCount => Count;
    }
}