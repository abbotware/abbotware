// -----------------------------------------------------------------------
// <copyright file="RowVector{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.LinearAlgebra
{
    using System;
    using System.Numerics;

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
        /// Inner Product / Dot Product
        /// </summary>
        /// <param name="left">row vector</param>
        /// <param name="right">column vector</param>
        /// <returns>dot/inner product</returns>
        /// <exception cref="InvalidOperationException">error cases</exception>
        public static T Multiply(RowVector<T> left, ColumnVector<T> right) => left * right;
    }
}