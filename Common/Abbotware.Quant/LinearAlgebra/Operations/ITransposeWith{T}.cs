// -----------------------------------------------------------------------
// <copyright file="ITransposeWith{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.LinearAlgebra.Operations
{
    using System.Numerics;

    /// <summary>
    /// interface for transpose helper functions
    /// </summary>
    /// <typeparam name="T">numeric data type</typeparam>
    public interface ITransposeWith<T>
       where T : INumber<T>
    {
        /// <summary>
        /// computes the transpose x sef
        /// </summary>
        /// <returns>square matrix</returns>
        SquareMatrix<T> Self();
    }
}