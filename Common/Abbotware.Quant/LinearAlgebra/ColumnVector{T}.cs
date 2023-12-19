// -----------------------------------------------------------------------
// <copyright file="ColumnVector{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.LinearAlgebra
{
    using System;
    using System.Numerics;

    /// <summary>
    /// Column Vector
    /// </summary>
    /// <typeparam name="T">numeric data type</typeparam>
    public class ColumnVector<T> : Matrix<T>
        where T : INumber<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnVector{T}"/> class.
        /// </summary>
        /// <param name="rows">number of rows</param>
        public ColumnVector(uint rows)
            : base(rows, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnVector{T}"/> class.
        /// </summary>
        /// <param name="values">column values</param>
        public ColumnVector(params T[] values)
        : base(values.Transpose())
        {
        }

        /// <summary>
        /// Column Vector x Matrix (𝒘ᵗ𝑨) => Column Vector (linear combination of columns)
        /// </summary>
        /// <param name="left">Matrix</param>
        /// <param name="right">column vector</param>
        /// <returns>linear combination of columns</returns>
        /// <exception cref="InvalidOperationException">error cases</exception>
        public static ColumnVector<T> operator *(Matrix<T> left, ColumnVector<T> right)
        {
            if (left.Rows != right.Rows)
            {
                throw new InvalidOperationException();
            }

            var linearCombination = new ColumnVector<T>(right.Rows);

            for (uint i = 0; i < left.Columns; ++i)
            {
                var sum = T.Zero;

                for (uint j = 0; j < right.Rows; ++j)
                {
                    sum += left[j, i] * right[j, 0];
                }

                linearCombination[i, 0] = sum;
            }

            return linearCombination;
        }

        /// <summary>
        /// Column Vector x Matrix (𝒘ᵗ𝑨) => Column Vector (linear combination of columns)
        /// </summary>
        /// <param name="left">Matrix</param>
        /// <param name="right">column vector</param>
        /// <returns>linear combination of columns</returns>
        /// <exception cref="InvalidOperationException">error cases</exception>
        public static ColumnVector<T> Multiply(ColumnVector<T> left, ColumnVector<T> right) => left * right;
    }
}
