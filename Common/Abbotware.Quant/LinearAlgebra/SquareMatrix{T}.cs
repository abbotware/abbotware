// -----------------------------------------------------------------------
// <copyright file="SquareMatrix{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.LinearAlgebra
{
    using System;
    using System.Numerics;

    /// <summary>
    /// Specialized Square Matrix
    /// </summary>
    /// <typeparam name="T">numeric data type</typeparam>
    public class SquareMatrix<T> : Matrix<T>
              where T : INumber<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SquareMatrix{T}"/> class.
        /// </summary>
        /// <param name="size">size of matrix</param>
        public SquareMatrix(uint size)
            : base(size, size)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareMatrix{T}"/> class.
        /// </summary>
        /// <param name="m">matrix to copy</param>
        public SquareMatrix(Matrix<T> m)
            : base(AssertIsSquare(m))
        {
        }

        /// <inheritdoc/>
        public override bool IsSquare => true;

        /// <summary>
        /// checks the matrix is a square matrix
        /// </summary>
        /// <param name="m">matrix</param>
        /// <returns>input matrix</returns>
        /// <exception cref="ArgumentException">matrix is not square</exception>
        protected static Matrix<T> AssertIsSquare(Matrix<T> m)
        {
            if (!m.IsSquare)
            {
                throw new ArgumentException("matrix is not square");
            }

            return m;
        }
    }
}