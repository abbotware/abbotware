// -----------------------------------------------------------------------
// <copyright file="Matrix{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Quant.LinearAlgebra
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Numerics;
    using System.Runtime.CompilerServices;
    using System.Text;
    using Abbotware.Core;
    using Abbotware.Core.Extensions;
    using Abbotware.Quant.LinearAlgebra.Extensions;
    using Abbotware.Quant.LinearAlgebra.Operations;

    /// <summary>
    /// Matrix class
    /// </summary>
    /// <typeparam name="T">numeric data type</typeparam>
    public class Matrix<T> : IRowTransform<T>, IColumnStatistics<T>
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

            this.m = new T[(uint)m.Length][];

            for (int i = 0; i < m.Length; ++i)
            {
                if (m[i].Length != this.Columns)
                {
                    throw new InvalidOperationException($"Jagged Array is not a complete Rectangular: Row:[{i}] has length:{m[i].Length} instead of expected:{this.Columns}");
                }

                this.m[i] = m[i][..];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix{T}"/> class.
        /// </summary>
        /// <param name="m">matrix to copy</param>
        public Matrix(Matrix<T> m)
            : this(m.m)
        {
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
        public uint Rows
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (uint)this.m.Length;
        }

        /// <summary>
        /// Gets the row transform operations
        /// </summary>
        public IRowTransform<T> RowTransform => this;

        /// <summary>
        /// Gets the column statistics
        /// </summary>
        public IColumnStatistics<T> ColumnStatistics => this;

        /// <summary>
        /// Gets the number of columns in the matrix
        /// </summary>
        public uint Columns
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        /// <summary>
        /// Gets a value indicating whether or not the matrix is a square matrix
        /// </summary>
        public virtual bool IsSquare => this.Rows == this.Columns;

        /// <summary>
        /// Gets a value indicating whether or not the matrix is symmetric
        /// </summary>
        public bool IsSymmetric => this.IsSquare ? this.Equals(this.Transpose()) : false;

        /// <summary>
        /// gets or sets the element at row,column position
        /// </summary>
        /// <param name="row">row number</param>
        /// <param name="column">column number</param>
        /// <returns>element</returns>
        public T this[uint row, uint column]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return this.m[row][column];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                this.m[row][column] = value;
            }
        }

        /// <summary>
        /// Matrix Multiplication
        /// </summary>
        /// <param name="left">left side</param>
        /// <param name="right">right side</param>
        /// <returns>multiplaction results</returns>
        /// <exception cref="InvalidOperationException">matrix size mismatch</exception>
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
        /// Scalar x Matrix Multiplication
        /// </summary>
        /// <param name="left">left side</param>
        /// <param name="right">right side</param>
        /// <returns>multiplaction results</returns>
        public static Matrix<T> operator *(T left, Matrix<T> right)
        {
            var m = new Matrix<T>(right.Rows, right.Columns);

            for (uint i = 0; i < m.Rows; ++i)
            {
                for (uint j = 0; j < m.Columns; ++j)
                {
                    for (uint k = 0; k < m.Columns; ++k)
                    {
                        m[i, j] = left * right[i, j];
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
        /// <exception cref="InvalidOperationException">matrix size mismatch</exception>
        public static Matrix<T> Multiply(Matrix<T> left, Matrix<T> right) => left * right;

        /// <summary>
        /// Computes the transpose of the matrix
        /// </summary>
        /// <returns>transposed matrix</returns>
        public Matrix<T> PercentageChange()
        {
            var pc = new Matrix<T>(this.Rows - 1, this.Columns);

            for (uint i = 0; i < this.Rows - 1; ++i)
            {
                for (uint j = 0; j < this.Columns; ++j)
                {
                    var prev = this[i, j];
                    var next = this[i + 1, j];
                    pc[i, j] = (next - prev) / prev;
                }
            }

            return pc;
        }

        /// <summary>
        /// Computes the transpose of the matrix
        /// </summary>
        /// <returns>transposed matrix</returns>
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

        /// <summary>
        /// Computes the covariance matrix
        /// </summary>
        /// <returns>covariance matrix</returns>
        public SquareMatrix<T> CovarianceMatrix()
        {
            var u = this.ColumnStatistics.Mean();

            var x = new Matrix<T>(this);

            // subtract mean from each row
            x.RowTransform.Subtract(u);

            var n_minus_one = (this.Rows - 1).ToGenericNumber<T>();

            var t = (T.One / n_minus_one) * (x.Transpose() * x);

            var c = new SquareMatrix<T>(t);

            return c;
        }

        /// <summary>
        /// Gets a row
        /// </summary>
        /// <param name="row">row number</param>
        /// <returns>the row</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> Row(uint row) => this.m[row];

        /// <summary>
        /// Gets a column
        /// </summary>
        /// <param name="column">column number</param>
        /// <returns>the column</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (!this.ClassPossiblyEquals<Matrix<T>>(obj, out var right))
            {
                return false;
            }

            if (right.Columns != this.Columns)
            {
                return false;
            }

            if (right.Rows != this.Rows)
            {
                return false;
            }

            for (uint i = 0; i < this.Rows; ++i)
            {
                for (uint j = 0; j < this.Columns; ++j)
                {
                    if (this[i, j] != right[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return ((IStructuralEquatable)this.m).GetHashCode(EqualityComparer<T>.Default);
        }

        /// <inheritdoc/>
        RowVector<T> IColumnStatistics<T>.Mean()
        {
            var u = new RowVector<T>(this.Columns);
            var n = this.Rows.ToGenericNumber<T>();

            // compute the means
            for (uint j = 0; j < this.Columns; ++j)
            {
                var sum = T.Zero;

                for (uint i = 0; i < this.Rows; ++i)
                {
                    sum += this[i, j];
                }

                u[j] = sum / n;
            }

            return u;
        }

        /// <inheritdoc/>
        void IRowTransform<T>.Add(RowVector<T> vector)
        {
            if (vector.Columns != this.Columns)
            {
                throw new InvalidOperationException();
            }

            for (uint i = 0; i < this.Rows; ++i)
            {
                for (uint j = 0; j < this.Columns; ++j)
                {
                    this[i, j] += vector[j];
                }
            }
        }

        /// <inheritdoc/>
        void IRowTransform<T>.Subtract(RowVector<T> vector)
        {
            if (vector.Columns != this.Columns)
            {
                throw new InvalidOperationException();
            }

            for (uint i = 0; i < this.Rows; ++i)
            {
                for (uint j = 0; j < this.Columns; ++j)
                {
                    this[i, j] -= vector[j];
                }
            }
        }
    }
}