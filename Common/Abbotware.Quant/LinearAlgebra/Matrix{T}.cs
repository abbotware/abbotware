// -----------------------------------------------------------------------
// <copyright file="Matrix{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.LinearAlgebra
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Text;
    using Abbotware.Core;

    /// <summary>
    /// Matrix class
    /// </summary>
    /// <typeparam name="T">numeric data type</typeparam>
    public class Matrix<T>
        where T : INumber<T>
    {
        private readonly T[][] m;

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix{T}"/> class.
        /// </summary>
        /// <param name="m">jagged arrays</param>
        public Matrix(T[][] m)
        {
            Arguments.IsPositiveAndNotZero(m.Length, nameof(m.Length));

            this.Columns = (uint)m[0].Length;
            Arguments.IsPositiveAndNotZero(this.Columns, nameof(this.Columns));

            for (int i = 0; i < m.Length; ++i)
            {
                if (m[i].Length != this.Columns)
                {
                    throw new InvalidOperationException($"Jagged Array is not a complete Rectangular: Row:[{i}] has length:{m[i].Length} instead of expected:{this.Columns}");
                }
            }

            this.m = m;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix{T}"/> class.
        /// </summary>
        /// <param name="m">rectangular array</param>
        public Matrix(T[,] m)
           : this(m.ToJaggedArray())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix{T}"/> class.
        /// </summary>
        /// <param name="rows">number of rows</param>
        /// <param name="columns">number of columns</param>
        public Matrix(uint rows, uint columns)
        {
            this.Columns = columns;
            this.m = new T[rows][];
            for (int i = 0; i < rows; ++i)
            {
                this.m[i] = new T[columns];
            }
        }

        /// <summary>
        /// Gets the number of rows in the matrix
        /// </summary>
        public uint Rows => (uint)this.m.Length;

        /// <summary>
        /// Gets the number of columns in the matrix
        /// </summary>
        public uint Columns { get; }

        /// <summary>
        /// Gets a value indicating whether or not the matrix is a square matrix
        /// </summary>
        public virtual bool IsSquare => this.Rows == this.Columns;

        /// <summary>
        /// gets or sets the element at row,column position
        /// </summary>
        /// <param name="row">row number</param>
        /// <param name="column">column number</param>
        /// <returns>element</returns>
        public T this[uint row, uint column]
        {
            get { return this.m[row][column]; }
            set { this.m[row][column] = value; }
        }

        public Matrix<T> Transpose()
        {
            var t = new Matrix<T>(this.Columns, this.Rows);

            for (uint i = 0; i < this.Rows; ++i)
            {
                for (uint j = 0; j < this.Columns; ++j)
                {
                    t[j, i] = this[i, j];
                }
            }

            return t;
        }

        public bool IsSymmetric() => throw new NotImplementedException();

        public bool IsPseudoInverse() => throw new NotImplementedException();

        /// <summary>
        /// Matrix Multiplication
        /// </summary>
        /// <param name="left">left side</param>
        /// <param name="right">right side</param>
        /// <returns>multiplaction results</returns>
        /// <exception cref="InvalidOperationException">error cases</exception>
        public static Matrix<T> operator *(Matrix<T> left, Matrix<T> right)
        {
            if (left.Columns != right.Rows)
            {
                throw new InvalidOperationException($"Can not multiply {left.Rows}x{left.Columns} and {right.Rows}x{right.Columns} matrices");
            }

            var m = new Matrix<T>(left.Rows, right.Columns);

            for (uint i = 0; i < m.Rows; ++i)
            {
                for (uint j = 0; j < m.Columns; ++j)
                {
                    for (uint k = 0; k < m.Columns; ++k)
                    {
                        m[i, j] += left[i, k] * right[k, j];
                    }
                }
            }

            return m;
        }

        /// <summary>
        /// Matrix Multiplication
        /// </summary>
        /// <param name="left">left side</param>
        /// <param name="right">right side</param>
        /// <returns>multiplaction results</returns>
        /// <exception cref="InvalidOperationException">error cases</exception>
        public static Matrix<T> Multiply(Matrix<T> left, Matrix<T> right) => left * right;

        /// <summary>
        /// Gets a row
        /// </summary>
        /// <param name="row">row number</param>
        /// <returns>the row</returns>
        public IEnumerable<T> Row(uint row) => this.m[row];

        /// <summary>
        /// Gets a column
        /// </summary>
        /// <param name="column">column number</param>
        /// <returns>the column</returns>
        public IEnumerable<T> Column(uint column)
        {
            foreach (var row in this.m)
            {
                yield return row[column];
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (uint i = 0; i < this.Rows; ++i)
            {
                sb.Append('[');
                sb.AppendJoin(' ', this.Row(i));
                sb.Append(']');
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}