// -----------------------------------------------------------------------
// <copyright file="IRowTransform{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.LinearAlgebra.Operations
{
    using System.Numerics;

    /// <summary>
    /// interface for row transform operations (modify in place)
    /// </summary>
    /// <typeparam name="T">numeric data type</typeparam>
    public interface IRowTransform<T>
           where T : INumber<T>
    {
        /// <summary>
        /// adds the row vector to each row
        /// </summary>
        /// <param name="vector">row vector</param>
        void Add(RowVector<T> vector);
    }
}