// -----------------------------------------------------------------------
// <copyright file="SquareMatrix.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.LinearAlgebra
{
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

        /// <inheritdoc/>
        public override bool IsSquare => true;
    }
}