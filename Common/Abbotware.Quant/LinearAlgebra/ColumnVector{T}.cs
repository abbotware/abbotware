// -----------------------------------------------------------------------
// <copyright file="ColumnVector{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.LinearAlgebra
{
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
    }
}
