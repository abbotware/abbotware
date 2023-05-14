namespace Abbotware.Quant
{
    using System;
    using System.Collections.Generic;
    using Abbotware.Core.Extensions;

    public class Curve<T>
    {
        private readonly SortedList<double, T> points = new();

        public Curve(params KeyValuePair<double, T>[] points)
        {
            foreach (var p in points)
            {
                this.points.Add(p.Key, p.Value);
            }
        }

        public T Lookup(double point)
        {
            var idx = points.Keys.BinarySearchIndexOf(point);

            if (idx >= 0)
            {
                return points.Values[idx];
            }
            else
            {
                if (idx == -1)
                {
                    return points.Values[0];
                }

                return points.Values[-idx - 2];
            }
        }
    }
}
