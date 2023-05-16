// -----------------------------------------------------------------------
// <copyright file="Curve{TX,TY}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Core.Math
{
    using System.Collections.Generic;
    using Abbotware.Core.Extensions;

    /// <summary>
    /// Represents a curve
    /// </summary>
    /// <typeparam name="TX">C-Value Type</typeparam>
    /// <typeparam name="TY">Y-Value Type</typeparam>
    public class Curve<TX, TY>
        where TX : struct
        where TY : struct
    {
        private readonly SortedList<TX, TY> points = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Curve{TX, TY}"/> class.
        /// </summary>
        /// <param name="points">points</param>
        public Curve(params IPoint<TX, TY>[] points)
        {
            foreach (var p in points)
            {
                this.points.Add(p.X, p.Y);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Curve{TX, TY}"/> class.
        /// </summary>
        /// <param name="points">points</param>
        public Curve(params KeyValuePair<TX, TY>[] points)
        {
            foreach (var p in points)
            {
                this.points.Add(p.Key, p.Value);
            }
        }

        /// <summary>
        /// Lookup x value
        /// </summary>
        /// <param name="x">x value</param>
        /// <returns>closest y value</returns>
        public TY Lookup(TX x)
        {
            var idx = this.points.Keys.BinarySearchIndexOf(x);

            if (idx >= 0)
            {
                return this.points.Values[idx];
            }
            else
            {
                if (idx == -1)
                {
                    return this.points.Values[0];
                }

                return this.points.Values[-idx - 1];
            }
        }
    }
}
