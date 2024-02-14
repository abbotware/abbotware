// -----------------------------------------------------------------------
// <copyright file="IColumnStatistics{T}.cs" company="Abbotware, LLC">
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
    public interface IColumnStatistics<T>
           where T : INumber<T>
    {

        /// <summary>
        /// Compute the column means
        /// </summary>
        /// <returns>column means as a row vector</returns>
        RowVector<T> Mean();
    }
}