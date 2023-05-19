// -----------------------------------------------------------------------
// <copyright file="DiscreteCurve{TX,TY}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Math
{
    using System.Collections;
    using System.Collections.Generic;
    using Abbotware.Core.Extensions;

    /// <summary>
    /// Represents a discretized curve
    /// </summary>
    /// <typeparam name="TX">C-Value Type</typeparam>
    /// <typeparam name="TY">Y-Value Type</typeparam>
    public class DiscreteCurve<TX, TY> : IFittedCurve<TX, TY>, IEnumerable<IPoint<TX, TY>>
        where TX : notnull
        where TY : notnull
    {
        private readonly SortedList<TX, TY> points = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscreteCurve{TX, TY}"/> class.
        /// </summary>
        /// <param name="points">points</param>
        public DiscreteCurve(params IPoint<TX, TY>[] points)
        {
            foreach (var p in points)
            {
                this.points.Add(p.X, p.Y);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscreteCurve{TX, TY}"/> class.
        /// </summary>
        /// <param name="points">points</param>
        public DiscreteCurve(params KeyValuePair<TX, TY>[] points)
        {
            foreach (var p in points)
            {
                this.points.Add(p.Key, p.Value);
            }
        }

        /// <inheritdoc/>>
        public IEnumerator<IPoint<TX, TY>> GetEnumerator()
        {
            foreach (var p in this.points)
            {
                yield return new Point<TX, TY>(p.Key, p.Value);
            }
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.points.GetEnumerator();
        }

        /// <inheritdoc/>
        public TY GetPoint(TX x)
        {
            var idx = this.points.Keys.BinarySearchIndexOf(x);

            if (idx >= 0)
            {
                return this.points.Values[idx];
            }

            throw new KeyNotFoundException($"no Y value for:{x}");
        }

        /// <inheritdoc/>>
        public TY Nearest(TX x)
        {
            var idx = this.points.Keys.BinarySearchIndexOf(x);

            if (idx >= 0)
            {
                return this.points.Values[idx];
            }

            return this.points.Values[-idx - 1];
        }
    }
}
