// -----------------------------------------------------------------------
// <copyright file="Point{TValueX,TValueY}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Math
{
    using System;
    using Abbotware.Core.Extensions;

    /// <summary>
    ///     Point with 2 dimensions
    /// </summary>
    /// <typeparam name="TValueX">X dimension data type</typeparam>
    /// <typeparam name="TValueY">Y dimension data type</typeparam>
    public struct Point<TValueX, TValueY> : IPoint<TValueX, TValueY>, IEquatable<Point<TValueX, TValueY>>
        where TValueX : struct
        where TValueY : struct
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Point{TValueX, TValueY}" /> struct.
        /// </summary>
        /// <param name="valueX">x value</param>
        /// <param name="valueY">y value</param>
        public Point(TValueX valueX, TValueY valueY)
        {
            this.X = valueX;
            this.Y = valueY;
        }

        /// <inheritdoc/>
        public TValueX X { get; }

        /// <inheritdoc/>
        public TValueY Y { get; }

        /// <summary>
        ///     Tests not equals
        /// </summary>
        /// <param name="left">left hand side operand</param>
        /// <param name="right">right hand side operand</param>
        /// <returns>true/false</returns>
        public static bool operator !=(Point<TValueX, TValueY> left, Point<TValueX, TValueY> right)
        {
            return !(left == right);
        }

        /// <summary>
        ///     Tests equals
        /// </summary>
        /// <param name="left">left hand side operand</param>
        /// <param name="right">right hand side operand</param>
        /// <returns>true/false</returns>
        public static bool operator ==(Point<TValueX, TValueY> left, Point<TValueX, TValueY> right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!this.StructPossiblyEquals<Point<TValueX, TValueY>>(obj, out var other))
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <inheritdoc />
        public bool Equals(Point<TValueX, TValueY> other)
        {
            if (!other.X.Equals(this.X))
            {
                return false;
            }

            if (!other.Y.Equals(this.Y))
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (this.X, this.Y).GetHashCode();
        }
    }
}