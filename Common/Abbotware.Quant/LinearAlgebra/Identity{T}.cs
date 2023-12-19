// -----------------------------------------------------------------------
// <copyright file="Identity{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.LinearAlgebra
{
    using System.Numerics;

    /// <summary>
    /// Identity Matrix
    /// </summary>
    /// <typeparam name="T">numeric data type</typeparam>
    public class Identity<T> : SquareMatrix<T>
        where T : INumber<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Identity{T}"/> class.
        /// </summary>
        /// <param name="size">size of matrix</param>
        public Identity(uint size)
            : base(size)
        {
            for (uint i = 0; i < size; i++)
            {
                this[i, i] = T.One;
            }
        }
    }
}