// -----------------------------------------------------------------------
// <copyright file="RowVector{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.LinearAlgebra
{
    using System;
    using System.Linq;
    using System.Numerics;
    using Abbotware.Quant.LinearAlgebra.Extensions;

    /// <summary>
    /// Row Vector ( 1xn Vector ) :  𝑣ᵗ = (𝑣₁,𝑣₂, ... 𝑣ₙ)
    /// </summary>
    /// <typeparam name="T">numeric data type</typeparam>
    public class RowVector<T> : Matrix<T>
        where T : INumber<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RowVector{T}"/> class.
        /// </summary>
        /// <param name="columns">number of columns</param>
        public RowVector(uint columns)
            : base(1, columns)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RowVector{T}"/> class.
        /// </summary>
        /// <param name="values">values</param>
        public RowVector(params T[] values)
            : base(values.AsMatrix())
        {
        }

        /// <summary>
        /// gets or sets the element at column position
        /// </summary>
        /// <param name="column">column number</param>
        /// <returns>element</returns>
        public T this[uint column]
        {
            get { return this[0, column]; }
            set { this[0, column] = value; }
        }

        /// <summary>
        /// Inner Product / Dot Product
        /// </summary>
        /// <param name="left">row vector</param>
        /// <param name="right">column vector</param>
        /// <returns>dot/inner product</returns>
        /// <exception cref="InvalidOperationException">error cases</exception>
        public static T operator *(RowVector<T> left, ColumnVector<T> right)
        {
            if (left.Columns != right.Rows)
            {
                throw new InvalidOperationException();
            }

            var sum = T.Zero;

            for (uint i = 0; i < left.Columns; ++i)
            {
                sum += left[0, i] * right[i, 0];
            }

            return sum;
        }

        /// <summary>
        /// Row Vector x Matrix (𝒘ᵗ𝑨) => Row Vector (linear combination of rows)
        /// </summary>
        /// <param name="left">row vector</param>
        /// <param name="right">Matrix</param>
        /// <returns>dot/inner product</returns>
        /// <exception cref="InvalidOperationException">error cases</exception>
        public static RowVector<T> operator *(RowVector<T> left, Matrix<T> right)
        {
            if (left.Columns != right.Columns)
            {
                throw new InvalidOperationException();
            }

            var linearCombination = new RowVector<T>(right.Columns);

            for (uint i = 0; i < left.Columns; ++i)
            {
                var sum = T.Zero;

                for (uint j = 0; j < right.Rows; ++j)
                {
                    sum += left[0, i] * right[j, i];
                }

                linearCombination[0, i] = sum;
            }

            return linearCombination;
        }

        /// <summary>
        /// Inner Product / Dot Product
        /// </summary>
        /// <param name="left">row vector</param>
        /// <param name="right">column vector</param>
        /// <returns>dot/inner product</returns>
        /// <exception cref="InvalidOperationException">error cases</exception>
        public static T Multiply(RowVector<T> left, ColumnVector<T> right) => left * right;

        /// <summary>
        /// Transpose a Row Vector to a Column Vector
        /// </summary>
        /// <returns>transposed column vector</returns>
        public new ColumnVector<T> Transpose() => new ColumnVector<T>(this.Row(0).ToArray());
    }
}